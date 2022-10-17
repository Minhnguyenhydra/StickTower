using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Model_Hero_Item : MonoBehaviour
{
    public Skin_Item_SO skin_Item_SO;
    Enum_State_Item_Skin _enum_State_Item_Skin;
    [Header("-------------NoHave---------------")]
    public GameObject obj_NoHave;
    public GameObject obj_Btn_Unlock;
    public GameObject obj_Btn_WatchADs;

    public GameObject obj_Btn_Gold_Parrent;
    public GameObject obj_Btn_Enough_Gold;
    public GameObject obj_Btn_Not_Enough_Gold;

    public GameObject obj_Btn_Gem_Parrent;
    public GameObject obj_Btn_Enough_Gem;
    public GameObject obj_Btn_Not_Enough_Gem;
    public GameObject obj_Btn_Try;
    public Text txt_Gold;
    public Text txt_Gem;
    public Text txt_Unlock_Level;
    [Header("-------------Have_No_Wear---------------")]
    public GameObject obj_Have_No_Wear;
    [Header("-------------Have_Wearing---------------")]
    public GameObject obj_Have_Wearing;
    [Header("-------------Phải kéo thả vào---------------")]
    public Transform tf_Spawn_Fire_Work;

    private void OnEnable()
    {
        PlayerPrefs_Manager.Set_First_Time_Open_Canvas_Skin();
        PlayerPrefs_Manager.Set_First_Time_Id_Skin_Wearing();
        Check_Init();
    }

    #region Button
    public void WatchADs_Button()
    {
        Debug.Log("4");
        Change_Hero();
    }
    public void Gold_Button()
    {
        int new_gold = PlayerPrefs_Manager.Get_Gold() - skin_Item_SO.gold;
        if (new_gold >= 0)
        {
            PlayerPrefs_Manager.Set_Gold(new_gold);
            ((CanvasSkin_Top)UIManager.Ins.GetUI(UIID.UICSkin_Top)).Set_Refresh_Gold_Gem();
            Change_Hero();
            GameManager.Ins.Set_Spawn_FireWord(tf_Spawn_Fire_Work);

        }
    }
    public void Gem_Button()
    {
        int new_gem = PlayerPrefs_Manager.Get_Gem() - skin_Item_SO.gem;
        Debug.Log(skin_Item_SO.gem);
        Debug.Log(new_gem);
        if (new_gem >= 0)
        {
            PlayerPrefs_Manager.Set_Gem(new_gem);
            ((CanvasSkin_Top)UIManager.Ins.GetUI(UIID.UICSkin_Top)).Set_Refresh_Gold_Gem();
            Change_Hero();
            GameManager.Ins.Set_Spawn_FireWord(tf_Spawn_Fire_Work);

        }
    }
    public void Try_Button()
    {
        Change_Hero();
    }
    
    public void Can_Click_If_Have_Button()
    {

        Get_State_Skin();
        if (_enum_State_Item_Skin == Enum_State_Item_Skin.Have_Wearing || _enum_State_Item_Skin == Enum_State_Item_Skin.Have_No_Wear)
        {
            Change_Hero();
        }
    }
    
    #endregion


    public void Change_Hero()
    {

        //Đổi trạng thái Skin cũ thành chưa mặc
        int _ID_old_Skin = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing();
        PlayerPrefs_Manager.Set_Have_No_Wear_Skin(_ID_old_Skin);
        PlayerPrefs_Manager.Set_Have_No_Wear_Skin(0);// Fix cái Skin đầu tiên ko thay đổi

        //
        ((CanvasSkin_Boot)UIManager.Ins.GetUI(UIID.UICSkin_Boot)).Set_No_Wear_One_Item(_ID_old_Skin);
        ((CanvasSkin_Boot)UIManager.Ins.GetUI(UIID.UICSkin_Boot)).Set_No_Wear_One_Item(0);
        //
        //Đổi trạng thái Skin mới thành đang mặc
        string str_Skin_Name = Constant.Get_Skin_Name_By_Id(skin_Item_SO.iD);

        Model_Hero_Main_Top.ins.Set_Skin_Action_Jump(str_Skin_Name);
        PlayerPrefs_Manager.Set_ID_Name_Skin_Wearing(skin_Item_SO.iD);
        /////Debug.Log(PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing());
        Set_Wearing();
    }
    public void Set_Wearing()
    {
        Set_Deactive_All_Btn();
        obj_Have_Wearing.SetActive(true);
    }
    public void Set_Have_No_Wear()
    {
        Set_Deactive_All_Btn();
        obj_Have_No_Wear.SetActive(true);
    }
    public void Get_State_Skin()
    {
        
        _enum_State_Item_Skin = PlayerPrefs_Manager.Get_Enum_State_Item_Skin(skin_Item_SO.iD);
        //Debug.Log(PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing());
        
    }
    public void Check_Init()
    {
        Get_State_Skin();
        Set_Deactive_All_Btn();
        if (_enum_State_Item_Skin == Enum_State_Item_Skin.Have_Wearing)
        {
            obj_Have_Wearing.SetActive(true);
        }
        else if (_enum_State_Item_Skin == Enum_State_Item_Skin.Have_No_Wear)
        {
            obj_Have_No_Wear.SetActive(true);
        }
        else
        {
            obj_NoHave.SetActive(true);
            Set_Deactive_All_Btn_No_have();
            int _level_current = PlayerPrefs_Manager.Get_Index_Level_Normal();
            if (skin_Item_SO.watch_ADs)
            {
                if (_level_current >= skin_Item_SO.level_UnLock)
                {
                    if (skin_Item_SO.gold == 0 && skin_Item_SO.gem == 0)
                    {
                        obj_Btn_WatchADs.SetActive(true);

                    }
                }
                else
                {
                    txt_Unlock_Level.text = Constant.String_Shop_Unlock_Level + skin_Item_SO.level_UnLock.ToString();
                    obj_Btn_Unlock.SetActive(true);
                }
                if (skin_Item_SO.watch_ADs)
                {
                    obj_Btn_Try.SetActive(true);
                }
                //
                if (skin_Item_SO.gold > 0)
                {
                    obj_Btn_Gold_Parrent.SetActive(true);
                    if (skin_Item_SO.gold < PlayerPrefs_Manager.Get_Gold())
                    {

                        obj_Btn_Enough_Gold.SetActive(true);
                    }
                    else
                    {
                        obj_Btn_Not_Enough_Gold.SetActive(true);
                    }
                    txt_Gold.text = skin_Item_SO.gold.ToString();
                    obj_Btn_Try.SetActive(true);
                }
                //
                if (skin_Item_SO.gem > 0)
                {
                    obj_Btn_Gem_Parrent.SetActive(true);
                    if (skin_Item_SO.gold < PlayerPrefs_Manager.Get_Gem())
                    {

                        obj_Btn_Enough_Gem.SetActive(true);
                    }
                    else
                    {
                        obj_Btn_Not_Enough_Gem.SetActive(true);
                    }
                    txt_Gem.text = skin_Item_SO.gem.ToString();
                    obj_Btn_Try.SetActive(true);
                }
            }
            else if (skin_Item_SO.gold > 0)
            {
                obj_Btn_Gold_Parrent.SetActive(true);
                if (skin_Item_SO.gold < PlayerPrefs_Manager.Get_Gold())
                {
                    
                    obj_Btn_Enough_Gold.SetActive(true);
                }
                else
                {
                    obj_Btn_Not_Enough_Gold.SetActive(true);
                }
                txt_Gold.text = skin_Item_SO.gold.ToString();
                obj_Btn_Try.SetActive(true);
            }
            else if (skin_Item_SO.gem > 0)
            {
                obj_Btn_Gem_Parrent.SetActive(true);
                if (skin_Item_SO.gold < PlayerPrefs_Manager.Get_Gem())
                {
                    
                    obj_Btn_Enough_Gem.SetActive(true);
                }
                else
                {
                    obj_Btn_Not_Enough_Gem.SetActive(true);
                }
                txt_Gem.text = skin_Item_SO.gem.ToString();
                obj_Btn_Try.SetActive(true);
            }
        }
    }
    public void Set_Deactive_All_Btn()//Tát cả các Btn nhỏ nằm trong các Btn trong 3 phần: đã mua, mua chưa mặc, và chưa mua
    {
        obj_NoHave.SetActive(false);
        obj_Have_No_Wear.SetActive(false);
        obj_Have_Wearing.SetActive(false);
    }
    public void Set_Deactive_All_Btn_No_have()//các btn trong phần chưa mở Skin
    {
        obj_Btn_Not_Enough_Gem.SetActive(false);
        obj_Btn_Gem_Parrent.SetActive(false);
        obj_Btn_Enough_Gem.SetActive(false);
        obj_Btn_Gold_Parrent.SetActive(false);
        obj_Btn_Enough_Gold.SetActive(false);
        obj_Btn_Not_Enough_Gold.SetActive(false);
        obj_Btn_Try.SetActive(false);
        obj_Btn_Unlock.SetActive(false);
        obj_Btn_WatchADs.SetActive(false);
    }
    
}
