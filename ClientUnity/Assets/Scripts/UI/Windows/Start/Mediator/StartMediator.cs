using UnityEngine;
using System.Collections;
using Assets.Scripts.MVC;
using System;
using Assets.Scripts.Managers;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers.UI.Windows;

public class StartMediator : MediatorBase
{
    private StartView _view;

    private WindowsManager _windowManager;
    private HUDManager _hudManager;

    public override ViewBase View
    {
        get
        {
            return _view;
        }
    }

    public override void Mediate(ViewBase view)
    {
        _view = (StartView)view;

        _windowManager = EntityContext.Get<WindowsManager>();
        _hudManager = EntityContext.Get<HUDManager>();

        _view.TextHeader = "Ha ha ha!";

        _view.Author = "Lizma - Klizma!!!";

        _view.OnNextButtonEvent += onNextButtonEventHeandler;
    }

    private void onNextButtonEventHeandler()
    {
        _view.TextHeader = "ppppppppppppppp";

        _windowManager.Open(WindowType.RegionsIndicators);
        _hudManager.ShowHUD();
    }

    public override void UnMediate()
    {
        
    }
}
