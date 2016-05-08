﻿using System;
using System.Collections.Generic;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers.UI.Windows;
using Assets.Scripts.MVC;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class WindowsManager : IEntity
    {
        private string _layoutName = "WindowsLayout";
        private UIItem _layoutGameObject;

        private MediatorViewMap _mediatorViewMap;

        private MediatorBase<ViewBase> _currentWindow;

        private Dictionary<WindowType, Func<UIItem, MediatorBase<ViewBase>>> _dictionary; 

        public void Install()
        {
            _mediatorViewMap = EntityContext.Get<MediatorViewMap>();
            _layoutGameObject = GameObject.Find(_layoutName).GetComponent<UIItem>();
        }

        public void Initialaze()
        {
            _dictionary = new Dictionary<WindowType, Func<UIItem, MediatorBase<ViewBase>>>();

            _dictionary.Add(WindowType.Indicators, _mediatorViewMap.Get<IndicatorMediator>);
        }

        public void Open(WindowType window)
        {
            
            if (_currentWindow != null)
            {
                GameObject.Destroy(_currentWindow.View.gameObject);
                _currentWindow.UnMediate();
            }

            _currentWindow = _dictionary[window].Invoke(_layoutGameObject);
        }
    }
}
