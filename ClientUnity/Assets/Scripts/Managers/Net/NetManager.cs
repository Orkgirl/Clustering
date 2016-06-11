using System;
using Assets.Scripts.Entity;
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
            add
            {
                _onConnectStatusEvent += value;
            }
            remove
            {
                _onConnectStatusEvent -= value;
            }
        }

        public void Install()
        {

        }

        public void Initialaze()
        {
            
        }

        public void Connect()
        {
            Connect("127.0.0.1", 11000);
        }

        public void Connect(string address, int port)
        {
            AsynchronousClient.StartClient(address, port);
        }
    }
}
