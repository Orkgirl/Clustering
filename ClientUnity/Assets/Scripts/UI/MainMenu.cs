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
    private LizmaState _lizmaState;

    [SerializeField]
    private Color[] _colors;

    private StorageMapData _mapData;


    void Start ()
    {
        _mapData = DataStorage.LoadData();

        InitDataGrid(_mapData);
        InitCluster(_mapData);

        ShowMapClaser(Clustering.GetClasters(4));

        _dataGrid.Hide();
        _map.Hide();
        _lizmaState.Hide();
    }

    void Update()
    {
        
    }

    public void ShowMapClaser(List<ClusterUnit> clasers)
    {
        var map = Clustering.GetNormalize();

        for (var i = 0; i < clasers.Count; i++)
        {
            var item = map.GetFirstInRow(clasers[i].Row);
            _map.SetColor(item.Id, _colors[clasers[i].Cluster]);
        }
    }

    public void ShowMapAllParam(string key, Color color)
    {
        var map = Clustering.GetNormalize();

        if (map == null)
        {
            return;
        }

        List<ClusterDataItem> column = map.ColumnsToList(key);
        
        column.Sort((x, y) => x.Value.CompareTo(y.Value));

        for (var i = 0; i < column.Count; i++)
        {
            _map.SetColor(column[i].Id, _colors[i]);
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
        _lizmaState.Hide();
    }

    public void ShowMapButtonHendler()
    {
        _map.Show();
        _dataGrid.Hide();
        _lizmaState.Hide();
    }

    public void SHowLizmaState()
    {
        _map.Hide();
        _dataGrid.Hide();
        _lizmaState.Show();
    }
}
