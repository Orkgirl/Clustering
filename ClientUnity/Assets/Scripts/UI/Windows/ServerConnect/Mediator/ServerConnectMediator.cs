using UnityEngine;
using System.Collections;
using Assets.Scripts.MVC;

public class ServerConnectMediator : MediatorBase
{

	{
    private ServerConnectView _view;

    public override void Mediate(ViewBase view)
    {
        _view = (ServerConnectView)view;
    }
    public override void UnMediate()
    {

    }
    public override ViewBase View
    {
        get { return _view; }
    }

}