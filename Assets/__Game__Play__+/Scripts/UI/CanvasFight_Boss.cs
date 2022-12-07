using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//Do nếu để cùng Canvas tổng thì Canvas Pig sẽ có oder layer bằng với Canvas tổng, mà Skeleton muốn gắn lên Canvas tổng phải có Oder layer cao hơn, do đó các Skeleton ko liên quan sẽ bị nổi lên ở Canvas Pink Bank này, do đó phải gắn canvas Pig Bank này ở canvas riêng có chỉ số Oder cao hơn
public class CanvasFight_Boss : UICanvas
{
    public bool _isPlayer_Stronger;
    public GameObject obj_Health_Red_L;
    public GameObject obj_Health_Red_R;
    public GameObject obj_Hand_tut;
    [Header("Điền idBoss level nay vào đây")]
    public int idBoss;//0-3
    public int numberStar_Rate; 
    public bool isRated;
    public Animator anim_RateUs;
    public List<GameObject> list_Obj_Head_Hero;
    public List<GameObject> list_Obj_Head_Enemy;
    [Tooltip("0: lock... 1 open")]
    public List<GameObject> list_Obj_Btn_Skill_1;//0: lock... 1 open
    [Tooltip("0: lock... 1 open")]
    public List<GameObject> list_Obj_Btn_Skill_2;//0: lock... 1 open
    [Tooltip("0: lock... 1 open")]
    public List<GameObject> list_Obj_Btn_Skill_3;//0: lock... 1 open
    [Header("img health vào đây")]
    public Image img_Health_Player;
    public Image img_Health_Boss;
    public Image img_Health_PlayerRed;
    public Image img_Health_BossRed;
    [Header("=============================")]
    public GameObject Obj_canvas_Start;
    public GameObject Obj_Panel_Start;
    public GameObject Obj_Panel_Hit;
    //
    public Image img_Count_Downt_Skill_1;
    public Image img_Count_Downt_Skill_2;
    public Image img_Count_Downt_Skill_3;
    //
    public float time_Count_Downt_Skill_1;
    public float time_Count_Downt_Skill_2;
    public float time_Count_Downt_Skill_3;
    public float time_Count_Downt_attack;
    //
    public bool isAttacking;
    public bool isSkill_1;
    public bool isSkill_2;
    public bool isSkill_3;
    public bool isBlock;

    private bool canAttack;
    private void Awake()
    {
        isRated = false;
        time_Count_Downt_attack = 0;
        time_Count_Downt_Skill_1 = 0;
        time_Count_Downt_Skill_2 = 0;
        time_Count_Downt_Skill_3 = 0;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (canAttack)
            Attack_Button_();
        
        time_Count_Downt_attack -= Time.deltaTime;
        if (time_Count_Downt_attack <= 0)
        {
            isAttacking = false;
        }
        time_Count_Downt_attack = Mathf.Clamp(time_Count_Downt_attack, 0, 10000);



        time_Count_Downt_Skill_1 -= Time.deltaTime;
        if (time_Count_Downt_Skill_1 <= 0)
        {
            isSkill_1 = false;
        }
        time_Count_Downt_Skill_1 = Mathf.Clamp(time_Count_Downt_Skill_1, 0, 10000);
        img_Count_Downt_Skill_1.fillAmount = time_Count_Downt_Skill_1 / Constant.Time_Count_Skill_1;
        //


        time_Count_Downt_Skill_2 -= Time.deltaTime;
        if (time_Count_Downt_Skill_2 <= 0)
        {
            isSkill_2 = false;
        }
        time_Count_Downt_Skill_2 = Mathf.Clamp(time_Count_Downt_Skill_2, 0, 10000);
        img_Count_Downt_Skill_2.fillAmount = time_Count_Downt_Skill_2 / Constant.Time_Count_Skill_2;
        //


        time_Count_Downt_Skill_3 -= Time.deltaTime;
        if (time_Count_Downt_Skill_3 <= 0)
        {
            isSkill_3 = false;
        }
        time_Count_Downt_Skill_3 = Mathf.Clamp(time_Count_Downt_Skill_3, 0, 10000);
        img_Count_Downt_Skill_3.fillAmount = time_Count_Downt_Skill_3 / Constant.Time_Count_Skill_3;

    }
   
    private void OnEnable()
    {
        if (GameManager.Ins.enemyBoss_auto_Asign != null)
        {
            _isPlayer_Stronger = (Player.ins.health > GameManager.Ins.enemyBoss_auto_Asign.health);
        }
        Player.ins.isHittingBoss = true;
        Obj_Panel_Start.SetActive(true);
        Obj_canvas_Start.SetActive(true);
        Obj_Panel_Hit.SetActive(false);
        int _level = PlayerPrefs_Manager.Get_Index_Level_Normal();//8 cái: 0-7
        int idSkin = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing();//8 cái: 0-7
        int numberSkill = PlayerPrefs_Manager.Get_Index_Skill_Reach() + 1;//3 cái: 0-2
        //
        for (int i = 0; i < list_Obj_Btn_Skill_1.Count; i++)//0: lock... 1 open
        {
            list_Obj_Btn_Skill_1[i].SetActive(false);
        }
        
        for (int i = 0; i < list_Obj_Btn_Skill_2.Count; i++)//0: lock... 1 open
        {
            list_Obj_Btn_Skill_2[i].SetActive(false);
        }
        
        for (int i = 0; i < list_Obj_Btn_Skill_3.Count; i++)//0: lock... 1 open
        {
            list_Obj_Btn_Skill_3[i].SetActive(false);
        }
        Player.ins.health_Bar.Set_Hide_Health_Bar();
        if (GameManager.Ins.enemyBoss_auto_Asign != null)
        {
            GameManager.Ins.enemyBoss_auto_Asign.health_Bar.Set_Hide_Health_Bar();
        }
        //
        //ko sử dụng đến 3 nút kỹ năng nữa
        /*
        #region Check show Skill by Level
        if (_level < 12)
        {
            //list_Obj_Btn_Skill_1[1].SetActive(true);
        }
        if (_level == 12)
        {
            list_Obj_Btn_Skill_1[1].SetActive(true);
        }
        if (_level == 15)
        {
            list_Obj_Btn_Skill_1[0].SetActive(true);
            list_Obj_Btn_Skill_2[0].SetActive(true);
            list_Obj_Btn_Skill_3[0].SetActive(false);
        }
        else if(_level > 16)
        {
            list_Obj_Btn_Skill_1[0].SetActive(true);
            list_Obj_Btn_Skill_2[0].SetActive(true);
            list_Obj_Btn_Skill_3[0].SetActive(true);
        }
        //
        #endregion
        */



        for (int i = 0; i < list_Obj_Head_Hero.Count; i++)
        {
            list_Obj_Head_Hero[i].SetActive(false);
            
        }

        for (int i = 0; i < list_Obj_Head_Enemy.Count; i++)
        {
            list_Obj_Head_Enemy[i].SetActive(false);
        }

        //Debug.Log(idSkin);
        list_Obj_Head_Hero[idSkin].SetActive(true);
        list_Obj_Head_Enemy[idBoss].SetActive(true);

        StartCoroutine(IE_DelayClose_CanvasFight_Start());

        Obj_Panel_Hit.transform.GetChild(1).gameObject.SetActive(false);
    }
    public int Convert_ID(int _id)
    {
        if (_id == 0)
        {
            return 0;
        }
        else if (_id == 1)
        {
            return 1;
        }
        else if (_id == 2)
        {
            return 5;
        }
        else if (_id == 3)
        {
            return 4;
        }
        else if (_id == 4)
        {
            return 6;
        }
        else if (_id == 5)
        {
            return 3;
        }
        else if (_id == 6)
        {
            return 2;
        }
        else
        {
            return 7;
        }

        //1 
        //2 lua
        //3 bit
        
        //4 bang
        //5 vang
        //6 mu
        //7
        //8

    }
    public bool Check_Can_Hit()
    {
        if (!isAttacking &&!isSkill_1 &&!isSkill_2 &&!isSkill_3 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //
    public void Set_Anim_Red_L()
    {
        anim_RateUs.SetTrigger("red_L");
    }
    public void Set_Anim_Red_R()
    {
        anim_RateUs.SetTrigger("red_R");
    }
    public void Set_Anim_Hand()
    {
        if (obj_Hand_tut != null)//lv 5 mới != null
        {
            obj_Hand_tut.SetActive(true);
            anim_RateUs.SetTrigger("hand");
            
        }
    }
    public void Set_Off_Anim_Hand()
    {
        if (obj_Hand_tut != null)
        {
            obj_Hand_tut.SetActive(false);

        }
    }
    public void Attack_Button_()
    {
        if (!isBlock && !GameManager.Ins.enemyBoss_auto_Asign.isDieing_Fight_Boss && !Player.ins.isD_Dieing_Fight_Boss)
        {
            //SoundManager.Ins.PlayFx(FxID.click);
            if (time_Count_Downt_attack <= 0 && Check_Can_Hit() && Player.ins != null)
            {
                if (!Player.ins.isDoneFight_Boss)
                {
                    Set_Off_Anim_Hand();


                    isAttacking = true;
                    Player.ins.Random_Skill_attack_BOSS();
                    //Debug.Log(index_Skill);
                    time_Count_Downt_attack = Player.ins.Get_Index_Skil_Using_Attack();
                    //if (index_Skill == 1)
                    //{
                    //}
                    //if (index_Skill == 2)
                    //{
                    //    time_Count_Downt_attack = Constant.Time_Count_Skill_2;
                    //}
                    //if (index_Skill == 3)
                    //{
                    //    time_Count_Downt_attack = Constant.Time_Count_Skill_3;
                    //}
                    //if (index_Skill == 4)
                    //{
                    //    time_Count_Downt_attack = Constant.Time_Count_Attack_Player;
                    //}
                    /////////time_Count_Downt_attack = Constant.Time_Count_Attack_Player;
                    ////Player.ins.Set_Anim_Attack();
                    GameManager.Ins.enemyBoss_auto_Asign.Set_Delay_Show_Health_Fight_Boos(_isPlayer_Stronger);
                }

            }
        }
        
    }
    //
    public void Set_Tru_Gold_Or_ADs_Mua_Skill(int _skill)
    {

        if (_skill == 1)
        {
            PlayerPrefs_Manager.Set_Gold(PlayerPrefs_Manager.Get_Gold() - Constant.Gold_By_Skill_1);
        }
        else if (_skill == 2)
        {
            //PlayerPrefs_Manager.Set_Gold(PlayerPrefs_Manager.Get_Gold() - Constant.Gold_By_Skill_2);
            //UNDO: gắn ADs

        }
        else if (_skill == 3)
        {
            //PlayerPrefs_Manager.Set_Gold(PlayerPrefs_Manager.Get_Gold() - Constant.Gold_By_Skill_3);
            //UNDO: gắn ADs

        }
    }
    #region
    public void Set_Fill_Health_Player(float per2,bool isLastHealth=false)
    {
        if (!isLastHealth)
        {
            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).img_Health_Player.DOFillAmount(per2, 0.5f);
            //((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).img_Health_PlayerRed.DOFillAmount(per2, 0.5f);

        }
        else
        {
            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).img_Health_Player.DOFillAmount(per2, 1.5f);
            //((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).img_Health_PlayerRed.DOFillAmount(per2, 1.5f);

        }
    }
    public float Get_Fill_Health_Player()
    {
       return ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).img_Health_Player.fillAmount;
    }
    #endregion
    public void Skill_1_Button()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        if (time_Count_Downt_Skill_1 <= 0 && Check_Can_Hit() && Player.ins != null)
        {
            if (!Player.ins.isDoneFight_Boss)
            {
                isSkill_1 = true;
                time_Count_Downt_Skill_1 = Constant.Time_Count_Skill_1;
                Player.ins.Set_Anim_Skill_1();
                
                float buff = Constant.BuffSkill_1;
                Player.ins.health_Bar.Set_Step_By_Step_Health(Player.ins.Get_Health(), Mathf.RoundToInt(Player.ins.Get_Health() * buff),0.5f);

                Set_Fill_Health_Player(Get_Fill_Health_Player() + (buff -1));




                Player.ins.health = Mathf.RoundToInt(Player.ins.Get_Health() * buff);


                Set_Tru_Gold_Or_ADs_Mua_Skill(1);


                GameManager.Ins.enemyBoss_auto_Asign.Set_Delay_Show_Health_Fight_Boos(_isPlayer_Stronger);
            }
            
        }
    }

    public void Lock_Skill_1_Button()
    {
        list_Obj_Btn_Skill_1[1].SetActive(true);
        list_Obj_Btn_Skill_1[0].SetActive(false);
    }
    //
    //
    public void Skill_2_Button()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        if (time_Count_Downt_Skill_2 <= 0 && Check_Can_Hit() && Player.ins != null)
        {
            if (!Player.ins.isDoneFight_Boss)
            {
                isSkill_2 = true;
                time_Count_Downt_Skill_2 = Constant.Time_Count_Skill_2;
                Player.ins.Set_Anim_Skill_2();
                Set_Tru_Gold_Or_ADs_Mua_Skill(2);
                float buff = Constant.BuffSkill_2;
                Set_Fill_Health_Player(Get_Fill_Health_Player() + (buff - 1));

                Player.ins.health_Bar.Set_Step_By_Step_Health(Player.ins.Get_Health(), Mathf.RoundToInt(Player.ins.Get_Health() * buff), 0.5f);
                Player.ins.health = Mathf.RoundToInt(Player.ins.Get_Health() * buff);

                GameManager.Ins.enemyBoss_auto_Asign.Set_Delay_Show_Health_Fight_Boos(_isPlayer_Stronger);
            }
            
        }

    }
    public void Lock_Skill_2_Button()
    {

        list_Obj_Btn_Skill_2[1].SetActive(true);
        list_Obj_Btn_Skill_2[0].SetActive(false);

    }
    //
    //
    public void Skill_3_Button()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        if (time_Count_Downt_Skill_3 <= 0 && Check_Can_Hit() && Player.ins != null)
        {
            if (!Player.ins.isDoneFight_Boss)
            {
                isSkill_3 = true;
                time_Count_Downt_Skill_3 = Constant.Time_Count_Skill_3;
                Player.ins.Set_Anim_Skill_3();
                Set_Tru_Gold_Or_ADs_Mua_Skill(3);
                float buff = Constant.BuffSkill_3;
                Set_Fill_Health_Player(Get_Fill_Health_Player() + (buff - 1));
                Player.ins.health_Bar.Set_Step_By_Step_Health(Player.ins.Get_Health(), Mathf.RoundToInt(Player.ins.Get_Health() * buff), 0.5f);
                Player.ins.health = Mathf.RoundToInt(Player.ins.Get_Health() * buff);


                GameManager.Ins.enemyBoss_auto_Asign.Set_Delay_Show_Health_Fight_Boos(_isPlayer_Stronger);
            }
            
        }
    }
    public void Lock_Skill_3_Button()
    {
        list_Obj_Btn_Skill_3[1].SetActive(true);
        list_Obj_Btn_Skill_3[0].SetActive(false);
    }
    //
    
    public void CloseButton()
    {
        StartCoroutine(IE_DelayClose());
    }
    IEnumerator IE_DelayClose()
    {
        anim_RateUs.SetTrigger(Constant.Trigger_PigBankClose);
        yield return Cache.GetWFS(Constant.Time_Delay_PigBank_Close * 2);
        Close();
    }
    IEnumerator IE_DelayClose_CanvasFight_Start()
    {
        yield return Cache.GetWFS(Constant.Time_DelayClose_CanvasFight_Start);
        
        

        Player.ins.health = 100;
        Player.ins.health_Bar.Set_Health_Imedetly(100);

        GameManager.Ins.enemyBoss_auto_Asign.health = 100;
        GameManager.Ins.enemyBoss_auto_Asign.health_Bar.Set_Health_Imedetly(100);



        GameManager.Ins.enemyBoss_auto_Asign.Set_Attack_8s_Fight_Boss(_isPlayer_Stronger);
        Obj_Panel_Hit.SetActive(true);
        Obj_canvas_Start.SetActive(false);
        Obj_Panel_Start.SetActive(false);
        //Set_Anim_Hand();//lv 5 obj_hand != null mới bật
        canAttack = true;
    }
    [ContextMenu("_Reset_Skill")]
    public void Reset_Skill()
    {
        PlayerPrefs_Manager.Set_Index_Skill_Reach(0);
    }
    [ContextMenu("_Set_Skill")]
    public void Set_Skill()
    {
        PlayerPrefs_Manager.Set_Index_Skill_Reach(1);
    }
}
