
using System;
using System.Collections.Generic;
using UnityEngine;



public static class DataStorageConfig
{

    public static Dictionary<string, string> TableNames = new Dictionary<string, string>()
    {
        {"1911 - 1939", "Data2"},
        {"1939 - 1945", "Data2"},
        {"1945 - 1970", "Data2"},
        {"1970 - 1990", "Data2"}
    };
}

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

public static class DataStorage
{
    
    public static StorageMapData LoadData(string name)
    {
        var textAsset = Resources.Load<TextAsset>(name);

        return JsonUtility.FromJson<StorageMapData>(textAsset.text);
    }
}

