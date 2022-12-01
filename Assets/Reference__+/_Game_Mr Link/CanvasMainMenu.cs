using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Spine.Unity;

public class CanvasMainMenu : UICanvas
{
    #region Gold_Pink_bank
    public GameObject obj_Full_Gold_Pink_bank;//gold ở bank đầy
    public GameObject obj_Normal_Gold_Pink_bank;//gold ở bank chưa đầy
    public Text txt_Gold_Pink_Bank;
    #endregion
    #region Gold_Gem
    public Text txt_Gold;//gold ở bank đầy
    public Text txt_Gem;//gold ở bank đầy
    #endregion
    public Animator anim_Home;
    private bool isFist_Click;
    //
    public RectTransform rect_gold;
    public RectTransform rect_gem;
    public RectTransform rect_Piggy_Bank;
    //
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;
    public GameObject objQuest;

    private void OnEnable()
    {
        SoundManager.Ins.PlaySound(SoundID.menu);
       // Set_Init_Gold_Pink_bank();
        Set_Reload_Gold_Gem_Title();
        isFist_Click = false;
        UIManager.Ins.OpenUI(UIID.UICFade);
        if (UIManager.Ins.IsOpened(UIID.UICFight_Boss))
        {
            UIManager.Ins.CloseUI(UIID.UICFight_Boss);
            if (GameManager.Ins.enemyBoss_auto_Asign != null)
            {
                Destroy(GameManager.Ins.enemyBoss_auto_Asign.gameObject);

            }
        }
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_In();

        int idSkin = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing();
        Set_Skin(Constant.Get_Skin_Name_By_Id(idSkin));
    }
    #region

    #endregion
    #region

    #endregion
    #region

    #endregion
    #region Challenge
    public void ChallengeButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        anim_Home.SetTrigger(Constant.Trigger_HomeOut);
        UIManager.Ins.OpenUI(UIID.UICChallenge);

        EventController.MAIN_CLICK("icon_challange_click");
    }
    
    public void Set_Anim_In()
    {
        anim_Home.SetTrigger(Constant.Trigger_HomeIn);
    }

    #endregion
    public void QuestButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        objQuest.SetActive(true);
    }

    #region Button Setting
    public void SettingButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        UIManager.Ins.OpenUI(UIID.UICSetting);

        EventController.MAIN_CLICK("main_setting_click");
    }
    #endregion
    #region Button Play
    public void PlayGameButton()
    {
        GameManager.Ins.GMState = GameManager.GameState.Playing;
        SoundManager.Ins.PlayFx(FxID.click);
        //UIManager.Ins.OpenUI(UIID.UICGamePlay);
        //UIManager.Ins.OpenUI(UIID.UICWin_Level);
        if (!isFist_Click)
        {
            isFist_Click = true;
            PlayerPrefs.SetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge, 1);
            StartCoroutine(IE_Delay_Fade_Start_Close());

            EventController.MAIN_CLICK("main_taptoplay_click");
        }
    }

    #endregion
    #region Button CHEST
    public void CHEST_Button()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        UIManager.Ins.OpenUI(UIID.UICChest);
        Close();
    }

    #endregion
    #region Gold Gem Title
    public void Set_Reload_Gold_Gem_Title()
    {
        txt_Gold.text = PlayerPrefs_Manager.Get_Gold().ToString();
        txt_Gem.text = PlayerPrefs_Manager.Get_Gem().ToString();
    }
    #endregion
    

    #region Skin
    public void SkinButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        UIManager.Ins.OpenUI(UIID.UICSkin_Top);
        UIManager.Ins.OpenUI(UIID.UICSkin_Boot);
        Close();

        EventController.MAIN_CLICK("icon_shop_click");
    }
    #endregion

    #region Area
    public void AreaButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);

        PlayerPrefs.SetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge, 2);
        StartCoroutine(LoadScene("Ar_Level_0"));

        EventController.MAIN_CLICK("icon_arena_click");
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
        Close();
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
            //obj_Normal_Gold_Pink_bank.SetActive(false);
        }
        else
        {
            obj_Full_Gold_Pink_bank.SetActive(false);
            //obj_Normal_Gold_Pink_bank.SetActive(true);
            //txt_Gold_Pink_Bank.text = gold_Pink_bank.ToString();
        }
        int idSkin = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing();
        Set_Skin(Constant.Get_Skin_Name_By_Id(idSkin));
    }
    public void Set_Off_Full_Pink_bank()
    {
        obj_Full_Gold_Pink_bank.SetActive(false);
        obj_Normal_Gold_Pink_bank.SetActive(false);
    }
    #endregion
    [ContextMenu("test_Win")]
    public void TTTTTT()
    {
        UIManager.Ins.OpenUI(UIID.UICWin_Level);
        Close();
    }
    [ContextMenu("test_Lose")]
    public void LLLL()
    {
        UIManager.Ins.OpenUI(UIID.UICFail);
        Close();
    }
    public void Set_Fade_And_Close()
    {
        StartCoroutine(IE_Delay_Fade_Close());
    }
    IEnumerator IE_Delay_Fade_Close()
    {
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();
        yield return Cache.GetWFS(Constant.Time_Fade);
        Close();
    }
    IEnumerator IE_Delay_Fade_Start_Close()
    {
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();
        yield return Cache.GetWFS(Constant.Time_Fade);
        Close();
        int indexLevel = PlayerPrefs_Manager.Get_Index_Level_Normal();
        PlayerPrefs_Manager.Set_Index_Level_Normal(indexLevel);

        Scene_Manager_Q.Load_Scene(Constant.StringLevel + indexLevel.ToString());
        //SceneManager.LoadScene(Constant.StringLevel + indexLevel.ToString(), LoadSceneMode.Single);
    }
    #region Base to set Skin, Anim Nhân vật
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
}
