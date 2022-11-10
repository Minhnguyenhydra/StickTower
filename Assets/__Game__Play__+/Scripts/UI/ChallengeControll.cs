using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeControll : MonoBehaviour
{
    public static ChallengeControll ins;
    public CanvasChallenge canvasChallenge;
    public Scrollbar scrollBar;
    public float scrollPos = 0;
    public float old_scrollPos = 0;
    private int max_Slot = 6; //Change Number Herre
    private float[] pos = new float[6];//Change Number Herre

    public GameObject point1;
    public GameObject point2;
    public GameObject point3;

    // button
    public GameObject leftBtn;
    public GameObject rightBtn;

    public int ii;
    public int level;

    private bool clickBtn = false;
    private bool resetClick = false;
    private int tmp;
    //
    public List<int> list_Gold;//số gold mỗi Challenge ứng với chỉ số trong list này
    public List<int> list_Gem;
    public List<string> list_unlock_level;
    //
    public Text txt_Gold;
    public Text txt_Gem;
    public Text txt_Level_UnLock;
    public List<GameObject> list_Lock;
    public List<int> list_Level_Unlock;
    private void Awake()
    {
        
        max_Slot = 6;
        
    }
    private void Start()
    {
        if (ins != null)
        {
            ins = this;
        }
        ii = 0;
        level = PlayerPrefs_Manager.Get_Index_Level_Normal();
        canvasChallenge = (CanvasChallenge)UIManager.Ins.GetUI(UIID.UICChallenge);
        Check_Active_Btn();
    }
    private void Update()
    {
        
    }
    public void Check_Active_Btn()
    {
        
        if (ii == 0)
        {
            if (level >= 11 )
            {
                bool isReplay = (1 == PlayerPrefs_Manager.Get_Replay_Level(22));
                if (!isReplay)
                {
                    canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.Buy);
                }
                else
                {
                    canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.Replay);

                }
            }
            else
            {
                canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.No_Reach_Level);
            }
        }
        if (ii == 1)
        {
            if (level >= 17 )
            {
                bool isReplay = (1 == PlayerPrefs_Manager.Get_Replay_Level(43));
                if (!isReplay)
                {
                    canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.Buy);
                }
                else
                {
                    canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.Replay);

                }
            }
            else
            {
                canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.No_Reach_Level);
            }
        }
        if (ii == 2)
        {
            if (level >= 53 )
            {
                bool isReplay = (1 == PlayerPrefs_Manager.Get_Replay_Level(53));
                if (!isReplay)
                {
                    canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.Buy);
                }
                else
                {
                    canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.Replay);

                }
            }
            else
            {
                canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.No_Reach_Level);
            }
        }
        if (ii == 3)
        {
            if (level >= 63 )
            {
                bool isReplay = (1 == PlayerPrefs_Manager.Get_Replay_Level(63));
                if (!isReplay)
                {
                    canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.Buy);
                }
                else
                {
                    canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.Replay);

                }
            }
            else
            {
                canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.No_Reach_Level);
            }
        }
        if (ii == 4)
        {
            if (level >= 73 )
            {
                bool isReplay = (1 == PlayerPrefs_Manager.Get_Replay_Level(73));
                if (!isReplay)
                {
                    canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.Buy);
                }
                else
                {
                    canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.Replay);

                }
            }
            else
            {
                canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.No_Reach_Level);
            }
        }
        if (ii == 5)
        {
            if (level >= 103 )
            {
                bool isReplay = (1 == PlayerPrefs_Manager.Get_Replay_Level(103));
                if (!isReplay)
                {
                    canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.Buy);
                }
                else
                {
                    canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.Replay);

                }
            }
            else
            {
                canvasChallenge.Set_Btn_On(Enum_Type_Btn_Challenge.No_Reach_Level);
            }
        }
    }
    private void FixedUpdate()
    {
        float distance = 1f / (pos.Length - 1.0f);

        for(int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if(Input.GetMouseButton(0) && clickBtn == false)
        {
            if(scrollBar.value > old_scrollPos)
            {
                scrollPos = scrollBar.value + distance*1/5.0f;
            }
            else if(scrollBar.value < old_scrollPos)
            {
                scrollPos = scrollBar.value - distance*1/5.0f;
            }
            else
            {
                scrollPos = scrollBar.value;
            }
            old_scrollPos = scrollBar.value;
        }
        else if(clickBtn == false)
        {
            for(int i = 0; i < pos.Length; i++)
            {
                if(scrollPos < pos[i] + (distance/2) && scrollPos > pos[i] - distance/2)
                {
                    scrollBar.value = Mathf.Lerp(scrollBar.value, pos[i], 0.2f);
                    ii = i;
                    if (tmp != ii) 
                    {
                        tmp = ii;
                        CalculatePoint(ii); 
                    }
                }
            }
        }
        else
        {
            scrollBar.value = Mathf.Lerp(scrollBar.value, pos[ii], 0.15f);
        }
    }

    public void Btn_LeftShop()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        if (ii > 0)
        {
            clickBtn = true;
            ii--;
            StartCoroutine(WaitClickBtn());
        }
    }
    public void Btn_RightShop()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        if (ii < max_Slot-1)//Change Number Herre
        {
            clickBtn = true;
            ii++;
            StartCoroutine(WaitClickBtn());
        }
    }

    private IEnumerator WaitClickBtn()
    {
        yield return Cache.GetWFS(0.2f);
        clickBtn = false;
        scrollPos = scrollBar.value;
    }

    private void CalculatePoint(int point)
    {
        txt_Gold.text = list_Gold[ii].ToString();
        txt_Gem.text = list_Gem[ii].ToString();
        txt_Level_UnLock.text = list_unlock_level[ii];
        
        if (list_Level_Unlock[ii] < PlayerPrefs_Manager.Get_Index_Level_Normal())
        {
            list_Lock[ii].SetActive(false);
        }
        else
        {
            list_Lock[ii].SetActive(true);
        }
        //
        if(ii == max_Slot - 1)//Change Number Herre
        {
            rightBtn.SetActive(false);
        }
        else if(ii == 0)
        {
            leftBtn.SetActive(false);
        }
        else
        {
            rightBtn.SetActive(true);
            leftBtn.SetActive(true);
        }
        Check_Active_Btn();
    }

    public void OpenShopEndCheckTab(int tabIdx)
    {
        if(tabIdx == 0)
        {
            leftBtn.SetActive(false);
            rightBtn.SetActive(true);
        }
        else if(tabIdx == max_Slot - 1)//Change Number Herre
        {
            leftBtn.SetActive(true);
            rightBtn.SetActive(false);
        }
    }

}
