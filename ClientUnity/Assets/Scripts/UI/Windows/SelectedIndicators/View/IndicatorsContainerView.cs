using UnityEngine;
using System.Collections;
using System;

public class IndicatorsContainerView : UIItem
{

    [SerializeField]
    private UIItem _indicatorContainer;

    [SerializeField]
    private IndicatorItemView _itemPrefab;

    public void SetData(SelectedRegionsData regionData)
    {
        foreach (var indicatorData in regionData.Indicators)
        {
            var region = _indicatorContainer.AddChild(_itemPrefab);
            region.Label = indicatorData.Value.ToString();
        }
    
    }
}
