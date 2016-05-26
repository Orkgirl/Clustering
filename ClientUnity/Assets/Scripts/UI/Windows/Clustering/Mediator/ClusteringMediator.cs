using UnityEngine;
using System.Collections;
using Assets.Scripts.MVC;

public class ClusteringMediator : MediatorBase
{
    private ClusteringView _view;

    public override void Mediate(ViewBase view)
    {
        _view = (ClusteringView) view;
    }
    public override void UnMediate()
    {

    }
    public override ViewBase View
    {
        get { return _view; }
    }

}
