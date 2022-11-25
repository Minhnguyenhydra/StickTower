using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopUpProperties : MonoBehaviour
{
    Vector2 scale = new Vector2(1.005f, 1.005f);
    public RectTransform rect;

    public void SetRect(Transform parent)
    {
        transform.parent = parent;
        rect.transform.localScale = scale;
        rect.offsetMin = Vector3.zero;
        rect.offsetMax = Vector3.zero;
        rect.anchorMax = Vector3.one;
        rect.anchorMin = Vector3.zero;
        rect.pivot = Vector3.one / 2;
        rect.localPosition = Vector3.zero;
    }

    public virtual void OpenMe()
    {
        gameObject.SetActive(true);
    }
    public virtual void CloseMe()
    {
        gameObject.SetActive(false);
    }
}
