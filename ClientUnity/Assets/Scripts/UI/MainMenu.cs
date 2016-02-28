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
    }

    public void ShowMapButtonHendler()
    {
        _map.Show();
        _dataGrid.Hide();
    }

}
