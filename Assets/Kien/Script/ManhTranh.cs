using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManhTranh : MonoBehaviour
{
    public int tranh, manhtranh;
    [SerializeField]
    private GameObject objCollection;
    [SerializeField]
    private Transform trfFragment;


    private void Start()
    {
        Point_In_Floor floor = GetComponentInParent<Point_In_Floor>();
        if (floor != null)
        {
            floor.SetPictureFragment(this);
            floor.Set_Not_Empty();
        }
    }

    public void NhanManhTranh()
    {
        Datacontroller.instance.TakePartDelete(tranh, manhtranh);
    }

    public void FlyToCollection()
    {
        objCollection.SetActive(true);
        trfFragment.DOMove(objCollection.transform.position, 1f).OnComplete(() => 
        {
            trfFragment.DOScale(1.3f, 1f).OnComplete(() => 
            {
                NhanManhTranh();
                SoundManager.Ins.PlayFx(FxID.collect_coin);

                objCollection.SetActive(false);
                gameObject.SetActive(false);
            });
        });
        ;
    }
}
