using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArena : MonoBehaviour
{
    public UserData userData;

    public List<LevelArena> levelArenaDatas;

    public LevelArena levelArena;

    public Transform targetPoint;
    public Transform initPoint;

    public EnemyGroup EnemyGroup => levelArena.enemyGroups[index];

    public int Damage => EnemyGroup.damage;

    int index;

    public ArenaEnemyData enemyData;

    private void Start()
    {
        enemyData.OnInit();
    }

    internal void OnInit()
    {
        //load level
        if (levelArena != null)
        {
            Destroy(levelArena.gameObject);
        }

        levelArena = Instantiate(levelArenaDatas[userData.levelArena], transform);
        levelArena.OnInit(enemyData.arenaDatas[userData.levelArena]);
        InitStage(0);
    }

    //bat dau tu 0
    private void InitStage(int index)
    {
        this.index = index;

        if (index > 0)
        {
            Destroy(levelArena.enemyGroups[index - 1].gameObject);
        }

        if (index < levelArena.enemyGroups.Count)
        {
            levelArena.enemyGroups[index].transform.position = initPoint.position;
         
            ChangeAnim(Init_Area.AnimName.idle.ToString(), true);
        }
    }

    public void NextStage()
    {
        InitStage(index + 1);
    }

    public void ChangeAnim(string anim, bool isLoop)
    {
        levelArena.ChangeAnim(index, anim, isLoop);
    }

    internal bool ContainStage()
    {
        return index < levelArena.enemyGroups.Count;
    }
}
