using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroArenaData", menuName = "ScriptableObjects/HeroArenaData", order = 1)]
public class HeroArenaData : ScriptableObject
{
    public List<HeroDataBase> heroDataBases;

    public int dmgBase;

    public int GetDamageBase(int index)
    {
        return heroDataBases[index].dmgBase;
    } 
    
    public int GetCoinBase(int index)
    {
        return heroDataBases[index].coin;
    }
}

[System.Serializable]
public struct HeroDataBase
{
    public int dmgBase;
    public int coin;
}
