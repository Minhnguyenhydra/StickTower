using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefs_Manager
{
    #region Pig Bank
    public static void Set_Pink_Bank_Gold(int _gold)
    {
        _gold = Mathf.Clamp(_gold,0,2000);
        PlayerPrefs.SetInt(Constant.PigBank_Gold, _gold);
    }
    public static int Get_Pink_Bank_Gold()
    {
        return PlayerPrefs.GetInt(Constant.PigBank_Gold, 0);
    }
    #endregion

    #region Level Challenge
    public static void Set_Replay_Level(int _level)
    {
        string _key = "Lv_Challenge" + _level.ToString();
        PlayerPrefs.SetInt(_key, 1);

    }
    public static int Get_Replay_Level(int _level)
    {
        string _key = "Lv_Challenge" + _level.ToString();
        return PlayerPrefs.GetInt(_key, 0);
    }
    #endregion

    #region  
    public static void Set_Index_Skill_Reach(int _i)
    {
        _i = Mathf.Clamp(_i, 1,3);
        PlayerPrefs.SetInt(Constant.string_Index_Skill_Reach, _i);
    }
    public static int Get_Index_Skill_Reach()
    {
        return PlayerPrefs.GetInt(Constant.string_Index_Skill_Reach, 1);
    }
    #endregion

    #region Gem,Gold
    public static int Get_Gold()
    {
        return PlayerPrefs.GetInt(Constant.Player_Gold, 0);
    }

    public static void Set_Gold(int _gold)
    {
        PlayerPrefs.SetInt(Constant.Player_Gold, _gold);
    }

    //
    public static int Get_Gem()
    {
        return PlayerPrefs.GetInt(Constant.Player_Gem, 0);
    }

    public static void Set_Gem(int _gem)
    {
        PlayerPrefs.SetInt(Constant.Player_Gem, _gem);
    }
    #endregion

    #region Level
    public static int Get_Index_Level_Normal()
    {
        if (PlayerPrefs.GetInt(Constant.Player_IndexLevel_Normal, 0) > 50)
        {
            return 30;
        }
        else
        {
            return PlayerPrefs.GetInt(Constant.Player_IndexLevel_Normal, 0);
        }
    }
    public static int Get_Index_Level_Area()
    {
        if (PlayerPrefs.GetInt(Constant.Player_IndexLevel_Area, 0) > 10)
        {
            return 10;
        }
        else
        {
            return PlayerPrefs.GetInt(Constant.Player_IndexLevel_Area, 0);
        }
    }

    public static void Set_Index_Level_Normal(int _level)
    {
        PlayerPrefs.SetInt(Constant.Player_IndexLevel_Normal, _level);
    }
    public static void Set_Index_Level_Area(int _level)
    {
        PlayerPrefs.SetInt(Constant.Player_IndexLevel_Area, _level);
    }
    #endregion

    #region Setting sound, music, vibra
    public static void SetSetting_Sound(int _On_Sound)
    {
        PlayerPrefs.SetInt(Constant.Setting_Sound,_On_Sound);
    }
    public static void SetSetting_Music(int _On_music)
    {
        PlayerPrefs.SetInt(Constant.Setting_Music, _On_music);
    }
    public static void SetSetting_Vibra(int _On_vibra)
    {
        PlayerPrefs.SetInt(Constant.Setting_vibra, _On_vibra);
    }
    public static int GetSetting_Sound()
    {
        return PlayerPrefs.GetInt(Constant.Setting_Sound,1);
    }
    public static int GetSetting_Music()
    {
        return PlayerPrefs.GetInt(Constant.Setting_Music, 1);
    }
    public static int GetSetting_Vibra()
    {
        return PlayerPrefs.GetInt(Constant.Setting_vibra, 1);
    }
    #endregion

    #region Shop Skin
    //0: chưa mua,   1 : mua nhưng chưa mặc,    10: đang mặc
    public static Enum_State_Item_Skin Get_Enum_State_Item_Skin(int _ID_Skin)
    {
        int i = PlayerPrefs.GetInt(_ID_Skin.ToString(), 0);
        if (_ID_Skin == PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing())
        {
            i = 10;
        }
        if (i == 0)
        {
            return Enum_State_Item_Skin.Not_Have;
        }
        else if (i == 1)
        {
            return Enum_State_Item_Skin.Have_No_Wear;
        }
        else//i==10
        {
            return Enum_State_Item_Skin.Have_Wearing;
        }
    }
    
    public static void Set_Have_No_Wear_Skin(int _ID_Skin)
    {
        PlayerPrefs.SetInt(_ID_Skin.ToString(), 1);
    }
    public static void SetHasNotSkin(int _ID_Skin)
    {
        PlayerPrefs.SetInt(_ID_Skin.ToString(), 0);
    }
    public static void Set_First_Time_Open_Canvas_Skin()
    {
        if (PlayerPrefs.GetInt(Constant.Canvas_Skin_First_Time_Open, 0) == 0)
        {
            PlayerPrefs.SetInt("0", 10);
            PlayerPrefs.SetInt("1", 0);
            //PlayerPrefs.SetInt(Constant.Canvas_Skin_First_Time_Open, 1);
        }
    }
    public static void Set_First_Time_Id_Skin_Wearing()
    {
        if (PlayerPrefs.GetInt(Constant.Canvas_Skin_Name_Wearing, -1) == -1)
        {
            PlayerPrefs.SetInt(Constant.Canvas_Skin_Name_Wearing, 0);
        }
    }
    public static void Set_ID_Name_Skin_Wearing(int _ID_Skin_Name)
    {
        PlayerPrefs.SetInt(Constant.Canvas_Skin_Name_Wearing, _ID_Skin_Name);
    }
    public static int Get_ID_Name_Skin_Wearing()
    {
        return PlayerPrefs.GetInt(Constant.Canvas_Skin_Name_Wearing, 0);
    }

    public static void SavePreSkin(int idSkin)
    {
        PlayerPrefs.SetInt("PreSkin", idSkin);
    }

    public static int GetPreSkin()
    {
        return PlayerPrefs.GetInt("PreSkin");
    }

    public static void SetExpireSkin(int idSkin, int expire)
    {
        PlayerPrefs.SetInt(idSkin.ToString(), expire);
    }

    public static int GetExpireSkin(int idSkin)
    {
        return PlayerPrefs.GetInt(idSkin.ToString(), 0);
    }

    #endregion

    #region Game Play
    public static void Set_Number_Key_Treasure(int _number)
    {
        PlayerPrefs.SetInt(Constant.String_Number_Key_Treasure, _number);
    }
    public static int Get_Number_Key_Treasure()
    {
        return PlayerPrefs.GetInt(Constant.String_Number_Key_Treasure, 0);
    }
    public static void Set_Number_Star_Rate(int _number)
    {
        PlayerPrefs.SetInt(Constant.String_Number_Star_Rate, _number);
    }
    public static int Get_Number_Star_Rate()
    {
        return PlayerPrefs.GetInt(Constant.String_Number_Star_Rate, 0);
    }
    
    public static void Set_Key_1GamPlay_Or_2Area_Or_3Challenge(int _Or_123)//int 1 2 3
    {
         PlayerPrefs.SetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge, _Or_123);
    }
    public static int Get_Key_1GamPlay_Or_2Area_Or_3Challenge()//int 1 2 3
    {
        return PlayerPrefs.GetInt(UserData.Key_1GamPlay_Or_2Area_Or_3Challenge, 1);
    }

    #endregion

    #region Challenge
    public static void Set_QLevel_Challenge(int _QLevel_Challenge)
    {
        PlayerPrefs.SetInt(Constant.String_Level_Challenge, _QLevel_Challenge);
    }
    public static int Get__QLevel_Challenge()
    {
        return PlayerPrefs.GetInt(Constant.String_Level_Challenge, 1);
    }
    #endregion






}
