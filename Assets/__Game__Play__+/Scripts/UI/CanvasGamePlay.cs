using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CanvasGamePlay : UICanvas
{
    public Grandfather_Castle grandfather_Castle;//biến này để xét bao nhiêu Castle được bật tùy từng level... Castle là ảnh các tường thành trên màn hình, cứ chiếm được 1 nhà là cái ảnh này sẽ được in đậm
    private int level_curent;
    public GameObject obj_Btn_ADs_Sword;
    public GameObject obj_Btn_NextLevel;
    private int number_Castle_This_Level;
    private Parrent_Castle parrent_Castle_This_Level;
    public Text txt_Level;
    public Animator anim_GamePlay;
    public int int_Damge;
    public TextMeshProUGUI txt_Damge;
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        SoundManager.Ins.PlaySound(SoundID.gamePlay);
        Set_Check_Hide_Show();
        //
        anim_GamePlay.SetTrigger(Constant.Trigger_GamePlay_Open);
        level_curent = PlayerPrefs_Manager.Get_Index_Level_Normal();
        txt_Level.text = Constant.Get_Tile_Game_Play_By_Level(level_curent);

        number_Castle_This_Level = Constant.Get_Type_Castle_By_Level(level_curent);
        //tắt hết các Castle trước khi bật cái nào đó
        for (int i = 0; i < grandfather_Castle.list_Parrent_Castle.Count; i++)
        {
            if (grandfather_Castle.list_Parrent_Castle[i] != null)
            {
                grandfather_Castle.list_Parrent_Castle[i].gameObject.SetActive(false);
            }
        }
        //hỉ số của Parrent_Castle ở trong list  tương ứng với number_Castle_This_Level TRỪ đi 1
        parrent_Castle_This_Level = grandfather_Castle.list_Parrent_Castle[number_Castle_This_Level - 1];
        //luôn luôn bật cái đầu tiên trước
        grandfather_Castle.list_Parrent_Castle[0].gameObject.SetActive(true);
        //




        int level = PlayerPrefs_Manager.Get_Index_Level_Normal();
        if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 1)
        {
            if (level == 30 || level == 26 || level == 16 || level == 14 || level == 22|| level == 35|| level == 43)
            {
                UIManager.Ins.OpenUI(UIID.UICPay_Gold_To_Play);
                StartCoroutine(IE_Waiting_Player_Initalize());

            }

        }
        int_Damge = Random.Range(5, 11);
        txt_Damge.SetText(int_Damge.ToString("N0"));

        if (level %5 == 0 && level > 4)
        {
            UIManager.Ins.OpenUI(UIID.UICBonusSkill);
        }
        if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 3)
        {
            level_curent = PlayerPrefs_Manager.Get__QLevel_Challenge();
            txt_Level.text = Constant.Get_Tile_Game_Play_By_Level(level_curent);

            if (UIManager.Ins.IsOpenedUI(UIID.UICPay_Gold_To_Play))
            {
                UIManager.Ins.IsOpenedUI(UIID.UICPay_Gold_To_Play);

            }
            
        }
        if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 3 || PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 2)
        {
            if (obj_Btn_NextLevel != null)
            {
                obj_Btn_NextLevel.SetActive(false);

            }
        }
    }

    IEnumerator IE_Waiting_Player_Initalize()
    {
        yield return new WaitUntil(() => (Player.ins != null));
        Player.ins.is_Block_Raycas = true;//
    }
    public void Set_Check_Hide_Show()
    {
        if (PlayerPrefs_Manager.Get_Index_Level_Normal() > 6)
        {
            obj_Btn_ADs_Sword.SetActive(true);
        }
        else
        {
            obj_Btn_ADs_Sword.SetActive(false);
        }
    }
    //Nếu level có nhiều nhà thì sau khi cam lia 1 lượt mới active
    public void Set_Active_Parrent_Castle_This_Level()
    {
        grandfather_Castle.list_Parrent_Castle[0].gameObject.SetActive(false);
        parrent_Castle_This_Level.gameObject.SetActive(true);

        parrent_Castle_This_Level.Set_Chua_Chiem_Duoc();
    }
    
    public void Set_Active_Castle_Each_Time_Chiem_Duoc(int _index_House_Chiem_Duoc)
    {
        if (_index_House_Chiem_Duoc < parrent_Castle_This_Level.list_Castle.Count)
        {
             parrent_Castle_This_Level.list_Castle[_index_House_Chiem_Duoc].Set_Chiem_Duoc();
        }
    }
    
    public void Set_Active_Castle_Nha_Cuoi_Chiem_Duoc()
    {
        int i = parrent_Castle_This_Level.list_Castle.Count - 1;
        parrent_Castle_This_Level.list_Castle[i].Set_Chiem_Duoc();
    }

    public void Home_Button()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        GameManager.Ins.GMState = GameManager.GameState.Stoped;
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        Close();
    }

    public void RePlay_Button()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        GameManager.Ins.GMState = GameManager.GameState.Stoped;
        anim_GamePlay.SetTrigger(Constant.Trigger_GamePlay_Close);
        StartCoroutine(IE_Delay_Replay());
        
    }
    IEnumerator IE_Delay_Replay()
    {
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();
        yield return Cache.GetWFS(Constant.Time_Fade);

        if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() != 3)
        {
            Scene_Manager_Q.Load_Scene(Constant.Get_Scene_Name_NormalBy_Level(level_curent));
        }
        else if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 3)
        {
            if (obj_Btn_NextLevel != null)
            {
                obj_Btn_NextLevel.SetActive(false);

                //trước khi vào Challenge đã lưu chỉ số level Challenge sẽ chơi rồi, Replay Challenge chỉ load lại chỉ số đó
               int level_Current_Challenge = PlayerPrefs_Manager.Get__QLevel_Challenge();
                Scene_Manager_Q.Load_Scene("Level_"+ level_Current_Challenge.ToString());
            }
        }



        
        Close();
    }
    public void SkipLevel_Button()
    {
        if (Player.ins != null)
        {
            if (!Player.ins.isDie)
            {
                SoundManager.Ins.PlayFx(FxID.click);
                GameManager.Ins.GMState = GameManager.GameState.Stoped;
                level_curent += 1;
                anim_GamePlay.SetTrigger(Constant.Trigger_GamePlay_Close);
                StartCoroutine(IE_Delay_SkipLevel());

            }
        }
    }
    IEnumerator IE_Delay_SkipLevel()
    {
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();
        yield return Cache.GetWFS(Constant.Time_Fade);
        
        PlayerPrefs_Manager.Set_Index_Level_Normal(level_curent);
        Scene_Manager_Q.Load_Scene(Constant.Get_Scene_Name_NormalBy_Level(level_curent));
        Close();
    }


    public void ADs_Take_Sword_Button()
    {
        if (Sword_Ads_TopLeft.Ins != null)
        {
            Sword_Ads_TopLeft.Ins.Set_Go_To_Herro(int_Damge);
            obj_Btn_ADs_Sword.SetActive(false);
        }
    }



}
