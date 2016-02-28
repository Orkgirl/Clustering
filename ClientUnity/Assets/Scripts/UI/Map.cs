using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[Serializable]
public class MapImageData
{
    [SerializeField]
    public string Key;

    [SerializeField]
    public Image Value;
}

public class Map : UIItem
{

    public List<MapImageData> MapList;

    public void SetColor(string key, Color color)
    {
        foreach (var mapImageData in MapList)
        {
            if (key == mapImageData.Key)
            {
                mapImageData.Value.color = color;
                break;
            }
        }
    }

    public void SetColorAll(Color color)
    {
        foreach (var mapImageData in MapList)
        {
           mapImageData.Value.color = color;
        }
    }

    public List<string> GetAllLocations()
    {
        var result = new List<string>();
        foreach (var mapImageData in MapList)
        {
            result.Add(mapImageData.Key);
        }
        return result;
    }
}
