using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        _locations = _map.GetAllLocations();

        _dataGrid.Init(new List<string>() { "name:", "size:" }, new Dictionary<string, List<string>>()
        {
            {
                "test1", new List<string>() {"fff", "dddd", "ffffss", "hhhhh"}

            },
            {
                "test2", new List<string>() {"fff", "dddd", "ffffss", "hhhhh"}

            },
            {
                "test3", new List<string>() {"fff", "dddd", "ffffss", "hhhhh"}

            }
        });

        _dataGrid.Hide();
        _map.Hide();
    }

    void Update()
    {
        if (_map.gameObject.activeSelf)
        {
            _map.SetColor(_locations[Random.Range(0, _locations.Count)], new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        }
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
