
using System;
using System.Collections.Generic;
using UnityEngine;



public static class DataStorageConfig
{

    public static Dictionary<string, string> TableNames = new Dictionary<string, string>()
    {
        {"2012 - 2013", "Data2"},
        {"2012 - 2013", "Data2"},
        {"2012 - 2013", "Data2"},
        {"2012 - 2013", "Data2"}
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

