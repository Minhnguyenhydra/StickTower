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
        gameObject.SetActive(false);
    }    
    public void BtnNext()
    {
        DataParam.currentLevel++;
        GameController.instance.levelController.PlayAnimWin();

        //Application.LoadLevel(Application.loadedLevelName);
        //Debug.LogError("======= load next level");
    }
}
