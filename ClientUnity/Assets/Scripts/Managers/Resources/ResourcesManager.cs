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

    [Serializable]
    public class ConfigTablesData
    {
        public ConfigTibleItemData[] tables;
    }

    [Serializable]
    public class ConfigTibleItemData
    {
        public string Key;
        public string Value;
    }

    public class ResourcesManager : IEntity
    {
        public void Install()
        {
            LoadConfig();
        }

        public void Initialaze()
        {
        }

        public void Tick(float deltaTime)
        {
            
        }

        private Dictionary<string, string> _configTable = new Dictionary<string, string>();
        //{
        //    {"2012 - 2013", "Data2"},
        //};

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

        public void LoadConfig()
        {
            var textAsset = Resources.Load<TextAsset>("Config");

            var configData = JsonUtility.FromJson<ConfigTablesData>(textAsset.text);

            foreach (var configItemData in configData.tables)
            {
                _configTable.Add(configItemData.Key, configItemData.Value);
            }
        }

        public StorageMapData LoadData(string name)
        {
            var textAsset = Resources.Load<TextAsset>(name);

            StorageMapData = JsonUtility.FromJson<StorageMapData>(textAsset.text);

            return StorageMapData;
        }

        public string GetTable(string value)
        {
            string result = String.Empty;
            if (_configTable.TryGetValue(value, out result))
            {
                return result;
            }

            return result;
        }

        public List<string> GetAllTables()
        {
            return _configTable.Keys.ToList();
        }
    }
}
