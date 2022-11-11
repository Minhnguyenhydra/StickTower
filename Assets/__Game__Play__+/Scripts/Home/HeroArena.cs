using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroArena : MonoBehaviour
{
    public DMGMess dmgMess;
    public SkeletonAnimation animation;
    public bool isPlayer = false;

    public void OnInit(int level, int damageBase)
    {
        SetAciveDMGMess(true);
        gameObject.SetActive(level > 0);
        dmgMess.SetDamage(damageBase * level);
    }

    public void ChangeAnim(string anim, bool isLoop)
    {
        if (isPlayer)
        {
            animation.GetComponent<MeshRenderer>().sortingOrder = 30;
        }

        if (isPlayer && anim == "attack" && Random.Range(0,100) < 30)
        {
            int i = Random.Range(0, 3);
            int index_hero = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing()+1;
            //Debug.Log(index_hero);
            switch (i)
            {
                case 0:
                    anim = "Hero"+ index_hero.ToString()+ "_skill_1";
                    break;
                case 1:
                    anim = "Hero" + index_hero.ToString() + "_skill_2";
                    break;
                case 2:
                    anim = "Hero" + index_hero.ToString() + "_skill_3";
                    break;
                default:
                    break;
            }

            animation.GetComponent<MeshRenderer>().sortingOrder = 100;
        }
        animation.loop = isLoop;
        animation.AnimationName = anim;
    }

    public void SetAciveDMGMess(bool active)
    {
        dmgMess.gameObject.SetActive(active); 
    }
}
