using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers;
using UnityEngine.UI;

public class Analyze : UIItem
{
    private ClasterManager _clasterManager;

    [SerializeField] private GameObject _togglePrefab;

    [SerializeField] private UIItem _rowTogglePanel;

    [SerializeField] private UIItem _columnTogglePanel;

    [SerializeField] private Dropdown _clusterCountDropdown;

    [SerializeField] private DataGrid _dataGrid;

    [SerializeField]
    public InputField _outputClustersCount;
    

    private Dictionary<string, Toggle> _rowToggleList;
    private Dictionary<string, Toggle> _columnToggleList;

    private int _clusterCount = 2;

    public override void Show()
    {
        base.Show();

        _clasterManager = EntityContext.Get<ClasterManager>();

        Clear();

        foreach (var columnsKey in _clasterManager.GetNormalize().ColumnsKeys)
        {
            var columnToggle = _columnTogglePanel.AddChild<Toggle>(_togglePrefab);

            columnToggle.GetComponentInChildren<Text>().text = columnsKey;

            columnToggle.onValueChanged.AddListener(OnToggle);
            _columnToggleList.Add(columnsKey, columnToggle);
        }

        foreach (var rowKey in _clasterManager.GetNormalize().RowsKeys)
        {
            var rowToggle = _rowTogglePanel.AddChild<Toggle>(_togglePrefab);

            rowToggle.GetComponentInChildren<Text>().text = rowKey;

            rowToggle.onValueChanged.AddListener(OnToggle);
            _rowToggleList.Add(rowKey, rowToggle);
        }

        
        var listClusterCount = new List<string>();
        for (var i = 2; i < 27; i++)
        {
            listClusterCount.Add(i.ToString());
        }
        _clusterCountDropdown.ClearOptions();
        _clusterCountDropdown.AddOptions(listClusterCount);

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
        _clusterCount = value + 2;
    }

    public void UpdateData()
    {
        InitDataGrid();
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

    private void InitDataGrid()
    {

        var clasrerUnits = _clasterManager.GetClasters(_clusterCount);

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

        _dataGrid.Init(header, stringData);

    }

    private void Clear()
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
