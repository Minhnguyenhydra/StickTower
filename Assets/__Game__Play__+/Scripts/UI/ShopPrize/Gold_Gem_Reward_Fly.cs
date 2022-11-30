using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Gold_Gem_Reward_Fly : MonoBehaviour
{
    public RectTransform rect_Gold_Reward_Fly;
    public Text txtnumber_Gold_Gem_Reward;
    public GameObject objImg_Gold;
    public GameObject objImg_Gem;
    public int ggold = 0;
    public int ggem = 0;
    public CanvasShop_Prize canvasShop_Prize;
    public bool isFist_Config;
    //public List<GameObject> listObj_3_Key_Gray;

    private void Start()
    {
        rect_Gold_Reward_Fly = GetComponent<RectTransform>();
        canvasShop_Prize = ((CanvasShop_Prize)UIManager.Ins.GetUI(UIID.UICShopPrize));
        
        rect_Gold_Reward_Fly.localScale = Vector3.zero;
    }
    public void Set_Gold_Gem(int gold,int gem)
    {
        if (gold == 0)
        {
            ggem = gem;
            txtnumber_Gold_Gem_Reward.text = "+" + gem.ToString();
            objImg_Gold.SetActive(false);
            objImg_Gem.SetActive(true);
        }
        else
        {
            ggold = gold;
            txtnumber_Gold_Gem_Reward.text = "+" + gold.ToString();
            objImg_Gold.SetActive(true);
            objImg_Gem.SetActive(false);
        }
    }
    public void Set_Fly()
    {
        SoundManager.Ins.PlayFx(FxID.collect_coin);
        if (ggold != 0)
        {
            EfxManager.ins.Set_Step_By_Step_Inscrease(PlayerPrefs_Manager.Get_Gold(), PlayerPrefs_Manager.Get_Gold() + ggold, canvasShop_Prize.txt_Gold,1);
            PlayerPrefs_Manager.Set_Gold(PlayerPrefs_Manager.Get_Gold() + ggold);
           // EfxManager.ins.GetGoldFx(rect_Gold_Reward_Fly.position, canvasShop_Prize.txt_Gold.gameObject.transform.position);
        }
        else if(ggem != 0)
        {
            EfxManager.ins.Set_Step_By_Step_Inscrease(PlayerPrefs_Manager.Get_Gem(), PlayerPrefs_Manager.Get_Gem() + ggem, canvasShop_Prize.txt_Gem, 1);
            PlayerPrefs_Manager.Set_Gem(PlayerPrefs_Manager.Get_Gem() + ggem);
          //  EfxManager.ins.GetGemFx(rect_Gold_Reward_Fly.position, canvasShop_Prize.txt_Gem.gameObject.transform.position);
        }

        if (rect_Gold_Reward_Fly != null)
        {
            rect_Gold_Reward_Fly.DOLocalMoveY(rect_Gold_Reward_Fly.localPosition.y + Constant.Reward_Off_Set_YPos_Fly_Up, Constant.Reward_Time_Fly_Up);
            rect_Gold_Reward_Fly.DOScale(Constant.Reward_Off_Set_Scale_Fly_Up, Constant.Reward_Time_Fly_Up).OnComplete(() => {
                rect_Gold_Reward_Fly.DOScale(Constant.Reward_Off_Set_Scale_Fly_Up, Constant.Reward_Time_Fly_Up).OnComplete(() => {
                    //Destroy(this.gameObject);
                });
            });
        }
        canvasShop_Prize.Set_Reload_Gold_Gem_Title();
       
    }
}
