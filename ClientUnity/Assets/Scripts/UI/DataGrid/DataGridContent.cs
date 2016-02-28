using System;
using System.Collections.Generic;
using UnityEngine;

public class DataGridContent : UIItem
{
    [SerializeField]
    private GameObject _line;

    private Dictionary<string, DataGridLine> _dataGridLines;

    [SerializeField]
    private GameObject _column;

    [SerializeField]
    private GameObject _item;

    [SerializeField]
    private Vector2 _itemSize;

    [SerializeField]
    private Vector2 _itemOffset;

    public void Init(Dictionary<string, List<string>> data)
    {
        _dataGridLines = new Dictionary<string, DataGridLine>();

        var lineNuber = 0;
        foreach (var key in data.Keys)
        {
            List<string> value;
            if (!data.TryGetValue(key, out value))
            {
                throw new Exception("DataGridContent Init: invalid key or value " + key + " : " + value);
            }

            var dataGridLine = AddChild<DataGridLine>(_line, new Vector3(0f, (_itemSize.y + _itemOffset.y) * -lineNuber, 0f));

            dataGridLine.Init(value, _column, _item, _itemSize, _itemOffset);

            _dataGridLines.Add(key, dataGridLine);

            lineNuber++;
        }
    }
}
