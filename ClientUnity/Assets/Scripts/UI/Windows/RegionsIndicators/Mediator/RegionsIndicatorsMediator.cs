using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MVC;

public class RegionsIndicatorsMediator : MediatorBase
{
    private RegionsIndicatorsView _view;

    public override void Mediate(ViewBase view)
    {
        _view = (RegionsIndicatorsView)view;

        _view.SetIndicators(new Dictionary<int, string>() { {104,"blabla"}, {1,"huut"}, { 45, "ololo"} });
    }
    public override void UnMediate()
    {

    }
    public override ViewBase View
    {
        get { return _view; }
    }

}
