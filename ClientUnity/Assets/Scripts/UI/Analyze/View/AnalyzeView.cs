using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers;
using Assets.Scripts.MVC;
using Assets.Scripts.UI;
using UnityEngine.UI;

public class AnalyzeView : ViewBase
{
   

    [SerializeField] private GameObject _togglePrefab;

    [SerializeField] private UIItem _rowTogglePanel;

    [SerializeField] private Dropdown _clusterCountDropdown;

    [SerializeField] private DataGridView _dataGrid;

    [SerializeField]
    public InputField _outputClustersCount;
    

    private Dictionary<string, Toggle> _rowToggleList;
    private Dictionary<string, Toggle> _columnToggleList;

   

    private event Action<int> _setClasterCountEvent;
    public event Action<int> SetClasterCountEvent
    {
        add { _setClasterCountEvent += value; }
        remove { _setClasterCountEvent -= value; }
    }

    private event Action _updateDataEvent;
    public event Action UpdateDataEvent
    {
        add { _updateDataEvent += value; }
        remove { _updateDataEvent -= value; }
    }

    public void SetClusterCountDropdown(List<string> data)
    {
        _clusterCountDropdown.ClearOptions();
        _clusterCountDropdown.AddOptions(data);
    }

    public void SetClusterRow(List<string> data)
    {
        foreach (var rowKey in data)
        {
            var rowToggle = _rowTogglePanel.AddChild<Toggle>(_togglePrefab);

            rowToggle.GetComponentInChildren<Text>().text = rowKey;

            rowToggle.onValueChanged.AddListener(OnToggle);
            _rowToggleList.Add(rowKey, rowToggle);
        }

    }

    public void OnToggle(bool selected)
    {
        var clusterMaxCount = 0;
        foreach (var value in _rowToggleList.Values)
        {
            if (value.isOn)
            {
                ++clusterMaxCount;
            }
        }

        var listClusterCount = new List<string>();
        for (var i = 2; i < clusterMaxCount + 1; i++)
        {
            listClusterCount.Add(i.ToString());
        }
        _clusterCountDropdown.ClearOptions();
        _clusterCountDropdown.AddOptions(listClusterCount);
    }

    public void SetClusterCount(int value)
    {
        if (_setClasterCountEvent != null)
        {
            _setClasterCountEvent.Invoke(value);
        }
    }

    public void UpdateData()
    {
        if (_updateDataEvent != null)
        {
            _updateDataEvent.Invoke();
        }
    }


    public void CalculateClustersCount()
    {
        var clusterMaxCount = 0;
        foreach (var value in _rowToggleList.Values)
        {
            if (value.isOn)
            {
                ++clusterMaxCount;
            }
        }
        foreach (var value in _columnToggleList.Values)
        {
            if (value.isOn)
            {
                ++clusterMaxCount;
            }
        }

        int result = clusterMaxCount / 4;

        _outputClustersCount.text = result.ToString();
    }

    public void InitDataGrid(List<ClusterUnit>clasrerUnits)
    {
        var header = new List<string>() { "Name:", "Cluster:" };

        Dictionary<string, List<string>> stringData = new Dictionary<string, List<string>>();

        foreach (var clusrerUnit in clasrerUnits)
        {
            if (_rowToggleList.ContainsKey(clusrerUnit.Row))
            {
                if (!_rowToggleList[clusrerUnit.Row].isOn)
                {
                    continue;
                }
            }
            var stringList = new List<string>() { clusrerUnit.Row, clusrerUnit.Cluster.ToString() };
            //foreach (var clusterDataItem in clusterMap.RowsToList(rowsKey))
            //{

            //    stringList.Add(clusterDataItem.Value.ToString());
            //}

            stringData.Add(clusrerUnit.Row, stringList);
        }

        _dataGrid.SetData(header, stringData);

    }

    public void Clear()
    {
        if (_rowToggleList != null && _rowToggleList.Count > 0)
        {
            foreach (var toggle in _rowToggleList.Values)
            {
                Destroy(toggle.gameObject);
            }
        }
        _rowToggleList = new Dictionary<string, Toggle>();

        if (_columnToggleList != null && _columnToggleList.Count > 0)
        {
            foreach (var toggle in _columnToggleList.Values)
            {
                Destroy(toggle.gameObject);
            }
        }
        _columnToggleList = new Dictionary<string, Toggle>();

    }

}
