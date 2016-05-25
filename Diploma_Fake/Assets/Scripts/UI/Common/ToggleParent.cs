using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class ToggleParent : Toggle {

    public Guid Id;
    public Text Label;
    public List<Toggle> child = new List<Toggle>();

    
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        
        foreach(var item in child)
        {
            item.isOn = this.isOn;
        }
    }
}
