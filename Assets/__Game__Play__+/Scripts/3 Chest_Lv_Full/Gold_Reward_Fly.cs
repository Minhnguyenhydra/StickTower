using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Gold_Reward_Fly : MonoBehaviour
{
    public Transform tf_Gold_Reward_Fly;
    public Text txtnumber_Gold_Reward;
    // Start is called before the first frame update
    void Start()
    {
        
        tf_Gold_Reward_Fly = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set_Fly()
    {
        SoundManager.Ins.PlayFx(FxID.collect_coin);
        if (tf_Gold_Reward_Fly != null)
        {
            tf_Gold_Reward_Fly.DOMoveY(tf_Gold_Reward_Fly.position.y + Constant.Reward_Lv_House_Full_Gold_Off_Set_YPos_Fly_Up, Constant.Reward_Time_Fly_Up);
            tf_Gold_Reward_Fly.DOScale(Constant.Reward_Off_Set_Scale_Fly_Up, Constant.Reward_Time_Fly_Up).OnComplete(() => {
                tf_Gold_Reward_Fly.DOScale(Constant.Reward_Off_Set_Scale_Fly_Up, Constant.Reward_Time_Fly_Up).OnComplete(() => {
                    Destroy(this.gameObject);
                });
            });
        }
    }
}
