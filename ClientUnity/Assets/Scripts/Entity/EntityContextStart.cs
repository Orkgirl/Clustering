using Assets.Scripts.Managers;
using Assets.Scripts.Managers.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Entity
{
    public class EntityContextStart : MonoBehaviour
    {
        public void Awake()
        {
            EntityContext.Install();
        }

        public void Start()
        {
            EntityContext.Initialize();

            EntityContext.Get<WindowsManager>().Open(WindowType.Start);
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
