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

}
