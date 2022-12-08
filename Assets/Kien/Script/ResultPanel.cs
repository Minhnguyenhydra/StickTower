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
        //#if WatchADs
        //        AdsManager.Instance.WatchInterstitialAds(LoadScene);
        //#else
        //        LoadScene();
        //#endif
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
        EventController.WIN_LEVEL_EVENT_DELETE(DataParam.currentLevel + 1);
        StartCoroutine(delayDisplayBtnNext());
    }
    IEnumerator delayDisplayBtnNext()
    {
        yield return DataParam.WAITDELETECHECK;
        btnNext.SetActive(true);
    }
}
