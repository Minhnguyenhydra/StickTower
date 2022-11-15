using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;
using System.Linq;

public class Player : MonoBehaviour
{
    #region Khai báo biến

    public Floor floor_stay;
    public SkeletonAnimation skeletonAnimation;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName_hit;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName_sfx_char_skill_1;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName_sfx_char_skill_2;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName_sfx_char_skill_3;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName_sfx_char_skill_4;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName_sfx_char_skill_5;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName_sfx_char_skill_6;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName_sfx_char_skill_7;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName_sfx_char_skill_8;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName_sfx_char_skill_9;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName_sfx_char_skill_10;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName_sfx_char_skill_11;

    [Space]
    public bool logDebugMessage = false;

    Spine.EventData eventData_hit;
    [Header("===========================")]
    Spine.EventData eventData_sfx_char_skill_1;
    Spine.EventData eventData_sfx_char_skill_2;
    Spine.EventData eventData_sfx_char_skill_3;
    Spine.EventData eventData_sfx_char_skill_4;
    Spine.EventData eventData_sfx_char_skill_5;
    Spine.EventData eventData_sfx_char_skill_6;
    Spine.EventData eventData_sfx_char_skill_7;
    Spine.EventData eventData_sfx_char_skill_8;
    Spine.EventData eventData_sfx_char_skill_9;
    Spine.EventData eventData_sfx_char_skill_10;
    Spine.EventData eventData_sfx_char_skill_11;
    //
    public Transform tf_Player;
    public Transform tf_Point_Sword_ADs_Go;
    public BoxCollider boxCollider_Player;
    public static Player ins;
    public int health;
    public House_Build house_Build_Of_Player;
    //
    [Header("Animation")]
    public List<AnimationReferenceAsset> lsAnim;
    public AnimationReferenceAsset Action_Attack;
    public AnimationReferenceAsset Action_Die;
    public AnimationReferenceAsset Action_Die_loop;
    //
    public AnimationReferenceAsset Action_char_1_Skill_1;
    public AnimationReferenceAsset Action_char_1_Skill_2;
    public AnimationReferenceAsset Action_char_1_Skill_3;
    //
    public AnimationReferenceAsset Action_char_2_Skill_1;
    public AnimationReferenceAsset Action_char_2_Skill_2;
    public AnimationReferenceAsset Action_char_2_Skill_3;
    //
    public AnimationReferenceAsset Action_char_3_Skill_1;
    public AnimationReferenceAsset Action_char_3_Skill_2;
    public AnimationReferenceAsset Action_char_3_Skill_3;
    //
    public AnimationReferenceAsset Action_char_4_Skill_1;
    public AnimationReferenceAsset Action_char_4_Skill_2;
    public AnimationReferenceAsset Action_char_4_Skill_3;
    //
    public AnimationReferenceAsset Action_char_5_Skill_1;
    public AnimationReferenceAsset Action_char_5_Skill_2;
    public AnimationReferenceAsset Action_char_5_Skill_3;
    //
    public AnimationReferenceAsset Action_char_6_Skill_1;
    public AnimationReferenceAsset Action_char_6_Skill_2;
    public AnimationReferenceAsset Action_char_6_Skill_3;
    //
    public AnimationReferenceAsset Action_char_7_Skill_1;
    public AnimationReferenceAsset Action_char_7_Skill_2;
    public AnimationReferenceAsset Action_char_7_Skill_3;
    //
    public AnimationReferenceAsset Action_char_8_Skill_1;
    public AnimationReferenceAsset Action_char_8_Skill_2;
    public AnimationReferenceAsset Action_char_8_Skill_3;
    //
    //
    public AnimationReferenceAsset Action_char_9_Skill_1;
    public AnimationReferenceAsset Action_char_9_Skill_2;
    public AnimationReferenceAsset Action_char_9_Skill_3;
    //
    public AnimationReferenceAsset Action_char_10_Skill_1;
    public AnimationReferenceAsset Action_char_10_Skill_2;
    public AnimationReferenceAsset Action_char_10_Skill_3;
    //
    public AnimationReferenceAsset Action_char_11_Skill_1;
    public AnimationReferenceAsset Action_char_11_Skill_2;
    public AnimationReferenceAsset Action_char_11_Skill_3;
    //
    public AnimationReferenceAsset Action_char_12_Skill_1;
    public AnimationReferenceAsset Action_char_12_Skill_2;
    public AnimationReferenceAsset Action_char_12_Skill_3;
    //
    public AnimationReferenceAsset Action_char_13_Skill_1;
    public AnimationReferenceAsset Action_char_13_Skill_2;
    public AnimationReferenceAsset Action_char_13_Skill_3;
    //
    public AnimationReferenceAsset Action_char_14_Skill_1;
    public AnimationReferenceAsset Action_char_14_Skill_2;
    public AnimationReferenceAsset Action_char_14_Skill_3;
    //
    public AnimationReferenceAsset Action_char_15_Skill_1;
    public AnimationReferenceAsset Action_char_15_Skill_2;
    public AnimationReferenceAsset Action_char_15_Skill_3;
    //
    public AnimationReferenceAsset Action_Hit;
    public AnimationReferenceAsset Action_Idle;
    public AnimationReferenceAsset Action_Lose;
    public AnimationReferenceAsset Action_Run;
    public AnimationReferenceAsset Action_buffdamge;
    public AnimationReferenceAsset Action_Victory;
    public AnimationReferenceAsset Action_Dam;
    [Header("------Not Need Asign--To view------")]
    public bool isDoneFight_Boss;
    public bool isDie;
    public bool isD_Dieing_Fight_Boss;
    public Enemy enemy_Hitting;
    public bool is_Block_Raycas;
    public Vector3 pos_Old_Player;
    public bool isCanHold;
    public bool isHittingBoss;//Đề ko lùi offset Player khi dùng Skill
                              //
    public int indext_Point_In_Floor_Stay;
    //
    public Health_Bar health_Bar;
    public bool isLast_Point_Level = false;//điểm cuối cùng Player đứng trên đó đánh Boss hoặc mở rương hoặc giải cứu CC
    public bool isReach_Last_Point_Level = false;//điểm cuối cùng Player đứng trên đó đánh Boss hoặc mở rương hoặc giải cứu CC
    public int index_Castle_current;//chỉ số nhà chiếm được.. thành chiếm được mỗi level
    //chỉ dành cho Floor Reward
    [Tooltip("chỉ Floor Reward mới dùng biến này")]
    public bool isTakingReward;
    [Header("------only Lv 26 --- not need asign------")]
    public bool is_Time_Lv26_Ready_To_Fix;
    [Header("------Only use to View Random Skill------")]
    private float anim_duration;

    private Button[] btnsInGame;
    #endregion

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadAnim();

        Init_sfx_char_skill_Player();

        isTakingReward = false;

        index_Castle_current = 0;

        Set_Spawn_Health_Bar();
        isCanHold = true;
        tf_Player = transform;

        Set_Anim_Idle();

        //if (CheckExpireSkin())
        //    ChangeSkinWhenExpired();

        Set_Skin(Constant.Get_Skin_Name_By_Id(PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing()));

        Set_Fix_Pos_Player();

        StartCoroutine(GetButtonsInGame());
    }

    private void LoadAnim()
    {
        int index = 0;
        Action_Attack = lsAnim[index++];
        Action_Die = lsAnim[index++];
        Action_Die_loop = lsAnim[index++];
        Action_char_1_Skill_1 = lsAnim[index++];
        Action_char_1_Skill_2 = lsAnim[index++];
        Action_char_1_Skill_3 = lsAnim[index++];
        Action_char_2_Skill_1 = lsAnim[index++];
        Action_char_2_Skill_2 = lsAnim[index++];
        Action_char_2_Skill_3 = lsAnim[index++];
        Action_char_3_Skill_1 = lsAnim[index++];
        Action_char_3_Skill_2 = lsAnim[index++];
        Action_char_3_Skill_3 = lsAnim[index++];
        Action_char_4_Skill_1 = lsAnim[index++];
        Action_char_4_Skill_2 = lsAnim[index++];
        Action_char_4_Skill_3 = lsAnim[index++];
        Action_char_5_Skill_1 = lsAnim[index++];
        Action_char_5_Skill_2 = lsAnim[index++];
        Action_char_5_Skill_3 = lsAnim[index++];
        Action_char_6_Skill_1 = lsAnim[index++];
        Action_char_6_Skill_2 = lsAnim[index++];
        Action_char_6_Skill_3 = lsAnim[index++];
        Action_char_7_Skill_1 = lsAnim[index++];
        Action_char_7_Skill_2 = lsAnim[index++];
        Action_char_7_Skill_3 = lsAnim[index++];
        Action_char_8_Skill_1 = lsAnim[index++];
        Action_char_8_Skill_2 = lsAnim[index++];
        Action_char_8_Skill_3 = lsAnim[index++];
        Action_char_9_Skill_1 = lsAnim[index++];
        Action_char_9_Skill_2 = lsAnim[index++];
        Action_char_9_Skill_3 = lsAnim[index++];
        Action_char_10_Skill_1 = lsAnim[index++];
        Action_char_10_Skill_2 = lsAnim[index++];
        Action_char_10_Skill_3 = lsAnim[index++];
        Action_char_11_Skill_1 = lsAnim[index++];
        Action_char_11_Skill_2 = lsAnim[index++];
        Action_char_11_Skill_3 = lsAnim[index++];
        Action_char_12_Skill_1 = lsAnim[index++];
        Action_char_12_Skill_2 = lsAnim[index++];
        Action_char_12_Skill_3 = lsAnim[index++];
        Action_char_13_Skill_1 = lsAnim[index++];
        Action_char_13_Skill_2 = lsAnim[index++];
        Action_char_13_Skill_3 = lsAnim[index++];
        Action_char_14_Skill_1 = lsAnim[index++];
        Action_char_14_Skill_2 = lsAnim[index++];
        Action_char_14_Skill_3 = lsAnim[index++];
        Action_char_15_Skill_1 = lsAnim[index++];
        Action_char_15_Skill_2 = lsAnim[index++];
        Action_char_15_Skill_3 = lsAnim[index++];
        Action_Hit = lsAnim[index++];
        Action_Idle = lsAnim[index++];
        Action_Lose = lsAnim[index++];
        Action_Run = lsAnim[index++];
        Action_buffdamge = lsAnim[index++];
        Action_Victory = lsAnim[index++];
        Action_Dam = lsAnim[index++];
    }

    private IEnumerator GetButtonsInGame()
    {
        yield return new WaitForEndOfFrame();

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("btn");
        btnsInGame = new Button[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            btnsInGame[i] = buttons[i].GetComponent<Button>();
        }

    }

    public void Init_sfx_char_skill_Player()
    {
        skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;
        skeletonAnimation.Initialize(false);
    }

    private bool CheckExpireSkin()
    {
        int level = PlayerPrefs_Manager.Get_Index_Level_Normal();
        int curIdSkin = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing();
        int expireSkin = PlayerPrefs_Manager.GetExpireSkin(curIdSkin);

        if (expireSkin == 0)
            return false;

        if (level == expireSkin)
            return true;

        return false;
    }

    public void ChangeSkinWhenExpired()
    {
        int curSkin = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing();
        PlayerPrefs_Manager.SetHasNotSkin(curSkin);

        int preSkin = PlayerPrefs_Manager.GetPreSkin();
        PlayerPrefs_Manager.Set_ID_Name_Skin_Wearing(preSkin);
    }

    private void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name.Equals("hit"))
            Set_Enemy_Hit_By_event();

        else
        {
            string soundName = e.Data.Name.Replace("mp3/","");
            SoundManager.Ins.PlayFxWithName(soundName);
        }
    }

    public void Set_Enemy_Hit_By_event()
    {
        //Debug.Log("===================Hit=================");
        if (enemy_Hitting != null)
        {
            if (!enemy_Hitting.isBoss_UNTIL)//ko bật aniHit khi đánh Boss
            {
                enemy_Hitting.Set_Anim_Takedame();
            }
        }
    }
    //Đã set Player Attack trước rồi mới set action cho enemy
    public float Get_Index_Skil_Using_Attack()
    {
        return anim_duration;
    }


    #region Set Floor mà Player bắt đầu được thả rồi đứng trên đó
    public void Set_Floor_Indext_Point(Floor _floor, int _indext_point)
    {
        floor_stay = _floor;
        indext_Point_In_Floor_Stay = _indext_point;
    }
    #endregion
    #region Set nhớ vị trí trước khi Player được thả
    public void Set_Pos_Old(Vector3 _pos_Old)
    {
        pos_Old_Player = _pos_Old;
    }
    public void Set_vi_tri_cu()//set vị trí cũ sau thả chuột
    {
        tf_Player.localPosition = pos_Old_Player;
    }
    #endregion
    #region Set Animation
    public void Set_Attack(bool _isOpn_Reward = false)
    {
        Set_Fix_Pos_Player();

        StartCoroutine(Delay_AttackTo_Idle(_isOpn_Reward));
    }
    public void Set_Anim_Attack()
    {
        Set_Fix_Pos_Player();
        //SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
        Set_Block_Colider_Player();
        ReSetCharacterState();
        SetCharacterState_NoLoop(Action_Attack);
        SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
    }
    public void Set_Anim_Skill_1()
    {
        Set_Block_Colider_Player();
        ReSetCharacterState();
        SetCharacterState_NoLoop(Action_char_1_Skill_1);
    }
    public void Set_Anim_Skill_2()
    {
        Set_Block_Colider_Player();
        ReSetCharacterState();
        SetCharacterState_NoLoop(Action_char_1_Skill_2);
    }
    public void Set_Anim_Skill_3()
    {
        Set_Block_Colider_Player();
        ReSetCharacterState();
        SetCharacterState_NoLoop(Action_char_1_Skill_3);
    }
    public void Set_Anim_Idle()
    {
        ReSetCharacterState();
        SetCharacterState_Loop(Action_Idle);
    }
    public void Set_Anim_Run()
    {
        Set_Block_Colider_Player();
        ReSetCharacterState();
        SetCharacterState_Loop(Action_Run);
    }
    public void Set_Anim_TakeSword()
    {
        SoundManager.Ins.PlayFx(FxID.level_up);
        ReSetCharacterState();
        SetCharacterState_NoLoop(Action_buffdamge);
        //SetCharacterState_Loop(Action_buffdamge);
    }

    public void Set_Anim_Hit()
    {
        SoundManager.Ins.PlayFx(FxID.hit_Player);
        Set_Block_Colider_Player();
        ReSetCharacterState();
        SetCharacterState_NoLoop(Action_Hit);
    }
    public void Set_Anim_Die()
    {
        for (int i = 0; i < btnsInGame.Length; i++)
            btnsInGame[i].interactable = false;

        isDie = true;
        //SoundManager.Ins.Play_Get_Hit_Player();
        Set_Block_Colider_Player();
        ReSetCharacterState();
        StartCoroutine(Delay_die());
    }
    public void Set_Anim_Victory()
    {
        for (int i = 0; i < btnsInGame.Length; i++)
            btnsInGame[i].interactable = false;

        SoundManager.Ins.PlayFx(FxID.yes);
        Set_Block_Colider_Player();

        SetCharacterState_Loop(Action_Victory);
        //StartCoroutine(IE_To_Fix_Action_Victory());

    }
    //    public IEnumerator IE_To_Fix_Action_Victory()
    //    {
    //        if (index_Skill_Use == 1)
    //        {
    //            yield return Cache.GetWFS(Constant.Time_Player_Hit_Skill_1 - Constant.Time_Player_Die_attack * 2);
    //        }
    //        else if (index_Skill_Use == 2)
    //        {
    //            yield return Cache.GetWFS(Constant.Time_Player_Hit_Skill_2 - Constant.Time_Player_Die_attack * 2);
    //        }
    //        else if (index_Skill_Use == 3)
    //        {
    //            yield return Cache.GetWFS(Constant.Time_Player_Hit_Skill_3 - Constant.Time_Player_Die_attack * 2);
    //        }
    //        else if (index_Skill_Use == 4)
    //        {

    //        }
    //        else
    //        {
    //            yield return Cache.GetWFS(Constant.Time_Player_Hit_Skill_3 - Constant.Time_Player_Die_attack * 2);
    //        }
    //        ReSetCharacterState();
    //#if UNITY_EDITOR
    //        ///Debug.Log(index_Skill_Use);
    //#endif
    //        SetCharacterState_Loop(Action_Victory);
    //    }
    public void Set_Lose()
    {
        Set_Block_Colider_Player();
        ReSetCharacterState();
        SetCharacterState_NoLoop(Action_Lose);
    }
    #endregion
    #region Set IE Delay Action, to die, attack to idle
    IEnumerator Delay_die()
    {
        Set_Block_Colider_Player();
        health_Bar.Set_Hide_Health_Bar();
        //SetCharacterState_NoLoop(Action_Attack);

        //Nếu là đánh Boss
        if (isLast_Point_Level)
        {
            GameManager.Ins.Set_Bool_Lose_Boss();
            Set_Config_Boss_Win_End_Level();
        }

        // Just wait 45% duration anim attack with Big Enemy, 60% with Normal Enemy
        if (enemy_Hitting != null)
        {
            float animDuration = enemy_Hitting.isBig_Enemy ? enemy_Hitting.Action_Attack.Animation.Duration * 0.45f
                                                       : enemy_Hitting.Action_Attack.Animation.Duration * 0.6f;
            yield return Cache.GetWFS(animDuration);
        }

        Set_Show_Blood();
        //ADD: Hit --> Die
        SetCharacterState_NoLoop(Action_Hit);
        SoundManager.Ins.PlayFx(FxID.hit_Player);
        //yield return Cache.GetWFS(Constant.Time_Player_Die_attack - Constant.Time_Player_Show_Blood);
        //
        yield return Cache.GetWFS(Constant.Time_Player_Die_attack - Constant.Time_Player_Show_Blood);
        ReSetCharacterState();
        SetCharacterState_NoLoop(Action_Die);
        yield return Cache.GetWFS(Constant.Time_Player_Die_die);
        ReSetCharacterState();
        SetCharacterState_Loop(Action_Die_loop);
        yield return Cache.GetWFS(0.1f);
        GameManager.Ins.Set_Lose_Level(floor_stay);
        //Destroy(this.gameObject);
        //gameObject.SetActive(false);
    }
    IEnumerator Delay_AttackTo_Idle(bool _isOpn_Reward = false)
    {
        //SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
        Set_Block_Colider_Player();


        #region Random Skill


        //hàm sau set  index_Skill_Use + set luôn anim attack
        if (!_isOpn_Reward)
        {
            Random_Skill_attack();

        }
        else
        {
            Random_Skill_attack(true);
        }



        //float time_Until_End_Skill = Constant.Get_Time_Skill(anim_duration);
        //if (_isOpn_Reward)
        //{
        //    //index_Skill_Use = 0;
        //    time_Until_End_Skill = Constant.Get_Time_Skill(anim_duration);

        //}




        //nếu random ra index dùng skill, sẽ Delay Boss attack lâu hơn
        yield return Cache.GetWFS(anim_duration);
        #endregion
        //Set_Un_Block_Colider_Player();
        ReSetCharacterState();
        SetCharacterState_Loop(Action_Idle);



        //Nếu là đánh Boss win
        yield return Cache.GetWFS(Constant.Time_Player_Show_Blood);
        Set_Un_Block_Colider_Player();
        if (isLast_Point_Level)
        {
            GameManager.Ins.Set_Bool_Win_Boss();
            Set_Config_Boss_Die_End_Level();
        }
        //
        //yield return Cache.GetWFS(time_Until_End_Skill - Constant.Time_Player_Show_Blood);


        //
        /////////Set_Check_Reach_Last_Point_Level(indext_Point_In_Floor_Stay,floor_stay);
        if (!isReach_Last_Point_Level)
        {
            Set_Check_Reach_Last_Point_Level();//1
        }

    }
    #endregion
    #region Function Random Skill
    public void Random_Skill_attack(bool _isOpn_Reward = false)
    {
        int ii = UnityEngine.Random.Range(1, 5);
        if (_isOpn_Reward)
        {
            ii = 0;
        }
        int id_Skin_Wearing = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing();
        /////int offset = 2;
        ///
        /////_index_Skin_Wearing bắt đầu từ 0


        if (id_Skin_Wearing == 0)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_1_Skill_1);
                anim_duration = Action_char_1_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_1_Skill_2);
                anim_duration = Action_char_1_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_1_Skill_3);
                anim_duration = Action_char_1_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 1)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_2_Skill_1);
                anim_duration = Action_char_2_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_2_Skill_2);
                anim_duration = Action_char_2_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_2_Skill_3);
                anim_duration = Action_char_2_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 2)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_3_Skill_1);
                anim_duration = Action_char_3_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_3_Skill_2);
                anim_duration = Action_char_3_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_3_Skill_3);
                anim_duration = Action_char_3_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 3)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_4_Skill_1);
                anim_duration = Action_char_4_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_4_Skill_2);
                anim_duration = Action_char_4_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_4_Skill_3);
                anim_duration = Action_char_4_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 4)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_5_Skill_1);
                anim_duration = Action_char_5_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_5_Skill_2);
                anim_duration = Action_char_5_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_5_Skill_3);
                anim_duration = Action_char_5_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 5)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_6_Skill_1);
                anim_duration = Action_char_6_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_6_Skill_2);
                anim_duration = Action_char_6_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_6_Skill_3);
                anim_duration = Action_char_6_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 6)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_7_Skill_1);
                anim_duration = Action_char_7_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_7_Skill_2);
                anim_duration = Action_char_7_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_7_Skill_3);
                anim_duration = Action_char_7_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 7)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_8_Skill_1);
                anim_duration = Action_char_8_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_8_Skill_2);
                anim_duration = Action_char_8_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_8_Skill_3);
                anim_duration = Action_char_8_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 8)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_9_Skill_1);
                anim_duration = Action_char_9_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_9_Skill_2);
                anim_duration = Action_char_9_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_9_Skill_3);
                anim_duration = Action_char_9_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 9)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_10_Skill_1);
                anim_duration = Action_char_10_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_10_Skill_2);
                anim_duration = Action_char_10_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_10_Skill_3);
                anim_duration = Action_char_10_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 10)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_11_Skill_1);
                anim_duration = Action_char_11_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_11_Skill_2);
                anim_duration = Action_char_11_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_11_Skill_3);
                anim_duration = Action_char_11_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 11)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_12_Skill_1);
                anim_duration = Action_char_12_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_12_Skill_2);
                anim_duration = Action_char_12_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_12_Skill_3);
                anim_duration = Action_char_12_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 12)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_13_Skill_1);
                anim_duration = Action_char_13_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_13_Skill_2);
                anim_duration = Action_char_13_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_13_Skill_3);
                anim_duration = Action_char_13_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else if (id_Skin_Wearing == 13)
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_14_Skill_1);
                anim_duration = Action_char_14_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_14_Skill_2);
                anim_duration = Action_char_14_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_14_Skill_3);
                anim_duration = Action_char_14_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }
        else
        {
            if (ii == 1)
            {
                SetCharacterState_NoLoop(Action_char_15_Skill_1);
                anim_duration = Action_char_15_Skill_1.Animation.Duration;
            }
            else if (ii == 2)
            {
                SetCharacterState_NoLoop(Action_char_15_Skill_2);
                anim_duration = Action_char_15_Skill_2.Animation.Duration;
            }
            else if (ii == 3)
            {
                SetCharacterState_NoLoop(Action_char_15_Skill_3);
                anim_duration = Action_char_15_Skill_3.Animation.Duration;
            }

            else// (ii == 4)
            {
                SetCharacterState_NoLoop(Action_Attack);
                anim_duration = Action_Attack.Animation.Duration;
                SoundManager.Ins.PlayFx(FxID.attack_GamePlay);
            }
        }

    }

    #endregion

    #region Function Random Skill___attack___BOSS
    public void Random_Skill_attack_BOSS()
    {
        Random_Skill_attack(false);
    }

    #endregion
    #region Set action Boss last Level
    public void Set_Config_Boss_Die_End_Level()
    {
        floor_stay.list_Point_In_Floor[0].enemy_Attack_This_Point.Set_Show_Blood();
        floor_stay.list_Point_In_Floor[0].enemy_Attack_This_Point.Set_Anim_Die();
    }
    public void Set_Config_Boss_Win_End_Level()
    {
        floor_stay.list_Point_In_Floor[0].enemy_Attack_This_Point.Set_Attack();
    }

    #endregion
    #region Delay_Win
    //Win ở đây khi nhận rương ...
    IEnumerator Delay_Win()
    {
        yield return Cache.GetWFS(anim_duration);
        GameManager.Ins.Set_Mai_Xanh_Delay_Win(floor_stay);
    }
    #endregion
    #region Base to set Skin, Anim
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


    #region Set Scale Player when drag, drop
    public void Set_Scale_Bigger()
    {
        tf_Player.localScale = new Vector3(Constant.Player_Scale_Bigger, Constant.Player_Scale_Bigger, Constant.Player_Scale_Bigger);
    }
    public void Set_Scale_Normal()
    {
        tf_Player.localScale = new Vector3(Constant.Player_Scale_Normal, Constant.Player_Scale_Normal, Constant.Player_Scale_Normal);
    }
    #endregion
    #region Set, Get Health
    public void Set_Add_Health(int _healthAdd)
    {
        health += _healthAdd;
    }
    public int Get_Health()
    {
        return health;
    }
    public void Set_Spawn_Health_Bar()
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load(Constant.Path_Frefab_Health_Bar_Blue), tf_Player);
        health_Bar = obj.GetComponent<Health_Bar>();

        health_Bar.tf_Health_Bar.localPosition = Constant.Player_Local_Pos_Health_Bar;
        health_Bar.Set_Health_Imedetly(health);
    }
    #endregion
    #region chọn cái nhà để cao lên khi đã chiếm đc nhà khác
    //chọn cái nhà để cao lên khi đã chiếm đc nhà khác
    public void Set_House_To_Up_Top_Player(House_Build _house_Build)
    {
        house_Build_Of_Player = _house_Build;
    }
    #endregion
    #region Set_Show_Blood
    public void Set_Show_Blood()
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load(Constant.Path_Frefab_Blood), tf_Player.position + Constant.Player_offset_Blood, tf_Player.rotation);
    }
    #endregion
    #region tự đến điểm cuối của level
    //Sau khi đánh thắng Enemy còn lại khi đến Boss cuối của 1 Level(Boss cuối, hoặc Công chúa, hoặc hòm,...) thì tự đến điểm cuối đó
    public void Set_Go_To_Point_End_Level(Transform _tf_End_Level, Enum_Type_Take_Last_Level _type_Take_Last_Level, int _health_If_Enemy = 0)
    {
        Set_Block_Colider_Player();
        Set_Anim_Run();
        tf_Player.DOMove(_tf_End_Level.position, Constant.Time_Player_Move_End_Level).OnComplete(() =>
        {
            if (_type_Take_Last_Level == Enum_Type_Take_Last_Level.Enemy)
            {
                if (health > _health_If_Enemy)
                {
                    Set_Attack();
                }
                else
                {

                    Set_Anim_Die();
                }
            }
            else if (_type_Take_Last_Level == Enum_Type_Take_Last_Level.Reward)
            {
                //UNDONE.............
                //Set anim Hòm bị mở+ vàng bay lên
                Set_Attack(true);
                if (floor_stay.list_Point_In_Floor[0].reward_Attack_This_Point != null)
                {
                    floor_stay.list_Point_In_Floor[0].reward_Attack_This_Point.Set_Open();

                }
                StartCoroutine(Delay_Win());
            }
            else if (_type_Take_Last_Level == Enum_Type_Take_Last_Level.Princess)
            {
                //UNDONE.............
                //Set anim Hòm bị mở+ vàng bay lên
                SetCharacterState_Loop(Action_Victory);
#if UNITY_EDITOR
                //Debug.Log("Player victory");
#endif
                if (floor_stay.list_Point_In_Floor[0].princess_Attack_This_Point != null)
                {
                    floor_stay.list_Point_In_Floor[0].princess_Attack_This_Point.Set_Victory();

                }
                GameManager.Ins.Set_Mai_Xanh_Delay_Win(floor_stay);
                //StartCoroutine(Delay_Win());
            }
        });
    }
    #endregion
    #region Kiểm tra đã đến điểm cuối của 1 Level
    //Kiểm tra Floor cuối Level có phần thưởng hay Enemy hay Công chúa để sét các action tiếp theo, mỗi lần thực hiện 1 hành động tại 1 điển của Floor
    public void Set_Check_Reach_Last_Point_Level()
    {
        if (indext_Point_In_Floor_Stay == 2 && floor_stay.is_Floor_Last_Of_Level)
        {
            //
            if (floor_stay.list_Point_In_Floor[0].enemy_Attack_This_Point != null)
            {
                isLast_Point_Level = true;
                isReach_Last_Point_Level = true;


                //
                if (PlayerPrefs_Manager.Get_Index_Level_Normal() != 38 && PlayerPrefs_Manager.Get_Index_Level_Normal() != 44 && PlayerPrefs_Manager.Get_Index_Level_Normal() != 47)
                {
                    //Set_Go_To_Point_End_Level(floor_stay.tf_Point_End_Level, Enum_Type_Take_Last_Level.Enemy, floor_stay.list_Point_In_Floor[0].enemy_Attack_This_Point.Get_Health());
                }
            }
            else if (floor_stay.list_Point_In_Floor[0].princess_Attack_This_Point != null)
            {
                isReach_Last_Point_Level = true;
                Set_Go_To_Point_End_Level(floor_stay.tf_Point_End_Level, Enum_Type_Take_Last_Level.Princess);
            }
            else if (floor_stay.list_Point_In_Floor[0].reward_Attack_This_Point != null)
            {
                isReach_Last_Point_Level = true;
                Set_Go_To_Point_End_Level(floor_stay.tf_Point_End_Level, Enum_Type_Take_Last_Level.Reward);
            }
        }
        else if (indext_Point_In_Floor_Stay == 1 && floor_stay.is_Floor_Last_Of_Level)
        {
            //
            if (floor_stay.list_Point_In_Floor[0].enemy_Attack_This_Point != null)
            {
                isLast_Point_Level = true;
                isReach_Last_Point_Level = true;


                //
                int level = PlayerPrefs_Manager.Get_Index_Level_Normal();
                if (level != 1
                 && level != 2
                 && level != 10
                 && level != 11
                 && level != 12
                 && level != 23
                 && level != 24
                 && level != 30
                 && level != 34
                 && level != 38
                 && level != 39
                 && level != 40
                 && level != 42
                 && level != 44
                 && level != 46
                 && level != 47)
                {
                    Set_Go_To_Point_End_Level(floor_stay.tf_Point_End_Level, Enum_Type_Take_Last_Level.Enemy, floor_stay.list_Point_In_Floor[0].enemy_Attack_This_Point.Get_Health());
                }
            }
            else if (floor_stay.list_Point_In_Floor[0].princess_Attack_This_Point != null)
            {
                isReach_Last_Point_Level = true;
                Set_Go_To_Point_End_Level(floor_stay.tf_Point_End_Level, Enum_Type_Take_Last_Level.Princess);
            }
            else if (floor_stay.list_Point_In_Floor[0].reward_Attack_This_Point != null)
            {
                isReach_Last_Point_Level = true;
                Set_Go_To_Point_End_Level(floor_stay.tf_Point_End_Level, Enum_Type_Take_Last_Level.Reward);
            }
        }
    }
    #endregion
    #region Chiếm được 1 nhà
    public void Set_Chiem_Duoc_1_Nha()
    {
        ((CanvasGamePlay)UIManager.Ins.GetUI(UIID.UICGamePlay)).Set_Active_Castle_Each_Time_Chiem_Duoc(index_Castle_current);
    }
    #endregion
    #region Mở rương
    //Đi từ điểm đầu đến điểm cuối của 1 Floor
    public void Set_Take_Reward(Floor floor_pos_End)
    {
        isTakingReward = true;
        StartCoroutine(IE_Delay_Run_To_End(floor_pos_End));
    }
    IEnumerator IE_Delay_Run_To_End(Floor floor_pos_End)
    {
        Set_Block_Colider_Player();
        yield return Cache.GetWFS(Constant.Time_Delay_Run_To_End_Reward);
        //SetCharacterState_Loop(Action_Run);
        SetAnimation(Action_Dam, true, 2f);
        tf_Player.DOMove(floor_pos_End.list_Point_In_Floor[1].tf_Point_In_Floor.position, Constant.Time_Player_Go_End_OpenReward).OnComplete(() =>
        {
            isTakingReward = false;
            floor_pos_End.Set_Open_Reward();
            floor_pos_End.house_Build_Of_This.Set_Check_Next_House_Or_Victory(floor_pos_End);



            //Nhà cuối thì set anim Victory ở điểm cuối cùng
            if (floor_pos_End.house_Build_Of_This.number_Floor_Remain == 1)
            {
                Set_Un_Block_Colider_Player();
                Set_Anim_Victory();
                is_Time_Lv26_Ready_To_Fix = true;

            }
            else
            {
                SetCharacterState_Loop(Action_Idle);
                Set_Un_Block_Colider_Player();
            }
            //Set_Pos_Old(floor_pos_End.list_Point_In_Floor[1].tf_Point_In_Floor.position);
            Set_Fix_Pos_Player();
            Set_Un_Block_Colider_Player();
        });
    }
    #endregion
    #region Block Player
    public void Set_Block_Colider_Player()
    {
        //Debug.Log("====");
        boxCollider_Player.enabled = false;
    }
    public void Set_Un_Block_Colider_Player()
    {
        boxCollider_Player.enabled = true;
    }
    #endregion
    public void Delay_Hit_To_Idle()
    {
        StartCoroutine(IE_Delay_Hit_To_Idle());
    }
    IEnumerator IE_Delay_Hit_To_Idle()
    {
        yield return Cache.GetWFS(1);
        Set_Anim_Idle();
        Set_Un_Block_Colider_Player();
    }
    public void Set_Fix_Pos_Player()
    {
        //TODO: Fix vị trí Player lơ lửng
        pos_Old_Player = tf_Player.localPosition;
    }
}

/*
  public void Set_Check_Reach_Last_Point_Level(int _indexPoint  , Floor _floor)
    {
        if (_indexPoint == 2 && _floor.is_Floor_Last_Of_Level)
        {
            //
            if (_floor.list_Point_In_Floor[0].enemy_Attack_This_Point != null)
            {
                Player.ins.Set_Go_To_Point_End_Level(_floor.tf_Point_End_Level, Type_Take_Last_Level.Enemy, _floor.list_Point_In_Floor[0].enemy_Attack_This_Point.Get_Health());
            }
            else if (_floor.list_Point_In_Floor[0].princess_Attack_This_Point != null)
            {
                Player.ins.Set_Go_To_Point_End_Level(_floor.tf_Point_End_Level, Type_Take_Last_Level.Princess);
            }
            else if (_floor.list_Point_In_Floor[0].reward_Attack_This_Point != null)
            {
                Player.ins.Set_Go_To_Point_End_Level(_floor.tf_Point_End_Level, Type_Take_Last_Level.Reward);
            }
        }
    }
 */