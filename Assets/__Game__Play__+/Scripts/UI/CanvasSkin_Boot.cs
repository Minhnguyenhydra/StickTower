using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasSkin_Boot : UICanvas
{
    public List<Model_Hero_Item> list_Model_Hero_Items;
    private void Start()
    {
        
    }

    public void Set_No_Wear_One_Item(int _id_Item)
    {
        for (int i = 0; i < list_Model_Hero_Items.Count; i++)
        {
            int idd = list_Model_Hero_Items[i].skin_Item_SO.iD;
            if (idd == _id_Item)
            {
                list_Model_Hero_Items[i].Set_Have_No_Wear();
            }
        }
    }
    //public void Homebutton()
    //{
    //    UIManager.Ins.OpenUI(UIID.UICMainMenu);
    //}
    //TOTEST
    [ContextMenu("Reset Shop To Origin")]
    public void tttt()
    {
        PlayerPrefs.SetInt("0", 10);
        PlayerPrefs.SetInt("1", 1);
        PlayerPrefs.SetInt("2", 0);
        PlayerPrefs.SetInt("3", 0);
        PlayerPrefs.SetInt("4", 0);
        PlayerPrefs.SetInt("5", 0);
        PlayerPrefs.SetInt("6", 0);
        PlayerPrefs.SetInt("7", 0);
        PlayerPrefs.SetInt(Constant.Canvas_Skin_First_Time_Open, 1);
    }
    [ContextMenu("Set Full Gold")]
    public void RRRR()
    {
        PlayerPrefs.SetInt(Constant.Player_Gold, 100000);
    }
    [ContextMenu("Reset 0 Gold")]
    public void RRRRQ()
    {
        PlayerPrefs.SetInt(Constant.Player_Gold, 0);
    }
    [ContextMenu("Set Full Gem")]
    public void RRsRR()
    {
        PlayerPrefs.SetInt(Constant.Player_Gem, 100000);
    }
    [ContextMenu("Reset 0 Gem")]
    public void RRdfRRQ()
    {
        PlayerPrefs.SetInt(Constant.Player_Gem, 0);
    }
}
