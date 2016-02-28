using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DataGridItem : UIItem
{
    [SerializeField] private Text _label;

    [SerializeField]
    private string _value;

    public void Init(string value)
    {
        _value = value;

        Draw();
    }

    void Draw()
    {
        if (_label != null)
        {
            _label.text = _value;
        }
    }
}