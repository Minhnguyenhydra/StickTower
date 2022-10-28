using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum UIID
{
    UICGamePlay = 0,
    UICBlockRaycast = 1,

    UICMainMenu = 2,

    UICSetting = 3,
    UICFail = 4,
    UICVictory = 5,
    //To test
    UICWin_Level = 6,
    //
    UICLoading = 7,
    UICPigBank = 8,
    UICSkin_Top = 9,
    UICSkin_Boot = 10,
    UICFade = 11,
    UICChallenge = 12,
    UICChest = 13,
    UICArea = 14,
    UICFreeSkin = 15,
    UICRateUs = 16,
    UICShopPrize = 17,
    UICBonusSkill = 18,
    UICFight_Boss = 19,
    UICPay_Gold_To_Play = 20,
    UICTry_Replay = 21,
    
}
public enum Screne_Size { Doc_720, Doc_1024, Nothing }//Nothing để lúc đầu đặt _screne_Size_current!= _screne_Size_last để check size luôn lần đầu chạy hàm checkScreen trong script BgScale...
public enum Enum_TypeHouse { player, enemy_Normal, enemy_Tree, enemy_Reward }
public enum Enum_Type_Take_Last_Level { Enemy, Princess, Reward }
public enum Enum_State_Attack_Boos { Not_Reach, Win, Lose }
public enum Enum_State_Item_Skin { Not_Have, Have_No_Wear, Have_Wearing }
public enum Enum_Type_Btn_Challenge { No_Reach_Level, Buy, Replay }
public enum Enum_Anim_0 { anim_01, anim_02, anim_03 }


/*
public enum TypeEnemy { Type1, Type2, Type3 }
//Debug.Log(pos_WorldMousePosition);
public enum GameMode { Normal, Challenge }

//public enum GameState { MainMenu, Pause, GamePlay }
public enum GameState { MainMenu, Pause, GameWaiting, GameStart, GameHitting, GameLose, GameWinlevel, GameVictory }

public enum GameResult { Win, Lose, levelCompleted, OnHitting }//use

//public enum UIID { GamePlay, BlockRaycast, MainMenu, Skin, Setting, Counting, ScoreBoard, LeaderBoard, IAP }

public enum ButtonState { Buy, Equip, Equipped, Ads, CommingSoon }

public enum PriceType { Ads, Gem }
public enum StateBox { Empty, Blocked, CanMerge }//use
public enum UIName { Live, Setting, Card, Victory, Failed, GetReward, BlockRaycast, SceneFader, WinLevel, ViewAds, Tut, Ulti, Gold }//use
public enum TeamCharacter { Player, Enemy }//use
public enum TypeCharacter { Player, Enemy }//use
public enum TagNameBullet
{
    Bullet_Solidier_1

}//Thêm tagNameBullet ở đây, sau đó sang script GetString để đổi enum sang string
public enum TagNameCharacter
{
    Solidier_1,
}//Thêm tagNameBullet ở đây, sau đó sang script GetString để đổi enum sang string
public enum StateCharacter//use
{
    attack_trigger,
    death_trigger,
    idle_trigger,
    run_trigger,
    win_trigger
}
public enum QuizID
{
    Wiring,
    Audition,
    Caculator,
    Memory,
    RollingConnect,
    SortOrder,
    Random
}

[System.Serializable]
public class SkinPrice
{
    public PriceType priceType;
    public int value;
}



 */