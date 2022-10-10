using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent_3_Chest : MonoBehaviour
{
    public List<Reward_Lv_Full> list_3_chest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set_Open_3_Chest()
    {
        StartCoroutine(IE_Delay_Open_3_Chest());
    }
    IEnumerator IE_Delay_Open_3_Chest()
    {
        if (list_3_chest[0] != null)
        {
            list_3_chest[0].Set_Open();
        }
        yield return Cache.GetWFS(0.1f);
        if (list_3_chest[1] != null)
        {
            list_3_chest[1].Set_Open();
        }
        yield return Cache.GetWFS(0.1f);
        if (list_3_chest[2] != null)
        {
            list_3_chest[2].Set_Open();
        }
    }
}
