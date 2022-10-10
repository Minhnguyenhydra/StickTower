using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Item_Shop_Prize : MonoBehaviour
{
    public Gold_Gem_Reward_Fly gold_Gem_Reward_Fly;
    public int number_Gold_Reward;
    public int number_Gem_Reward;
    public GameObject obj_Btn;
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;

    public AnimationReferenceAsset Action_Idle;
    public AnimationReferenceAsset Action_Open;
    [Header("---Not---Need_---Asight")]
    public CanvasShop_Prize canvasShop_Prize;
    // Start is called before the first frame update
    void Start()
    {
        canvasShop_Prize = GetComponentInParent<CanvasShop_Prize>();
        //gold_Gem_Reward_Fly.rect_Gold_Reward_Fly.localScale = Vector3.zero;
        gold_Gem_Reward_Fly.Set_Gold_Gem(number_Gold_Reward, number_Gem_Reward);
        Set_Idle();
        canvasShop_Prize.e_Event_Close.AddListener(Set_Off_Gold_Fly);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set_Open()
    {
        if (canvasShop_Prize.intKey > 0)
        {
            gold_Gem_Reward_Fly.Set_Fly();
            SetCharacterState_NoLoop(Action_Open);
            Destroy(obj_Btn);
        }
        else
        {
            //Sound 
            
            
        }
            canvasShop_Prize.Set_Open_1_Key();
    }
    public void Set_Off_Gold_Fly()
    {
        gold_Gem_Reward_Fly.gameObject.SetActive(false);
    }
    public void Set_Idle()
    {
        SetCharacterState_Loop(Action_Idle);
    }
    //TODO: đổi skin
    //**********************************************************
    //Anim
    public void SetAnimation(AnimationReferenceAsset _anim, bool _loop, float _time_Scale)//Set No loop
    {
        skeletonAnimation.state.SetAnimation(0, _anim, _loop).TimeScale = _time_Scale;
    }
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
}
