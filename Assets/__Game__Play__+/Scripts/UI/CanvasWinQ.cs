using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Spine.Unity;
public class CanvasWinQ : UICanvas
{
    #region Show each level enough
    //public GameObject obj_PigBank;
    //public GameObject obj_Arena;
    public GameObject obj_Hand_Tut_Lv_0;
    #endregion

    #region Gold_Pink_bank
    public GameObject obj_Full_Gold_Pink_bank;//gold ở bank đầy
    public GameObject obj_Normal_Gold_Pink_bank;//gold ở bank chưa đầy
    public Text txt_Gold_Pink_Bank;
    #endregion
    public RectTransform arrow;
    private float timeKeep;
    private bool isStop;
    private float range = 350f;
    //private float wallXTime;
    public Text reward_ADsTxt;
    public Text claimTxt;
    public Text gem_all;
    //
    public GameObject obj_Btn_No_Thank;
    public GameObject obj_Image_Claim_ADs;
    //
    public GameObject obj_Image_BG_ADs_Xanh;
    public GameObject obj_Image_BG_ADs__Xam;
    public bool isFist_Click;
    public Transform tf_Spawn_Fire_Work;
    #region Gold_Gem
    public Text txt_Gold;
    public Text txt_Gem;
    public Text txt_Level;
    //
    public Text txt_Gold_Boot;
    public Text txt_Gem_Boot;
    #endregion
    public List<GameObject> list_Tile_BG_L;
    public List<GameObject> list_Tile_BG_R;
    #region Tile

    #endregion
    #region Icon Tower....màu index: 0: là xanh.. 1: là trắng... 2 là nâu tương ứng là: pass.. Standing... và chưa chơi đến level đó
    public RectTransform rect_Paren_Off_Icon;
    public List<GameObject> list_Prefabs_Icon_Normal;
    public List<GameObject> list_Prefabs_Icon_Reward;
    public List<GameObject> list_Prefabs_Icon_Boss;
    #endregion
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;
    [Header("------Not Need Asign--To view------")]
    public List<RectTransform> list_RectTransform_Icon_before;
    public RectTransform rect_Icon_Living;
    public int multiX;
    public int goldCollected;
    //

    private void OnEnable()
    {
        if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 1)
        {
            EventController.WIN_LEVEL_EVENT(PlayerPrefs_Manager.Get_Index_Level_Normal());
        }
        else if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 3)
        {
            EventController.WIN_LEVEL_EVENT_CHALLENGE(PlayerPrefs_Manager.Get__QLevel_Challenge());
        }

        Set_Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeKeep += Time.deltaTime;
        if (isStop || timeKeep < 0.6f) return;
        float posx = range * Mathf.Sin(Time.time * 4f);
        arrow.anchoredPosition = new Vector2(posx, arrow.anchoredPosition.y);
        multiX = GetXRewardCount();
        //rewardTxt.text = ((int)((int)(gemCollected * wallXTime) * multiX)).ToString();
        reward_ADsTxt.text = ((int)((int)(goldCollected) * multiX)).ToString();
    }
    public void Set_Init()
    {
        SoundManager.Ins.PlayFx(FxID.win);

        //
        isFist_Click = false;
        int level = PlayerPrefs_Manager.Get_Index_Level_Normal() - 1;//trước khi vào đây đã tăng level ở Gamanager rồi
        if(level == 0)
        {
            if (obj_Hand_Tut_Lv_0 != null)
            {
                obj_Hand_Tut_Lv_0.SetActive(true);

            }
        }


        //if (level < 2)
        //{
        //    obj_Image_Claim_ADs.SetActive(false);
        //}
        //else
        //{
        //    //EfxManager.ins.Set_Gold_Fly_Pig_OK();
        //    obj_Image_Claim_ADs.SetActive(true);
        //}
        //UNDONE:  LOAD........ gold của Player hiện lên canvas
        //UNDONE: Set............ gemCollected
        goldCollected = Constant.Get_Gold_Reward_By_level(PlayerPrefs_Manager.Get_Index_Level_Normal());
        txt_Gold_Boot.text = goldCollected.ToString();
        txt_Gem_Boot.text = "1";
        StartCoroutine(Set_Delay_Show_No_Thank());
        //
        //Set_Check_Show_Btn();
        //Set_Step_By_Step_Gold(PlayerPrefs_Manager.Get_Gold(), PlayerPrefs_Manager.Get_Gold() + Constant.Get_Gold_Reward_By_level(PlayerPrefs_Manager.Get_Index_Level_Normal() - 1), 1);
        //Set_Step_By_Step_Gem(PlayerPrefs_Manager.Get_Gem(), PlayerPrefs_Manager.Get_Gem() + Constant.Get_Gem_By_level(PlayerPrefs_Manager.Get_Index_Level_Normal() - 1), 1);


        //Lấy level -1 là level vừa chơi vì đã cộng sau khi win rồi
        //Debug.Log("==sdfs===" + level);
        int _level_fix = level + 1;
        if (_level_fix != 6 && _level_fix != 10 && _level_fix != 13 && _level_fix != 20 && _level_fix != 23 && _level_fix != 30 && _level_fix != 31 && _level_fix != 46 && _level_fix != 1 && _level_fix != 2 && _level_fix != 3)
        {
            //Debug.Log("==sdfs===");
            Set_Gold_EFX();

        }
        

        int gold_Current = PlayerPrefs_Manager.Get_Gold() + Constant.Get_Gold_Bonus_By_Level(PlayerPrefs_Manager.Get_Index_Level_Normal() - 1);


        int gem_Current = PlayerPrefs_Manager.Get_Gem() + 1;

        //
        PlayerPrefs_Manager.Set_Gold(gold_Current);
        PlayerPrefs_Manager.Set_Gem(gem_Current);
        if (PlayerPrefs_Manager.Get_Index_Level_Normal() > 3)
        {
            PlayerPrefs_Manager.Set_Pink_Bank_Gold(PlayerPrefs_Manager.Get_Pink_Bank_Gold() + 100);
        }
        //
        //Set_Init_Gold_Pink_bank();
        Set_Init_Gold_Gem_Title();
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_In();
        //
        GameManager.Ins.Set_Spawn_FireWord(tf_Spawn_Fire_Work);
        //
        //init Skin
        int id_Skin = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing();
        string name_Skin = Constant.Get_Skin_Name_By_Id(id_Skin);
        Set_Skin(name_Skin);
        //
        
        //
        Set_Tile_BG();
        Set_Spawn_Icon(level+2);
        StartCoroutine(IE_Scale_iceon_before());
    }
    public void Set_Gold_EFX()
    {
        EfxManager.ins.Set_GoldTop_FX();
        EfxManager.ins.Set_Gold_Fly_Pig_OK();
        Invoke("Set_Up_Gold_Fly", 1.5f);
    }
    public void Set_update_Gold()
    {
        txt_Gold_Boot.text = PlayerPrefs_Manager.Get_Gold().ToString();
    }
    public void Set_Up_Gold_Fly()
    {
        
        Set_Step_By_Step_Gold(PlayerPrefs_Manager.Get_Gold(), PlayerPrefs_Manager.Get_Gold() + Constant.Get_Gold_Reward_By_level(PlayerPrefs_Manager.Get_Index_Level_Normal() - 1), 1);
        Set_Step_By_Step_Gem(PlayerPrefs_Manager.Get_Gem(), PlayerPrefs_Manager.Get_Gem() + Constant.Get_Gem_By_level(PlayerPrefs_Manager.Get_Index_Level_Normal() - 1), 1);
    }
    
    public void Set_Tile_BG()
    {
        //Reset image
        for (int i = 0; i < list_Tile_BG_L.Count; i++)
        {
            list_Tile_BG_L[i].SetActive(false);
            list_Tile_BG_R[i].SetActive(false);
        }
        //
        int level = PlayerPrefs_Manager.Get_Index_Level_Normal() - 1;//trước khi vào đây đã tăng level ở Gamanager rồi
        int indext_tile_BG_Show = 0;
        if (level <= 6)
        {
            indext_tile_BG_Show = 0;
        }
        else if (level <= 16)
        {
            indext_tile_BG_Show = 1;
        }
        else if (level <= 25)
        {
            indext_tile_BG_Show = 2;
        }
        else if (level <= 35)
        {
            indext_tile_BG_Show = 3;
        }
        else if (level <= 45)
        {
            indext_tile_BG_Show = 4;
        }
        else
        {
            indext_tile_BG_Show = 5;
        }
        if (indext_tile_BG_Show < list_Tile_BG_L.Count && indext_tile_BG_Show < list_Tile_BG_R.Count)
        {
            list_Tile_BG_L[indext_tile_BG_Show].SetActive(true);
            list_Tile_BG_R[Mathf.Min(indext_tile_BG_Show + 1, 5)].SetActive(true);
        }
    }

    //public void Set_Check_Show_Btn()
    //{
    //    if (PlayerPrefs_Manager.Get_Index_Level_Normal() > 1)
    //    {
    //        obj_Arena.SetActive(true);
    //    }
    //    else
    //    {
    //        obj_Arena.SetActive(false);
    //    }
    //    if (PlayerPrefs_Manager.Get_Index_Level_Normal() > 2)
    //    {
    //        obj_PigBank.SetActive(true);
    //    }
    //    else
    //    {
    //        obj_PigBank.SetActive(false);
    //    }
    //}
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
    #region Gold Gem Title
    public void Set_Init_Gold_Gem_Title()
    {


        txt_Gold.text = PlayerPrefs_Manager.Get_Gold().ToString("N0");




        txt_Gem.text = PlayerPrefs_Manager.Get_Gem().ToString("N0");

        if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 1)
        {
            txt_Level.text = "LEVEL " + (PlayerPrefs_Manager.Get_Index_Level_Normal()+1).ToString();

        }
        else if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 2)
        {
            txt_Level.text = "LEVEL " + (PlayerPrefs.GetInt(UserData.Key_LevelArena)).ToString();
        }
        else if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 3)
        {
            
            txt_Level.text = "CHALLENGE " + (PlayerPrefs_Manager.Get__QLevel_Challenge()).ToString();
            PlayerPrefs_Manager.Set_Key_1GamPlay_Or_2Area_Or_3Challenge(3);
        }
    }
    #endregion
    #region Area
    public void AreaButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);

        PlayerPrefs.SetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge, 2);
        StartCoroutine(LoadScene("Ar_Level_0"));

        EventController.GAME_PLAY("icon_arena_click");
    }
    #endregion
    #region Challenge
    public void ChallengeButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        UIManager.Ins.OpenUI(UIID.UICChallenge);

        EventController.GAME_PLAY("icon_challange_click");
    }
    #endregion
    #region Skin
    public void SkinButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        UIManager.Ins.OpenUI(UIID.UICSkin_Top);
        UIManager.Ins.OpenUI(UIID.UICSkin_Boot);
        Close();
        EventController.GAME_PLAY("icon_shop_click");
    }
    #endregion
    #region Home
    public void Homebutton()
    {
        SoundManager.Ins.PlayFx(FxID.click);

        StartCoroutine(LoadScene("Loading"));
        EventController.GAME_PLAY("icon_home");
    }
    private IEnumerator LoadScene(string sceneName)
    {
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();

        AsyncOperation homeScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!homeScene.isDone)
        {
            yield return null;
        }
    }
    #endregion
    #region Gold_Pink_bank
    public void PigBankButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        UIManager.Ins.OpenUI(UIID.UICPigBank);
    }
    public void Set_Init_Gold_Pink_bank()
    {
        int gold_Pink_bank = PlayerPrefs_Manager.Get_Pink_Bank_Gold();
        if (gold_Pink_bank == Constant.Gold_Max_Pink_Bank)
        {
            obj_Full_Gold_Pink_bank.SetActive(true);
            obj_Normal_Gold_Pink_bank.SetActive(false);
        }
        else
        {
            obj_Full_Gold_Pink_bank.SetActive(false);
            obj_Normal_Gold_Pink_bank.SetActive(true);
            txt_Gold_Pink_Bank.text = gold_Pink_bank.ToString();
        }
    }

    #endregion

    public int GetXRewardCount()
    {
        
        if (Mathf.Abs(arrow.anchoredPosition.x) < Constant.Pos_X_Rollbar_X5)
        {
            //claimTxt.text = "CLAIM X5";
            return 5;
        }
        else if (Mathf.Abs(arrow.anchoredPosition.x) < Constant.Pos_X_Rollbar_X4)
        {
            //claimTxt.text = "CLAIM X4";
            return 4;
        }
        else if (Mathf.Abs(arrow.anchoredPosition.x) < Constant.Pos_X_Rollbar_X3)
        {
            //claimTxt.text = "CLAIM X3";
            return 3;
        }
        else
        {
            //claimTxt.text = "CLAIM X2";
            return 2;
        }
    }
    public void Set_ADs_To_Stop_Random()
    {
        SoundManager.Ins.PlayFx(FxID.click);

#if WatchADs
        AdsManager.Instance.WatchRewardedAds(TakeRandomGold, "video_get_xGold_screenWin");
#else
        TakeRandomGold();
#endif
    }

    private void TakeRandomGold()
    {
        if (!isFist_Click)
        {
            isFist_Click = true;
            //


            //
            obj_Image_BG_ADs_Xanh.SetActive(false);
            obj_Image_BG_ADs__Xam.SetActive(true);
            isStop = true;
            StartCoroutine(Delay_Increa_Gem());
            Set_Fade_And_Close();
        }
        int level = PlayerPrefs_Manager.Get_Index_Level_Normal() - 1;//trước khi vào đây đã tăng level ở Gamanager rồi
        if (level == 0)
        {
            if (obj_Hand_Tut_Lv_0 != null)
            {
                obj_Hand_Tut_Lv_0.SetActive(false);

            }
        }
    }

    public void Set_Claim_To_Stop_Random_No_ADs()
    {
        SoundManager.Ins.PlayFx(FxID.click);

#if WatchADs
        AdsManager.Instance.WatchRewardedAds(GetGoldRandom,"video_get_randomGold_screenWin");
#else
        GetGoldRandom();
#endif
    }

    private void GetGoldRandom()
    {
        if (!isFist_Click)
        {
            isFist_Click = true;
            isStop = true;
            StartCoroutine(Delay_Increa_Gem());
            Set_Fade_And_Close();
        }
    }

    public void Set_Fade_And_Close()
    {
        StartCoroutine(IE_Delay_Fade_ADs_Close());
    }
    IEnumerator IE_Delay_Fade_ADs_Close()
    {
        yield return Cache.GetWFS(Constant.Time_Fade);
        yield return Cache.GetWFS(Constant.Time_Fade);
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();
        yield return Cache.GetWFS(Constant.Time_Fade);
        Close();
        
        
        ////PlayerPrefs_Manager.Set_Index_Level_Normal(indexLevel);

        if (PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge) == 1)
        {
            int indexLevel = PlayerPrefs_Manager.Get_Index_Level_Normal();
            Scene_Manager_Q.Load_Scene(Constant.StringLevel + indexLevel.ToString());
        }
        else if (PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge) == 2)
        {
            Scene_Manager_Q.Load_Scene("Ar_Level_0");
        }
        else if (PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge) == 3)
        {
            int indexLevel = PlayerPrefs_Manager.Get__QLevel_Challenge();
            if (indexLevel <= 6)
                Scene_Manager_Q.Load_Scene(Constant.StringChallengeLevel + indexLevel.ToString());
            else
                Scene_Manager_Q.Load_Scene("Loading");
        }

        //SceneManager.LoadScene(Constant.StringLevel + indexLevel.ToString(), LoadSceneMode.Single);
    }
    
    public void Set_No_Thank()
    {
        SoundManager.Ins.PlayFx(FxID.click);

#if WatchADs
         if (PlayerPrefs_Manager.Get_Index_Level_Normal() >= 4)
            AdsManager.Instance.WatchInterstitialAds(NoThanksClicked);
        else
            NoThanksClicked();
#else
        NoThanksClicked();
#endif

        EventController.GAME_PLAY("nothank_click_win");
    }

    private void NoThanksClicked()
    {
        if (!isFist_Click)
        {
            isFist_Click = true;
            isStop = true;
            //StartCoroutine(Delay_Increa_Gem());


            if (PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge) == 1)
            {
                int indexLevel = PlayerPrefs_Manager.Get_Index_Level_Normal();
                Scene_Manager_Q.Load_Scene(Constant.StringLevel + indexLevel.ToString());
            }
            else if (PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge) == 3)
            {
                int indexLevel = PlayerPrefs_Manager.Get__QLevel_Challenge();
                if (indexLevel <= 6)
                    Scene_Manager_Q.Load_Scene(Constant.StringChallengeLevel + indexLevel.ToString());
                else
                    Scene_Manager_Q.Load_Scene("Loading");
            }
            else if (PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge) == 2)
            {
                Scene_Manager_Q.Load_Scene("Ar_Level_0");
            }
            //else if (PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge) == 3)
            //{
            //    UIManager.Ins.OpenUI(UIID.UICMainMenu);
            //    UIManager.Ins.OpenUI(UIID.UICChallenge);

            //}



            //StartCoroutine(Set_Delay_Load_Scene(Constant.StringLevel + indexLevel.ToString()));
            UIManager.Ins.OpenUI(UIID.UICFade);
            ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();
        }

        if (obj_Hand_Tut_Lv_0 != null)
        {
            obj_Hand_Tut_Lv_0.SetActive(false);

        }
    }
    

    IEnumerator Delay_Increa_Gem()
    {
        yield return Cache.GetWFS(0.5f);
        Set_Step_By_Step_Gold(PlayerPrefs_Manager.Get_Gold(), PlayerPrefs_Manager.Get_Gold() + (int)(goldCollected) * multiX, 1);
        EfxManager.ins.Set_GoldTop_FX();

        int gold_Current = PlayerPrefs_Manager.Get_Gold() + (int)(goldCollected) * multiX;
        //
        PlayerPrefs_Manager.Set_Gold(gold_Current);
    }
    public void Set_Step_By_Step_Gem(int _score, int target, float transitionTime)
    {
        Tween t = DOTween.To(() => _score, x => _score = x, target, transitionTime).OnUpdate(() => txt_Gem.text = _score.ToString("N0"));
    }
    public void Set_Step_By_Step_Gold(int _score, int target, float transitionTime)
    {
        Tween t = DOTween.To(() => _score, x => _score = x, target, transitionTime).OnUpdate(
            () =>
            {
                txt_Gold.text = _score.ToString("N0");
                PlayerPrefs_Manager.Set_Gold(_score);
            });
    }


    IEnumerator Set_Delay_Load_Scene(string _str_Scene)
    {
        yield return Cache.GetWFS(Constant.Time_Delay_Load_Scene);

        Scene_Manager_Q.Load_Scene(_str_Scene);
        //SceneManager.LoadScene(_str_Scene, LoadSceneMode.Single);
        Close();
    }
    IEnumerator Set_Delay_Show_No_Thank()
    {
        yield return Cache.GetWFS(Constant.Time_Delay_Show_No_Thank);
        obj_Btn_No_Thank.SetActive(true);
    }




#region Get Icon prefabs
    IEnumerator IE_Scale_iceon_before()
    {
        yield return Cache.GetWFS(1f);
        if (list_RectTransform_Icon_before.Count > 0)
        {
            for (int i = 0; i < list_RectTransform_Icon_before.Count; i++)
            {
                list_RectTransform_Icon_before[i].DOScale(new Vector3(1.2f, 1.2f, 1.2f), .25f).OnComplete(() =>
                {
                    list_RectTransform_Icon_before[i].DOScale(Vector3.one, .25f);
                });
                yield return Cache.GetWFS(.25f);
            }
        }
        rect_Icon_Living.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .25f).SetLoops(-1, LoopType.Yoyo);
    }
    public void Set_Spawn_Icon(int _level)
    {
#if UNITY_EDITOR
        //Debug.Log("================="+ _level);
#endif
        int start_level_Icon = Get_level_Start_Icon(_level);//chỉ số icon đầu tiên trong số 10 icon
        if (_level < 7)
        {
            for (int i = 0; i < 7; i++)
            {
                if (i < _level)
                {
                    GameObject obj_icon = (GameObject)Instantiate(Get_List_Prefabs_Icon_By_Level(i)[0], rect_Paren_Off_Icon);//sinh icon màu xanh
                    list_RectTransform_Icon_before.Add(obj_icon.GetComponent<RectTransform>());
                }
                else if (i == _level)
                {
                    GameObject obj_icon = (GameObject)Instantiate(Get_List_Prefabs_Icon_By_Level(i)[1], rect_Paren_Off_Icon);//sinh icon màu trắng
                    rect_Icon_Living = obj_icon.GetComponent<RectTransform>();
                }
                else if (i > _level)
                {
                    GameObject obj_icon = (GameObject)Instantiate(Get_List_Prefabs_Icon_By_Level(i)[2], rect_Paren_Off_Icon);//sinh icon màu nâu
                }

            }
        }
        else
        {
            for (int i = start_level_Icon; i < start_level_Icon + 10; i++)
            {
                if (i < _level)
                {
                    GameObject obj_icon = (GameObject)Instantiate(Get_List_Prefabs_Icon_By_Level(i)[0], rect_Paren_Off_Icon);//sinh icon màu xanh
                    list_RectTransform_Icon_before.Add(obj_icon.GetComponent<RectTransform>());
                }
                else if (i == _level)
                {
                    GameObject obj_icon = (GameObject)Instantiate(Get_List_Prefabs_Icon_By_Level(i)[1], rect_Paren_Off_Icon);//sinh icon màu trắng
                    rect_Icon_Living = obj_icon.GetComponent<RectTransform>();
                }
                else if (i > _level)
                {
                    GameObject obj_icon = (GameObject)Instantiate(Get_List_Prefabs_Icon_By_Level(i)[2], rect_Paren_Off_Icon);//sinh icon màu nâu
                }
            }
        }

    }

    public int Get_level_Start_Icon(int _level)
    {
        if (_level < 7)
        {
            return 0;
        }
        else if (_level < 17)
        {
            return 7;
        }
        else if (_level < 27)
        {
            return 17;
        }
        else if (_level < 37)
        {
            return 27;
        }
        else if (_level < 47)
        {
            return 37;
        }
        else if (_level < 57)
        {
            return 47;
        }
        else
        {
            return 0;
        }
    }

    public List<GameObject> Get_List_Prefabs_Icon_By_Level(int _level)
    {
        if (Get_Type_Icon(_level) == 0)
        {
            return list_Prefabs_Icon_Normal;
        }
        else if (Get_Type_Icon(_level) == 1)
        {
            return list_Prefabs_Icon_Reward;
        }
        else if (Get_Type_Icon(_level) == 2)
        {
            return list_Prefabs_Icon_Boss;
        }
        else
        {
            return list_Prefabs_Icon_Normal;
        }
    }
    public int Get_Type_Icon(int _level)
    {
        //0-normal... 1-Reward...2-Boss
        switch (_level)
        {
            case 0:
                return 0;
            case 1:
                return 0;
            case 2:
                return 0;
            case 3:
                return 0;
            case 4:
                return 0;
            case 5:
                return 2;
            case 6:
                return 1;
            case 7:
                return 0;
            case 8:
                return 0;
            case 9:
                return 0;
            case 10:
                return 0;
            case 11:
                return 0;
            case 12:
                return 0;
            case 13:
                return 0;
            case 14:
                return 0;
            case 15:
                return 2;
            case 16:
                return 1;
            case 17:
                return 0;
            case 18:
                return 2;
            case 19:
                return 0;
            case 20:
                return 2;
            case 21:
                return 0;
            case 22:
                return 2;
            case 23:
                return 0;
            case 24:
                return 0;
            case 25:
                return 2;
            case 26:
                return 1;
            case 27:
                return 0;
            case 28:
                return 0;
            case 29:
                return 0;
            case 30:
                return 0;
            case 31:
                return 0;
            case 32:
                return 0;
            case 33:
                return 0;
            case 34:
                return 0;
            case 35:
                return 1;
            case 36:
                return 0;
            case 37:
                return 0;
            case 38:
                return 0;
            case 39:
                return 0;
            case 40:
                return 0;
            case 41:
                return 0;
            case 42:
                return 0;
            case 43:
                return 0;
            case 44:
                return 0;
            case 45:
                return 1;
            case 46:
                return 0;
            case 47:
                return 0;
            case 48:
                return 0;
            case 49:
                return 0;
            case 50:
                return 0;
            default:
                return 0;
        }
    }
#endregion

}
