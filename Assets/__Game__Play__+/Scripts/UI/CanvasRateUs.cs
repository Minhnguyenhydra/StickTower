using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//Do nếu để cùng Canvas tổng thì Canvas Pig sẽ có oder layer bằng với Canvas tổng, mà Skeleton muốn gắn lên Canvas tổng phải có Oder layer cao hơn, do đó các Skeleton ko liên quan sẽ bị nổi lên ở Canvas Pink Bank này, do đó phải gắn canvas Pig Bank này ở canvas riêng có chỉ số Oder cao hơn
public class CanvasRateUs : UICanvas
{
    public int numberStar_Rate; 
    public bool isRated;
    public Animator anim_RateUs;
    public List<GameObject> list_Obj_GoldStar;
    private void Awake()
    {
        isRated = false;
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        for (int i = 0; i < list_Obj_GoldStar.Count; i++)
        {
            list_Obj_GoldStar[i].SetActive(false);
        }
    }
    
    //
    public void Star_Button_1()
    {
        if (!isRated)
        {
            isRated = true;
            Set_Star_Rate(1);
            CloseButton();
        }
    }
    //
    public void Star_Button_2()
    {
        if (!isRated)
        {
            isRated = true;
            Set_Star_Rate(2);
            CloseButton();
        }
    }
    //
    public void Star_Button_3()
    {
        if (!isRated)
        {
            isRated = true;
            Set_Star_Rate(3);
            CloseButton();
        }
    }
    //
    public void Star_Button_4()
    {
        if (!isRated)
        {
            isRated = true;
            Set_Star_Rate(4);
            CloseButton();
        }
    }
    //
    public void Star_Button_5()
    {
        if (!isRated)
        {
            isRated = true;
            Set_Star_Rate(5);
            CloseButton();
        }
    }
    public void Set_Show_Gold_Star(int _star)
    {
        for (int i = 0; i < _star; i++)
        {
            list_Obj_GoldStar[i].SetActive(true);
        }
        
    }
    public void Set_Star_Rate(int _star)
    {
        Set_Show_Gold_Star(_star);
        numberStar_Rate = _star;
        PlayerPrefs_Manager.Set_Number_Star_Rate(_star);
    }
    public void CloseButton()
    {
        StartCoroutine(IE_DelayClose());
    }
    IEnumerator IE_DelayClose()
    {
        anim_RateUs.SetTrigger(Constant.Trigger_PigBankClose);
        yield return Cache.GetWFS(Constant.Time_Delay_PigBank_Close*2);
        Close();
    }
}
