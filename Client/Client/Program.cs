using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Common.Net.Commands;

namespace Client
{ // State object for receiving data from remote device.

    public class Program
    {
        // The port number for the remote device.
        private const int port = 11000;
        private const string address = "127.0.0.1";
        private static AsynchronousClient _client;

        public static int Main(String[] args)
        {
            _client = new AsynchronousClient();
            _client.Connect(address, port);

            _client.ReciveEvent += ClientOnReciveEvent;
            _client.ConnectEvent += ClientOnConnectEvent;

            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        private static void ClientOnConnectEvent()
        {
            _client.Send(new GetRegionCommand());
        }

        private static void ClientOnReciveEvent(CommandBase commandBase)
        {
            Console.WriteLine("[ClientOnReciveEvent] commandBase: " + commandBase.CommandType);
        }
    }
}