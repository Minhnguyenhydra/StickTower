using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using DG.Tweening;
public class X2Damge : MonoBehaviour
{
    public int health;
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
        Set_Spawn_Health_Bar();
        //Phải gắn Enemy vào Point trước
        if (GetComponentInParent<Point_In_Floor>() != null)
        {
            GetComponentInParent<Point_In_Floor>().Set_X2Damge_Attach(this);
            
        }
        SetCharacterState_Loop(Action_Idle);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFist_config)
        {
            isFist_config = true;
            GetComponentInParent<Point_In_Floor>().Set_Not_Empty();
        }

    }
    public void Set_Spawn_Health_Bar()
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load(Constant.Path_Frefab_Health_Bar_Blue), tf_TrapHit);
        health_Bar = obj.GetComponent<Health_Bar>();

        health_Bar.tf_Health_Bar.localPosition = Constant.Enemy_Local_Pos_Health_Bar_Normal;
        health_Bar.Set_X2Damage_Imedetly(health);
    }
    public void Set_Destroy_This_Trap()
    {
        SetCharacterState_NoLoop(Action_Close);
        Destroy(health_Bar.gameObject);
    }

    public int Get_Health()
    {
        return health;
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
