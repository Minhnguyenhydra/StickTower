using UnityEngine;
[CreateAssetMenu(fileName = "Skin_Item_SO", menuName = "ScriptableObjects/Skin_Item_SO")]

public class Skin_Item_SO : ScriptableObject
{
    public int iD;
    public int gold;
    public int gem;
    public int level_UnLock;
    public bool watch_ADs;
    public int numberAdsNeed;
    public Enum_State_Item_Skin Enum_State_Item_Skin;
}
/*
 
public float timeOutAnimation;
public GameObject prefabs;
public int price;
//public WeaponSO weaponSO;
public Sprite icon_UnLock;
    public Sprite icon_Lock;
public float timePointFireAnimation;
public string nameObj;
 */