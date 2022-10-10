using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasSkin_Top : UICanvas
{
    public Text txt_Gold;
    public Text txt_Gem;
    private void OnEnable()
    {
        Set_Refresh_Gold_Gem();
    }
    public void Homebutton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        UIManager.Ins.CloseUI(UIID.UICSkin_Boot);
        Close();

    }
    public void Set_Refresh_Gold_Gem()
    {
        txt_Gold.text = PlayerPrefs_Manager.Get_Gold().ToString("N0");
        txt_Gem.text = PlayerPrefs_Manager.Get_Gem().ToString("N0");
    }
}
