using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnShowManhTranh : MonoBehaviour
{
    public GameObject warning;
    void DisplayWarningPart()
    {
        Debug.LogError("=========" + DataParam.newPartDelete);
        warning.SetActive(DataParam.newPartDelete);
    }    
    private void Start()
    {
        DataParam.displayWarningPart = DisplayWarningPart;
        DataParam.displayWarningPart();
    }
    public void BtnManhTranh()
    {
        Datacontroller.instance.ShowLoadingPanel(true,"DeleteScene");
        SoundManager.Ins.PlayFx(FxID.click);

        if(SceneManager.GetActiveScene().name == "Loading")
        {
            EventController.MAIN_CLICK("icon_manhtranh_click");
        }    
        else
        {
            EventController.GAME_PLAY("icon_manhtranh_click");
        }

        DataParam.newPartDelete = false;
    }
}
