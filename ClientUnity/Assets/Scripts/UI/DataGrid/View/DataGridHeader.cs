using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DataGridHeader : UIItem
{
    [SerializeField] private GameObject _column;

    private List<DataGridColumn> _dataGridColumns;

    [SerializeField] private GameObject _item;

    [SerializeField] private Vector2 _itemSize;

    [SerializeField] private Vector2 _itemOffset;

    private List<string> _data;

    public void Init(List<string> data)
    {
        if (_dataGridColumns != null && _dataGridColumns.Count > 0)
        {
            foreach (var dataGridColumn in _dataGridColumns)
            {
                Destroy(dataGridColumn.gameObject);
            }
        }

        _dataGridColumns = new List<DataGridColumn>();

        _data = data;

        Draw();
    }

    private void Draw()
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