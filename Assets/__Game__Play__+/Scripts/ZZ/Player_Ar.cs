using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DG.Tweening;
public class Player_Ar : MonoBehaviour
{
    public static Player_Ar ins;
    public Transform tf_Player_Ar;
    public Transform tf_Target;
    public int int_damge;
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;

    public AnimationReferenceAsset Action_Idle;
    public AnimationReferenceAsset Action_Run;
    public AnimationReferenceAsset Action_Attack;
    public AnimationReferenceAsset Action_Lose;
    public AnimationReferenceAsset Action_Win;
    // Start is called before the first frame update
    void Start()
    {
        if (ins == null)
        {
            ins = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Anim
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

    public void Set_Loop_Anim(AnimationReferenceAsset _anim)//Set loop
    {
        SetAnimation(_anim, true, 1f);
    }
    public void Set_No_Loop_Anim(AnimationReferenceAsset _anim)//Set loop
    {
        SetAnimation(_anim, false, 1f);
    }

    public void ReSetCharacter_Anim()
    {
        skeletonAnimation.ClearState();
    }
    #endregion
    #region
    public void Set_Idle()
    {
        Set_Loop_Anim(Action_Idle);
    }
    public void Set_Run()
    {
        Set_Loop_Anim(Action_Run);
    }
    public void Set_Attack()
    {
        Set_Loop_Anim(Action_Attack);
    }
    public void Set_Lose()
    {
        Set_Loop_Anim(Action_Lose);
    }
    public void Set_Win()
    {
        Set_Loop_Anim(Action_Win);
    }
    #endregion
    #region

    #endregion
    #region

    #endregion
    #region

    #endregion
    #region

    #endregion
    #region

    #endregion
    #region

    #endregion



}
