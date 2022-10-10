using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int health;
    public int id_Sword;
    [Header("------Not Need Asign--To view------")]
    public Transform tf_Sword;
    public Health_Bar health_Bar;
    public bool isFist_config;
    // Start is called before the first frame update
    void Start()
    {
        tf_Sword = transform;
        Set_Spawn_Health_Bar();
        //Phải gắn Enemy vào Point trước
        if (GetComponentInParent<Point_In_Floor>() != null)
        {
            GetComponentInParent<Point_In_Floor>().Set_Sword_Attach(this);
            GetComponentInParent<Point_In_Floor>().Set_Not_Empty();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFist_config)
        {
            isFist_config = true;
            GetComponentInParent<Point_In_Floor>().Set_Not_Empty();
        }
    }
    public void Set_Spawn_Health_Bar()
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load(Constant.Path_Frefab_Health_Bar_Blue), tf_Sword);
        health_Bar = obj.GetComponent<Health_Bar>();

        health_Bar.tf_Health_Bar.localPosition = Constant.Enemy_Local_Pos_Health_Bar_Normal;
        health_Bar.Set_Damage_Sword_Imedetly(health);
    }
    public void Set_Destroy_This_Sword()
    {
        Destroy(this.gameObject);
    }
    public string Get_Skin_Name_By_Id_Sword()
    {
       return  Constant.Get_Skin_Name_By_Id_Sword(id_Sword);
    }
    public int Get_Health()
    {
        return health;
    }
}
