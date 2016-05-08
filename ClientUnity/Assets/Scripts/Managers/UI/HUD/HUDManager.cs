using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;
using Assets.Scripts.MVC;
using Assets.Scripts.UI.HUD;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
    public class HUDManager : IEntity
    {
        private MediatorViewMap _mediatorViewMap;

        private string _layoutName = "HUDLayout";
        private UIItem _layoutGameObject;

        private TopMenuMediator _topMenu;

        public void Install()
        {
            _mediatorViewMap = EntityContext.Get<MediatorViewMap>();
        }

        public void Initialaze()
        {

            _layoutGameObject = GameObject.Find(_layoutName).GetComponent<UIItem>();

            _topMenu = _mediatorViewMap.Get<TopMenuMediator>(_layoutGameObject);
        }
    }
}
