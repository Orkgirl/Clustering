
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class locationDataKeyValue
{
    public string key;
    public float value;
}

[Serializable]
public class locationData
{
    public string name;
    public string id;
    public locationDataKeyValue[] data;
}


[Serializable]
public class MapData
{
    public string[] header;
    public locationData[] map;
}

public static class DataStorage
{
    
    public static MapData LoadData()
    {
        var textAsset = Resources.Load<TextAsset>("Data2");

        return JsonUtility.FromJson<MapData>(textAsset.text);
    }
}

