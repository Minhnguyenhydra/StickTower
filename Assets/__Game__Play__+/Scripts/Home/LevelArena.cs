using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArena : MonoBehaviour
{
    public List<EnemyGroup> enemyGroups;

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        enemyGroups.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            EnemyGroup animation = transform.GetChild(i).GetComponent<EnemyGroup>();

            if (animation != null)
            {
                enemyGroups.Add(animation);
            }
        }

    }

#endif

    public void ChangeAnim(int index, string anim,bool isLoop)
    {
        if (enemyGroups.Count > index)
            enemyGroups[index].ChangeAnim(anim, isLoop);
    }

    internal void OnInit(ArenaData data)
    {
        enemyGroups[0].OnInit(data.damage_1);
        enemyGroups[1].OnInit(data.damage_2);
        enemyGroups[2].OnInit(data.damage_3);
        enemyGroups[3].OnInit(data.damage_4);
    }
}
