using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public int damage;
    public DMGMess dmgMess;
    public List<SkeletonAnimation> skeletonAnimations;

//#if UNITY_EDITOR

//    private void OnValidate()
//    {
//        if (!gameObject.activeInHierarchy)
//        {
//            return;
//        }

//        //OnInit();

//        skeletonAnimations.Clear();

//        for (int i = 0; i < transform.childCount; i++)
//        {
//            SkeletonAnimation animation = transform.GetChild(i).GetComponent<SkeletonAnimation>();

//            if (animation != null)
//            {
//                skeletonAnimations.Add(animation);
//            }
//        }

//    }

//#endif

    public void OnInit(int damage)
    {
        this.damage = damage;
        dmgMess.SetDamage(damage);
    }

    public void ChangeAnim(string anim, bool isLoop)
    {
        for (int i = 0; i < skeletonAnimations.Count; i++)
        {
            skeletonAnimations[i].loop = isLoop;
            skeletonAnimations[i].AnimationName = anim;
        }
    }
}
