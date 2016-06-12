using UnityEngine;
using System.Collections;
using Assets.Scripts.MVC;
using System;
using Assets.Scripts.Managers;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers.Windows;

public class HeaderMediator : MediatorBase
{
    private WindowsManager _windowManager;

    private HeaderView _view;

    public override ViewBase View
    {
        get
        {
            return _view;
        }
    }

    public override void Mediate(ViewBase view)
    {
        _view = (HeaderView)view;
        _windowManager = EntityContext.Get<WindowsManager>();

        _view.OnFirstButtonEvent += onFirstButtonEventHeandler;
        _view.OnSecondButtonEvent += onSecondButtonEventHeandler;
        _view.OnThirdButtonEvent += onThirdButtonEventHeandler;
        _view.OnFourthButtonEvent += onFourthButtonEventHeandler;
    }

    private void onFirstButtonEventHeandler()
    {
        _windowManager.Open(WindowType.RegionsIndicators);
    }

    private void onSecondButtonEventHeandler()
    {
        _windowManager.Open(WindowType.SelectedIndicators);
    }

    private void onThirdButtonEventHeandler()
    {
        _windowManager.Open(WindowType.Clustering);
    }

    private void onFourthButtonEventHeandler()
    {
        _windowManager.Open(WindowType.Map);
    }

    public override void UnMediate()
    {
       
    }
}
