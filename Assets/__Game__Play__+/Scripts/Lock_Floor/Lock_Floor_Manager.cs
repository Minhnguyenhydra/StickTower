using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock_Floor_Manager : Singleton<Lock_Floor_Manager>
{
    [Tooltip("Điền Floor có index ứng với Key")]
    public List<Lock_Floor> list_LockFloor;
    public List<Key_Floor> list_Key_Floor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
