using System;
using System.Collections.Generic;
using UnityEngine;

public class DataGridLine : UIItem
{
    [SerializeField]
    private GameObject _column;

    private List<DataGridColumn> _dataGridColumns;

    [SerializeField]
    private GameObject _item;

    [SerializeField]
    private Vector2 _itemSize;

    [SerializeField]
    private Vector2 _itemOffset;

    private List<string> _data;

    public void Init(List<string> data, GameObject column, GameObject item, Vector2 itemSize, Vector2 itemOffset)
    {
        _column = column;
        _item = item;
        _itemSize = itemSize;
        _itemOffset = itemOffset;

        _dataGridColumns = new List<DataGridColumn>();

        _data = data;

        Draw();
    }

    void Draw()
    {
        var columNuber = 0;
        foreach (var value in _data)
        {
            var dataGridColumn = AddChild<DataGridColumn>(_column);

            dataGridColumn.Init(value, _item);

            _dataGridColumns.Add(dataGridColumn);

            columNuber++;
        }
    }
}
