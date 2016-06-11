using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class IndicatorsContainerView : UIItem
{

    [SerializeField]
    private UIItem _indicatorContainer;

    [SerializeField]
    private IndicatorItemView _itemPrefab;


    [SerializeField]
    private Text _title;

    public string Title { get { return _title.text; } set { _title.text = value; } }

    public void SetData(float[] list)
    {
        for (var i = 0; i < list.Length; i++)
        {
           var region = _indicatorContainer.AddChild(_itemPrefab);
            region.Label = list[i].ToString();
        }    
    }
}
