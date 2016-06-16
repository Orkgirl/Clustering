using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Net;
using Common.Net.Commands;

namespace Client
{
    public class AsynchronousClient
    {

        private event Action<CommandBase> _reciveEvent;
        public event Action<CommandBase> ReciveEvent
        {
            add
            {
                _reciveEvent += value;
            }
            remove
            {
                _reciveEvent -= value;
            }
        }

        private event Action _connectEvent;
        public event Action ConnectEvent
        {
            add
            {
                _connectEvent += value;
            }
            remove
            {
                _connectEvent -= value;
            }
        }

        // ManualResetEvent instances signal completion.
        private ManualResetEvent connectDone =
            new ManualResetEvent(false);

        private ManualResetEvent sendDone =
            new ManualResetEvent(false);

        private ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        private Socket client;

        public void Connect(string address, int port)
        {
            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // The name of the 
                // remote device is "host.contoso.com".
                IPHostEntry ipHostInfo = Dns.GetHostEntry(address);
                IPAddress ipAddress = null;
                foreach (var item in ipHostInfo.AddressList)
                {
                    if (item.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = item;
                        break;
                    }
                }
                if (ipAddress == null)
                {
                    return;
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.
                connectDone.Set();

                if (_connectEvent != null)
                {
                    _connectEvent.Invoke();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Receive()
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject) ar.AsyncState;

                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                CommandBase command;
                if (SocketParser.TryDeserialize(state.Buffer, bytesRead, out command))
                {
                    receiveDone.Set();
                    if (_reciveEvent != null)
                    {
                        _reciveEvent.Invoke(command);
                    }
                }
                else
                {
                    state.Position += bytesRead;
                    // Get the rest of the data.
                    client.BeginReceive(state.Buffer, state.Position, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Send(CommandBase data)
        {
            byte[] byteData;

            if (!SocketParser.TrySerialize(data, out byteData))
            {
                Console.WriteLine("Send error : " + data.CommandType);
                return;
            }
            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);

            // Send test data to the remote device.
            //Send(client, "This is a test<EOF>");
            sendDone.WaitOne();

            // Receive the response from the remote device.
            Receive();

            receiveDone.WaitOne();

            //// Write the response to the console.
            //Console.WriteLine("Response received : {0}", response);

            //// Release the socket.
            //client.Shutdown(SocketShutdown.Both);
            //client.Close();
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
