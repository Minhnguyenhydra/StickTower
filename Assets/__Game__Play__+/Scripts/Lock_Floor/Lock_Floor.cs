using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using DG.Tweening;
public class Lock_Floor : MonoBehaviour
{
    public Transform tf;
    public Floor _floor_This_Lock;
    //
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;

    public AnimationReferenceAsset Action_Idle;
    public AnimationReferenceAsset Action_Close;


    [Header("------Not Need Asign--To view------")]
    public Transform tf_TrapHit;
    public Health_Bar health_Bar;
    public bool isFist_config;
    // Start is called before the first frame update
    void Start()
    {
        isFist_config = false;
        tf_TrapHit = transform;
        SetCharacterState_Loop(Action_Idle);
    }
    
    public void Set_Open_This_Lock()
    {
        SetCharacterState_NoLoop(Action_Close);
        _floor_This_Lock.isLocking = false;
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
