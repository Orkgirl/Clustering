using UnityEngine;
using System.Collections;
using Assets.Scripts.MVC;

public class MapMediator : MediatorBase
{
    private MapView _view;

    public override void Mediate(ViewBase view)
    {
        _view = (MapView)view;
    }
    public override void UnMediate()
    {

    }
    public override ViewBase View
    {
        get { return _view; }
    }

}
