using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
public class CanvasShop_Prize : UICanvas
{
    #region Gold_Gem
    public Text txt_Gold;
    public Text txt_Gem;
    #endregion
    public GameObject obj_X3_Key;
    public GameObject Group_All_Key;
    public int intKey;
    public List<GameObject> listObj_3_Key_Gold;
    public UnityEvent e_Event_Close;
    [Tooltip("mở hết 2 lần ads ko được hiện nút ads nữa")]
    public int count_Open;//mở hết 2 lần ads ko được hiện nút ads nữa
    private void OnEnable()
    {
        count_Open = 0;
        intKey = Mathf.Clamp(PlayerPrefs_Manager.Get_Number_Key_Treasure(), 0, 3);
        for (int i = 0; i < intKey; i++)
            listObj_3_Key_Gold[i].SetActive(true);
        
        obj_X3_Key.SetActive(false);

        Set_Reload_Gold_Gem_Title();
    }
    public void Set_Reload_Gold_Gem_Title()
    {
        txt_Gold.text = PlayerPrefs_Manager.Get_Gold().ToString("N0");
        txt_Gem.text = PlayerPrefs_Manager.Get_Gem().ToString("N0");
    }
    public void Set_Open_1_Key()
    {
        SoundManager.Ins.PlayFx(FxID.treasure_open);
        if (intKey > 0)
        {
            intKey--;
            PlayerPrefs_Manager.Set_Number_Key_Treasure(intKey);
            listObj_3_Key_Gold[intKey].SetActive(false);
            Set_Reload_Gold_Gem_Title();

        }
        if (intKey == 0)
        {
            Group_All_Key.SetActive(false);
            if (count_Open < 2)
            {
                obj_X3_Key.SetActive(true);
            }
        }
    }

    public void Homebutton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        e_Event_Close.Invoke();
        //UIManager.Ins.OpenUI(UIID.UICMainMenu);
        //Fix UI hero của canvas FreSkin nổi lên ở canvas Prize
        int lv = PlayerPrefs_Manager.Get_Index_Level_Normal() - 1;//vì đã tăng level ở Manager trước đó
        //Debug.Log(lv);
        if (lv == 20)//|| lv == 20
        {
            UIManager.Ins.OpenUI(UIID.UICFreeSkin);
        }
        ((CanvasWinQ)UIManager.Ins.GetUI(UIID.UICWin_Level)).Set_Gold_EFX();

        if (lv == 20)//|| lv == 20
        {
            UIManager.Ins.OpenUI(UIID.UICFreeSkin);
        }
        ((CanvasWinQ)UIManager.Ins.GetUI(UIID.UICWin_Level)).Set_Gold_EFX();

        Close();
    }
    public void ADsTake_3_Key_button()
    {
        AdsManager.Instance.WatchRewardedAds(OpenThreeKeys);
    }

    private void OpenThreeKeys()
    {
        if (count_Open < 2)
        {
            count_Open++;
            SoundManager.Ins.PlayFx(FxID.click);
            obj_X3_Key.SetActive(false);
            Group_All_Key.SetActive(true);
            intKey = 3;
            listObj_3_Key_Gold[0].SetActive(true);
            listObj_3_Key_Gold[1].SetActive(true);
            listObj_3_Key_Gold[2].SetActive(true);
        }
    }
    
}
