using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;
public class Model_Hero_Main_Top : MonoBehaviour
{
    #region Khai báo biến
    public static Model_Hero_Main_Top ins;
    //
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset Action_Victory;
    public AnimationReferenceAsset IdleAnim;

    private Coroutine corIdleAnim;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        if (ins == null)
        {
            ins = this;
        }
    }
    private void OnEnable()
    {
        Init_Model();
    }
    public void Init_Model()
    {
        int i = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing();
        string str_Skin_Name = Constant.Get_Skin_Name_By_Id(i);
        Set_Skin_Action_Jump(str_Skin_Name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set_Skin_Action_Jump(string _str_Skin_Name)
    {
        Set_Skin(_str_Skin_Name);
        ReSetCharacterState();
        SetCharacterState_NoLoop(Action_Victory);

        if (corIdleAnim != null)
            StopCoroutine(corIdleAnim);

        corIdleAnim = StartCoroutine(SwitchToIdleAnim());
    }

    private IEnumerator SwitchToIdleAnim()
    {
        yield return new WaitForSeconds(Action_Victory.Animation.Duration);
        SetCharacterState_Loop(IdleAnim);
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
