using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers.Net;
using UnityEngine;

namespace Assets.Scripts.Managers.Task
{
    public class TaskManager : IEntity
    {
        private CoroutineExecuterComponent _coroutineExecuter;

        public void Install()
        {
            var coroutineExecuterGameObject = new GameObject();
            coroutineExecuterGameObject.name = "CoroutineExecuter";
            _coroutineExecuter = coroutineExecuterGameObject.AddComponent<CoroutineExecuterComponent>();
        }

        public void Initialaze()
        {
            
        }
    }
}
