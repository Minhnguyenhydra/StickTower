using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//Do nếu để cùng Canvas tổng thì Canvas Pig sẽ có oder layer bằng với Canvas tổng, mà Skeleton muốn gắn lên Canvas tổng phải có Oder layer cao hơn, do đó các Skeleton ko liên quan sẽ bị nổi lên ở Canvas Pink Bank này, do đó phải gắn canvas Pig Bank này ở canvas riêng có chỉ số Oder cao hơn
public class CanvasBonusSkill : UICanvas
{
    public Animator anim_RateUs;
    public List<GameObject> list_Obj_Btn_Skill;
    private void Awake()
    {
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        //int level = PlayerPrefs_Manager.Get_Index_Level_Normal() - 1;
        //for (int i = 0; i < list_Obj_Btn_Skill.Count; i++)
        //{
        //    list_Obj_Btn_Skill[i].SetActive(false);
        //}
        //if (level == 11)
        //{
        //    list_Obj_Btn_Skill[ 0].SetActive(true);
        //}
        //else if (level == 14)
        //{
        //    list_Obj_Btn_Skill[ 1].SetActive(true);
        //}
        //else if (level == 16)
        //{
        //    list_Obj_Btn_Skill[2 ].SetActive(true);
        //}
        //else
        //{
        //    list_Obj_Btn_Skill[0].SetActive(true);
        //}
        Player.ins.is_Block_Raycas = true;
    }
    
    //
    //public void Get_Button1()
    //{
    //    SoundManager.Ins.PlayFx(FxID.click);
    //    int level = PlayerPrefs_Manager.Get_Index_Level_Normal() - 1;//vì đã tăng trc khi vào đây nên - 1
    //    if (level == 11 )
    //    {
    //        PlayerPrefs_Manager.Set_Index_Skill_Reach(1);
    //    }
    //    else if (level == 14)
    //    {
    //        PlayerPrefs_Manager.Set_Index_Skill_Reach(2);
    //    }
    //    StartCoroutine(IE_DelayClose());
    //}
    public void Sword_Button1()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        string name_Skin = Constant.Get_Skin_Name_By_Id_Sword(2);
        Player.ins.Set_Skin(name_Skin);
        Player.ins.health_Bar.Set_Step_By_Step_Health(Player.ins.health, Player.ins.health + 2, 1);// +2 damge
        Player.ins.Set_Add_Health(2);//X2 damge
                                          //bật anim ở Hero nhận đc dame
        Player.ins.Set_Anim_TakeSword();
        StartCoroutine(IE_DelayClose());
    }
    //
    
    public void Sword_Button2()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        string name_Skin = Constant.Get_Skin_Name_By_Id_Sword(2);
        Player.ins.Set_Skin(name_Skin);
        Player.ins.health_Bar.Set_Step_By_Step_Health(Player.ins.health, Player.ins.health + 2, 1);// +2 damge
        Player.ins.Set_Add_Health(2);//X2 damge
                                          //bật anim ở Hero nhận đc dame
        Player.ins.Set_Anim_TakeSword();
        StartCoroutine(IE_DelayClose());
    }
    //
    
    
    public void No_Thank_Button()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        
        StartCoroutine(IE_DelayClose());
    }
    IEnumerator IE_DelayClose()
    {
        Player.ins.is_Block_Raycas = false;
        anim_RateUs.SetTrigger(Constant.Trigger_PigBankClose);
        yield return Cache.GetWFS(Constant.Time_Delay_PigBank_Close*2);
        Close();
    }
}
