using UnityEngine;
using System.Collections;
using Assets.Scripts.MVC;
using System;

public class SelectedIndicatorsMediator : MediatorBase
 {
    private SelectedIndicatorsView _view;

    public override void Mediate(ViewBase view)
    {
        _view = (SelectedIndicatorsView)view;
    }
    public override void UnMediate()
    {

    }
    public override ViewBase View
    {
        get { return _view; }
    }

}
