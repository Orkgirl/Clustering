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

    private List<string> _locations;

    // Use this for initialization
    void Start ()
    {
        var mapData = DataStorage.LoadData();

        _locations = _map.GetAllLocations();

        var header = mapData.header.ToList();

        Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
        foreach (var location in mapData.map)
        {
            var dataList = new List<string>();
            foreach (var locationDataKeyValue in location.data)
            {
                dataList.Add(locationDataKeyValue.value.ToString());
            }
            data.Add(location.name, dataList);

            _map.SetColor(location.id, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        }

        _dataGrid.Init(header, data);

        //_map.SetColor(_locations[Random.Range(0, _locations.Count)], new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));

        _dataGrid.Hide();
        _map.Hide();
    }

    void Update()
    {
        
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
