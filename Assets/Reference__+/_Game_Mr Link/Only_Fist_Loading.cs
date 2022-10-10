using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Only_Fist_Loading : MonoBehaviour
{
    //dùng để chỉ load 1 lần ở scene Loading
    private void Awake()
    {
        UIManager.Ins.OpenUI(UIID.UICLoading);
    }
}
