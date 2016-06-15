using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class ArrayView<T> : IEnumerable<T>
{
    private readonly T[] array;
    private readonly int offset, count;

    public ArrayView(T[] array, int offset, int count)
    {
        this.array = array;
        this.offset = offset;
        this.count = count;
    }

    public int Length
    {
        get { return count; }
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.count)
                throw new IndexOutOfRangeException();
            else
                return this.array[offset + index];
        }
        set
        {
            if (index < 0 || index >= this.count)
                throw new IndexOutOfRangeException();
            else
                this.array[offset + index] = value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = offset; i < offset + count; i++)
            yield return array[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        IEnumerator<T> enumerator = this.GetEnumerator();

        while (enumerator.MoveNext())
        {
            yield return enumerator.Current;
        }
    }
}


public static class ProxyConnectionHelper
{
    const int TIMEOUT_MILLISECONDS = 20000; // 20 sec 
    const int MAX_BUFFER_SIZE = 8192;

    private static Socket _socket = null;
    private static ManualResetEvent _clientDone = new ManualResetEvent(false);
    private static byte[] _bytesPartOfPacket;

    public static string Connect(string hostName, int portNumber)
    {
        string result = string.Empty;
        DnsEndPoint hostEntry = new DnsEndPoint(hostName, portNumber);
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
        socketEventArg.RemoteEndPoint = hostEntry;

        socketEventArg.Completed +=
            new EventHandler<SocketAsyncEventArgs>(delegate (object s, SocketAsyncEventArgs e)
            {
                result = e.SocketError.ToString();
                _clientDone.Set();
            });

        _clientDone.Reset();
        _socket.ConnectAsync(socketEventArg);
        _clientDone.WaitOne(TIMEOUT_MILLISECONDS);

        return result;
    }

    public static string Send(byte[] bytes)
    {
        string response = "Operation Timeout";

        if (_socket != null)
        {
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;
            socketEventArg.UserToken = null;
            socketEventArg.Completed +=
                new EventHandler<SocketAsyncEventArgs>(delegate (object s, SocketAsyncEventArgs e)
                {
                    response = e.SocketError.ToString();
                    _clientDone.Set();
                });
            socketEventArg.SetBuffer(bytes, 0, bytes.Length);
            _clientDone.Reset();
            _socket.SendAsync(socketEventArg);
            _clientDone.WaitOne(TIMEOUT_MILLISECONDS);
        }
        else
        {
            response = "Socket is not initialized";
        }

        return response;
    }

    public static byte[] ReceiveBytes()
    {
        ErrorInfo errorInfo = new ErrorInfo() { value = "Operation timeout" };

        byte[] response = errorInfo.Serialize();

        if (_socket != null)
        {
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;

            socketEventArg.SetBuffer(new Byte[MAX_BUFFER_SIZE], 0, MAX_BUFFER_SIZE);

            socketEventArg.Completed +=
                new EventHandler<SocketAsyncEventArgs>(delegate (object s, SocketAsyncEventArgs e)
                {
                    if (e.SocketError == SocketError.Success)
                        response = new ArrayView<byte>(e.Buffer, e.Offset, e.BytesTransferred).ToArray();
                    else
                    {
                        errorInfo.value = e.SocketError.ToString();
                        response = errorInfo.Serialize();
                    }

                    List<IOPacket> ioPackets;
                    IOPacket.Deserialize(response, out ioPackets, out _bytesPartOfPacket);

                    _clientDone.Set();
                });

            _clientDone.Reset();

            _socket.ReceiveAsync(socketEventArg);
            _clientDone.WaitOne(TIMEOUT_MILLISECONDS);
        }
        else
        {
            errorInfo.value = "Socket is not initialized";
            response = errorInfo.Serialize();
        }

        return response;
    }

    public static void Close()
    {
        if (_socket != null) //&& _socket.Connected
            _socket.Close();
    }
}
