using UnityEngine;
using System.Collections;
using Assets.Scripts.MVC;
using UnityEngine.UI;
using System.Collections.Generic;

public class IndicatorSelectView : ViewBase {
    
    public ToggleParent ToggleParentPrefab;

    public UIItem target;
    public IndicatorNoda indicatorMap;

    public float offsetX = 10;
    public float offsetY = -10;

    public float pendingX = 35;
    public float pendingY = -30;
    public float targetWidth = 500;

    private List<ToggleParent> _list;

    void Start()
    {
        _list = new List<ToggleParent>();

        indicatorMap = new IndicatorNoda();
        indicatorMap.Name = "root";
        indicatorMap.Child = new List<IndicatorNoda>()
        {
            new IndicatorNoda()
            {
                Name = "top1",
                Child = new List<IndicatorNoda>()
                {
                    new IndicatorNoda()
                    {
                        Name = "i1"
                    },
                    new IndicatorNoda()
                    {
                        Name = "i2"
                    },
                    new IndicatorNoda()
                    {
                        Name = "i3"
                    }
                }

            },

            new IndicatorNoda()
            {
                Name = "top2",
                Child = new List<IndicatorNoda>()
                {
                    new IndicatorNoda()
                    {
                        Name = "r1"
                    },
                    new IndicatorNoda()
                    {
                        Name = "r2"
                    },
                    new IndicatorNoda()
                    {
                        Name = "r3"
                    }
                }

            },
        };

        DrawMapItem(indicatorMap, new Vector3(offsetX, offsetY));

        target.GetComponent<RectTransform>().sizeDelta = new Vector2(targetWidth,  Mathf.Abs(_list.Count * pendingY + offsetY * 2));
    }

    

    private List<ToggleParent> DrawMapItem(IndicatorNoda map, Vector3 offset)
    {
        var result = new List<ToggleParent>();

        Vector3 position = new Vector3(offset.x, offsetY + _list.Count * pendingY);

        var itemToggle = target.AddChild<ToggleParent>(ToggleParentPrefab.gameObject);
        itemToggle.transform.localPosition = position;
        itemToggle.Id = map.Id;
        itemToggle.name = map.Name;
        itemToggle.Label.text = map.Name;       
        itemToggle.onValueChanged.AddListener(OnValueChanged);        
        _list.Add(itemToggle);

        if (map.Child != null)
        {
            foreach (var itemMap in map.Child)
            {
                result.AddRange(DrawMapItem(itemMap, new Vector3(offset.x + pendingX, offset.y)));
            }
        }

        foreach (var toggle in result)
        {
            itemToggle.child.Add(toggle);
        }

        result.Add(itemToggle);
        return result;        
    }

    private void OnValueChanged(bool value)
    {

    }

    // Update is called once per frame
    void Update () {
	
	}
}
