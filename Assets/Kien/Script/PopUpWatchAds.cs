using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWatchAds : PopUpProperties
{
    public void BtnWatchAds()
    {

        SoundManager.Ins.PlayFx(FxID.click);
#if UNITY_EDITOR
        Reward();
#else
        AdsManager.Instance.WatchRewardedAds(Reward, "video_show_nextstep_delete_" + (DataParam.currentLevel + 1));
#endif

    }
    void Reward()
    {
            GameController.instance.levelController.NextStep();
           // Debug.LogError("======= chua xoa lan nao"); 
        CloseMe();
    }    
    public void BtnNext()
    {
       // DataParam.currentLevel++;
        GameController.instance.ShowPopUpResult();
        CloseMe();

        //Application.LoadLevel(Application.loadedLevelName);
        //Debug.LogError("======= load next level");
    }
}
