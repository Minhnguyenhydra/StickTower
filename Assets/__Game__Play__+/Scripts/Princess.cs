using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class Princess : MonoBehaviour
{
    public bool isBossLass;
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;

    public AnimationReferenceAsset Action_Idle;
    public AnimationReferenceAsset Action_Win;

    ////[Header("------Enemy cuối mới cần điền floor_This------")]
    [Header("------Not Need Asign--To view------")]
    public Floor floor_This;
    public Transform tf_Reward;
    public bool isFist_config;

    //[Header("------Not Need Asign--To view------")]
    [Header("------Not Need Asign--To view------")]
    public Transform tf_Princess;
    public Health_Bar health_Bar;
    // Start is called before the first frame update
    void Start()
    {
        tf_Princess = transform;
        //Phải gắn Enemy vào Point trước
        if (GetComponentInParent<Point_In_Floor>() != null)
        {
            GetComponentInParent<Point_In_Floor>().Set_Princess_Attach(this);
        }
        //
        isFist_config = false;

        Set_Idle();
    }

    private void Update()
    {
        Set_ConFig_Floor_One_Time();
    }
    //**********************************************************
    public void Set_Victory()
    {
        SetCharacterState_Loop(Action_Win);
    }
    public void Set_Idle()
    {
        SetCharacterState_Loop(Action_Idle);
    }
    //**********************************************************
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
    //**********************************************************
    public void Set_ConFig_Floor_One_Time()
    {
        if (!isFist_config)
        {
            isFist_config = true;
            if (GetComponentInParent<Point_In_Floor>() != null)
            {
                GetComponentInParent<Point_In_Floor>().Set_Princess_Attach(this);
                GetComponentInParent<Point_In_Floor>().Set_Not_Empty();
            }
        }
    }
}
