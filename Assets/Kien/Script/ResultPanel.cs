using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanel : PopUpProperties
{
    [SerializeField]
    GameObject btnNext;
    public void BtnNext()
    {
        DataParam.currentLevel++;
        Application.LoadLevel(Application.loadedLevelName);
        Debug.LogError("======= load next level");
    }
    public override void OpenMe()
    {
        base.OpenMe();
        btnNext.SetActive(false);
        StartCoroutine(delayDisplayBtnNext());
    }
    IEnumerator delayDisplayBtnNext()
    {
        yield return DataParam.WAITDELETECHECK;
        btnNext.SetActive(true);
    }
}
