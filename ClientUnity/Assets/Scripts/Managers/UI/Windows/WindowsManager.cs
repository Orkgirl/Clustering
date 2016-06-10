using System;
using System.Collections.Generic;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers.UI.Windows;
using Assets.Scripts.MVC;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class WindowsManager : IEntity
    {
        private string _layoutName = "WindowsLayout";
        private UIItem _layoutGameObject;

        private MediatorViewMap _mediatorViewMap;

        private MediatorBase _currentWindow;

        private Dictionary<WindowType, Func<UIItem, MediatorBase>> _dictionary; 

        public void Install()
        {
            _mediatorViewMap = EntityContext.Get<MediatorViewMap>();
            _layoutGameObject = GameObject.Find(_layoutName).GetComponent<UIItem>();
        }

        public void Initialaze()
        {
            _dictionary = new Dictionary<WindowType, Func<UIItem, MediatorBase>>();

            _dictionary.Add(WindowType.Start, _mediatorViewMap.Get<StartMediator>);
            _dictionary.Add(WindowType.RegionsIndicators, _mediatorViewMap.Get<RegionsIndicatorsMediator>);
            _dictionary.Add(WindowType.SelectedIndicators, _mediatorViewMap.Get<SelectedIndicatorsMediator>);
            _dictionary.Add(WindowType.Clustering, _mediatorViewMap.Get<ClusteringMediator>);
            _dictionary.Add(WindowType.Map, _mediatorViewMap.Get<MapMediator>);
            _dictionary.Add(WindowType.ServerConnect, _mediatorViewMap.Get<ServerConnectMediator>);

            //_dictionary.Add(WindowType.Indicator, _mediatorViewMap.Get<IndicatorMediator>);

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
