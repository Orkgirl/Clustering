using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Managers.Task
{
    public class CoroutineExecuterComponent : MonoBehaviour
    {
        public void ExecuteThread(Action task)
        {
            Thread oThread = new Thread(new ThreadStart(task));
            oThread.Start();
        }

        public void ExecuteCoroutine(Func<IEnumerator> task)
        {
            StartCoroutine(task.Invoke());
        }
    }
}