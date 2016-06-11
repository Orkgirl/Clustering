using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class IndicatorItemView : MonoBehaviour {

    [SerializeField]
    private Text _label;

    public string Label
    {
        set
        {
            _label.text = value;
        }

        get

        {
            return _label.text;
        }
    }   
}
