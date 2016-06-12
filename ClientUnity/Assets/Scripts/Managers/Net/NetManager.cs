using System;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers.Claster;
using Common.Net.Commands;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Managers.Net
{
    // State object for receiving data from remote device.
    public class NetManager : IEntity
    {
        private event Action<bool> _connectStatusEvent;
        public event Action<bool> ConnectStatusEvent
        {
            add { _connectStatusEvent += value; }
            remove { _connectStatusEvent -= value; }
        }

        private event Action _dataReceivedEvent;
        public event Action DataReceivedEvent
        {
            add { _dataReceivedEvent += value; }
            remove { _dataReceivedEvent -= value; }
        }

        private AsynchronousClient _client;
        private Queue<CommandBase> _commands;
        private ClasterManager _clasterManager;

        private bool _connectStatus = false;
        private bool _connectStatusUpdated = true;

        public void Install()
        {
            _commands = new Queue<CommandBase>();
            _client = new AsynchronousClient();

            _client.ConnectStatusEvent += ClientOnConnectStatusEvent;
            _client.ReciveEvent += ClientOnReciveEvent;
        }

        public void Initialaze()
        {
            _clasterManager = EntityContext.Get<ClasterManager>();
        }

        public void Tick(float deltaTime)
        {
            ClientStatusUpdate();
            if (_connectStatus)
            {
                if (_commands.Count > 0)
                {
                    var command = _commands.Dequeue();

                    switch (command.CommandType)
                    {
                        case CommandType.SetRegion:
                            _clasterManager.SetRegion((SetRegionCommand)command);
                            _client.Send(new GetIndicatorCommand());
                            break;

                        case CommandType.SetIndicator:
                            _clasterManager.SetIndicators((SetIndicatorCommand)command);
                            _client.Send(new GetDataCommand("all"));
                            break;
                       
                        case CommandType.SetData:
                            _clasterManager.SetData((SetDataCommand)command);
                            if (_dataReceivedEvent != null)
                            {
                                _dataReceivedEvent.Invoke();
                            }
                            break;
                    }
                }
            }
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

            _client.Send(new GetRegionCommand());

            if (_connectStatusEvent != null)
            {
                _connectStatusEvent.Invoke(_connectStatus);
            }
        }

        private void ClientOnReciveEvent(CommandBase commandBase)
        {
            _commands.Enqueue(commandBase);
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
