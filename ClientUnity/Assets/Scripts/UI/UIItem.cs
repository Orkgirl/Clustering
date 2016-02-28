using System;
using UnityEngine;
using System.Collections;

public class UIItem : MonoBehaviour
{

    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        this.gameObject.SetActive(true);
    }

    public virtual T AddChild<T>(GameObject prefab) where T : UIItem
    {
        return AddChild<T>(prefab, Vector3.zero, Quaternion.identity);
    }

    public virtual T AddChild<T>(GameObject prefab, Vector3 position) where T : UIItem
    {
        return AddChild<T>(prefab, position, Quaternion.identity);
    }

    public virtual T AddChild<T>(GameObject prefab, Vector3 position, Quaternion rotation) where T : UIItem
    {
        if (prefab == null)
        {
            throw new Exception(this.name + " AddChild invalid prefab: null");
            return null;
        }
        var go = Instantiate(prefab) as GameObject;

        go.transform.SetParent(this.transform);
        go.transform.localPosition = position;
        go.transform.localRotation = rotation;
        go.transform.localScale = Vector3.one;

        var result = go.GetComponent<T>();

        if (result == null)
        {
            Destroy(go);
            throw new Exception(this.name + " AddChild invalid prefab: " + prefab.name);
            return null;
        }

        return result;
    }

}
