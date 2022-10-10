using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//Do nếu để cùng Canvas tổng thì Canvas Pig sẽ có oder layer bằng với Canvas tổng, mà Skeleton muốn gắn lên Canvas tổng phải có Oder layer cao hơn, do đó các Skeleton ko liên quan sẽ bị nổi lên ở Canvas Pink Bank này, do đó phải gắn canvas Pig Bank này ở canvas riêng có chỉ số Oder cao hơn
public class CanvasPigBank : UICanvas
{
    private int gold_In_bank; 
    private int gold_Max_In_bank; 
    public Text txt_Gold_In_Bank; 
    //
    public Image img_Progres;
    public Animator anim_PigBank;
    //
    public GameObject obj_Enough_Open_Btn;
    public GameObject obj_Not_Enough_Open_Btn;
    private void Awake()
    {
        gold_Max_In_bank = Constant.Gold_Max_Pink_Bank;
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {

        gold_In_bank = PlayerPrefs_Manager.Get_Pink_Bank_Gold();


        /////gold_In_bank = 2000;


        txt_Gold_In_Bank.text = gold_In_bank.ToString() + "/" + gold_Max_In_bank.ToString();
        img_Progres.fillAmount = (float)gold_In_bank / (float)gold_Max_In_bank;
        if (gold_In_bank < gold_Max_In_bank)
        {
            obj_Enough_Open_Btn.SetActive(false);
            obj_Not_Enough_Open_Btn.SetActive(true);
        }
        else
        {
            obj_Enough_Open_Btn.SetActive(true);
            obj_Not_Enough_Open_Btn.SetActive(false);
        }
        //////gold_In_bank = 2000;
    }
    public void Set_View_ADs_To_Get_Gold()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        if (gold_In_bank == gold_Max_In_bank)
        {
            //Cộng gold cho Player
            int goldPlayer = PlayerPrefs_Manager.Get_Gold();
            Set_Step_By_Step_Health(goldPlayer, gold_In_bank + goldPlayer, 2);
            PlayerPrefs_Manager.Set_Gold(gold_In_bank + goldPlayer);


            /////////((CanvasMainMenu)UIManager.Ins.GetUI(UIID.UICMainMenu)).Set_Reload_Gold_Gem_Title();


            EfxManager.ins.GetGoldFx(((CanvasMainMenu)UIManager.Ins.GetUI(UIID.UICMainMenu)).rect_Piggy_Bank.position, ((CanvasMainMenu)UIManager.Ins.GetUI(UIID.UICMainMenu)).rect_gold.position);

            gold_In_bank = 0;
            PlayerPrefs_Manager.Set_Pink_Bank_Gold(gold_In_bank);
            //UNDONE: Close Canvas ..thoát ra màn chính..anim nổ hũ, vàng bay lên và lợn biến mất ..còn khung tròn
            ((CanvasMainMenu)UIManager.Ins.GetUI(UIID.UICMainMenu)).Set_Reload_Gold_Gem_Title();
            ((CanvasMainMenu)UIManager.Ins.GetUI(UIID.UICMainMenu)).Set_Off_Full_Pink_bank();
            ((CanvasMainMenu)UIManager.Ins.GetUI(UIID.UICMainMenu)).Set_Init_Gold_Pink_bank();
            Close();
        }
    }
    //
    public void Set_Delay_Incre_Gold(int _score, int target, float transitionTime)
    {
        StartCoroutine(IE_Delay_Incre_Gold(_score, target, transitionTime));
    }
    IEnumerator IE_Delay_Incre_Gold(int _score, int target, float transitionTime)
    {
        yield return Cache.GetWFS(Constant.Time_Delay_Gold_Piggy_Inscrease);
        Set_Step_By_Step_Health( _score,  target,  transitionTime);
    }
    public void Set_Step_By_Step_Health(int _score, int target, float transitionTime)
    {
        Tween t = DOTween.To(() => _score, x => _score = x, target, transitionTime).OnUpdate(() => ((CanvasMainMenu)UIManager.Ins.GetUI(UIID.UICMainMenu)).txt_Gold.text = _score.ToString("N0"));
    }
    //
    public void CloseButton()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        StartCoroutine(IE_DelayClose());
    }
    IEnumerator IE_DelayClose()
    {
        anim_PigBank.SetTrigger(Constant.Trigger_PigBankClose);//set 0.2s
        yield return Cache.GetWFS(Constant.Time_Delay_PigBank_Close);
        Close();
    }
    //
}
