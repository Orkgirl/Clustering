using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.MVC;
using System;

public class SelectedIndicatorsMediator : MediatorBase
 {
    private SelectedIndicatorsView _view;

    public override void Mediate(ViewBase view)
    {
        _view = (SelectedIndicatorsView)view;

        _view.SetData(new List<SelectedRegionsData>() { new SelectedRegionsData() { Id = 0, Name = "hhhh", Indicators = new List<SelectedIndicatorsData> { new SelectedIndicatorsData() { Id = 0, Name = "gggg", Value = 6666999 } } } });
    }
    public override void UnMediate()
    {

    }
    public override ViewBase View
    {
        get { return _view; }
    }

}
