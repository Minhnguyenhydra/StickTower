using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine;
using Spine.Unity;
//Do nếu để cùng Canvas tổng thì Canvas Pig sẽ có oder layer bằng với Canvas tổng, mà Skeleton muốn gắn lên Canvas tổng phải có Oder layer cao hơn, do đó các Skeleton ko liên quan sẽ bị nổi lên ở Canvas Pink Bank này, do đó phải gắn canvas Pig Bank này ở canvas riêng có chỉ số Oder cao hơn
public class CanvasFreeSkin : UICanvas
{
    public int idSkin;
    public GameObject obj_Btn_Get;
    public bool isGet;
    public Transform tf_Spawn_Fire_Work;
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;

    private void Awake()
    {
        isGet = false;
    }
    private void Start()
    {

    }
    private void OnEnable()
    {
        idSkin = Constant.Get_Id_Skin_Free_By_Level(PlayerPrefs_Manager.Get_Index_Level_Normal());

        string nameSkin = Constant.Get_Skin_Name_By_Id(idSkin);
        Set_Skin(nameSkin);
    }

    //
    public void GetButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);

#if WatchADs
        AdsManager.Instance.WatchRewardedAds(TakeFreeSkin, "video_show_freeskin");
#else
        TakeFreeSkin();
#endif
    }

    private void TakeFreeSkin()
    {
        isGet = true;
        Change_Hero();
        CloseButton();

        this.PostEvent(QuestManager.QuestID.Quest09, 1);
    }

    public void CloseButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        StartCoroutine(IE_DelayClose());
    }
    IEnumerator IE_DelayClose()
    {
        if (isGet)
        {
            GameManager.Ins.Set_Spawn_FireWord(tf_Spawn_Fire_Work);
        }
        yield return Cache.GetWFS(Constant.Time_Delay_PigBank_Close);



        ((CanvasWinQ)UIManager.Ins.GetUI(UIID.UICWin_Level)).Set_Gold_EFX();


        Close();
    }
    //
    public void Change_Hero()
    {
        string trSkin = Constant.Get_Skin_Name_By_Id(idSkin);
        ((CanvasWinQ)UIManager.Ins.GetUI(UIID.UICWin_Level)).Set_Skin(trSkin);
        PlayerPrefs_Manager.Set_ID_Name_Skin_Wearing(idSkin);
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
}
