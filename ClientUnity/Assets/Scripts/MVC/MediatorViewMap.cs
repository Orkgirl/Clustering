using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers;


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

            Map<StartMediator, StartView>("Prefabs/UI/Windows/StartView");
            Map<RegionsIndicatorsMediator, RegionsIndicatorsView>("Prefabs/UI/Windows/RegionsIndicators/RegionsIndicators");
            Map<SelectedIndicatorsMediator, SelectedIndicatorsView>("Prefabs/UI/Windows/SelectedIndicators/SelectedIndicators");
            Map<ClusteringMediator, ClusteringView>("Prefabs/UI/Windows/Clustering");
            Map<MapMediator, MapView>("Prefabs/UI/Windows/Map");
            Map<HeaderMediator, HeaderView>("Prefabs/UI/HUD/Header");
            Map<ServerConnectMediator, ServerConnectView>("Prefabs/UI/Windows/ServerConnect/ServerConnect");

        }

        private void Map<TMediator, TView>(string value) where TMediator : MediatorBase where TView : ViewBase
        {
            _dictionaryMediatorView.Add(typeof (TMediator), typeof (TView));
            _dictionaryViewPrefab.Add(typeof (TView), value);
        }

        public T Get<T>(UIItem layout) where T : MediatorBase, new()
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
