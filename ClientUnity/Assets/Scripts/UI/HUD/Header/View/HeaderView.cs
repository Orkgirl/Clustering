using UnityEngine;
using System.Collections;
using Assets.Scripts.MVC;
using System;

public class HeaderView : ViewBase
{

    private event Action _onNextButtonEvent;
    public event Action OnNextButtonEvent
    {
        add
        {
            _onNextButtonEvent += value;
        }
        remove
        {
            _onNextButtonEvent -= value;
        }
    }

    public void OnNextButton()
    {
        if (_onNextButtonEvent != null)
        {
            _onNextButtonEvent.Invoke();
        }
    }
}
