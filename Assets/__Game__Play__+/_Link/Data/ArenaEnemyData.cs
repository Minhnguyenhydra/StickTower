using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ArenaEnemyData", menuName = "ScriptableObjects/ArenaEnemyData", order = 1)]
public class ArenaEnemyData : ScriptableObject
{
    public List<ArenaData> arenaDatas;

    public void OnInit()
    {
        int[] arr = new int[1000];
        arr[0] = 11;
        for (int i = 1; i < 1000; i++)
        {
            arr[i] = arr[i - 1] + RandomDamage((i + 4) / 4);
        }

        for (int i = 0; i < 200; i+= 4)
        {
            ArenaData data = new ArenaData();
            data.damage_1 = arr[i];
            data.damage_2 = arr[i + 1];
            data.damage_3 = arr[i + 2];
            data.damage_4 = arr[i + 3];

            arenaDatas.Add(data);
        }
    }

    private int RandomDamage(int level)
    {
        if (level <= 10)
        {
            return Random.Range(6, 10);
        }
        else
        if (level <= 20)
        {
            return Random.Range(9, 13);
        }
        else
        if (level <= 30)
        {
            return Random.Range(9, 13);
        }
        else
        if (level <= 40)
        {
            return Random.Range(12, 16);
        }
        else
        {
            return Random.Range(16, 20);
        }
    }
}

[System.Serializable]
public class ArenaData
{
    public int damage_1;
    public int damage_2;
    public int damage_3;
    public int damage_4;
}