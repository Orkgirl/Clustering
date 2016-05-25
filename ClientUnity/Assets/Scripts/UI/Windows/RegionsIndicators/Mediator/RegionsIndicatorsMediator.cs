using UnityEngine;
using System.Collections;

using Assets.Scripts.MVC;

public class RegionsIndicatorsMediator : MediatorBase
{
    private RegionsIndicatorsView _view;

    public override void Mediate(ViewBase view)
    {
        _view = (RegionsIndicatorsView)view;
    }
    public override void UnMediate()
    {

    }
    public override ViewBase View
    {
        get { return _view; }
    }

}
