using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_In_Floor : MonoBehaviour
{
    [Header("------Not Need Asign--To view------")]
    public bool isEmpty;
    public Transform tf_Point_In_Floor;
    public Enemy enemy_Attack_This_Point;
    public Trap_Hit trap_Hit;
    public X2Damge x2Damge;
    public Sword sword_Attack_This_Point;
    public Princess princess_Attack_This_Point;
    public Reward_At_Point reward_Attack_This_Point;
    //
    public bool isFist_config;
    public Floor floor_This;
    // Start is called before the first frame update
    void Start()
    {
        isEmpty = true;
        tf_Point_In_Floor = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //
    public void Set_Enemy_Attach(Enemy _enemy)
    {
        enemy_Attack_This_Point = _enemy;
    }
    public void Set_Not_Empty()
    {
        isEmpty = false;
    }
    
    public void Set_trap_Hit_Attach(Trap_Hit _trap_Hit)
    {
        trap_Hit = _trap_Hit;
    }
    public void Set_X2Damge_Attach(X2Damge _x2Damge)
    {
        x2Damge = _x2Damge;
    }
    public void Set_Sword_Attach(Sword _sword)
    {
        sword_Attack_This_Point = _sword;
    }
    public void Set_Princess_Attach(Princess _princess)
    {
        princess_Attack_This_Point = _princess;
    }
    public void Set_Reward_Attach(Reward_At_Point _reward)
    {
        reward_Attack_This_Point = _reward;
    }
    //***********************************************************
    public void ReSet_Enemy_Attach()
    {
        enemy_Attack_This_Point = null;
    }
    public void ReSet_trap_Hit_Attach()
    {
        trap_Hit = null;
    }
    public void ReSet_Sword_Attach()
    {
        sword_Attack_This_Point = null;
    }
    public void ReSet_Princess_Attach()
    {
        princess_Attack_This_Point = null;
    }
    public void ReSet_reward_Attach()
    {
        reward_Attack_This_Point = null;
    }
    public void ReSet_X2Attach()
    {
        x2Damge = null;
    }
   
}
/*
public void Set_ConFig_Floor_One_Time()
    {
        if (!isFist_config)
        {
            isFist_config = true;
            floor_This = GetComponentInParent<Floor>();
            floor_This.list_Point_In_Floor.Add(this);
        }
    }
 */