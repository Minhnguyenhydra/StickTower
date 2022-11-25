using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Do nếu để cùng Canvas tổng thì Canvas Pig sẽ có oder layer bằng với Canvas tổng, mà Skeleton muốn gắn lên Canvas tổng phải có Oder layer cao hơn, do đó các Skeleton ko liên quan sẽ bị nổi lên ở Canvas Pink Bank này, do đó phải gắn canvas Pig Bank này ở canvas riêng có chỉ số Oder cao hơn
public class CanvasBonusSkill : UICanvas
{
    public Animator anim_RateUs;

    [SerializeField]
    private List<Sprite> lsSprite;
    [SerializeField]
    private Image btnLeftSword;
    [SerializeField]
    private TextMeshProUGUI txtLeftSword;
    [SerializeField]
    private Image btnRightSword;
    [SerializeField]
    private TextMeshProUGUI txtRightSword;

    private int indexLeftSword;
    private int indexRightSword;
    private int leftBonus;
    private int rightBonus;


    private void Start()
    {
        indexLeftSword = Random.Range(1, lsSprite.Count);
        btnLeftSword.sprite = lsSprite[indexLeftSword];

        indexRightSword = indexLeftSword + 1 > lsSprite.Count - 1 ? 1 : indexLeftSword + 1;
        btnRightSword.sprite = lsSprite[indexRightSword];

        leftBonus = Random.Range(1, 4) * 5;
        txtLeftSword.text = leftBonus.ToString();

        rightBonus = Random.Range(2, 4);
        txtRightSword.text = "x" + rightBonus;
    }


    private void OnEnable()
    {
        Player.ins.is_Block_Raycas = true;
    }
    
    public void Sword_Button1()
    {
        SoundManager.Ins.PlayFx(FxID.click);
#if WatchADs
        AdsManager.Instance.WatchRewardedAds(TakeLeftSword, "video_unlock_bonus_damge_btn1");
#else
        TakeLeftSword();
#endif
        EventController.GAME_PLAY("popup_bonusDamge_2_click");
    }
   
    private void TakeLeftSword()
    {
        string name_Skin = Constant.Get_Skin_Name_By_Id_Sword(indexLeftSword);
        Player.ins.Set_Skin(name_Skin);
        Player.ins.health_Bar.Set_Step_By_Step_Health(Player.ins.health, Player.ins.health + leftBonus, 1);
        Player.ins.Set_Add_Health(leftBonus);

        Player.ins.Set_Anim_TakeSword();
        StartCoroutine(IE_DelayClose());
    }
    
    public void Sword_Button2()
    {
        SoundManager.Ins.PlayFx(FxID.click);
#if WatchADs
        AdsManager.Instance.WatchRewardedAds(TakeRightSword, "video_unlock_bonus_damge_btn1");
#else
        TakeRightSword();
#endif
        EventController.GAME_PLAY("popup_bonusDamge_2_click");
    }
    

    private void TakeRightSword()
    {
        string name_Skin = Constant.Get_Skin_Name_By_Id_Sword(indexRightSword);
        Player.ins.Set_Skin(name_Skin);
        Player.ins.health_Bar.Set_Step_By_Step_Health(Player.ins.health, Player.ins.health * rightBonus, 1);
        Player.ins.Set_Add_Health(Player.ins.health * (rightBonus - 1));
                             
        Player.ins.Set_Anim_TakeSword();
        StartCoroutine(IE_DelayClose());
    }
    
    
    public void No_Thank_Button()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        
        StartCoroutine(IE_DelayClose());

        EventController.GAME_PLAY("nothank_click_bonusskill");
    }
    IEnumerator IE_DelayClose()
    {
        Player.ins.is_Block_Raycas = false;
        anim_RateUs.SetTrigger(Constant.Trigger_PigBankClose);
        yield return Cache.GetWFS(Constant.Time_Delay_PigBank_Close*2);
        Close();
    }
}
