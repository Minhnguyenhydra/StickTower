using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrent_Castle : MonoBehaviour
{
    // Mỗi Parrent_Castle có 1 hoặc nhiều Castle con, tùy vào Level có mấy nhà địch
    public List<Castle> list_Castle;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Set_Chua_Chiem_Duoc()
    {
        for (int i = 0; i < list_Castle.Count; i++)
        {
            list_Castle[i].gameObject.SetActive(true);
            list_Castle[i].Set_Chua_Chiem_Duoc();
        }
    }
}
