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

    public void SetData (float[][] list)
    {

        for(var i = 0; i < list.Length; i++)
        {
            var region = _HorisontalIndicatorsContainer.AddChild(_verticalIndicatorsPrefab);
            region.Title = i.ToString();
            region.SetData(list[i]);           
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