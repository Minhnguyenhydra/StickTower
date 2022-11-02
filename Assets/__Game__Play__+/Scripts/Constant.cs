using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    # region Player
    public readonly static float Player_Speed_Follow = 20;
    public readonly static float Player_Time_To_Point = 0.002f;
    public readonly static float Floor_Time_Fall_Downt = 0.4f;
    public readonly static float Floor_Y_Fall_Downt = -4.185f;//=Scale(Transform)*size(colider) của ảnh đó
    //
    public readonly static float Pos_X_Rollbar_X5 = 66;
    public readonly static float Pos_X_Rollbar_X4 = 168;
    public readonly static float Pos_X_Rollbar_X3 = 273;
    //
    public readonly static float Time_Player_Show_Blood = 0.6f;//0.5
    public readonly static float Time_Player_Hit_Skill_1 = 1.5f;//0.5
    public readonly static float Time_Player_Hit_Skill_2 = 2.9f;//0.5
    public readonly static float Time_Player_Hit_Skill_3 = 3.5f;//0.5
    public readonly static float Time_Player_Die_attack = 1f;//0.5
    public readonly static float Time_Player_Die_die = 1f;//1.834f;
    public readonly static float Time_Delay_Win_Reward = 1;
    public readonly static float Time_Delay_move_to_next_house = 1;
    //
    public readonly static float Time_Player_Attack_idle = 1.5f;
    public readonly static float Time_Cam_Move = 2f;
    public readonly static float Time_Cam_Delay_Move_Next_House = 1f;
    public readonly static float Time_Delay_Load_Scene = 1f;
    public readonly static float Time_Delay_Fade_Win = 0.5f;
    public readonly static int Time_Delay_Show_No_Thank = 2;
    public readonly static float Time_Fade = 1;
    public readonly static float Time_Loading = 2;
    public readonly static float Time_Delay_PigBank_Close = 0.2f;
    public readonly static float Time_Delay_Setting_Close = 0.2f;
    public readonly static float Time_Delay_Challenge_Close = 0.6f;
    public readonly static float Time_Delay_Gold_Piggy_Inscrease = 0.6f;
    public readonly static float Time_Key_Fly = 1;
    public readonly static float Time_Player_Go_End_OpenReward = 0.5f;
    public readonly static float Time_Delay_Run_To_End_Reward = 0.2f;
    public readonly static float Time_DelayClose_CanvasFight_Start = 2f;
    //
    public readonly static float Time_Count_Attack_Player = 1;
    public readonly static float Time_Count_Attack_Enemy = 2;
    public readonly static float Time_Count_Skill_1 = 2f;
    public readonly static float Time_Count_Skill_2 = 2.7f;
    public readonly static float Time_Count_Skill_3 = 2.7f;
    //Sword_Ads_TopLeft
    public readonly static float Time_Sword_ADs_Go_To_Mid = 0.6f;
    public readonly static float Time_Sword_ADs_Mid_To_Player = 0.9f;
    //Thời gian Player chạy đến Rương báu vật
    public readonly static float Time_Player_Move_End_Level = 1;
    public readonly static float Reward_Off_Set_YPos_Fly_Up = 98;
    public readonly static float Reward_Lv_House_Full_Gold_Off_Set_YPos_Fly_Up = 1.5f;
    public readonly static Vector3 Reward_Off_Set_Scale_Fly_Up = new Vector3(1,1,1);
    public readonly static float Reward_Time_Fly_Up = 0.5f;
    //
    public readonly static float Player_Scale_Bigger = 1.2f;
    public readonly static float Player_Scale_Normal = 1;
    //
    public readonly static Vector3 Floor_Vec_Scale_Small = new Vector3(0,0,0);
    public readonly static Vector3 Floor_Vec_Scale_Big = new Vector3(1,1,1);
    //
    public readonly static Vector3 Player_Local_Pos_Health_Bar = new Vector3(0,3.2f,0);
    public readonly static Vector3 Player_offset_Blood = new Vector3(0, 1.3f, 0);
    //
    public readonly static Vector3 Enemy_Local_Pos_Health_Bar_Normal = new Vector3(0, 3.0f, 0);
    public readonly static Vector3 Enemy_Local_Pos_Health_Bar_Fly = new Vector3(0, 2.0f, 0);
    public readonly static Vector3 Enemy_Local_Pos_Health_Bar_Boar = new Vector3(0, 2.5f, 0);
    public readonly static Vector3 Enemy_offset_Blood = new Vector3(0, 1.3f, 0);
    public readonly static int Gold_Max_Pink_Bank = 2000;

    //Level challege
    public readonly static int LEVEL_1 = 22;
    public readonly static int LEVEL_2 = 43;
    public readonly static int LEVEL_3 = 53;
    public readonly static int LEVEL_4 = 63;
    public readonly static int LEVEL_5 = 73;
    public readonly static int LEVEL_6 = 103;

    #endregion
    #region Buff Skill
    public readonly static float BuffSkill_1 = 1f;
    public readonly static float BuffSkill_2 = 1f;
    public readonly static float BuffSkill_3 = 1f;
    public readonly static int Gold_By_Skill_1 = 30;
    public readonly static int Gold_By_Skill_2 = 50;
    public readonly static int Gold_By_Skill_3 = 100;
    #endregion
    #region Skin name
    //
    public readonly static string Skin_1_And_Weapon_nomal = "hero1/weapon_nomal";
    public readonly static string Skin_1_And_Weapon_1 = "hero1/weapon_bonus_1";
    public readonly static string Skin_1_And_Weapon_2 = "hero1/weapon_bonus_2";
    public readonly static string Skin_1_And_Weapon_3 = "hero1/weapon_bonus_3";
    public readonly static string Skin_1_And_Weapon_4 = "hero1/weapon_bonus_4";
    //
    public readonly static string Skin_2_And_Weapon_nomal = "hero2/weapon_nomal";
    public readonly static string Skin_2_And_Weapon_1 = "hero2/weapon_bonus_1";
    public readonly static string Skin_2_And_Weapon_2 = "hero2/weapon_bonus_2";
    public readonly static string Skin_2_And_Weapon_3 = "hero2/weapon_bonus_3";
    public readonly static string Skin_2_And_Weapon_4 = "hero2/weapon_bonus_4";
    //
    public readonly static string Skin_3_And_Weapon_nomal = "hero3/weapon_nomal";
    public readonly static string Skin_3_And_Weapon_1 = "hero3/weapon_bonus_1";
    public readonly static string Skin_3_And_Weapon_2 = "hero3/weapon_bonus_2";
    public readonly static string Skin_3_And_Weapon_3 = "hero3/weapon_bonus_3";
    public readonly static string Skin_3_And_Weapon_4 = "hero3/weapon_bonus_4";
    //
    public readonly static string Skin_4_And_Weapon_nomal = "hero4/weapon_nomal";
    public readonly static string Skin_4_And_Weapon_1 = "hero4/weapon_bonus_1";
    public readonly static string Skin_4_And_Weapon_2 = "hero4/weapon_bonus_2";
    public readonly static string Skin_4_And_Weapon_3 = "hero4/weapon_bonus_3";
    public readonly static string Skin_4_And_Weapon_4 = "hero4/weapon_bonus_4";
    //
    public readonly static string Skin_5_And_Weapon_nomal = "hero5/weapon_nomal";
    public readonly static string Skin_5_And_Weapon_1 = "hero5/weapon_bonus_1";
    public readonly static string Skin_5_And_Weapon_2 = "hero5/weapon_bonus_2";
    public readonly static string Skin_5_And_Weapon_3 = "hero5/weapon_bonus_3";
    public readonly static string Skin_5_And_Weapon_4 = "hero5/weapon_bonus_4";
    //
    public readonly static string Skin_6_And_Weapon_nomal = "hero6/weapon_nomal";
    public readonly static string Skin_6_And_Weapon_1 = "hero6/weapon_bonus_1";
    public readonly static string Skin_6_And_Weapon_2 = "hero6/weapon_bonus_2";
    public readonly static string Skin_6_And_Weapon_3 = "hero6/weapon_bonus_3";
    public readonly static string Skin_6_And_Weapon_4 = "hero6/weapon_bonus_4";
    //
    public readonly static string Skin_7_And_Weapon_nomal = "hero7/weapon_nomal";
    public readonly static string Skin_7_And_Weapon_1 = "hero7/weapon_bonus_1";
    public readonly static string Skin_7_And_Weapon_2 = "hero7/weapon_bonus_2";
    public readonly static string Skin_7_And_Weapon_3 = "hero7/weapon_bonus_3";
    public readonly static string Skin_7_And_Weapon_4 = "hero7/weapon_bonus_4";
    //
    public readonly static string Skin_8_And_Weapon_nomal = "hero8/weapon_nomal";
    public readonly static string Skin_8_And_Weapon_1 = "hero8/weapon_bonus_1";
    public readonly static string Skin_8_And_Weapon_2 = "hero8/weapon_bonus_2";
    public readonly static string Skin_8_And_Weapon_3 = "hero8/weapon_bonus_3";
    public readonly static string Skin_8_And_Weapon_4 = "hero8/weapon_bonus_4";
    #endregion
    //Path
    public readonly static string Path_Frefab_Floor = "Prefabs/Floor_Prefs";
    public readonly static string Path_Frefab_Health_Bar_Red = "Prefabs/Health_Bar/Health_Bar_Red";
    public readonly static string Path_Frefab_Health_Bar_Blue = "Prefabs/Health_Bar/Health_Bar_Blue";
    public readonly static string Path_Frefab_Stroke = "Prefabs/Stroke";
    public readonly static string Path_Frefab_fx_win_ball = "Prefabs/fx_win_ball";
    public readonly static string Path_Frefab_Blood = "Prefabs/Blood";
    //
    //Animation
    public readonly static string Trigger_Stroke_OutSide = "outside";
    public readonly static string Trigger_Stroke_Center = "center";
    public readonly static string Trigger_PigBankClose = "Pig_CloseTrigger";
    public readonly static string Trigger_HomeOut = "HomeOut_Trigger";
    public readonly static string Trigger_HomeIn = "HomeIn_Trigger";
    public readonly static string Trigger_Challenge_Up = "Challen_Up_Trigger";
    public readonly static string Trigger_Challenge_Downt = "Challen_Downt_Trigger";
    public readonly static string Trigger_Setting_Close = "Setting_Close_Trigger";
    public readonly static string Trigger_GamePlay_Open = "GamePlay_Open_Trigger";
    public readonly static string Trigger_GamePlay_Close = "GamePlay_Close_Trigger";
    //
    public readonly static string Trigger_Tut_0_3 = "tut_03";
    public readonly static string Trigger_Tut_0_1 = "tut_01";
    public readonly static string Trigger_Tut_0_2 = "tut_02";


    public readonly static string Trigger_Fade_In = "Fade_In_Trigger";
    public readonly static string Trigger_Fade_Out = "Fade_Out_Trigger";
    public readonly static string string_Index_Skill_Reach = "Skil_Rich";
    //
    public readonly static int SortingOrder_Show = 60;
    public readonly static int SortingOrder_Hide = -60;
    public readonly static float offset_Pos_Y_Raycas_Herro = -1.5f;//offset điểm Raycast qua Player...để raycast qua giữa người Hero
    //Lưu Player Pref
    public readonly static string Player_Gold = "player_gold";
    public readonly static string Player_Gem = "player_gem";
    public readonly static string Player_IndexLevel_Normal = "player_IndexLevel";
    public readonly static string Player_IndexLevel_Area = "Area_IndexLevel";
    public readonly static string StringLevel = "Level_";
    public readonly static string PigBank_Gold = "PigBankGold";
    public readonly static string Setting_Sound = "setting_sound";
    public readonly static string Setting_Music = "setting_music";
    public readonly static string Setting_vibra = "setting_vibra";
    public readonly static string Canvas_Skin_First_Time_Open = "Canvas_Skin_First_Time_Open";
    public readonly static string Canvas_Skin_Name_Wearing = "Name_Skin_Ware";
    public readonly static string String_Shop_Unlock_Level = "UNLOCK LEVEL ";
    public readonly static string String_Number_Key_Treasure = "NumBerKeyHave";
    public readonly static string String_Number_Star_Rate = "NumberStar_Rate";
    public readonly static string String_Level_Challenge = "QLevel_Challenge";
    
    public static int Get_Gold_Bonus_By_Level(int _level)
    {
        if (_level == 6)
        {
            return 480;
        }
        else
        {
            return 100;
        }
    }

    public static string Get_Scene_Name_NormalBy_Level(int _level)
    {
        return "Level_" + _level.ToString();
    }
    public static string Get_Scene_Name_Area_By_Level(int _level)
    {
        return "Ar_Level_" + _level.ToString();
    }
    public static string Get_Skin_Name_By_Id(int _ID_Skin)
    {
        switch (_ID_Skin)
        {
            case 0:
                return "hero1/weapon_nomal";
            case 1:
                return "hero2/weapon_nomal";
            case 2:
                return "hero3/weapon_nomal";
            case 3:
                return "hero4/weapon_nomal";
            case 4:
                return "hero5/weapon_nomal";
            case 5:
                return "hero6/weapon_nomal";
            case 6:
                return "hero7/weapon_nomal";
            case 7:
                return "hero8/weapon_nomal";
            case 8:
                return "hero9/weapon_nomal";
            case 9:
                return "hero10/weapon_nomal";
            case 10:
                return "hero11/weapon_nomal";
            case 11:
                return "hero12/weapon_nomal";
            case 12:
                return "hero13/weapon_nomal";
            case 13:
                return "hero14/weapon_nomal";
            case 14:
                return "hero15/weapon_nomal";
            default:
                Debug.Log("K co Skin " + _ID_Skin);
                return "hero1/weapon_nomal";
        }

    }
    public static float Get_Time_Skill(int _index_Skill)
    {
        switch (_index_Skill)
        {
            case 11:
                return 2.4f;
            case 12:
                return 3.7f;
            case 13:
                return 3.9f;
            case 21:
                return 2.2f;
            case 22:
                return 2.1f;
            case 23:
                return 2.8f;
            case 31:
                return 2.4f;
            case 32:
                return 2.4f;
            case 33:
                return 2.1f;
            case 41:
                return 2.4f;
            case 42:
                return 3.7f;
            case 43:
                return 2.1f;
            case 51:
                return 2.4f;
            case 52:
                return 2.4f;
            case 53:
                return 2.1f;
            case 61:
                return 2f;
            case 62:
                return 2.5f;
            case 63:
                return 2.1f;
            case 71:
                return 2.4f;
            case 72:
                return 3.9f;
            case 73:
                return 2.1f;
            case 81:
                return 2.2f;
            case 82:
                return 2.4f;
            case 83:
                return 2.1f;

            case 91:
                return 2.4f;
            case 92:
                return 3.7f;
            case 93:
                return 2.2f;
                
            case 101:
                return 2.1f;
            case 102:
                return 3.7f;
            case 103:
                return 3.9f;
                
            case 111:
                return 2.8f;
            case 112:
                return 2.4f;
            case 113:
                return 2.4f;
                
            case 121:
                return 2f;
            case 122:
                return 2.5f;
            case 123:
                return 2.1f;
                
            case 131:
                return 3.7f;
            case 132:
                return 2.1f;
            case 133:
                return 2.4f;


            case 141:
                return 3.7f;
            case 142:
                return 3.9f;
            case 143:
                return 2.8f;


            default:
                return 1.2f;
        }

    }
    public static float Get_Time_action_Hit_Enemy(bool _isBig_Enemy)
    {
        if (!_isBig_Enemy)
        {
            return 0.4f;
        }
        else
        {
            return 0.4f;
        }
    }
    public static float Get_Time_action_Die_Enemy(bool _isBig_Enemy)
    {
        if (!_isBig_Enemy)
        {
            return 1.5f;
        }
        else
        {
            return 2.6f;
        }
    }
    public static string Get_Skin_Name_By_Id_Sword(int _ID_Sword)
    {
        int id_Skin = PlayerPrefs_Manager.Get_ID_Name_Skin_Wearing();
        switch (id_Skin)
        {
            case 0:
                return "hero1/weapon_bonus"+ _ID_Sword.ToString();
            case 1:
                return "hero2/weapon_bonus" + _ID_Sword.ToString();
            case 2:
                return "hero3/weapon_bonus" + _ID_Sword.ToString();
            case 3:
                return "hero4/weapon_bonus" + _ID_Sword.ToString();
            case 4:
                return "hero5/weapon_bonus" + _ID_Sword.ToString();
            case 5:
                return "hero6/weapon_bonus" + _ID_Sword.ToString();
            case 6:
                return "hero7/weapon_bonus" + _ID_Sword.ToString();
            case 7:
                return "hero8/weapon_bonus" + _ID_Sword.ToString();
            case 8:
                return "hero9/weapon_bonus" + _ID_Sword.ToString();
            case 9:
                return "hero10/weapon_bonus" + _ID_Sword.ToString();
            case 10:
                return "hero11/weapon_bonus" + _ID_Sword.ToString();
            case 11:
                return "hero12/weapon_bonus" + _ID_Sword.ToString();
            case 12:
                return "hero13/weapon_bonus" + _ID_Sword.ToString();
            case 13:
                return "hero14/weapon_bonus" + _ID_Sword.ToString();
            case 14:
                return "hero15/weapon_bonus" + _ID_Sword.ToString();
            default:
                Debug.Log("K co weapon " + _ID_Sword);
                return "hero1/weapon_bonus" + _ID_Sword.ToString();
        }

    }
    public static List<int> Get_Reward_Gold_Gem_By_Pay_Gold(int level)
    {
        List<int> li = new List<int>();
        
        if (level == 14 || level == 30)
        {
            li.Add(100);//0: gold
            li.Add(1);//1 gem
            return li;
        }
        else if (level == 22)
        {
            li.Add(200);//0: gold
            li.Add(1);//1 gem
            return li;
        }
        else
        {
            li.Add(100);//0: gold
            li.Add(1);//1 gem
            return li;
        }

    }
    public static string Get_Tile_Game_Play_By_Level(int _level)
    {
        switch (_level)
        {
            case 0:
                return "Level 0 - Kill enemies";
            case 1:
                return "Level 1 - Kill enemies";
            case 2:
                return "Level 2 - Kill enemies";
            case 3:
                return "Level 3 - Find treasure";
            case 4:
                return "Level 4 - Save ally";
            case 5:
                return "Level 5 - Kill enemies";
            case 6:
                return "Level 6 - Bonus level";
            case 7:
                return "Level 7 - Kill enemies";
            case 8:
                return "Level 8 - Save ally";
            case 9:
                return "Level 9 - Kill enemies";
            case 10:
                return "Level 10 - Kill enemies";
            case 11:
                return "Level 11 - Save ally";
            case 12:
                return "Level 12 - Kill enemies";
            case 13:
                return "Level 13 - Find treasure";
            case 14:
                return "Level 14 - Find treasure";
            case 15:
                return "Level 15 - Kill enemies";
            case 16:
                return "Level 16 - Bonus level";
            case 17:
                return "Level 17 - Save ally";
            case 18:
                return "Level 18 - Kill enemies";
            case 19:
                return "Level 19 - Kill enemies";
            case 20:
                return "Level 20 - Find treasure";
            case 21:
                return "Level 21 - Save ally";
            case 22:
                return "Level 22 - Kill enemies";
            case 23:
                return "Level 23 - Find treasure";
            case 24:
                return "Level 24 - Save ally";
            case 25:
                return "Level 25 - Save ally";
            case 26:
                return "Level 26 - Bonus level";
            case 27:
                return "Level 27 - Find treasure";
            case 28:
                return "Level 28 - Kill enemies";
            case 29:
                return "Level 29 - Kill enemies";
            case 30:
                return "Level 30 - Save ally";
           
            case 31:
                return "Level 31 - Find treasure";

            case 32:
                return "Level 32 - Save ally";
            case 33:
                return "Level 33 - Kill enemies";
            case 34:
                return "Level 34 - Save ally";
            case 35:
                return "Level 35 - Bonus level";
            case 36:
                return "Level 36 - Find treasure";
            case 37:
                return "Level 37 - Kill enemies";
            case 38:
                return "Level 38 - Kill enemies";
            case 39:
                return "Level 39 - Save ally";
            case 40:
                return "Level 40 - Kill enemies";
            case 41:
                return "Level 41 - Save ally";
            case 42:
                return "Level 42 - Kill enemies";
            case 43:
                return "Level 43 - Save ally"; 
            case 44:
                return "Level 44 - Kill enemies";
            case 45:
                return "Level 45 - Bonus level";
            case 46:
                return "Level 46 - Find treasure";
            case 47:
                return "Level 47 - Kill enemies";
            case 48:
                return "Level 48 - Save Ally";
            case 49:
                return "Level 49 - Kill enemies";
            case 50:
                return "Level 50 - Kill enemies";
            case 51:
                return "Level 51 - Save Ally";
            case 52:
                return "Level 52 - Save Ally";
            case 53:
                return "Level 53 - Save Ally";
            case 54:
                return "Level 54 - Save Ally";
            case 55:
                return "Level 55 - Save Ally";
            case 56:
                return "Level 56 - Save Ally";
            case 57:
                return "Level 57 - Save Ally";
            case 58:
                return "Level 58 - Save Ally";
            case 59:
                return "Level 59 - Save Ally";
            case 60:
                return "Level 60 - Save Ally";
            
            default:
                return "Level 0 - Save Ally";
        }

    }
    public static int Get_Type_Castle_By_Level(int _level)
    {
        switch (_level)
        {
            case 0:
                return 1;
            case 1:
                return 2;
            case 2:
                return 1;
            case 3:
                return 1;
            case 4:
                return 1;
            case 5:
                return 1;
            case 6:
                return 1;//rate us
            case 7:
                return 1;
            case 8:
                return 1;
            case 9:
                return 1;
            case 10:
                return 2;//Hint  xem ads lấy kiếm, Free skin
            case 11:
                return 2;
            case 12:
                return 1;
            case 13:
                return 1;
            case 14:
                return 1;
            case 15:
                return 1;
            case 16:
                return 1;
            case 17:
                return 1;
            case 18:
                return 1;
            case 19:
                return 1;
            case 20:
                return 1;
            case 21:
                return 1;
            case 22:
                return 5;
            case 23:
                return 1;
            case 24:
                return 1;
            case 25:
                return 1;
            case 26:
                return 2;
            case 27:
                return 1;
            case 28:
                return 1;
            case 29:
                return 1;
            case 30:
                return 1;
            case 31:
                return 1;
            case 32:
                return 2;
            case 33:
                return 2;
            case 34:
                return 1;
            case 35:
                return 1;
            case 36:
                return 1;
            case 37:
                return 1;
            case 38:
                return 1;
            case 39:
                return 1;
            case 40:
                return 1;
            case 41:
                return 3;
            case 42:
                return 1;
            case 43:
                return 6;
            case 44:
                return 1;
            case 45:
                return 1;
            case 46:
                return 1;
            case 47:
                return 1;
            case 48:
                return 1;
            case 49:
                return 2;
            case 50:
                return 1;
            case 51:
                return 1;
            case 52:
                return 1;
            case 53:
                return 1;
            case 54:
                return 1;
            case 55:
                return 1;
            case 56:
                return 1;
            case 57:
                return 1;
            case 58:
                return 1;
            case 59:
                return 1;
            case 60:
                return 1;
            default:
                return 1;
        }

    }
    public static int Get_Gold_Reward_By_level(int _level)
    {
        switch (_level)
        {
            case 0:
                return 100;
            case 1:
                return 150;
            case 2:
                return 100;
            case 3:
                return 100;
            case 4:
                return 100;
            case 5:
                return 100;
            case 6:
                return 480;
            case 7:
                return 100;
            case 8:
                return 100;
            case 9:
                return 100;
            case 10:
                return 100;
            case 11:
                return 100;
            case 12:
                return 100;
            case 13:
                return 100;
            case 14:
                return 100;
            case 15:
                return 100;
            case 16:
                return 480;
            case 17:
                return 100;
            case 18:
                return 100;
            case 19:
                return 100;
            case 20:
                return 100;
            case 21:
                return 100;
            case 22:
                return 100;
            case 23:
                return 100;
            case 24:
                return 100;
            case 25:
                return 100;
            case 26:
                return 300;
            case 27:
                return 100;
            case 28:
                return 100;
            case 29:
                return 100;
            case 30:
                return 100;
            default:
                return 100;
        }

    }
    public static int Get_Gem_By_level(int _level)
    {
        switch (_level)
        {
            case 1:
                return 1;
            case 2:
                return 1;
            case 3:
                return 1;
            case 4:
                return 1;
            case 5:
                return 1;
            case 6:
                return 1;
            case 7:
                return 1;
            case 8:
                return 1;
            case 9:
                return 1;
            case 10:
                return 1;
            case 11:
                return 1;
            case 12:
                return 1;
            default:
                return 1;
        }

    }
    public static int Get_Id_Skin_Free_By_Level(int _level)
    {
        switch (_level)
        {
            case 10:
                return 1;
            case 20:
                return 6;
            case 30:
                return 5;
            default:
                return 4;
        }

    }

    //public readonly static string Scene_Lv_0 = "Level_0";
    //public readonly static string Scene_Lv_1 = "Level_1";
    //public readonly static string Scene_Lv_2 = "Level_2";
    //public readonly static string Scene_Lv_3 = "Level_3";
    //public readonly static string Scene_Lv_4 = "Level_4";
    //public readonly static string Scene_Lv_5 = "Level_5";
}
/*
hero1-giap
hero2-soi
hero3-mu
hero4-vang
hero5-chot
hero6-lua
hero7-bang
hero8-vong



```````//1 
        //2 h7
        //3 h6
        
        //4 h4
        //5 h3
        //6 h5
        //7
        //8
 * 
 * 
 * 
 * 
 * 
Before - After
0-0
1-1
2-5
3-4
4-6
5-3
6-2
7-7


1-1
2-2
3-6
4-5
5-7
6-4
7-3
8-8



order in layer
Floor: 20
Canvas: 500
Player Canvas: 501
Player: 100
Enemy: 50
Stroke: 60
Firework: 200
Blood: 200
skeleton Main Menu: 600
Canvas Pink Bank: 1000
Canvas Bound: 601
 [ContextMenu("test")]
Kiểm tra giữ chuột, Bắn Racast chỉ với các Colider 3D,

#if UNITY_EDITOR
        Debug.Log("=================");
#endif



 */

#region

#endregion
#region

#endregion
#region

#endregion
#region

#endregion
#region

#endregion
#region

#endregion



/*
  ammo_box_01
ammo_box_02
ammo_box_03
apartment_damaged_02_b
barracks_01_a
barracks_01_b
barrel_01
barrel_03
dirt_box_02
Map_01
Map_02
Map_03
Map_04
Map_05
palm_01
palm_02
Plane
power_pole
rock_brown_01
rubble_02
sandbags_01
wall_damaged_01
wall_damaged_02
wall_damaged_03

//Hệ số Gold By Dmg
    public static long GetFactor(long _level)
    {
        switch (_level)//TODO: dùng String này truy cập Resource nhận Prefabs
        {
            case 1:
                return 1;
            case 2:
                return 1;
            case 3:
                return 8;
            case 4:
                return 8;
            case 5:
                return 8;
            case 6:
                return 16;
            case 7:
                return 16;
            case 8:
                return 32;
            case 9:
                return 32;
            case 10:
                return 32;
            case 11:
                return 64;
            case 12:
                return 64;
            default:
                return 1;
        }

    }




public static int GetHealt_By_Type(TypeEnemy _type_enemy)
    {
        switch (_type_enemy)//TODO: dùng String này truy cập Resource nhận Prefabs
        {
            case TypeEnemy.Type1:
                return 10;
            case TypeEnemy.Type2:
                return 20;
            case TypeEnemy.Type3:
                return 20;
            default:
                return 10;
        }

    }

click player
    nhận First_Floor
        bật sáng các Floor enemy khác
            di chuyển chuột đến Floor Enemy khác bật xanh Inside các Floor đó
            di chuyển chuột đến Floor Enemy khác bật xanh Inside các Floor đó


                Debug.Log(floor_Light_First_Chose);
Debug.Log("d");























 */