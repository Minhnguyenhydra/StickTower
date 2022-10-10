using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public GameObject obj_Castle_Full;
    public GameObject obj_Castle_Line;
    private void Awake()
    {
        Set_Chua_Chiem_Duoc();
    }
    // Start is called before the first frame update
    void Start()
    {
        obj_Castle_Full.SetActive(false);
        obj_Castle_Line.SetActive(true);
    }
    //Castle đầu tiên trong hình bao h cũng có animation to lên lần đầu tiên chiếm được
    public void Set_Chiem_Duoc()//bật sáng mỗi lần chiếm được 1 nhà
    {
        obj_Castle_Full.SetActive(true);
        obj_Castle_Line.SetActive(true);
    }
    public void Set_Chua_Chiem_Duoc()//bật sáng mỗi lần chiếm được 1 nhà
    {
        obj_Castle_Full.SetActive(false);
        obj_Castle_Line.SetActive(true);
    }
    public void Set_Off_All_Castle()//bật sáng mỗi lần chiếm được 1 nhà
    {
        obj_Castle_Full.SetActive(false);
        obj_Castle_Line.SetActive(false);
    }
}
