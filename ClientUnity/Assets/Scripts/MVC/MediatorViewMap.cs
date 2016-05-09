using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers;
using Assets.Scripts.UI;
using Assets.Scripts.UI.HUD;
using UnityEngine;

namespace Assets.Scripts.MVC
{
    public class MediatorViewMap : IEntity
    {
        private ResourcesManager _resourcesManager;


        private Dictionary<Type, Type> _dictionaryMediatorView;
        private Dictionary<Type, string> _dictionaryViewPrefab;

        public void Install()
        {
            _resourcesManager = EntityContext.Get<ResourcesManager>();
        }

        public void Initialaze()
        {

        }

        public MediatorViewMap()
        {
            _dictionaryMediatorView = new Dictionary<Type, Type>();
            _dictionaryViewPrefab = new Dictionary<Type, string>();

            Map<TopMenuMediator, TopMenuView>("Prefabs/UI/HUD/TopMenuView");

            Map<IndicatorMediator, IndicatorView>("Prefabs/UI/Windows/Indicator/IndicatorView");
            Map<DataGridMediator, DataGridView>("Prefabs/UI/Windows/DataGrid/DataGridView");
            Map<AnalyzeMediator, AnalyzeView>("Prefabs/UI/Windows/Analyze/AnalyzeView");
            Map<MapMediator, MapView>("Prefabs/UI/Windows/Map/MapView");
            Map<StartMediator, StartView>("Prefabs/UI/Windows/Start/StartView");
        }

        private void Map<TMediator, TView>(string value) where TMediator : MediatorBase<ViewBase> where TView : ViewBase
        {
            _dictionaryMediatorView.Add(typeof (TMediator), typeof (TView));
            _dictionaryViewPrefab.Add(typeof (TView), value);
        }

        public T Get<T>(UIItem layout) where T : MediatorBase<ViewBase>, new()
        {
            T mediator = new T();

            Type viewT;

            if (_dictionaryMediatorView.TryGetValue(typeof (T), out viewT))
            {
                string value;
                if (_dictionaryViewPrefab.TryGetValue(viewT, out value))
                {
                    var viewPrefab = _resourcesManager.LoadView(viewT, value);
                    var view = layout.AddChild(viewPrefab.gameObject, viewT);
                    mediator.Mediate(view as ViewBase);
                }
            }
            return mediator;
        }
    }
}
