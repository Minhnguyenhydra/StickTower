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
    bool isFist_Click;
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;
    //
    #region Gold_Gem,level
    public Text txt_Gold;//gold ở bank đầy
    public Text txt_Gem;//gold ở bank đầy
    public Text txt_Level;//gold ở bank đầy
    #endregion
    private void OnEnable()
    {
        SoundManager.Ins.PlayFx(FxID.lose);
        Set_Init_Gold_Gem_Level_Title();
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


        if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge()==1|| PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 3)
        {
            txt_Level.text = "LEVEL " + (PlayerPrefs_Manager.Get_Index_Level_Normal()).ToString();

        }else if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 2)
        {
            txt_Level.text = "LEVEL " + (PlayerPrefs.GetInt(UserData.Key_LevelArena)).ToString();
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
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        UIManager.Ins.CloseUI(UIID.UICSkin_Boot);
        Close();

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
    public void Set_Ads_To_Pass_Level()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        if (!isFist_Click)
        {
            isFist_Click = true;
            Set_Fade_And_ADs_Close();
        }
            
    }
    public void Set_No_Thank()
    {
        SoundManager.Ins.PlayFx(FxID.click);
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
            int indexLevel = PlayerPrefs_Manager.Get_Index_Level_Normal();
            Scene_Manager_Q.Load_Scene(Constant.StringLevel + indexLevel.ToString());

        }
        //SceneManager.LoadScene(Constant.StringLevel + indexLevel.ToString(), LoadSceneMode.Single);
    }
    IEnumerator IE_Delay_Fade_ADs_Close()
    {
        yield return Cache.GetWFS(Constant.Time_Fade);
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();
        yield return Cache.GetWFS(Constant.Time_Fade);
        Close();
        Debug.Log(PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge));
        if (PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge) == 1)
        {
            int indexLevel = PlayerPrefs_Manager.Get_Index_Level_Normal() + 1;
            Scene_Manager_Q.Load_Scene(Constant.StringLevel + indexLevel.ToString());
        }
        else if (PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge) == 2)
        {
            Scene_Manager_Q.Load_Scene("Ar_Level_0");
        }
        else if (PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge) == 3)
        {
            int indexLevel = PlayerPrefs_Manager.Get_Index_Level_Normal();
            Scene_Manager_Q.Load_Scene(Constant.StringLevel + indexLevel.ToString());
        }
        //SceneManager.LoadScene(Constant.StringLevel + indexLevel.ToString(), LoadSceneMode.Single);
    }
}
