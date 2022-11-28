using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManhTranh : MonoBehaviour
{
    public int tranh, manhtranh;
    public void NhanManhTranh()
    {
        Datacontroller.instance.TakePartDelete(tranh, manhtranh);
    }
}
