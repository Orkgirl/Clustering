using System;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Entity;
using Common.Net.Commands;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Managers.Net
{
    // State object for receiving data from remote device.
    public class NetManager : IEntity
    {
        private event Action<bool> _onConnectStatusEvent;

        public event Action<bool> OnConnectStatusEvent
        {
            add { _onConnectStatusEvent += value; }
            remove { _onConnectStatusEvent -= value; }
        }

        private AsynchronousClient _client;
        private Queue<CommandBase> _commands;

        private bool _connectStatus = false;
        private bool _connectStatusUpdated = true;

        public void Install()
        {
            _commands = new Queue<CommandBase>();
            _client = new AsynchronousClient();

            _client.ConnectStatusEvent += ClientOnConnectStatusEvent;
            _client.ReciveEvent += ClientOnReciveEvent;
        }

        private void ClientOnConnectStatusEvent(bool status)
        {
            _connectStatus = status;
            _connectStatusUpdated = false;
        }

        private void ClientStatusUpdate()
        {
            if (_connectStatusUpdated)
            {
                return;
            }
            _connectStatusUpdated = true;

            if (_onConnectStatusEvent != null)
            {
                _onConnectStatusEvent.Invoke(_connectStatus);
            }
        }

        private void ClientOnReciveEvent(CommandBase commandBase)
        {
            _commands.Enqueue(commandBase);
        }

        public void Initialaze()
        {

        }

        public void Tick(float deltaTime)
        {
            ClientStatusUpdate();
        }

        public void Connect()
        {
            Connect("127.0.0.1", 11000);
        }

        public void Connect(string address, int port)
        {
            Common.Logger.Log("[NetManager][Connect] " + address + " : " + port);
            _client.StartClient(address, port);
        }
    }
}
