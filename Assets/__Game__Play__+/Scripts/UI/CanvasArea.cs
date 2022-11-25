using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CanvasArea : UICanvas
{
    private int level_curent;
    private int number_Castle_This_Level;
    private Parrent_Castle parrent_Castle_This_Level;
    public Text txt_Level;
    public Text txt_Coin;
    public Text txt_gem;
    public int gold_In_Level;//Số tiêng còn lại khi mua, qua màn mới trừ thật vào Data lưu ở máy
    public int gem_In_Level;//Số tiêng còn lại khi mua, qua màn mới trừ thật vào Data lưu ở máy

    public UserData userData;
    public bool isOnFight_Once;//chỉ được ấn Fight 1 lần mỗi level
    public RectTransform rect_Boot_Of_Canvas;//chỉ được ấn Fight 1 lần mỗi level

    private int numberWatchedAds;
    private int orderHero;
    private bool isComeBack;


    private void OnEnable()
    {
        SoundManager.Ins.PlaySound(SoundID.arenaMusic);
        level_curent = userData.levelArena;
        //Debug.Log(PlayerPrefs.GetInt(UserData.Key_Level, 1));
        txt_Level.text = "Level " + (level_curent + 1).ToString();
        gold_In_Level = PlayerPrefs_Manager.Get_Gold();
        gem_In_Level = PlayerPrefs_Manager.Get_Gem();
        txt_Coin.text = gold_In_Level.ToString();
        txt_gem.text = gem_In_Level.ToString();
        SetOrigin_Boot_Canvas();

        EventController.ARENA_EVENT_ARENA((level_curent + 1).ToString());
    }
    public void Set_Downt_Boot_Canvas()
    {
        rect_Boot_Of_Canvas.DOAnchorPosY(-2000, 1);
    }

    private void MoveBootCanvasComeBack()
    {
        rect_Boot_Of_Canvas.DOAnchorPosY(-26.36f, 1).OnComplete(() =>
        {
            isComeBack = false;
        });
    }

    public void SetOrigin_Boot_Canvas()
    {
        rect_Boot_Of_Canvas.anchoredPosition = new Vector3(0, -26.36f, 0);
    }


    public void Home_Button()
    {
        SoundManager.Ins.PlayFx(FxID.click);
        //Để tắt các hành động lính khi mở canvas Home... để ko bị hiện canvas win/lose sau khi lính đánh xong mà trong khi đang bật canvas Home
        Init_Area.Ins.userData.SetIntData(UserData.Key_LevelArena, ref Init_Area.Ins.userData.levelArena, Init_Area.Ins.userData.levelArena);

        Init_Area.Ins.OnInit();

        //
        //UIManager.Ins.OpenUI(UIID.UICMainMenu);
        StartCoroutine(LoadHomeScene());
    }
    private IEnumerator LoadHomeScene()
    {
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();

        AsyncOperation homeScene = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        while (!homeScene.isDone)
        {
            yield return null;
        }
    }
    public void ReLoad_UI_Gold()
    {
        txt_Coin.text = gold_In_Level.ToString();
        txt_gem.text = gem_In_Level.ToString();
    }

    public void RePlay_Button()
    {
        //anim_GamePlay.SetTrigger(Constant.Trigger_GamePlay_Close);
        //StartCoroutine(IE_Delay_Replay());
        if (isComeBack)
            return;

        Init_Area.Ins.userData.SetIntData(UserData.Key_LevelArena, ref Init_Area.Ins.userData.levelArena, Init_Area.Ins.userData.levelArena);

        Init_Area.Ins.OnInit();
        SoundManager.Ins.PlayFx(FxID.click);
        isComeBack = true;
        MoveBootCanvasComeBack();
    }

    public void Fight_Button()
    {

        SoundManager.Ins.PlayFx(FxID.click);
        if (!isOnFight_Once)
        {
            Set_Downt_Boot_Canvas();
            isOnFight_Once = true;
            Init_Area.Ins.Fighting();
        }
    }

    public void SetResetFight()
    {
        isOnFight_Once = false;
    }

    public void SetSave_Gold()
    {
        PlayerPrefs_Manager.Set_Gold(gold_In_Level);
    }

    public void BuyHeroButton(int index)
    {
        SoundManager.Ins.PlayFx(FxID.click);
        EventController.ARENA_EVENT_CARD(index);
        //TODO: them phan check tien du hay chua
        if (index == 0)
        {
            if (gold_In_Level >= 50)
            {
                gold_In_Level -= 50;
                BoughtHero(index);
                ReLoad_UI_Gold();
            }
        }

        if (index == 1)
        {
            if (gold_In_Level >= 100)
            {
                gold_In_Level -= 100;
                BoughtHero(index);
                ReLoad_UI_Gold();
            }
        }

        if (index == 2)
        {
            if (gold_In_Level >= 150)
            {
                gold_In_Level -= 150;
                BoughtHero(index);
                ReLoad_UI_Gold();
            }
        }

        if (index == 3)
        {
            if (gem_In_Level >= 4)
            {
                gem_In_Level -= 4;
                BoughtHero(index);
                ReLoad_UI_Gold();
            }
        }

        if (index == 4)
        {
            if (gem_In_Level >= 12)
            {
                gem_In_Level -= 12;
                BoughtHero(index);
                ReLoad_UI_Gold();
            }
        }

    }

    public void WatchAds(int index)
    {
        SoundManager.Ins.PlayFx(FxID.click);
        orderHero = index;
# if WatchADs
        AdsManager.Instance.WatchRewardedAds(BuyHero,"video_show_arena_buy_hero_" + index.ToString());
#else
        BuyHero();
#endif
    }

    private void BuyHero()
    {
        numberWatchedAds++;
        if (orderHero == 4 && numberWatchedAds < 2)
            return;

        numberWatchedAds = 0;
        BoughtHero(orderHero);
    }

    public void BoughtHero(int index)
    {
        int level = userData.GetDataState(UserData.Keys_HeroLevelArena, index, 0);
        userData.SetDataState(UserData.Keys_HeroLevelArena, index, level + 1);

        Init_Area.Ins.player.OnInit();
    }

    IEnumerator IE_Delay_Replay()
    {
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();
        yield return Cache.GetWFS(Constant.Time_Fade);
        Scene_Manager_Q.Load_Scene(Constant.Get_Scene_Name_NormalBy_Level(level_curent));
        Close();
    }

}
/*
soi 
mu
bang bit mat
mo
*/