using UnityEngine;
using System.Collections;
using Assets.Scripts.MVC;
using System.Collections.Generic;

public class SelectedIndicatorsView : ViewBase
{
    [SerializeField]
    private UIItem _HorisontalIndicatorsContainer;

    [SerializeField]
    private IndicatorsContainerView _verticalIndicatorsPrefab;

    public void SetData (List<SelectedRegionsData> list)
    {
        foreach (var regionData in list)
        {
            var region = _HorisontalIndicatorsContainer.AddChild(_verticalIndicatorsPrefab);
            region.SetData(regionData);
        } 
    }
}

public struct SelectedIndicatorsData
{
    public int Id;
    public string Name;
    public float Value;
}

public struct SelectedRegionsData
{
    public int Id;
    public string Name;
    public List<SelectedIndicatorsData> Indicators;
}