using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWatchAds : PopUpProperties
{
    public void BtnWatchAds()
    {
        Reward();
    }
    void Reward()
    {
        DataParam.canDelete = true;
        CloseMe();
    }    
    public void BtnNext()
    {
        DataParam.currentLevel++;
        GameController.instance.levelController.PlayAnimWin();
        CloseMe();

        //Application.LoadLevel(Application.loadedLevelName);
        //Debug.LogError("======= load next level");
    }
}
