using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnShowManhTranh : MonoBehaviour
{
    public void BtnManhTranh()
    {
        Datacontroller.instance.ShowLoadingPanel(true,"DeleteScene");
        SoundManager.Ins.PlayFx(FxID.click);

        if(Application.loadedLevelName == "Loading")
        {
            EventController.MAIN_CLICK("icon_manhtranh_click");
        }    
        else
        {
            EventController.GAME_PLAY("icon_manhtranh_click");
        }    
    }
}
