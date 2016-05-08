using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;
using Assets.Scripts.MVC;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    [Serializable]
    public class StoragelocationDataKeyValue
    {
        public string key;
        public float value;
    }

    [Serializable]
    public class StoragelocationData
    {
        public string name;
        public string id;
        public StoragelocationDataKeyValue[] data;
    }


    [Serializable]
    public class StorageMapData
    {
        public string[] header;
        public StoragelocationData[] map;
    }

    public class ResourcesManager : IEntity
    {
        public void Install()
        {
            
        }

        public void Initialaze()
        {

        }

        public Dictionary<string, string> TableNames = new Dictionary<string, string>()
        {
            {"2012 - 2013", "Data2"},
        };

        public T LoadView<T>(string value) where T : ViewBase
        {
            var go = Resources.Load<GameObject>(value);
            return go.GetComponent<T>();
        }

        public ViewBase LoadView(Type view, string value)
        {
            var go = Resources.Load<GameObject>(value);
            return go.GetComponent(view) as ViewBase;
        }

        public StorageMapData StorageMapData;
        public StorageMapData LoadData(string name)
        {
            var textAsset = Resources.Load<TextAsset>(name);

            StorageMapData = JsonUtility.FromJson<StorageMapData>(textAsset.text);

            return StorageMapData;
        }
    }
}
