using System;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.Claster;
using Assets.Scripts.Managers.Net;
using Assets.Scripts.MVC;

namespace Assets.Scripts.Entity
{
    public static class EntityContext
    {
        private static Dictionary<Type, IEntity> _dictionary;

        static EntityContext()
        {
            _dictionary = new Dictionary<Type, IEntity>();
        }

        public static T Get<T>() where T : IEntity
        {
            IEntity entuty;

            if (_dictionary.TryGetValue(typeof (T), out entuty))
            {
                return (T) entuty;
            }
            throw new Exception("[EntityContext][Get] Type " + typeof (T) + " not found");

        }

        public static void Install()
        {
            _dictionary.Add(typeof (WindowsManager), new WindowsManager());
            _dictionary.Add(typeof (HUDManager), new HUDManager());
            _dictionary.Add(typeof(MediatorViewMap), new MediatorViewMap());
            _dictionary.Add(typeof(ResourcesManager), new ResourcesManager());
            _dictionary.Add(typeof(ClasterManager), new ClasterManager());
            _dictionary.Add(typeof(NetManager), new NetManager());

            foreach (var entity in _dictionary.Values)
            {
                entity.Install();
            }
        }

        public static void Initialize()
        {
            foreach (var entity in _dictionary.Values)
            {
                entity.Initialaze();
            }
        }
    }
}
