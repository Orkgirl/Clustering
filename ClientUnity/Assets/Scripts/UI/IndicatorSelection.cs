using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IndicatorSelection : UIItem
{
    [SerializeField]
    private IndicatorItem _itemPrefab;

    [SerializeField]
    private GameObject _selectedContent;

    [SerializeField]
    private GameObject _allContent;
}

