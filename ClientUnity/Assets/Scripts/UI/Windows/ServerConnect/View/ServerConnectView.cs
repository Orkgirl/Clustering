using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.MVC;
using UnityEngine.UI;

public class ServerConnectView : ViewBase
{
    private event Action _onConnectButtonEvent;
    public event Action OnConnectButtonEvent
    {
        add
        {
            _onConnectButtonEvent += value;
        }
        remove
        {
            _onConnectButtonEvent -= value;
        }
    }

    [SerializeField]
    private InputField _serverAdressInput;
    [SerializeField]
    private InputField _serverPortInput;
    [SerializeField]
    private InputField _serverUserNameInput;
    [SerializeField]
    private InputField _serverPasswordInput;

    public string ServerAdressInput
    {
        get { return _serverAdressInput.text; }
        set { _serverAdressInput.text = value; }
    }

    public string ServerPortInput
    {
        get { return _serverPortInput.text; }
        set { _serverPortInput.text = value; }
    }

    public string ServerUserNameInput
    {
        get { return _serverUserNameInput.text; }
        set { _serverUserNameInput.text = value; }
    }

    public string ServerPasswordInput
    {
        get { return _serverPasswordInput.text; }
        set { _serverPasswordInput.text = value; }
    }

    public void ConnectButtonHebdler()
    {
        if (_onConnectButtonEvent != null)
        {
            _onConnectButtonEvent.Invoke();
        }
    }
}
