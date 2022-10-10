using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
//Rương nếu Level có ở cuối màn
public class Reward_At_Point : MonoBehaviour
{
    public Gold_Reward_Fly gold_Reward_Fly;
    public int number_Gold_Reward;
    public bool isBossLass;
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;

    public AnimationReferenceAsset Action_Idle;
    public AnimationReferenceAsset Action_Open;

    ////[Header("------Enemy cuối mới cần điền floor_This------")]
    [Header("------Not Need Asign--To view------")]
    public Floor floor_This;
    public Transform tf_Reward;
    public bool isFist_config;
    
    //[Header("------Not Need Asign--To view------")]

    // Start is called before the first frame update
    void Start()
    {
        isFist_config = false;
        gold_Reward_Fly.tf_Gold_Reward_Fly.localScale = Vector3.zero;
        gold_Reward_Fly.txtnumber_Gold_Reward.text = "+" + number_Gold_Reward.ToString();
        Set_Idle();
    }

    // Update is called once per frame
    void Update()
    {
        Set_ConFig_Floor_One_Time();
    }
    //**********************************************************
    public void Set_Open()
    {
        //gold_Reward_Fly.Set_Fly();
        StartCoroutine(IE_Delay_Set_Open());
        SetCharacterState_NoLoop(Action_Open);
    }
    IEnumerator IE_Delay_Set_Open()
    {
        yield return Cache.GetWFS(0.25f);
        gold_Reward_Fly.Set_Fly();
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
                GetComponentInParent<Point_In_Floor>().Set_Reward_Attach(this);
                GetComponentInParent<Point_In_Floor>().Set_Not_Empty();
            }
        }
    }

}
