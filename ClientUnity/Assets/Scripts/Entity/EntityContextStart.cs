using Assets.Scripts.Managers;
using Assets.Scripts.Managers.Windows;
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

        protected void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
