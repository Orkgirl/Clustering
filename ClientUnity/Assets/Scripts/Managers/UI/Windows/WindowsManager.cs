using System;
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

            _dictionary.Add(WindowType.Indicator, _mediatorViewMap.Get<IndicatorMediator>);
            _dictionary.Add(WindowType.DataGrid, _mediatorViewMap.Get<DataGridMediator>);
            _dictionary.Add(WindowType.Analize, _mediatorViewMap.Get<AnalyzeMediator>);
            _dictionary.Add(WindowType.Map, _mediatorViewMap.Get<MapMediator>);
            _dictionary.Add(WindowType.Start, _mediatorViewMap.Get<StartMediator>);

            Open(WindowType.Start);
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
