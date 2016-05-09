using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.MVC;
using UnityEngine.UI;
using System;

public class TopMenuView : ViewBase
{
    [SerializeField]
    private Dropdown _dropdownDataTable;

    private event Action _loadButtonEvent;
    public event Action LoadButtonEvent
    {
        add { _loadButtonEvent += value; }
        remove { _loadButtonEvent -= value; }
    }

    private event Action _dataGridButtonEvent;
    public event Action DataGridButtonEvent
    {
        add { _dataGridButtonEvent += value; }
        remove { _dataGridButtonEvent -= value; }
    }

    private event Action _indicatorButtonEvent;
    public event Action IndicatorButtonEvent
    {
        add { _indicatorButtonEvent += value; }
        remove { _indicatorButtonEvent -= value; }
    }

    private event Action _analizeButtonEvent;
    public event Action AnalizeButtonEvent
    {
        add { _analizeButtonEvent += value; }
        remove { _analizeButtonEvent -= value; }
    }

    private event Action _mapButtonEvent;
    public event Action MapButtonEvent
    {
        add { _mapButtonEvent += value; }
        remove { _mapButtonEvent -= value; }
    }

    private event Action _startButtonEvent;
    public event Action StartButtonEvent
    {
        add { _startButtonEvent += value; }
        remove { _startButtonEvent -= value; }
    }

    private event Action<string> _selectTableSelect;
    public event Action<string> SelectTableSelect
    {
        add { _selectTableSelect += value; }
        remove { _selectTableSelect -= value; }
    }

    private List<string> _dataList;
    public void SetTableSelect(List<string> value)
    {
        _dataList = value;
        if (_dataList.Count > 0)
        {
            ChooseDropdownHendler(0);
            _dropdownDataTable.AddOptions(_dataList);
        }
    }

    public void ChooseDropdownHendler(int index)
    {
        if (index < _dataList.Count)
        {
            var key = _dataList[index];
            ChooseDropdownHendler(key);
        }
    }

    public void ChooseDropdownHendler(string value)
    {
        if (_selectTableSelect != null)
        {
            _selectTableSelect.Invoke(value);
        }
    }

    public void LoadButtonHendler()
    {

        if (_loadButtonEvent != null)
        {
            _loadButtonEvent.Invoke();
        }
    }

    public void IndicatorButtonHendler()
    {

        if (_indicatorButtonEvent != null)
        {
            _indicatorButtonEvent.Invoke();
        }
    }

    public void DataGridButtonHendler()
    {
        if (_dataGridButtonEvent != null)
        {
            _dataGridButtonEvent.Invoke();
        }
    }

    public void AnalizeButtonHendler()
    {
        if (_analizeButtonEvent != null)
        {
            _analizeButtonEvent.Invoke();
        }
    }

    public void MapButtonHendler()
    {
        if (_mapButtonEvent != null)
        {
            _mapButtonEvent.Invoke();
        }
    }
}
