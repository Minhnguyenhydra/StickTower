using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManhTranh : MonoBehaviour
{
    public int tranh, manhtranh;
    [SerializeField]
    Point_In_Floor floor;

    private void Start()
    {
        floor = GetComponentInParent<Point_In_Floor>();
        if (floor != null)
        {
            floor.SetPictureFragment(this);
            floor.Set_Not_Empty();
        }
    }

    public void NhanManhTranh()
    {
        Datacontroller.instance.TakePartDelete(tranh, manhtranh);
        this.PostEvent(QuestManager.QuestID.Quest10, 1);
        this.PostEvent(QuestManager.QuestID.Quest11, 1);
    }

    public void FlyToCollection()
    {
        Debug.LogError("======= an manh tranh");
       // CanvasGamePlay.instance.targetPartDelete.SetActive(true);
        transform.DOMove(CanvasGamePlay.instance.targetPartDelete.transform.position, 1f).OnComplete(() => 
        {
            transform.DOScale(1.3f, 1f).OnComplete(() => 
            {
                NhanManhTranh();
                SoundManager.Ins.PlayFx(FxID.collect_coin);

              //  CanvasGamePlay.instance.targetPartDelete.SetActive(false);
                gameObject.SetActive(false);
            });
        });
        ;
    }
}
