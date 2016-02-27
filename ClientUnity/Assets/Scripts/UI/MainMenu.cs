using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private Map _map;

    [SerializeField]
    private DataGrid _dataGrid;

    // Use this for initialization
    void Start ()
    {
        _dataGrid.Hide();
        _map.Hide();
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
