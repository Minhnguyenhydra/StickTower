using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
//Do nếu để cùng Canvas tổng thì Canvas Pig sẽ có oder layer bằng với Canvas tổng, mà Skeleton muốn gắn lên Canvas tổng phải có Oder layer cao hơn, do đó các Skeleton ko liên quan sẽ bị nổi lên ở Canvas Pink Bank này, do đó phải gắn canvas Pig Bank này ở canvas riêng có chỉ số Oder cao hơn
public class CanvasChallenge : UICanvas
{
    public Animator anim_Challenge;
    public ChallengeControll challengeControll;
    public UnityEvent e_Event_Close;
    public GameObject obj_No_Reach;
    public GameObject obj_Buy;
    public GameObject obj_RePlay;
    private void Start()
    {
        challengeControll = transform.GetChild(1).gameObject.GetComponentInChildren<ChallengeControll>();//vì n ở vị trí child thứ 2
    }


    public void Play_Button()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        PlayerPrefs_Manager.Set_Key_1GamPlay_Or_2Area_Or_3Challenge(3);
        //UNDO: làm vội chưa làm hết các logic level
        int level = PlayerPrefs_Manager.Get_Index_Level_Normal();
        if (true)
        {
            if (level >= 22)
            {
                Scene_Manager_Q.Load_Scene("Challenge_Level_22");
                
            }
            else
            {

            }
        }
        else if (ChallengeControll.ins.ii == 1)
        {

        }

        else if (ChallengeControll.ins.ii == 2)
        {

        }
        else if (ChallengeControll.ins.ii == 3)
        {

        }
        else if (ChallengeControll.ins.ii == 4)
        {

        }
        else if (ChallengeControll.ins.ii == 5)
        {

        }
        
    }
    public void Buy_Btn()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        PlayerPrefs_Manager.Set_Gold(PlayerPrefs_Manager.Get_Gold() - 100);
        PlayerPrefs_Manager.Set_Replay_Level(22);
        Set_Btn_On(Enum_Type_Btn_Challenge.Replay);
    }
    public void Set_Btn_On(Enum_Type_Btn_Challenge ee)
    {
        if (ee == Enum_Type_Btn_Challenge.No_Reach_Level)
        {
            obj_No_Reach.SetActive(true);
            obj_Buy.SetActive(false);
            obj_RePlay.SetActive(false);
        }
        else if (ee == Enum_Type_Btn_Challenge.Buy)
        {
            obj_No_Reach.SetActive(false);
            obj_Buy.SetActive(true);
            obj_RePlay.SetActive(false);

        }
        else if(ee == Enum_Type_Btn_Challenge.Replay)
        {
            obj_No_Reach.SetActive(false);
            obj_Buy.SetActive(false);
            obj_RePlay.SetActive(true);

        }
    }
    public void CloseButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        anim_Challenge.SetTrigger(Constant.Trigger_Challenge_Downt);
        ((CanvasMainMenu)UIManager.Ins.GetUI(UIID.UICMainMenu)).Set_Anim_In();

        anim_Challenge.SetTrigger(Constant.Trigger_Challenge_Downt);
        StartCoroutine(IE_Delay_Downt());
    }
    IEnumerator IE_Delay_Downt()
    {
        yield return Cache.GetWFS(Constant.Time_Delay_Challenge_Close);
        Close();
    }
}
