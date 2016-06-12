using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.HUD;
using Assets.Scripts.Managers.Net;
using Assets.Scripts.Managers.Windows;
using Assets.Scripts.MVC;

public class ServerConnectMediator : MediatorBase
{
    private ServerConnectView _view;

    private HUDManager _hudManager;
    private WindowsManager _windowManager;
    private NetManager _netManager;


    public override void Mediate(ViewBase view)
    {
        _view = (ServerConnectView)view;

        _windowManager = EntityContext.Get<WindowsManager>();
        _hudManager = EntityContext.Get<HUDManager>();
        _netManager = EntityContext.Get<NetManager>();

        _view.OnConnectButtonEvent += OnConnectButtonEvent;
        _netManager.ConnectStatusEvent += OnConnectStatusEvent;

        _netManager.DataReceivedEvent += NetManagerOnDataReceivedEvent;

    }

    private void NetManagerOnDataReceivedEvent()
    {
        _windowManager.Open(WindowType.RegionsIndicators);
        _hudManager.ShowHUD();
    }

    private void OnConnectStatusEvent(bool status)
    {
        if (status)
        {
            //TODO Show status
        }
        else
        {
            //TODO Show error
        }
    }

    private void OnConnectButtonEvent()
    {
        string address = _view.ServerAdressInput;
        if (string.IsNullOrEmpty(address))
        {
            //TODO Show error
            return;
        }
        int port;
        if (!int.TryParse(_view.ServerPortInput, out port))
        {
            //TODO Show error
            return;
        }
        _netManager.Connect(address, port);
    }

    public override void UnMediate()
    {
        _view.OnConnectButtonEvent -= OnConnectButtonEvent;
    }
    public override ViewBase View
    {
        get { return _view; }
    }
}
