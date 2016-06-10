using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MVC;

public class RegionsIndicatorsView : ViewBase
{
    [SerializeField]
    private UIItem _indicatorsContainer;

    [SerializeField]
    private ToggleParent _toggleParentPrefab;

    public ToggleParent ToggleParentPrefab
    {
        get { return _toggleParentPrefab; }
        set { _toggleParentPrefab = value; }
    }

    public void SetIndicators(Dictionary<int, string> indicators)
    {
        foreach (var indicator in indicators)
        {
            var item = _indicatorsContainer.AddChild<ToggleParent>(_toggleParentPrefab.gameObject);
            item.Id = indicator.Key;
            item.Label.text = indicator.Value;
        }
    }

}
