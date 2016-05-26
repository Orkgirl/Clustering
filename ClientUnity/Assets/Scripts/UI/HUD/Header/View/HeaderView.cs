using UnityEngine;
using System.Collections;
using Assets.Scripts.MVC;
using System;

public class HeaderView : ViewBase
{

    private event Action _onFirstButtonEvent;
    public event Action OnFirstButtonEvent
    {
        add
        {
            _onFirstButtonEvent += value;
        }
        remove
        {
            _onFirstButtonEvent -= value;
        }
    }

    private event Action _onSecondButtonEvent;
    public event Action OnSecondButtonEvent
    {
        add
        {
            _onSecondButtonEvent += value;
        }
        remove
        {
            _onSecondButtonEvent -= value;
        }
    }

    private event Action _onThirdButtonEvent;
    public event Action OnThirdButtonEvent
    {
        add
        {
            _onThirdButtonEvent += value;
        }
        remove
        {
            _onThirdButtonEvent -= value;
        }
    }

    private event Action _onFourthButtonEvent;
    public event Action OnFourthButtonEvent
    {
        add
        {
            _onFourthButtonEvent += value;
        }
        remove
        {
            _onFourthButtonEvent -= value;
        }
    }




    public void OnFirstButton()
    {
        if (_onFirstButtonEvent != null)
        {
            _onFirstButtonEvent.Invoke();
        }
    }

    public void OnSecondButton()
    {
        if (_onSecondButtonEvent != null)
        {
            _onSecondButtonEvent.Invoke();
        }
    }

    public void OnThirdButton()
    {
        if (_onThirdButtonEvent != null)
        {
            _onThirdButtonEvent.Invoke();
        }
    }

    public void OnFourthButton()
    {
        if (_onFourthButtonEvent != null)
        {
            _onFourthButtonEvent.Invoke();
        }
    }

}
