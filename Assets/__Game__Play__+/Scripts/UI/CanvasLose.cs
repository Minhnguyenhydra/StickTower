using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Spine.Unity;
public class CanvasLose : UICanvas
{
    public GameObject obj_Btn_No_Thank;
    public GameObject obj_Btn_ADs;
    public GameObject obj_Btn_Replay_Arena;
    bool isFist_Click;
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;
    public GameObject objQuest;

    public GameObject objBtnCollection;
    public GameObject objBtnSkin;
    public GameObject objBtnArena;
    public GameObject objBtnChallenge;

    //
    #region Gold_Gem,level
    public Text txt_Gold;//gold ở bank đầy
    public Text txt_Gem;//gold ở bank đầy
    public Text txt_Level;//gold ở bank đầy
    #endregion
    private void OnEnable()
    {

        if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 1)
        {
            EventController.LOSE_LEVEL_EVENT(PlayerPrefs_Manager.Get_Index_Level_Normal());
        }
        else if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 3)
        {
            EventController.LOSE_LEVEL_EVENT_CHALLENGE(PlayerPrefs_Manager.Get__QLevel_Challenge());
        }
        else
        {
            EventController.ARENA_EVENT_ARENA_LOSE();
        }

        SoundManager.Ins.PlayFx(FxID.lose);
        Set_Init_Gold_Gem_Level_Title();



        int curLevel = PlayerPrefs_Manager.Get_Index_Level_Normal();

        if (curLevel >= 3)
        {
            objBtnCollection.SetActive(true);
            objBtnSkin.SetActive(true);
        }
        if (curLevel >= 5)
        {
            objBtnArena.SetActive(true);
        }
        if (curLevel >= 11)
        {
            objBtnChallenge.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isFist_Click = false;
        //UNDONE:  LOAD........ gold của Player hiện lên canvas
        //UNDONE: Set............ gemCollected
        StartCoroutine(Set_Delay_Show_No_Thank());
    }
    #region Gold Gem Title
    public void Set_Init_Gold_Gem_Level_Title()
    {
        txt_Gold.text = PlayerPrefs_Manager.Get_Gold().ToString("N0");
        txt_Gem.text = PlayerPrefs_Manager.Get_Gem().ToString("N0");


        if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge()==1)
        {
            obj_Btn_ADs.SetActive(true);
            txt_Level.text = "LEVEL " + (PlayerPrefs_Manager.Get_Index_Level_Normal()).ToString();

        }else if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 2)
        {
            obj_Btn_ADs.SetActive(true);
            txt_Level.text = "LEVEL " + (PlayerPrefs.GetInt(UserData.Key_LevelArena)).ToString();
        }
        else if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 3)
        {
            if (obj_Btn_ADs != null)
            {

                obj_Btn_ADs.SetActive(false);
                obj_Btn_Replay_Arena.SetActive(true);
                txt_Level.text = "LEVEL " + (PlayerPrefs_Manager.Get__QLevel_Challenge()).ToString();
            }
        }

        //init Skin
        int id_Skin = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing();
        string name_Skin = Constant.Get_Skin_Name_By_Id(id_Skin);
        Set_Skin(name_Skin);
        
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

    #region Gold_Pink_bank
    public void PigBankButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        UIManager.Ins.OpenUI(UIID.UICPigBank);
    }
    #endregion

    public void QuestClicked()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        objQuest.SetActive(true);
    }

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
    public void Set_Ads_To_Pass_Level()
    {
        SoundManager.Ins.PlayFx(FxID.click);
#if WatchADs
        AdsManager.Instance.WatchRewardedAds(SkipLevel,"video_show_skip_lose");  
#else
        SkipLevel();
#endif
    }

    private void SkipLevel()
    {
        if (!isFist_Click)
        {
            isFist_Click = true;
            Set_Fade_And_ADs_Close();
        }
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
        EventController.GAME_PLAY("nothank_click_lose");
    }

    private void NoThanksClicked()
    {
        if (!isFist_Click)
        {
            isFist_Click = true;
            Set_Fade_And_NoThank_Close();
        }
    }

    public void Set_Fade_And_NoThank_Close()
    {
        StartCoroutine(IE_Delay_Fade_NoThanks_Close());
    }
    public void Set_Fade_And_ADs_Close()
    {
        StartCoroutine(IE_Delay_Fade_ADs_Close());
    }
    
    IEnumerator Set_Delay_Show_No_Thank()
    {
        yield return Cache.GetWFS(Constant.Time_Delay_Show_No_Thank);
        obj_Btn_No_Thank.SetActive(true);
    }

    IEnumerator IE_Delay_Fade_NoThanks_Close()
    {
        yield return Cache.GetWFS(Constant.Time_Fade);
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();
        yield return Cache.GetWFS(Constant.Time_Fade);
        Close();
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
            Scene_Manager_Q.Load_Scene("Loading");
        }
        //SceneManager.LoadScene(Constant.StringLevel + indexLevel.ToString(), LoadSceneMode.Single);
    }
    IEnumerator IE_Delay_Fade_ADs_Close()
    {
        int lv = PlayerPrefs_Manager.Get_Index_Level_Normal();
        lv = Mathf.Min(lv + 1, 50);
        PlayerPrefs_Manager.Set_Index_Level_Normal(lv);
        yield return Cache.GetWFS(Constant.Time_Fade);
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();
        yield return Cache.GetWFS(Constant.Time_Fade);
        Close();
        //Debug.Log(PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge));

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
            Scene_Manager_Q.Load_Scene("Loading");
        }
        //SceneManager.LoadScene(Constant.StringLevel + indexLevel.ToString(), LoadSceneMode.Single);
    }

    public void RePlay_Challenge_Button()
    {
        SoundManager.Ins.PlayFx(FxID.click);
#if WatchADs
        AdsManager.Instance.WatchRewardedAds(RewardRetry, "video_show_retry");
#else
       RewardRetry();
#endif
    }
    void RewardRetry()
    {
        int level = PlayerPrefs_Manager.Get__QLevel_Challenge();
        Scene_Manager_Q.Load_Scene(Constant.StringChallengeLevel + level.ToString());
        SoundManager.Ins.PlayFx(FxID.click);
    }    
}

