using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers;
using Assets.Scripts.MVC;
using UnityEngine.UI;

public class MapView : ViewBase
{
   

    [SerializeField]
    public Dropdown _mapColumnDropdown;

    [SerializeField]
    public Dropdown _mapClusterCountDropdown;

    [SerializeField]
    private Color[] _colors;
    public Color[] Colors { get { return _colors; } }

    [SerializeField] public List<MapImageData> MapList;

    private event Action<int> _setClasterCountEvent;
    public event Action<int> SetClasterCountEvent
    {
        add { _setClasterCountEvent += value; }
        remove { _setClasterCountEvent -= value; }
    }

    private event Action<int> _showOnMapColumnEvent;
    public event Action<int> ShowOnMapColumnEvent
    {
        add { _showOnMapColumnEvent += value; }
        remove { _showOnMapColumnEvent -= value; }
    }

    private event Action _updateButtonEvent;
    public event Action UpdateButtonEvent
    {
        add { _updateButtonEvent += value; }
        remove { _updateButtonEvent -= value; }
    }

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

   

    public void SetMapColumn(List<string> value)
    {
        _mapColumnDropdown.ClearOptions();
        _mapColumnDropdown.AddOptions(value);
    }

    public void SetMapClusterCount(List<string> value)
    {
        _mapClusterCountDropdown.ClearOptions();
        _mapClusterCountDropdown.AddOptions(value);
    }

    public void UpdateData()
    {
        if (_updateButtonEvent != null)
        {
            _updateButtonEvent.Invoke();
        }
    }

    public void SetClasterCount(int index)
    {
        if (_setClasterCountEvent != null)
        {
            _setClasterCountEvent.Invoke(index);
        }
    }

    public void ShowOnMapColumn(int index)
    {
        if (_showOnMapColumnEvent != null)
        {
            _showOnMapColumnEvent.Invoke(index);
        }
    }

    

    
}
