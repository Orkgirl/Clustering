using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private BaseState _base;

    [SerializeField]
    private Map _map;

    [SerializeField]
    private DataGrid _dataGrid;

    [SerializeField]
    private Analyze _analyze;

    

    [SerializeField]
    private Dropdown _dropdownDataTable;

    //private StorageMapData _mapData;


    private void Start()
    {
        var dataList = DataStorageConfig.TableNames.Keys.ToList();
        if (dataList.Count > 0)
        {
            ChooseDropdownHendler(0);
            _dropdownDataTable.AddOptions(dataList);
        }

        _analyze.Hide();
        _base.Show();
        _dataGrid.Hide();
        _map.Hide();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    

    private void InitCluster(StorageMapData storageMapData)
    {
        var result = new ClusterMap ();

        foreach (var location in storageMapData.map)
        {
            foreach (var storagelocationDataKeyValue in location.data)
            {
                result.Add(new ClusterDataItem() {Row = location.name, Column = storagelocationDataKeyValue.key, Id = location.id, Value = storagelocationDataKeyValue .value});
            }
        }

        Clustering.Init(result);
    }

    private void InitDataGrid(ClusterMap clusterMap)
    {
        var header = new List<string>() {"Name:"};
        header.AddRange(clusterMap.ColumnsKeys);

        Dictionary<string, List<string>> stringData = new Dictionary<string, List<string>>();

        foreach (var rowsKey in clusterMap.RowsKeys)
        {
            var stringList = new List<string>() {rowsKey};
            foreach (var clusterDataItem in clusterMap.RowsToList(rowsKey))
            {

                stringList.Add(clusterDataItem.Value.ToString());
            }

            stringData.Add(rowsKey, stringList);
        }

        _dataGrid.Init(header, stringData);

    }

    public void LoadButtonHendler()
    {
        if (!Clustering.IsInitialize)
        {
            return;
        }

        _analyze.Hide();
        _base.Hide();
        _map.Hide();
        _dataGrid.Show();

        InitDataGrid(Clustering.GetRaw());
    }

    public void NormalizeButtonHendler()
    {
        if (!Clustering.IsInitialize)
        {
            return;
        }

        _analyze.Hide();
        _base.Hide();
        _map.Hide();
        _dataGrid.Show();

        InitDataGrid(Clustering.GetNormalize());
    }

    public void ShowMapButtonHendler()
    {
        if (!Clustering.IsInitialize)
        {
            return;
        }

        _analyze.Hide();
        _base.Hide();
        _dataGrid.Hide();
        _map.Show();
    }


    public void AnalyzeButtonHendler()
    {
        if (!Clustering.IsInitialize)
        {
            return;
        }

        _base.Hide();
        _dataGrid.Hide();
        _map.Hide();
        _analyze.Show();
    }

    public void ChooseDropdownHendler(int index)
    {
        var list = DataStorageConfig.TableNames.Keys.ToList();
        if (index < list.Count)
        {
            var key = list[index];

            string tableName;
            if (DataStorageConfig.TableNames.TryGetValue(key, out tableName))
            {
                ChooseDropdownHendler(tableName);
            }
        }

    }

    public void ChooseDropdownHendler(string value)
    {
        _base.Hide();
        _dataGrid.Hide();
        _map.Hide();

        InitCluster(DataStorage.LoadData(value));
    }
}
