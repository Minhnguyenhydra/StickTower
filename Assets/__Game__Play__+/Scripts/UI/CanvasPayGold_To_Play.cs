using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
//Do nếu để cùng Canvas tổng thì Canvas Pig sẽ có oder layer bằng với Canvas tổng, mà Skeleton muốn gắn lên Canvas tổng phải có Oder layer cao hơn, do đó các Skeleton ko liên quan sẽ bị nổi lên ở Canvas Pink Bank này, do đó phải gắn canvas Pig Bank này ở canvas riêng có chỉ số Oder cao hơn
public class CanvasPayGold_To_Play : UICanvas
{
    public Text txtGoldReward;
    public Text txtGemReward;
    public Text txtPAY_100_GOLD;
    public GameObject obj_RewardArea;//Khối viết chữ gold, gem nhận được.. có level có.. có level ko, đây đang gộp lại
    public Animator anim;

    private int[] rewards = new int[] {100, 1};

    private void OnEnable()
    {
        int level = PlayerPrefs_Manager.Get_Index_Level_Normal();
        //0:Gold .... 1 Gem
        rewards = Constant.Get_Reward_Gold_Gem_By_Pay_Gold(level);
        txtGoldReward.text = rewards[0].ToString();
        txtGemReward.text = rewards[1].ToString();

        obj_RewardArea.SetActive(false);
    }

    //
    public void PlayButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);

#if WatchADs
        AdsManager.Instance.WatchRewardedAds(CloseButton);
#else
        CloseButton();
#endif
    }

    private void PayToPlay()
    {
        int _gold_current = PlayerPrefs_Manager.Get_Gold();
        _gold_current = Mathf.Clamp(_gold_current - rewards[0], 0, int.MaxValue);
        PlayerPrefs_Manager.Set_Gold(_gold_current);
        CloseButton();
    }

    public void NextLevelButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        PlayerPrefs_Manager.Set_Index_Level_Normal(PlayerPrefs_Manager.Get_Index_Level_Normal() + 1);
        Scene_Manager_Q.Load_Scene(Constant.Get_Scene_Name_NormalBy_Level(PlayerPrefs_Manager.Get_Index_Level_Normal()));
    }
    public void CloseButton()
    {
        StartCoroutine(IE_DelayClose());
    }
    IEnumerator IE_DelayClose()
    {
        anim.SetTrigger(Constant.Trigger_PigBankClose);
        yield return Cache.GetWFS(Constant.Time_Delay_PigBank_Close);
        Close();
        Player.ins.is_Block_Raycas = false;//
    }
    //TOTEST
    [ContextMenu("TEST_SET_LEVEL...")]
    public void Test_Reset_Level()
    {
        PlayerPrefs_Manager.Set_Index_Level_Normal(7);


    }
}
