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
    public GameObject obj_Btn_Home;
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
        intKey = 3;
        obj_Btn_Home.SetActive(false);
        obj_X3_Key.SetActive(false);
        StartCoroutine(IE_Delay_ShowHomeBtn());
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
    IEnumerator IE_Delay_ShowHomeBtn()
    {
        yield return Cache.GetWFS(3);
        obj_Btn_Home.SetActive(true);
    }
    public void Homebutton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        e_Event_Close.Invoke();
        //UIManager.Ins.OpenUI(UIID.UICMainMenu);
        Close();
    }
    public void ADsTake_3_Key_button()
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
