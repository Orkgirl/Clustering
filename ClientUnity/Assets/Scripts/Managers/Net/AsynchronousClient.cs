﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Common.Net;
using Common.Net.Commands;

namespace Assets.Scripts.Managers.Net
{
    public class AsynchronousClient
    {
        private event Action<bool> _connectStatusEvent;
        public event Action<bool> ConnectStatusEvent
        {
            add
            {
                _connectStatusEvent += value;
            }
            remove
            {
                _connectStatusEvent -= value;
            }
        }

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

        private Socket _client;

        public void StartClient(string address, int port)
        {
            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // The name of the 
                // remote device is "host.contoso.com".
                IPHostEntry ipHostInfo = Dns.GetHostEntry(address);//Dns.GetHostName());
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

                Common.Logger.Log("[AsynchronousClient][StartClient] " + ipAddress + " : " + port);

                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                _client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                _client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), _client);
                //connectDone.WaitOne();

                //// Send test data to the remote device.
                //Send(client, new GetRegionCommand());
                //Send(client, new GetIndicatorCommand());
                //Send(client, new GetDataCommand("all"));
                //sendDone.WaitOne();

                //// Receive the response from the remote device.
                //Receive(client);
                //receiveDone.WaitOne();

                //// Write the response to the console.
                ////Console.WriteLine("Response received : {0}", response);

                //// Release the socket.
                //client.Shutdown(SocketShutdown.Both);
                //client.Close();

            }
            catch (Exception e)
            {
                Common.Logger.LogException(e);
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket) ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                Common.Logger.Log("Socket connected to " + client.RemoteEndPoint.ToString());

                Receive(client);

                // Signal that the connection has been made.
                //connectDone.Set();
                if (_connectStatusEvent != null)
                {
                    _connectStatusEvent.Invoke(true);
                }

            }
            catch (Exception e)
            {
                Common.Logger.LogException(e);
                if (_connectStatusEvent != null)
                {
                    _connectStatusEvent.Invoke(false);
                }
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Common.Logger.LogException(e);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject) ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    CommandBase command;

                    if (SocketParser.TryDeserialize(state.buffer, bytesRead, out command))
                    {
                        Common.Logger.Log("[AsynchronousClient][ReceiveCallback] command: " + command.CommandType);
                        if (_reciveEvent != null)
                        {
                            _reciveEvent.Invoke(command);
                        }
                    }
                    else
                    {
                        // Not all data received. Get more.

                        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(ReceiveCallback), state);
                    }
                }
                else
                {
                    //// All the data has arrived; put it in response.
                    //if (state.sb.Length > 1)
                    //{
                    //    response = state.sb.ToString();
                    //}
                    // Signal that all bytes have been received.
                    //receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Common.Logger.LogException(e);
            }
        }

        public void Send(CommandBase data)
        {
            byte[] byteData;
            if (!SocketParser.TrySerialize(data, out byteData))
            {
                return;
            }

            // Begin sending the data to the remote device.
            _client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), _client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket) ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                Common.Logger.Log("Sent " + bytesSent + " bytes to server.");

                // Signal that all bytes have been sent.
                //sendDone.Set();
            }
            catch (Exception e)
            {
                Common.Logger.LogException(e);
            }
        }
    }
}