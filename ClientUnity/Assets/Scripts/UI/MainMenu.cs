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

   
    void Start ()
    {
        var mapData = DataStorage.LoadData();

        InitDataGrid(mapData);
        InitCluster(mapData);
        
        //_map.SetColor(_locations[Random.Range(0, _locations.Count)], new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));

        _dataGrid.Hide();
        _map.Hide();
    }

    void Update()
    {
        
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
