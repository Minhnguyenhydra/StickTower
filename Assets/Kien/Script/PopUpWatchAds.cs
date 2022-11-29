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
            GameController.instance.levelController.NextStep();
            Debug.LogError("======= chua xoa lan nao"); 
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
