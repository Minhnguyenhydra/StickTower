using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasChest : UICanvas
{
    #region Gold_Gem
    public Text txt_Gold;//gold ở bank đầy
    public Text txt_Gem;//gold ở bank đầy
    public Text txt_Level_Set;//gold ở bank đầy
    public Text txt_Level_Set_arena;//gold ở bank đầy
    #endregion
    private void OnEnable()
    {
        Set_Reload_Gold_Gem_Title();
    }
    public void Set_Reload_Gold_Gem_Title()
    {
        txt_Gold.text = PlayerPrefs_Manager.Get_Gold().ToString();
        txt_Gem.text = PlayerPrefs_Manager.Get_Gem().ToString();
    }
    public void Homebutton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        SoundManager.Ins.PlayFx(FxID.click);
        Close();
    }
    [ContextMenu("Set Level")]
    public void SetLEVEL()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        int result = System.Int32.Parse(txt_Level_Set.text);
        result = Mathf.Clamp(result, 0, 50);
        PlayerPrefs_Manager.Set_Index_Level_Normal(result);
    }
    
    [ContextMenu("Set Level")]
    public void SetLEVEL_arena()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        int result = System.Int32.Parse(txt_Level_Set_arena.text);
        result = Mathf.Clamp(result, 0, 50);
        PlayerPrefs.SetInt("LevelArena", result);
    }





    [ContextMenu("Set Full Gold")]
    public void SetFullGold()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        PlayerPrefs.SetInt(Constant.Player_Gold, 100000);
        Set_Reload_Gold_Gem_Title();
    }

    [ContextMenu("Set Full Gold Bank")]
    public void SetFullGoldBank()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        PlayerPrefs.SetInt(Constant.PigBank_Gold, 2000);
        //Set_Reload_Gold_Gem_Title();
    }
    [ContextMenu(" Re Set Full Gold Bank")]
    public void ReSetFullGoldBank()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        PlayerPrefs.SetInt(Constant.PigBank_Gold, 0);
        //Set_Reload_Gold_Gem_Title();
    }
    [ContextMenu("Reset 0 Gold")]
    public void Reset0Gold()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        PlayerPrefs.SetInt(Constant.Player_Gold, 0);
        Set_Reload_Gold_Gem_Title();
    }
    [ContextMenu("Set Full Gem")]
    public void SetFullGem()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        PlayerPrefs.SetInt(Constant.Player_Gem, 100000);
        Set_Reload_Gold_Gem_Title();
    }
    [ContextMenu("Reset 0 Gem")]
    public void Reset0Gem()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        PlayerPrefs.SetInt(Constant.Player_Gem, 0);
        Set_Reload_Gold_Gem_Title();
    }
    [ContextMenu("RESET_LEVEL_0")]
    public void RESET_LEVEL_0()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        PlayerPrefs_Manager.Set_Index_Level_Normal(0);
        Set_Reload_Gold_Gem_Title();

    }
    [ContextMenu("Reset Skin")]
    public void ResetSkin()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        PlayerPrefs.SetInt("0", 10);
        PlayerPrefs.SetInt("1", 0);
        PlayerPrefs.SetInt("2", 0);
        PlayerPrefs.SetInt("3", 0);
        PlayerPrefs.SetInt("4", 0);
        PlayerPrefs.SetInt("5", 0);
        PlayerPrefs.SetInt("6", 0);
        PlayerPrefs.SetInt("7", 0);
        PlayerPrefs.SetInt(Constant.Canvas_Skin_First_Time_Open, 1);
    }
}
