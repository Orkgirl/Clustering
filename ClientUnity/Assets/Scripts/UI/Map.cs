using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    public Dropdown _mapColumnDropdown;

    [SerializeField]
    public Dropdown _mapClusterCountDropdown;

    [SerializeField]
    private Color[] _colors;

    [SerializeField] public List<MapImageData> MapList;

    private int _clustersCount = 2;

    private ClusterMap _clusterMap;

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

    public override void Show()
    {
        base.Show();
        _clusterMap = Clustering.GetRaw();
        _mapColumnDropdown.ClearOptions();
        _mapColumnDropdown.AddOptions(_clusterMap.ColumnsKeys.ToList());

        var listClusterCount = new List<string>();
        for (var i = 0; i < 27; i++)
        {
            listClusterCount.Add(i.ToString());
        }
        _mapClusterCountDropdown.ClearOptions();
        _mapClusterCountDropdown.AddOptions(listClusterCount);
    }

    public void SetClasterCount(int index)
    {
        _clustersCount = index;
    }

    public void ShowOnMapColumn(int index)
    {
        var list = _clusterMap.ColumnsKeys.ToList();
        if (index < list.Count)
        {
            ShowOnMapColumn(list[index]);
        }
    }

    public void ShowOnMapClaser(List<ClusterUnit> clasers)
    {
        var map = Clustering.GetNormalize();

        for (var i = 0; i < clasers.Count; i++)
        {
            var item = map.GetFirstInRow(clasers[i].Row);
            SetColor(item.Id, _colors[clasers[i].Cluster]);
        }
    }

    public void ShowOnMapColumn(string column)
    {
        if (_clustersCount < 1)
        {
            return;
        }

        List<ClusterDataItem> columns = _clusterMap.ColumnsToList(column);

        int itemInClaster = (int)Mathf.Round((float)columns.Count/(float)_clustersCount);

        int currentClaster = 0;
        int columnsInClusterCount = 0;

        columns.Sort((x, y) => x.Value.CompareTo(y.Value));

        for (var i = 0; i < columns.Count; i++)
        {
            SetColor(columns[i].Id, _colors[currentClaster]);

            ++columnsInClusterCount;

            if (columnsInClusterCount >= itemInClaster)
            {
                columnsInClusterCount = 0;
                if (currentClaster < _clustersCount - 1)
                {
                    ++currentClaster;
                }
            }

            
        }
    }
}
