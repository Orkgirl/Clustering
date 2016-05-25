using UnityEngine;
using Assets.Scripts.MVC;
using UnityEngine.UI;
using System;

public class StartView : ViewBase
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

    [SerializeField]
    private Text _textHeader;
    public string TextHeader
    {
        get
        {
            return _textHeader.text;
        }
        set
        {
            _textHeader.text = value;
        }
    }


    [SerializeField]
    private Text _author;
    public string Author
    {
        get
        {
            return _author.text;
        }
        set
        {
            _author.text = value;
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
