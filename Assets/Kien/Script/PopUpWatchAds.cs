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
        if (DataParam.begin)
        {
            GameController.instance.levelController.SetStep();
            Debug.LogError("======= chua xoa lan nao");
        }
        else
        {
            GameController.instance.levelController.CallIELevel();
            Debug.LogError("======= da xoa lan roi");
        }    
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
