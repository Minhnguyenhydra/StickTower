using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/UserData", order = 1)]
public class UserData : ScriptableObject
{
    public int PlayingLevel = 0;
    public int Cash = 0;

    public bool removeAds = false;

    public bool musicIsOn = true;
    public bool fxIsOn = true;
    public bool tutorialed = false;
    public int progressItem;
    public int burgerCount;
    public int burgerRequire;
    public int burgerIndex;
    public int skinProgress;
    public int shop_SkinCount;
    public string playerName;
    public float curY;
    public float curZ;

    public int winBoard;
    public int winRank;
    internal int levelArena;
    internal int _1GamPlay_Or_2Area_Or_3Challenge;
    //Quan Add:
    public bool isVibraOn = true;
    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    /// </summary>
    /// <param name="data"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public void SetDataState(string data, int ID, int state)
    {
        PlayerPrefs.SetInt(data + ID, state);
    }

    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    /// </summary>
    /// <param name="data"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public int GetDataState(string data, int ID, int defaultID = 0)
    {
        return PlayerPrefs.GetInt(data + ID, defaultID);
    }

    /// <summary>
    /// Key_Name
    /// if(bool) true == 1
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value"></param>
    //Thay set 1 biến Playerpref sẽ Update luôn biến đó ở UserData
    public void SetIntData(string data, ref int variable, int value)
    {
        variable = value;
        PlayerPrefs.SetInt(data, value);
    } 
    
    public void SetBoolData(string data, ref bool variable, bool value)
    {
        variable = value;
        PlayerPrefs.SetInt(data, value ? 1 : 0);
    }

    public void SetFloatData(string data, ref float variable, float value)
    {
        variable = value;
        PlayerPrefs.GetFloat(data, value);
    }

    public void SetStringData(string data, ref string variable, string value)
    {
        variable = value;
        PlayerPrefs.SetString(data, value);
    }

    public void OnInitData()
    {
        levelArena = PlayerPrefs.GetInt(Key_LevelArena, 0);
        //PlayerPrefs.SetInt(Key_LevelArena, 1);
        if (levelArena > 50)
        {
            levelArena = 30;
        }
        //levelArena = Mathf.Clamp(levelArena,0, 50);
        PlayingLevel = PlayerPrefs.GetInt(Key_Level, 0);
        Cash = PlayerPrefs.GetInt(Key_Cash, 0);
        musicIsOn = PlayerPrefs.GetInt(Key_FxIsOn, 1) == 1;
        fxIsOn = PlayerPrefs.GetInt(Key_MusicIsOn, 1) == 1;
        removeAds = PlayerPrefs.GetInt(Key_RemoveAds, 0) == 1;
        tutorialed =  PlayerPrefs.GetInt(Key_Tutorial, 0) == 1;
        progressItem = PlayerPrefs.GetInt(Key_Progress, 0);
        burgerCount = PlayerPrefs.GetInt(Key_BurgerCount, 0);
        burgerRequire = PlayerPrefs.GetInt(Key_BurgerRequire, 25);
        burgerIndex = PlayerPrefs.GetInt(Key_BurgerIndex, 0);
        skinProgress =  PlayerPrefs.GetInt(Key_Skin_Progress, 0);
        shop_SkinCount =  PlayerPrefs.GetInt(Key_Skin_Count, 1);
        playerName =  PlayerPrefs.GetString(Key_PlayerName, "YOU");

        curY =  PlayerPrefs.GetFloat(Key_CurvedWorldY, 0.3f);
        curZ =  PlayerPrefs.GetFloat(Key_CurvedWorldZ, 0);

        winBoard = PlayerPrefs.GetInt(Key_Win_Board, 0);
        winRank = PlayerPrefs.GetInt(Key_Win_Rank, 600);
        _1GamPlay_Or_2Area_Or_3Challenge = PlayerPrefs.GetInt(Key_1GamPlay_Or_2Area_Or_3Challenge, 1);
        //
        isVibraOn = PlayerPrefs.GetInt(Key_isVibraOn, 1) == 1;
    }

    public void OnResetData()
    {
        PlayerPrefs.DeleteAll();
        OnInitData();
    }

    public const string Keys_HeroLevelArena = "HeroArena";
    public const string Key_LevelArena = "LevelArena";

    public const string Key_Level = "Level";
    public const string Key_Cash = "Cash";
    public const string Key_FxIsOn = "SoundIsOn";
    public const string Key_MusicIsOn = "MusicIsOn";
    public const string Key_RemoveAds = "RemoveAds";
    public const string Key_Tutorial = "Tutorial";
    public const string Key_Progress = "Progress";
    public const string Key_BurgerCount = "BurgerCount";
    public const string Key_BurgerRequire = "BurgerRequire";
    public const string Key_BurgerIndex = "BurgerIndex";
    public const string Key_Skin_Progress = "SkinProgress";
    public const string Key_Skin_Count = "SkinCount";
    public const string Key_Skin_ = "Skin_";
    public const string Key_Skin_Item_Ads_ = "AdsSkin_";
    public const string Key_PlayerName = "PlayerName";

    public const string Key_CurvedWorldY = "CurvedWorldY";
    public const string Key_CurvedWorldZ = "CurvedWorldZ";

    public const string Key_Win_Board = "WinBoard";
    public const string Key_Win_Rank = "WinRank";
    public const string Key_1GamPlay_Or_2Area_Or_3Challenge = "Key_1GamPlay_Or_2Area_Or_3Challenge";//int 1 2 3
    public const string Key_isVibraOn = "isVibraOn_Q";//int 1 2 3

}


