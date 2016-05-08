using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DataGrid : UIItem
{

    [SerializeField] private DataGridHeader _header;

    [SerializeField] private DataGridContent _content;

    public void Init(List<string> header, Dictionary<string, List<string>> data)
    {
        _content.Init(data);
        _header.Init(header);
    }
}
