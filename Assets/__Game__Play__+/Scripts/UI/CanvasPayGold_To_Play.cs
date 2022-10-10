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

    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        int level = PlayerPrefs_Manager.Get_Index_Level_Normal();
        //0:Gold .... 1 Gem
        txtGoldReward.text = Constant.Get_Reward_Gold_Gem_By_Pay_Gold(level)[0].ToString();
        txtGemReward.text = Constant.Get_Reward_Gold_Gem_By_Pay_Gold(level)[1].ToString();
        if (level == 14)
        {
            txtPAY_100_GOLD.text = "Watch advertisement\nto play this level";
        }
        else if (level == 22)
        {
            txtPAY_100_GOLD.text = "Pay 100 gold to play\nthis level";
        }
        else if (level == 30 || level == 26|| level == 16|| level == 14)
        {
            txtPAY_100_GOLD.text = "Watch advertisement\nto play this level";
        }
        if (level == 26)
        {
            obj_RewardArea.SetActive(false);
        }
    }
    
    //
    public void PlayButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);

        CloseButton();

    }
    //
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
