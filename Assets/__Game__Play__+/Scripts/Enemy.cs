using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using DG.Tweening;
[DefaultExecutionOrder(10)]
public class Enemy : MonoBehaviour
{
    #region Khai báo biến
    public bool isBossLass;
    public bool isBoss_UNTIL;
    public bool isEnemyFly;
    public bool isEnemyBoar;
    public bool isEnemy_Have_Key;
    public int health;
    [Header("Con to hay nhỏ===== để set time die")]

    public bool isBig_Enemy;
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;

    public AnimationReferenceAsset Action_Idle;
    public AnimationReferenceAsset Action_Attack;
    public AnimationReferenceAsset Action_Run;
    public AnimationReferenceAsset Action_Takedamge;
    public AnimationReferenceAsset Action_Die;
    public AnimationReferenceAsset Action_Victory;
    public AnimationReferenceAsset Action_Lose;
    [Header("------Enemy nào cầm chìa mới cần điền------")]
    public Key_Floor key_Floor_To_Open_Lock_Floor;
    ////[Header("------Enemy cuối mới cần điền floor_This------")]
    [Header("------Not Need Asign--To view------")]
    public Floor floor_This;
    public Transform tf_Enemy;
    public bool isDieing;// đang chạy anim die, thì ko attack Enemy này thêm được nữa
    public bool isDieing_Fight_Boss;// Chỉ đánh Boss
    public bool isFist_config;
    public Health_Bar health_Bar;
    public bool isFist_time_Die;
    [Header("------Indext Fix= 4 lần Attack Player------")]
    public int index_Hit_Player = 0;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        isFist_config = false;
        tf_Enemy = transform;
        //health = Constant.GetHealt_By_Type();
        RegisterHandleAnimEvent();
        Set_Spawn_Health_Bar();
        //
        //Phải gắn Enemy vào Point trước
        if (GetComponentInParent<Point_In_Floor>() != null)
        {
            GetComponentInParent<Point_In_Floor>().Set_Enemy_Attach(this);
            GetComponentInParent<Point_In_Floor>().Set_Not_Empty();
        }
        Set_Idle();
    }

    private void RegisterHandleAnimEvent()
    {
        skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;
        skeletonAnimation.Initialize(false);
    }

    private void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Set_ConFig_Floor_One_Time();
    }

    #region Đánh dấu đã chít để Player ko đánh Enemy này nữa trong khi chạy anim die, attack
    public void Set_BoolIsDieing()
    {
        isDieing = true;
    }
    #endregion
    #region Show máu bắn ra :))
    public void Set_Show_Blood()
    {
        if (tf_Enemy != null)
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load(Constant.Path_Frefab_Blood), tf_Enemy.position + Constant.Enemy_offset_Blood, tf_Enemy.rotation);
        }

        if (key_Floor_To_Open_Lock_Floor != null)
        {
            key_Floor_To_Open_Lock_Floor.Set_Move_To_Lock();

        }
    }
    #endregion
    #region Base to set Skin, Anim
    //Anim
    public void SetAnimation(AnimationReferenceAsset _anim, bool _loop, float _time_Scale)//Set No loop
    {
        skeletonAnimation.state.SetAnimation(0, _anim, _loop).TimeScale = _time_Scale;
    }
    //TODO: đổi skin
    public void Set_Skin(string _str_Skin)
    {
        skeletonAnimation.Skeleton.SetSkin(_str_Skin);
        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
        skeletonAnimation.LateUpdate();
    }

    public void SetCharacterState_Loop(AnimationReferenceAsset _anim)//Set loop
    {
        SetAnimation(_anim, true, 1f);
    }
    public void SetCharacterState_NoLoop(AnimationReferenceAsset _anim)//Set loop
    {
        SetAnimation(_anim, false, 1f);
    }

    public void ReSetCharacterState()
    {
        skeletonAnimation.ClearState();
    }
    #endregion
    #region Set IE Delay Action, to die, attack to idle
    IEnumerator Delay_die()
    {
        
        
        //nếu màn có con Enemy này cầm key
        if (isEnemy_Have_Key)
        {
            Key_3_Manager.Ins.Set_Key_Fly();
        }
        //Từ đầu .... Show máu
        health_Bar.Set_Hide_Health_Bar();
        SetCharacterState_NoLoop(Action_Attack);

        float _time_show_blood = Constant.Time_Player_Show_Blood;
        yield return Cache.GetWFS(_time_show_blood);


        //Show máu...................
        SoundManager.Ins.Play_FX_Hit_Enemy_Random();
        Set_Show_Blood();

        //SetCharacterState_Loop(Action_Takedamge);


        //Delay player dùng hết Skill.......
        #region  Get_Index_Skill Player đang dùng
        //Đã set Player Attack trước rồi mới set action cho enemy
        float skillDuration = Player.ins.Get_Index_Skil_Using_Attack();

        float time_action_Hit = Constant.Get_Time_action_Hit_Enemy(isBig_Enemy);

        yield return Cache.GetWFS(skillDuration - time_action_Hit - _time_show_blood);



        //Hit cách 1 lúc trước khi kết thúc action Skill của Player.......
        //ADD: enemy Hit.............
        //SetCharacterState_NoLoop(Action_Takedamge);

        //nếu random ra index dùng skill, sẽ Delay Boss attack lâu hơn
        yield return Cache.GetWFS(time_action_Hit);
        #endregion



        //bật anim enemy die........
        SetCharacterState_NoLoop(Action_Die);
        SoundManager.Ins.Play_FX_Hit_Enemy_Random();

        //If level is zero or one need enable tut
        if (PlayerPrefs_Manager.Get_Index_Level_Normal() == 0 || PlayerPrefs_Manager.Get_Index_Level_Normal() == 1)
            Tut_0_Game_Play.Ins.Set_Tut();

        float time_action_Die = Constant.Get_Time_action_Die_Enemy(isBig_Enemy);
        yield return Cache.GetWFS(time_action_Die);


        
        if (floor_This.is_Floor_Last_Of_house && !floor_This.is_Floor_Last_Of_Level)
        {
            //Nếu enemy là cuối cùng của Floor cuối cùng của 1 nhà... set delay chuyển nhà
            Point_In_Floor pp = GetComponentInParent<Point_In_Floor>();

            if (pp == floor_This.list_Point_In_Floor[0])
            {
                //Drag_Drop_Manager.Instance.Set_Delay_Take_House(floor_This,0);
                floor_This.Set_Floor_To_Floor_Of_Player();
                floor_This.house_Build_Of_This.Set_Mai_Xanh();
                floor_This.house_Build_Of_This.Set_This_To_Team_Player();
                Drag_Drop_Manager.Instance.Set_Delay_Take_House(floor_This, 0);
            }

        }
        


        //xóa enemy...//
        if (isBossLass)
        {
            yield return Cache.GetWFS(0.1f);//0.8
            //Set Mái xanh.....delay win
            GameManager.Ins.Set_Mai_Xanh_Delay_Win(floor_This);
#if UNITY_EDITOR
            
#endif
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);

        }
        //gameObject.SetActive(false);
    }
    IEnumerator Delay_AttackTo_Idle()
    {

        SetCharacterState_NoLoop(Action_Attack);
        yield return Cache.GetWFS(Action_Attack.Animation.Duration);
        ReSetCharacterState();
        SetCharacterState_Loop(Action_Idle);
    }
    #endregion
    #region Set Animation
    public void Set_Attack()
    {
        StartCoroutine(Delay_AttackTo_Idle());
    }
    public void Set_Idle()
    {
        SetCharacterState_Loop(Action_Idle);
    }
    public void Set_Anim_Attack()
    {
        SetCharacterState_NoLoop(Action_Attack);
    }
    public void Set_Anim_Run()
    {
        SetCharacterState_Loop(Action_Run);
    }
    public void Set_Anim_Takedame()
    {
        SetCharacterState_NoLoop(Action_Takedamge);
    }
    public void Set_Anim_Die()
    {
        if (!isFist_time_Die)
        {
            isFist_time_Die = true;
            StartCoroutine(Delay_die());

        }
    }
    public void Set_Anim_Victory()
    {
        SetCharacterState_Loop(Action_Victory);
    }
    public void Set_Anim_Lose()
    {
        SetCharacterState_NoLoop(Action_Lose);
    }
    #endregion
    #region Health
    public void Set_Spawn_Health_Bar()
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load(Constant.Path_Frefab_Health_Bar_Red), tf_Enemy);
        health_Bar = obj.GetComponent<Health_Bar>();
        if (isEnemyFly)
        {
            health_Bar.tf_Health_Bar.localPosition = Constant.Enemy_Local_Pos_Health_Bar_Fly;
        }
        else if (isEnemyBoar)
        {
            health_Bar.tf_Health_Bar.localPosition = Constant.Enemy_Local_Pos_Health_Bar_Boar;
        }
        else
        {
            health_Bar.tf_Health_Bar.localPosition = Constant.Enemy_Local_Pos_Health_Bar_Normal;
        }
        
        health_Bar.Set_Health_Imedetly(health);
    }
    public int Get_Health()
    {
        return health;
    }
    #endregion
    #region Lấy thông tin Floor mà Enemy đang đứng lúc vào game
    public void Set_ConFig_Floor_One_Time()
    {
        if (!isFist_config)
        {
            isFist_config = true;
            floor_This = GetComponentInParent<Floor>();
            if (isBoss_UNTIL)
            {
                GameManager.Ins.enemyBoss_auto_Asign = this;
            }
        }
    }
    #endregion
    public void Set_Attack_8s_Fight_Boss(bool _isPlayer_Stronger)
    {
        if (Player.ins != null)
        {
            Player.ins.enemy_Hitting = this;
        }
        StartCoroutine(IE_Attack_8s(_isPlayer_Stronger));
    }
    IEnumerator IE_Attack_8s(bool _isPlayer_Stronger)
    {
        
        if (_isPlayer_Stronger)
        {
            // tối đa 5 lần
            Set_Anim_Attack();
            yield return Cache.GetWFS(1);
            if (!isDieing_Fight_Boss)
            {
                Player.ins.Set_Show_Blood();
                //Player.ins.Set_Anim_Hit();
            }
            int _health_Player = Player.ins.Get_Health();
            //damge 25
            if (!isDieing_Fight_Boss)
            {
                Player.ins.health_Bar.Set_Step_By_Step_Health(_health_Player,80, 1);

                ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.8f);
            }
            //1s
            yield return Cache.GetWFS(4);
            // hết lan 1
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Set_Anim_Attack();
            }
            yield return Cache.GetWFS(1);
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.Set_Show_Blood();
                //Player.ins.Set_Anim_Hit();
            }
            //damge 25
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.health_Bar.Set_Step_By_Step_Health(_health_Player,60, 1);

                ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.6f);
            }
            //1s
            yield return Cache.GetWFS(4);
            //hết  lan 2
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Set_Anim_Attack();
            }
            yield return Cache.GetWFS(1);
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.Set_Show_Blood();
                //Player.ins.Set_Anim_Hit();
            }
            //damge 25
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.health_Bar.Set_Step_By_Step_Health(_health_Player,40, 1);

                ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.4f);
            }
            //1s
            yield return Cache.GetWFS(4);
            //hết  lan 2
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Set_Anim_Attack();
            }
            yield return Cache.GetWFS(1);
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.Set_Show_Blood();
                //Player.ins.Set_Anim_Hit();
            }
            //damge 25
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.health_Bar.Set_Step_By_Step_Health(_health_Player, 20, 1);

                ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.2f);
            }
            //1s
            yield return Cache.GetWFS(4);
            //hết  lan 3
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Set_Anim_Attack();
            }
            yield return Cache.GetWFS(1);
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.Set_Show_Blood();
                //Player.ins.Set_Anim_Hit();
            }
            //damge 25
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.health_Bar.Set_Step_By_Step_Health(_health_Player,0, 1);

                ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.0f);
                
            }
            ////1s
            //yield return Cache.GetWFS(1);
            //hết  lan 4
            if (!isDieing_Fight_Boss)
            {
                Player.ins.Set_Show_Blood();
                Player.ins.isD_Dieing_Fight_Boss = true;
                //Player.ins.Set_Anim_Hit();
            }
            //yield return Cache.GetWFS(0.5f);
            if (!isDieing_Fight_Boss)
            {
                Player.ins.Set_Anim_Die();
                ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Anim_Red_L();
                SetCharacterState_NoLoop(Action_Victory);
                //SoundManager.Ins.PlayFx(FxID.giantDeath);
                yield return Cache.GetWFS(2.6f);

                GameManager.Ins.Set_Lose_Level(floor_This);
            }
        }
        else//Damge Player nho hon
        {

            // tối đa 3 lần
            Set_Anim_Attack();
            yield return Cache.GetWFS(1);
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.Set_Show_Blood();
                //Player.ins.Set_Anim_Hit();
            }
            int _health_Player = Player.ins.Get_Health();
            //damge 25
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.health_Bar.Set_Step_By_Step_Health(_health_Player, 70, 1);

                ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.70f);
            }
            //1s
            yield return Cache.GetWFS(1);
            // hết lan 1

            Set_Anim_Attack();
            yield return Cache.GetWFS(1);
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.Set_Show_Blood();
                //Player.ins.Set_Anim_Hit();
            }
            //damge 25
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.health_Bar.Set_Step_By_Step_Health(_health_Player, 40, 1);

                ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.4f);
            }
            //1s
            yield return Cache.GetWFS(1);
            //hết  lan 2

            Set_Anim_Attack();
            yield return Cache.GetWFS(1);
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.Set_Show_Blood();
                //Player.ins.Set_Anim_Hit();
            }
            //damge 25
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.health_Bar.Set_Step_By_Step_Health(_health_Player, 10, 1);

                ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.1f);
            }
            //1s
            yield return Cache.GetWFS(1);
            //hết  lan 3

            Set_Anim_Attack();
            yield return Cache.GetWFS(1);
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.Set_Show_Blood();
                //Player.ins.Set_Anim_Hit();
            }
            //damge 25
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.health_Bar.Set_Step_By_Step_Health(_health_Player, 0, 1);

                ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.0f);

            }
            //1s
            //yield return Cache.GetWFS(1);
            //hết  lan 3
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                Player.ins.Set_Show_Blood();
                Player.ins.isD_Dieing_Fight_Boss = true;
                //Player.ins.Set_Anim_Hit();
            }
            //yield return Cache.GetWFS(0.5f);
            if (!isDieing_Fight_Boss && Player.ins != null)
            {
                ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Anim_Red_L();

                Set_Anim_Victory();
                Player.ins.Set_Anim_Die();
                yield return Cache.GetWFS(2.2f);
                GameManager.Ins.Set_Lose_Level(floor_This);
            }
        }

        #region old
        /*
        yield return Cache.GetWFS(1);
        // 1



        Set_Anim_Attack();
        yield return Cache.GetWFS(1);
        //2

        Player.ins.Set_Show_Blood();
        int _health_Player = Player.ins.Get_Health();
        Player.ins.health_Bar.Set_Step_By_Step_Health(_health_Player, Mathf.RoundToInt(_health_Player * 0.85f),1);

        ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.85f);
        yield return Cache.GetWFS(1);
        //3



        
        Set_Anim_Attack();
        yield return Cache.GetWFS(1);
        //4



        Player.ins.Set_Show_Blood();
        Player.ins.health_Bar.Set_Step_By_Step_Health(Mathf.RoundToInt(_health_Player * 0.85f), Mathf.RoundToInt(_health_Player * 0.5f), 1);


        ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.5f);

        yield return Cache.GetWFS(1);
        //5



        //
        //2s
        Set_Anim_Attack();
        yield return Cache.GetWFS(1);
        //6


        Player.ins.Set_Show_Blood();
        Player.ins.health_Bar.Set_Step_By_Step_Health(Mathf.RoundToInt(_health_Player * 0.5f), Mathf.RoundToInt(_health_Player * 0.2f), 1);

        ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.2f);

        yield return Cache.GetWFS(1);
        //7



        //
        //2s
        Set_Anim_Attack();
        yield return Cache.GetWFS(1);
        //8



        ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).isBlock = true;
        Player.ins.Set_Show_Blood();
        if (Player.ins.Get_Health() > health)
        {
            
            Player.ins.health_Bar.Set_Step_By_Step_Health(Mathf.RoundToInt(_health_Player * 0.2f), Mathf.RoundToInt(_health_Player * 0.1f), 1);

            //((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0.1f);
            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).img_Health_Boss.DOFillAmount(0, 0.5f).OnComplete(()=> { health_Bar.gameObject.SetActive(false);
            });
            health_Bar.Set_Step_By_Step_Health(Mathf.RoundToInt(health), 0, 0.5f);
            //((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).img_Health_BossRed.DOFillAmount(0, 0.5f);
            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Anim_Red_R();
        }
        else
        {
            Player.ins.health_Bar.Set_Step_By_Step_Health(Mathf.RoundToInt(_health_Player * 0.2f), 0, 1);

            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Fill_Health_Player(0,true);
            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).obj_Health_Red_L.SetActive(true);
            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Anim_Red_L();
        }
        yield return Cache.GetWFS(1);
        //9




        Player.ins.isDoneFight_Boss = true;
        if (Player.ins.Get_Health() > health)
        {
            SetCharacterState_NoLoop(Action_Die);
            SoundManager.Ins.PlayFx(FxID.giantDeath);
            yield return Cache.GetWFS(2.6f);





            GameManager.Ins.Set_Mai_Xanh_Delay_Win(floor_This);
        }
        else
        {
            Set_Anim_Victory();
            Player.ins.Set_Anim_Die();
            yield return Cache.GetWFS(2.2f);
            GameManager.Ins.Set_Lose_Level(floor_This);
        }

        //yield return Cache.GetWFS(3);





        ////
        //if (Player.ins.Get_Health() > health)
        //{
        //    GameManager.Ins.Set_Mai_Xanh_Delay_Win(floor_This);
        //}
        //else
        //{
        //    GameManager.Ins.Set_Lose_Level(floor_This);
        //}

        //Debug.Log(Time.time - tt);
        */
        #endregion 
    }
    //
    public void Set_Delay_Show_Health_Fight_Boos(bool _isPlayer_Stronger)
    {
        if (_isPlayer_Stronger)
        {
            StartCoroutine(IE_Show_Blood_In_8s_Fight_Boot());
        }
        else
        {
            StartCoroutine(IE_Show_Blood_In_8s_Fight_Boot());
        }
        
    }

    IEnumerator IE_Show_Blood_In_8s_Fight_Boot()
    {
        yield return Cache.GetWFS(1);

        if (index_Hit_Player == 0)
        {
            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).img_Health_Boss.fillAmount = (0.8f);
            health = 80;
            health_Bar.Set_Step_By_Step_Health(100, health, 1);
        }
        else if (index_Hit_Player == 1)
        {
            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).img_Health_Boss.fillAmount = (0.6f);
            health = 60;
            health_Bar.Set_Step_By_Step_Health(80, 60, 1);
        }
        else if (index_Hit_Player == 2)
        {
            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).img_Health_Boss.fillAmount = (0.4f);
            health = 40;
            health_Bar.Set_Step_By_Step_Health(60, 40, 1);
        }
        else if (index_Hit_Player == 3)
        {
            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).img_Health_Boss.fillAmount = (0.2f);
            health = 20;
            health_Bar.Set_Step_By_Step_Health(40, 20, 1);
        }
        else
        {
            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).img_Health_Boss.fillAmount = (0.0f);
            health = 0;
            health_Bar.Set_Step_By_Step_Health(20, 0, 1);
            

            
        }
        index_Hit_Player++;

        Set_Show_Blood();
        //Set_Anim_Takedame();
        if (index_Hit_Player == 5)
        {
            isDieing_Fight_Boss = true;
            health_Bar.Set_Hide_Health_Bar();
            ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Anim_Red_R();
        }
        Cache.GetWFS(0.5f);
        if (index_Hit_Player == 5)
        {
            //isDieing_Fight_Boss = true;
            //Set_Anim_Die();
            if (!Player.ins.isD_Dieing_Fight_Boss)
            {
                SetCharacterState_NoLoop(Action_Die);
                SoundManager.Ins.PlayFx(FxID.giantDeath);
                ((CanvasFight_Boss)UIManager.Ins.GetUI(UIID.UICFight_Boss)).Set_Anim_Red_R();
                Cache.GetWFS(0.1f);//2.5
                GameManager.Ins.Set_Mai_Xanh_Delay_Win(floor_This);

            }
        }
    }
}
