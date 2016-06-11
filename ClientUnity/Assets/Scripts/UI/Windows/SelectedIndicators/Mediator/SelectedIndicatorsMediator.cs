using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.MVC;
using System;

public class SelectedIndicatorsMediator : MediatorBase
 {
    private SelectedIndicatorsView _view;

    public override void Mediate(ViewBase view)
    {
        _view = (SelectedIndicatorsView)view;

        var ssss = new float[5][];
        for (var i = 0; i < ssss.Length; i++)
        {
            ssss[i] = new float[5];
            for (var j = 0; j < ssss[i].Length; j++)
            {
                ssss[i][j] = UnityEngine.Random.Range(0, 100);
            }
        }
            _view.SetData(ssss);
    }
    public override void UnMediate()
    {

    }
    public override ViewBase View
    {
        get { return _view; }
    }

}
