using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DG.Tweening;
public class Enemy_Ar : MonoBehaviour
{
    public enum AnimName { attack, idle, hit, die, lose, run, victory }

    public List<EnemyGroup> enemyGroups;
    public EnemyGroup enemyGroup;

    public UserData userData;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        //khoi tao data player va hero
        //TODO: change main hero
        ChangeAnim(AnimName.idle.ToString(), true);
    }

    public void ChangeAnim(string anim, bool isLoop)
    {
        enemyGroup.ChangeAnim(anim, isLoop);
    }


}
