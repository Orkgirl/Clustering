using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private Map _map;

    [SerializeField]
    private DataGrid _dataGrid;

    [SerializeField]
    private Color[] _colors;

    private StorageMapData _mapData;


    void Start ()
    {
        _mapData = DataStorage.LoadData();

        InitDataGrid(_mapData);
        InitCluster(_mapData);
       
        _dataGrid.Hide();
        _map.Hide();
    }

    void Update()
    {
        
    }

    public void ShowMapClaser(List<ClusterUnit> clasers)
    {
        for (var i = 0; i < clasers.Count; i++)
        {
            foreach (var id in clasers[i].Id)
            {
                _map.SetColor(id, _colors[i]);
            }
        }
    }

    public void ShowMapAllParam(string key, Color color)
    {
        var map = Clustering.GetNormalize();

        if (map == null)
        {
            return;
        }

        List<ClusterColumn> column;
        if (map.Columns.TryGetValue(key, out column))
        {
            column.Sort((x, y) => x.Value.CompareTo(y.Value));

            for (var i = 0; i < column.Count; i++)
            {
                _map.SetColor(column[i].Id, _colors[i]);
            }
        }
    }


    private void InitCluster(StorageMapData storageMapData)
    {
        var result = new ClusterMap {Columns = new Dictionary<string, List<ClusterColumn>>()};

        var header = storageMapData.header.ToList();
        foreach (var VARIABLE in header)
        {
            result.Columns.Add(VARIABLE, new List<ClusterColumn>());
        }

        foreach (var location in storageMapData.map)
        {
            foreach (var storagelocationDataKeyValue in location.data)
            {
                result.Columns[storagelocationDataKeyValue.key].Add(new ClusterColumn() {Name = location.name, Id = location.id, Value = storagelocationDataKeyValue .value});
            }
        }

        Clustering.Init(result);
    }

    private void InitDataGrid(StorageMapData storageMapData)
    {
        var header = storageMapData.header.ToList();

        Dictionary<string, List<string>> stringData = new Dictionary<string, List<string>>();

        foreach (var location in storageMapData.map)
        {
            var stringList = new List<string>();

            stringList.Add(location.name);

            foreach (var locationDataKeyValue in location.data)
            {
                stringList.Add(locationDataKeyValue.value.ToString());
            }

            stringData.Add(location.name, stringList);
        }

        _dataGrid.Init(header, stringData);

    }

    public void LoadButtonHendler()
    {
        _dataGrid.Show();
        _map.Hide();
    }

    public void ShowMapButtonHendler()
    {
        _map.Show();
        _dataGrid.Hide();
    }

}
