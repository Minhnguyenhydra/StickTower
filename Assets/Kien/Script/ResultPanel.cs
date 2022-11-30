using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanel : PopUpProperties
{
    [SerializeField]
    GameObject btnNext;
    public void BtnNext()
    {
        //  DataParam.currentLevel++;
        // Application.LoadLevel(Application.loadedLevelName);
        SoundManager.Ins.PlayFx(FxID.click);
        AdsManager.Instance.WatchInterstitialAds(LoadScene);
        Debug.LogError("======= load next level");
    }
    void LoadScene()
    {
        Datacontroller.instance.ShowLoadingPanel(true, "DeleteScene");
    }    
    public override void OpenMe()
    {
        base.OpenMe();
        btnNext.SetActive(false);
        GameController.instance.selectLevelController.DisableBtn();
        StartCoroutine(delayDisplayBtnNext());
    }
    IEnumerator delayDisplayBtnNext()
    {
        yield return DataParam.WAITDELETECHECK;
        btnNext.SetActive(true);
    }
}
