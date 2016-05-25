using UnityEngine;
using System.Collections;
using Assets.Scripts.MVC;
using System;
using Assets.Scripts.Managers;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers.UI.Windows;

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

        _view.OnNextButtonEvent += onNextButtonEventHeandler;
    }

    private void onNextButtonEventHeandler()
    {
        _windowManager.Open(WindowType.RegionsIndicators);
    }

    public override void UnMediate()
    {
       
    }
}
