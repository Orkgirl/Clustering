using Assets.Scripts.Entity;
using Assets.Scripts.MVC;
using UnityEngine;

namespace Assets.Scripts.Managers.HUD
{
    public class HUDManager : IEntity
    {
        private string _layoutName = "HUDLayout";
        private UIItem _layoutGameObject;

        private MediatorViewMap _mediatorViewMap;

        public void Install()
        {
            _mediatorViewMap = EntityContext.Get<MediatorViewMap>();
        }

        public void Initialaze()
        {
            _layoutGameObject = GameObject.Find(_layoutName).GetComponent<UIItem>();
        }

        public void Tick(float deltaTime)
        {
            
        }

        private HeaderMediator _header;

        public void ShowHUD()
        {
            _header = _mediatorViewMap.Get<HeaderMediator>(_layoutGameObject);
        }

        public void HideHUD()
        {
            _header.UnMediate();
            GameObject.Destroy(_header.View.gameObject);
        }
    }
}
