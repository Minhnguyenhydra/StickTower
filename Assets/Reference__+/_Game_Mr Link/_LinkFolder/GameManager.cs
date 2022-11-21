using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
/*
yield return new WaitUntil(() => isDone);
 */
public class GameManager : Singleton<GameManager>
{
    public static bool isStarted;
    public bool isChallengeMode;
    public int lastLevel;
    public enum GameState
    {
        Playing,
        Stoped
    }


    [Header("------Not need asing------")]
    public Enemy enemyBoss_auto_Asign;
    [Header("------Mr Link------")]
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    //private static GameState gameState = GameState.MainMenu;
    private Enum_State_Attack_Boos state_Attack_Boos;
    private bool isFireWork_1_lv_38;


    public GameState GMState;


    protected void Awake()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
        //UIManager.Ins.OpenUI(UIID.UICLoading);

        //csv.OnInit();
        //userData?.OnInitData();

        //ChangeState(GameState.MainMenu);

        //TOTEST
        //UIManager.Ins.OpenUI(UIID.UICMainMenu);

    }

    //public static void ChangeState(GameState state)
    //{
    //    gameState = state;
    //}

    //public static bool IsState(GameState state)
    //{
    //    return gameState == state;
    //}

    private void OnEnable()
    {
        GMState = GameState.Playing;
    }

    //Vì lỡ đặt Script Floor ở Object ko phải Parent của obj Point nên ko Getcompont in parent <Floor> được, nên Floor cuối phải kéo vào
    private void Start()
    {
        ///// TOTEST:  StartCoroutine(IE_Load_Fade_In());
        //SoundManager.Ins.PlaySound(SoundID.menu);
        //corShowCanvasWin = StartCoroutine(Set_Delay_Show_Canvas_Win());
        Datacontroller.instance.ShowLoadingPanel(false, "");
    }

    public void Set_Mai_Xanh_Delay_Win(Floor _floor)
    {
        if (GMState == GameState.Stoped)
            return;

        if (Player.ins != null)
        {
            Player.ins.health_Bar.Set_Hide_Health_Bar();
        }
        #region Chỉ level nhặt Key.... Set_Number_Key_Treasure, Player chơi hết màn mới tính là nhặt đc key
        if (PlayerPrefs_Manager.Get_Index_Level_Normal() == 3
         || PlayerPrefs_Manager.Get_Index_Level_Normal() == 8
         || PlayerPrefs_Manager.Get_Index_Level_Normal() == 13
         || PlayerPrefs_Manager.Get_Index_Level_Normal() == 17
         || PlayerPrefs_Manager.Get_Index_Level_Normal() == 20
         || PlayerPrefs_Manager.Get_Index_Level_Normal() == 23
         || PlayerPrefs_Manager.Get_Index_Level_Normal() == 31
         || PlayerPrefs_Manager.Get_Index_Level_Normal() == 46)
        {
            PlayerPrefs_Manager.Set_Number_Key_Treasure(PlayerPrefs_Manager.Get_Number_Key_Treasure() + 1);
            int numberKeys = PlayerPrefs_Manager.Get_Number_Key_Treasure() + 1;
            PlayerPrefs_Manager.Set_Number_Key_Treasure(numberKeys);
            this.PostEvent(QuestManager.QuestID.Quest01, 1);
        }
        #endregion

        _floor.Set_Floor_To_Floor_Of_Player();
        _floor.house_Build_Of_This.Set_Mai_Xanh();
        _floor.house_Build_Of_This.Set_This_To_Team_Player();
        //Fire work

        if (PlayerPrefs_Manager.Get_Index_Level_Normal() != 32)
        {
            Set_Spawn_FireWord(Player.ins.tf_Player);
        }
        else
        {
            if (!isFireWork_1_lv_38)
            {
                isFireWork_1_lv_38 = true;
                Set_Spawn_FireWord(Player.ins.tf_Player);
            }
        }


        Player.ins.Set_Anim_Victory();
        SoundManager.Ins.PlayFx(FxID.yes);
        //GameObject obj = (GameObject)Instantiate(Resources.Load(Constant.Path_Frefab_fx_win_ball), Player.ins.tf_Player.position, Player.ins.tf_Player.rotation);

        ((CanvasGamePlay)UIManager.Ins.GetUI(UIID.UICGamePlay)).Set_Active_Castle_Nha_Cuoi_Chiem_Duoc();

        StartCoroutine(Set_Delay_Show_Canvas_Win());
    }
    public void Set_Spawn_FireWord(Transform _tf_Where_Spawn)
    {
        SoundManager.Ins.PlayFx(FxID.fireWork);
        GameObject obj = (GameObject)Instantiate(Resources.Load(Constant.Path_Frefab_fx_win_ball), _tf_Where_Spawn.position, _tf_Where_Spawn.rotation);
    }
    public void Set_Lose_Level(Floor _floor)
    {
        StartCoroutine(Set_Delay_Show_Canvas_Lose());

    }
    public void Set_Bool_Win_Boss()
    {
        state_Attack_Boos = Enum_State_Attack_Boos.Win;
    }
    public void Set_Bool_Lose_Boss()
    {
        state_Attack_Boos = Enum_State_Attack_Boos.Lose;
    }
    public Enum_State_Attack_Boos Get_Bool_Win_Boss()
    {
        return state_Attack_Boos;
    }
    //
    IEnumerator Set_Delay_Show_Canvas_Win()
    {
#if UNITY_EDITOR
        //Debug.Log("~~~~~");
#endif
        yield return Cache.GetWFS(Constant.Time_Delay_Fade_Win);
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();
        yield return Cache.GetWFS(Constant.Time_Delay_Load_Scene);
        ((CanvasGamePlay)UIManager.Ins.GetUI(UIID.UICGamePlay)).Close();



        UIManager.Ins.OpenUI(UIID.UICWin_Level);

        //Mở khóa canvas rương nếu level đủ 3 chìa
        int lv = PlayerPrefs_Manager.Get_Index_Level_Normal();
        //if (lv == 3 || lv == 8 || lv == 13 || lv == 17 || lv == 20 || lv == 23 || lv == 31 || lv == 46)
        //{
        //PlayerPrefs_Manager.Set_Number_Key_Treasure(0);
        Debug.LogError("=========number key:" + PlayerPrefs_Manager.Get_Number_Key_Treasure());
        if (PlayerPrefs_Manager.Get_Number_Key_Treasure() >= 3)
        {
            UIManager.Ins.OpenUI(UIID.UICShopPrize);
            Debug.LogError("========= open shop");
        }
       // }

        if (lv == 6)
        {
            UIManager.Ins.OpenUI(UIID.UICRateUs);
        }
        if (lv == 10 || lv == 30)//|| lv == 20
        {
            UIManager.Ins.OpenUI(UIID.UICFreeSkin);
        }

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.Contains("Challenge"))
        {
            int level = PlayerPrefs_Manager.Get__QLevel_Challenge();
            PlayerPrefs_Manager.Set_QLevel_Challenge(level + 1);
        }
        else
            PlayerPrefs_Manager.Set_Index_Level_Normal(lv + 1);

        //Fight Boos
        UIManager.Ins.OpenUI(UIID.UICFight_Boss);
        UIManager.Ins.CloseUI(UIID.UICFight_Boss);
        //


    }
    //
    IEnumerator Set_Delay_Show_Canvas_Lose()
    {
        if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() != 3)
        {
            yield return Cache.GetWFS(Constant.Time_Delay_Fade_Win);
        }
        else
        {
            float time_change = Constant.Time_Delay_Fade_Win - 2;
            time_change = Mathf.Clamp(time_change, 0.5f, 10);
            yield return Cache.GetWFS(time_change);//TODO: Fix màn rewward thua hiển thị chậm ở Challenge
        }

        //UIManager.Ins.OpenUI(UIID.UICFade);
        //((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();
        //yield return Cache.GetWFS(Constant.Time_Delay_Load_Scene);
        ((CanvasGamePlay)UIManager.Ins.GetUI(UIID.UICGamePlay)).Close();


        UIManager.Ins.OpenUI(UIID.UICFail);

        //Fight Boos
        UIManager.Ins.OpenUI(UIID.UICFight_Boss);
        if (UIManager.Ins.IsOpenedUI(UIID.UICFight_Boss))
        {
            UIManager.Ins.CloseUI(UIID.UICFight_Boss);
        }
        //

    }

    //TOTEST
    [ContextMenu("TEST_SET_LEVEL...")]
    public void Test_Reset_Level()
    {
        PlayerPrefs_Manager.Set_Number_Key_Treasure(2);


    }
    [ContextMenu("TEST_RESET_LEVEL_0")]
    public void Test_Reset_Leveaal()
    {
        PlayerPrefs_Manager.Set_Index_Level_Normal(0);
        PlayerPrefs_Manager.Set_Gem(0);
        PlayerPrefs_Manager.Set_Gold(0);

    }
    [ContextMenu("TEST_RESETsss_LEVEL_0")]
    public void Testdd_Reset_Leveaal()
    {

        PlayerPrefs_Manager.Set_Pink_Bank_Gold(2000);

    }

}
/*

1. Kiếm bay lên
    tắt ADs obj_Btn
        
        tf_Kiếm(world space).Domove()--> tf_top.Oncomplete-->domove>> tf Player 
            -->oncomplete: change skin Player, anim win..... destroy( sword)
                Cộng dame player
        
2. take damge Trap chưa dừng lại



3. Key
    Phải có 3 key
        key script______: 
        Lv2: key  0
        Lv3: key  1
        Lv4: key  2
            kết thúc Lv4 mà win_... end game hiện canvas Treasua
                canvas Treasua.....
                    mở hết 3key mới hiện Btn Back( Wincanvas )




4. Canvas win: BG trái ---Phải




5. Canvas Tresua
    9 hòm 
        Mỗi hòm sẽ có Script Treasua
        mỗi hòm sẽ có button ...ẩn ảnh Button đi
        mở...kiểm tra index < 3 thì cho mở, ko thì thôi
            Click vào hòm sẽ mở hòm tương ứng, và đánh dấu hòm đó đã mở 
                mỗi lần mở tăng + 1 indext_Treasua
                reLoad lại camvas Gold, Gem
                hòm sẽ có : gold, gem bên dưới, mở sẽ bay lên và hòm bật action biến mất

        Xem ADs để hiện lại 3 key và reset lại indext_Treasua = 0;

        3 Key ....
            Mỗi Key sẽ có active và deactive
                Set_hide()    ...     Set_Show()



6. Canvas Win
    OnEnable: pháo hoa bắn ra
        gắn thêm WORLD Empty obj vào để sinh pháo hoa ở điểm đó


7. Arrrre aaaa
    AreaManager:
        State_Idle
            Set: Player và Enemy idle
        State_Run
            Set: Player và Enemy Run đến Target
        State_Attack
            
            Set Anim : Player và Enemy attack
                Con nào nhiều máu hơn sẽ attack lâu hơn, con nào ít máu hơn sẽ đang attack 0.6s ... sẽ đổ máu. lăn ra chéc
            Sau... 2s con nào thắng bật anim thắng
                Sau... 3s Fade Out rồi quay lại vị trí lúc đầu, thay đổi Quái
    Player_Area_Script
        
    Scene   
        
    5 Button
        Damge_1
            Trừ vàng+tăng dame
        Damge_2
        Damge_3
        Damge_4
        Damge_5

8. Chest xong thay id skin đang mặc ở Player Pref

----------------------------------------------------------------------------------------------------------------------------
1 1 quái nhỏ
1 1 BOSS

1 2 Gai cuu cc




















State
    Hold
        


Player
    Colider
        Raycast 
           Hold >>> x Anim 1.2 

Drag_Drop_Manager
    Box:  colider,
        Box_Player
            1 ô có thể chèn từ tầng 1 lên

        Box_Enemy
            Có các điểm để xếp Enemy + điểm
                Các điểm đánh số 




Click chuột
    Raycast 
            Hold 
            Có Vào ô Enemy 
                Xanh lõi ô




            -------------------------------------------
            Có Vào Player 
                
                Hold >>> x Anim 1.2 
                vào ô nào
                    ô có colider
                    Trong ô thì vào điểm nào
                Thả tay
                    Check nếu vào 1 ô địch
                        Có vào ô địch
                            
                        Không vào ô địch

                        về ô ban đầu gần nhất đã đứng
            Không_vào PLYer

Check Raycast Player
    Nếu giữ 
/////////////////////////////////////

Drag_Drop_Manager

    *** Free_State
            Check input mouse + hold vào PLYER
                Check PLYER >>> can_Hold_Player
                    Change state ( Hold_Player_Mouse_State )
            

    *** Hold_Player_Mouse_State
            Cho Player TO ra
            Update vị trí Player theo chuột 
            Check_Raycast_Floor
                
            Check thả chuột
                Nếu CÓ vào ô tag_Floor
                    Tìm index ô trống NHỎ nhất
                        truyền vào Set_Pos_Player---vị trí  này
                            tf_Stay_Now = indexPos_here
                        truyền vào Set_Pos_Player---vị trí  này
                    Tìm index kề Player chứa Enemy = player -1( vì chắc chắn có Enemy mới thả vào đc... nếu ko n đã bị sụp xuống )
                        Check level Player và Enemy
                        Delay
                            Anim_idle:  Player - nearest Enemy
                                0.5s
                                    Anim_attack:  Player - nearest Enemy
                                        1s
                                           Nếu Level Player > Enemy 
                                                Anim_Die:  nearest Enemy
                                                Anim_Idle:  Player
                                                Anim_health: bắn ra
                                                    0.6s
                                                        Check_Reward();
                                                            Nếu CÓ hòm Reward ở cuối
                                                                Chạy đến điểm Reward
                                                                    Anim Player mở
                                                                    Anim Hòm mở
                                                                        0.7s
                                                                            Nếu Đã hết Enemy
                                                                                Change state ( Victory )
                                                                            Nếu CHƯA hết Enemy
                                                                                Change state ( Free_State )
                                                            Nếu KHÔNG có hòm Reward ở cuối
                                                            Change state ( Free_State )
                                           Nếu Level Player < Enemy 
                                                Anim_Die:  Player
                                                Anim_Idle:  nearest Enemy
                                                Anim_health: bắn ra
                                                    Change state ( Free_State )
                Nếu KHÔNG vào ô Enemy
                    Cho Player về vị trí tại thời điểm gần nhất n đứng
                        tf_Stay_Now.position
                            Change state ( Free_State )

/////////////////////////////////////
Player 
    Idle_State
    
    
    Attack_State



    Die_State




    Victory_State




/////////////////////////////////////
Enemy
    Idle_State
    
    
    Attack_State



    Die_State




    Victory_State
/////////////////////////////////////
House
    House_Player
        Mái xanh+mái đỏ>>> On Off
        -----------------------
        List các tầng  list_Floor<>;
            Floor
                list_Enemy<>
                list_Point_Container<>
                bool is_Have_Player
        -----------------------
        Sinh ra 1 tầng tại vị trí gốc--của nhà
        Tăng lên 1 tầng từ dưới lên
            Tầng thêm:
                        DoScale tâng mới (0~1) +              |  trong cùng 1 khoảng thời gian
                        DoMove lên vị trí = 0.5 lần chiều cao |  trong cùng 1 khoảng thời gian
            Các tầng còn lại
                Domove(lên chiều cao của 1 tẩng)              |  trong cùng 1 khoảng thời gian

        -----------------------

        Có các tầng
            Mỗi tầng có các điểm chứa
                Mỗi điểm chứa có Player ~ hoặc Enemy
                Enemy hoặc Player --Chít
                    Remove Enemy hoặc Player khỏi điểm
                        Điểm update Enemy lên tầng-tâng có list_Enemy
                        Floor
                            list_Enemy<>
                            list_Point_Container<>
                
                            Tâng update số tầng lên nhà
                                Nếu nhà chỉ còn 1 tầng và các điểm ko chứa cái gì nữa
                                    Player Win
    House_Enemy
        Function Sụp_Xuống(){}
        Tầng rỗng bị sụp xuống
            các tâng phải có Index
                Nếu 1 tầng sụp xuống                         |  trong cùng 1 khoảng thời gian
                    DoScale nhỏ đi (1~0) +                   |  trong cùng 1 khoảng thời gian
                    DoMove xuống vị trí = 0.5 lần chiều cao  |  trong cùng 1 khoảng thời gian
                        rồi biến mất


                    Các tầng cao hơn( có index > hơn) sẽ
                    House.Floor
                        Domove(xuống chiều cao của 1 tẩng)   |  trong cùng 1 khoảng thời gian


-đổi màu mai
-cái Floor được đánh dấu là Last
    khi đã đánh hết index1
        Tự động đến cuối cùng
            Trong mọi state 
                Check index cuối cùng
                    point cuối cùng của Floor cuối cùng sẽ có tf_điểm để đến
                        Player sẽ đến đó lúc cuối
                            domove.onComlete >> anim mở hòm hoặc state
                             chỉ cẩn domove rồi check State tiếp theo giống lúc thả chuột
                                bật anim thắng thua
                                Firework                
                                     load Scene thắng, thua       

SceneManager___dont destroyOnload

CanvasFader
thanh điểm chạy
Bug: kéo đc vào Floor đầu tiên của Player

Chuyển tiếp giữa các nhà
Point có gắn: Script  giải cứu CC








-------------------------------------------------------------------------------------------------------------------------
Win
    Scene nhận vàng random
        2s hiện No, thank
Lose
    Scene Lose, Skip Ads để qua màn---no, thank: về level---chơi lại

canvas Ingame: Btn: Home, restart game, Next level, phải xem ads
        Banner quảng cáo


Menu
    Load 
        Gold: dùng để mua Skin
        Gem
    
    
    
    
    Play
        Có Level kiếm đc vàng
        Lv: 1/2/3/4/5/6/7---Scene---
            Win
                1s
                    Fade
                        Win--Scene
                            Xem ADs để X2/3/4/5 Gold
                                
                            No thank
                                Next Level
            Lose
                1s
                    Fade
                        Lose--Scene
                            Xem ADs để skip
    ARR
    Skin
        Xếp char thay đổi được Skin, ko click vào được nếu mở rồi

        Xếp Skin cùng Char Raycast 3D vào các Skin này sẽ triger nhân vật thay đổi skin + action level up
         Skine manager--- khi thoát khỏi scene này sẽ lưu skin vào data và load skin ở Scene Play
        
        
        
    HHHHE

Canvas
    Fade
    
    Ingame
        Button go to Menu
            Lưu level, Play load lại level
    
    
 Level TOÀN RƯƠNG   
                ko đc thả Player vào khi 
                raycast thả Player vào ô nào sẽ active anim xiên trong 4s   
                đánh dấu ô Raycast rồi ko thả đc vào nữa   
    
    
    


******************************************************************************************
    Data
        


+-X:    trap, kiếm
        kiếm
            Thay kiếm
Nhặt key
Nhặt Hòm
Lvel Cây xanh
Level toàn rương

Floor cuối 1 nhà:       chạy lại tấn công tướng
                        Giải cứu Cong tua
                        Giải cứu Cong tua

                    Nếu Player đang ở điểm có chỉ số là 2 chỉ còn cách target  cuối cùng của 1 nhà 1 Point
                        thì khi đánh xong Obj ở giữa sẽ chạy lại luôn điểm gần target cuối rồi tấn công, hoặc giải cứu, hoặc mở hòm


 */