
using System;
using UnityEngine;

public class DataGridColumn : UIItem
{
    [SerializeField]
    private GameObject _item;

    private DataGridItem _dataGridItem;

    [SerializeField]
    private string _value;

    public void Init(string value, GameObject item)
    {
        _item = item;
        _value = value;

        Draw();
    }

    void Draw()
    {
        var _dataGridItem = AddChild<DataGridItem>(_item);
        _dataGridItem.Init(_value);
    }
}
