using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//Do nếu để cùng Canvas tổng thì Canvas Pig sẽ có oder layer bằng với Canvas tổng, mà Skeleton muốn gắn lên Canvas tổng phải có Oder layer cao hơn, do đó các Skeleton ko liên quan sẽ bị nổi lên ở Canvas Pink Bank này, do đó phải gắn canvas Pig Bank này ở canvas riêng có chỉ số Oder cao hơn
public class CanvasTryReplay : UICanvas
{
    public Animator anim;

    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        
    }
    public void PlayButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        Scene_Manager_Q.Load_Scene(Constant.Get_Scene_Name_NormalBy_Level(PlayerPrefs_Manager.Get_Index_Level_Normal()));
        PlayerPrefs_Manager.Set_Index_Level_Normal(PlayerPrefs_Manager.Get_Index_Level_Normal());
    }
    //
    public void NextLevelButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        Scene_Manager_Q.Load_Scene(Constant.Get_Scene_Name_NormalBy_Level(PlayerPrefs_Manager.Get_Index_Level_Normal()));
        PlayerPrefs_Manager.Set_Index_Level_Normal(PlayerPrefs_Manager.Get_Index_Level_Normal() + 1);
    }
    public void CloseButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        StartCoroutine(IE_DelayClose());
    }
    IEnumerator IE_DelayClose()
    {
        anim.SetTrigger(Constant.Trigger_PigBankClose);
        yield return Cache.GetWFS(Constant.Time_Delay_PigBank_Close);
        Close();
        Player.ins.is_Block_Raycas = false;//
    }
    //
}
