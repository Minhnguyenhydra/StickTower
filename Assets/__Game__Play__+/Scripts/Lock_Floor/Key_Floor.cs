using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Key_Floor : MonoBehaviour
{
    public Transform tf;
    public SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
        spr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("TEEE")]
    public void Set_Move_To_Lock()
    {
        int iidenx = Lock_Floor_Manager.Ins.list_Key_Floor.IndexOf(this);
        Lock_Floor lockFloor = Lock_Floor_Manager.Ins.list_LockFloor[iidenx];
        tf.DOMove(lockFloor.tf.position, 0.8f).SetEase(Ease.InOutQuart).OnComplete(()=> {
            SetFade();
            lockFloor.Set_Open_This_Lock();
        });
    }
    public void SetFade()
    {
        spr.DOFade(0, 0.5f);
    }
}
