
using System;
using System.Collections.Generic;
using UnityEngine;

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
    
    public static StorageMapData LoadData()
    {
        var textAsset = Resources.Load<TextAsset>("Data2");

        return JsonUtility.FromJson<StorageMapData>(textAsset.text);
    }
}

