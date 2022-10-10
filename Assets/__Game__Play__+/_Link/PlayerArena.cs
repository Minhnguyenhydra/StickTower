using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerArena : MonoBehaviour
{

    public List<HeroArena> heroArenas;
    public HeroArena mainHero;

    public UserData userData;

    public HeroArenaData heroArenaData;
    public Transform targetPoint;
    public Transform initPoint;

    int totalDamage = 0;
    public int Damage => totalDamage;

    public SkeletonAnimation skeletonAnimation;

    public void OnInit()
    {
        //khoi tao data player va hero
        //change main hero

        int id_Skin = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing();
        string name_Skin = Constant.Get_Skin_Name_By_Id(id_Skin);
        Set_Skin(name_Skin);

        //
        transform.position = initPoint.position;

        totalDamage = heroArenaData.dmgBase;

        for (int i = 0; i < heroArenas.Count; i++)
        {
            int level = userData.GetDataState(UserData.Keys_HeroLevelArena, i, 0);
            int damageBase = heroArenaData.GetDamageBase(i);
            totalDamage += damageBase * level;
            heroArenas[i].OnInit(level, damageBase);
        }

        mainHero.OnInit(1, totalDamage);
        ChangeAnim(Init_Area.AnimName.idle.ToString(), true);


    }

    public void ChangeAnim(string anim, bool isLoop)
    {
        for (int i = 0; i < heroArenas.Count; i++)
        {
            heroArenas[i].ChangeAnim(anim, isLoop);
        }

        mainHero.ChangeAnim(anim, isLoop);
    }

    public void Set_Skin(string _str_Skin)
    {
        skeletonAnimation.Skeleton.SetSkin(_str_Skin);
        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
        skeletonAnimation.LateUpdate();
    }

    public void SetAciveDMGMess(bool active)
    {
        for (int i = 0; i < heroArenas.Count; i++)
        {
            heroArenas[i].SetAciveDMGMess(active);
        }
        //mainHero.SetAciveDMGMess(active);
    }

}
