using Assets.Scripts.Entity;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Managers.Net
{
    // State object for receiving data from remote device.

    public class NetManager : IEntity
    {
        public void Install()
        {

        }

        public void Initialaze()
        {
            
        }

        public void Connect()
        {
            var coroutineExecuterGameObject = new GameObject();
            coroutineExecuterGameObject.name = "CoroutineExecuter";
            var coroutineExecuter = coroutineExecuterGameObject.AddComponent<CoroutineExecuterComponent>();
            coroutineExecuter.ExecuteThread(AsynchronousClient.StartClient);
        }
    }
}
