using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.MVC;
using UnityEngine.UI;
using System;

public class TopMenuView : ViewBase
{
    [SerializeField]
    private Dropdown _dropdownDataTable;

    private event Action _onLoadButtonEvent;
    public event Action OnLoadButtonEvent
    {
        add { _onLoadButtonEvent += value; }
        remove { _onLoadButtonEvent -= value; }
    }

    private event Action<string> _onSelectTableSelect;
    public event Action<string> OnSelectTableSelect
    {
        add { _onSelectTableSelect += value; }
        remove { _onSelectTableSelect -= value; }
    }

    private List<string> _dataList;

    public void SetTableSelect(List<string> value)
    {
        _dataList = value;
        if (_dataList.Count > 0)
        {
            ChooseDropdownHendler(0);
            _dropdownDataTable.AddOptions(_dataList);
        }
    }

    public void ChooseDropdownHendler(int index)
    {
        if (index < _dataList.Count)
        {
            var key = _dataList[index];
            ChooseDropdownHendler(key);
        }
    }

    public void ChooseDropdownHendler(string value)
    {
        if (_onSelectTableSelect != null)
        {
            _onSelectTableSelect.Invoke(value);
        }
    }

    public void LoadButtonHendler()
    {

        if (_onLoadButtonEvent != null)
        {
            _onLoadButtonEvent.Invoke();
        }
        //InitDataGrid(Clustering.GetRaw());
    }

    //public void NormalizeButtonHendler()
    //{
    //    _analyze.Hide();
    //    _base.Hide();
    //    _map.Hide();
    //    _dataGrid.Show();
    //    _indicator.Hide();

    //    //InitDataGrid(Clustering.GetNormalize());
    //}

    //public void ShowMapButtonHendler()
    //{
    //    _analyze.Hide();
    //    _base.Hide();
    //    _dataGrid.Hide();
    //    _map.Show();
    //    _indicator.Hide();
    //}


    //public void AnalyzeButtonHendler()
    //{
    //    _base.Hide();
    //    _dataGrid.Hide();
    //    _map.Hide();
    //    _analyze.Show();
    //    _indicator.Hide();
    //}

    //public void IndicatorSelectButtonHendler()
    //{
    //    _analyze.Hide();
    //    _base.Hide();
    //    _map.Hide();
    //    _dataGrid.Hide();
    //    _indicator.Show();
    //}

  

    //private void InitCluster(StorageMapData storageMapData)
    //{
    //    var result = new ClusterMap ();

    //    foreach (var location in storageMapData.map)
    //    {
    //        foreach (var storagelocationDataKeyValue in location.data)
    //        {
    //            result.Add(new ClusterDataItem() {Row = location.name, Column = storagelocationDataKeyValue.key, Id = location.id, Value = storagelocationDataKeyValue .value});
    //        }
    //    }

    //    Clustering.Init(result);
    //}

    //private void InitDataGrid(ClusterMap clusterMap)
    //{
    //    var header = new List<string>() {"Name:"};

    //    header.AddRange(Clustering.Indicators);

    //    Dictionary<string, List<string>> stringData = new Dictionary<string, List<string>>();

    //    foreach (var rowsKey in clusterMap.RowsKeys)
    //    {
    //        var stringList = new List<string>() {rowsKey};
    //        foreach (var clusterDataItem in clusterMap.RowsToList(rowsKey))
    //        {
    //            if (Clustering.Indicators.Contains(clusterDataItem.Column))
    //            {
    //                stringList.Add(clusterDataItem.Value.ToString());
    //            }
    //        }

    //        stringData.Add(rowsKey, stringList);
    //    }

    //    _dataGrid.Init(header, stringData);

    //}


}
