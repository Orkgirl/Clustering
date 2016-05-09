using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IndicatorItemView : UIItem
{
    [SerializeField]
    private Text _idTextLabel;
    private string _id;
    public string Id
    {
        get { return _id; }
        set
        {
            _id = value;
            _idTextLabel.text = _id;
        }
    }

    private event Action<string> _clickEvent;
    public event Action<string> ClickEvent
    {
        add { _clickEvent += value; }
        remove { _clickEvent -= value; }
    }

    public void ClicItemHendler()
    {
        if (_clickEvent != null)
        {
            _clickEvent.Invoke(Id);
        }
    }
}
