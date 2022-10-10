using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    public bool isMusicOn = true;
    public bool isSoundOn = true;
    public bool isVibraOn = true;
    //
    public GameObject obj_On_Music;
    public GameObject obj_Off_Music;
    //
    public GameObject obj_On_Sound;
    public GameObject obj_Off_Sound;
    //
    public GameObject obj_On_Vibra;
    public GameObject obj_Off_Vibra;
    public Animator anim_Setting;

    private void OnEnable()
    {
        Init_Setting();
    }
    public void CloseButton()
    {
        anim_Setting.SetTrigger(Constant.Trigger_Setting_Close);
        StartCoroutine(IE_Delay_Downt());
        SoundManager.Ins.PlayFx(FxID.click);
    }

    IEnumerator IE_Delay_Downt()
    {
        yield return Cache.GetWFS(Constant.Time_Delay_Setting_Close);
        Close();
    }

    public void SoundButton()//FX
    {
        //isSoundOn = (PlayerPrefs_Manager.GetSetting_Sound() == 1);
        isSoundOn = (InitUserData_Gamplay.Ins.userData.fxIsOn == true);
        isSoundOn = !isSoundOn;
        ReloadUISettingSound(isSoundOn);
        if (isSoundOn)
        {
            //PlayerPrefs_Manager.SetSetting_Sound(1);
            InitUserData_Gamplay.Ins.userData.SetBoolData(UserData.Key_FxIsOn, ref InitUserData_Gamplay.Ins.userData.fxIsOn, true);
        }
        else
        {
            //PlayerPrefs_Manager.SetSetting_Sound(0);
            InitUserData_Gamplay.Ins.userData.SetBoolData(UserData.Key_FxIsOn, ref InitUserData_Gamplay.Ins.userData.fxIsOn, false);
        }
        //SoundManager.Ins.PlayFx(FxID.fireWork);
    }
    public void MusicButton()//Music
    {
        //isSoundOn = (PlayerPrefs_Manager.GetSetting_Music() == 1);
        isMusicOn = (InitUserData_Gamplay.Ins.userData.musicIsOn == true);
        isMusicOn = !isMusicOn;
        ReloadUISettingMusic(isMusicOn);
        if (isMusicOn)
        {
            //PlayerPrefs_Manager.SetSetting_Music(1);
            InitUserData_Gamplay.Ins.userData.SetBoolData(UserData.Key_MusicIsOn, ref InitUserData_Gamplay.Ins.userData.musicIsOn, true);
        }
        else
        {
            //PlayerPrefs_Manager.SetSetting_Music(0);
            InitUserData_Gamplay.Ins.userData.SetBoolData(UserData.Key_MusicIsOn, ref InitUserData_Gamplay.Ins.userData.musicIsOn, false);
        }
        SoundManager.Ins.PlayMusic(isMusicOn);
    }
    public void VibrationButton()
    {
        //isVibraOn = (PlayerPrefs_Manager.GetSetting_Vibra() == 1);
        isVibraOn = (InitUserData_Gamplay.Ins.userData.isVibraOn == true);
        isVibraOn = !isVibraOn;
        ReloadUISettingVibra(isVibraOn);
        if (isVibraOn)
        {
            //PlayerPrefs_Manager.SetSetting_Vibra(1);
            InitUserData_Gamplay.Ins.userData.SetBoolData(UserData.Key_isVibraOn, ref InitUserData_Gamplay.Ins.userData.isVibraOn, true);
        }
        else
        {
            //PlayerPrefs_Manager.SetSetting_Vibra(0);
            InitUserData_Gamplay.Ins.userData.SetBoolData(UserData.Key_isVibraOn, ref InitUserData_Gamplay.Ins.userData.isVibraOn, false);

        }
    }

    #region Setup
    public void Init_Setting()
    {
        if (InitUserData_Gamplay.Ins.userData.fxIsOn)
        {
            ReloadUISettingSound(true);
        }
        else
        {
            ReloadUISettingSound(false);
        }

        if (InitUserData_Gamplay.Ins.userData.musicIsOn)
        {
            ReloadUISettingMusic(true);
        }
        else
        {
            ReloadUISettingMusic(false);
        }

        if (InitUserData_Gamplay.Ins.userData.isVibraOn)
        {
            ReloadUISettingVibra(true);
        }
        else
        {
            ReloadUISettingVibra(false);
        }
    }

    void ReloadUISettingSound(bool isOn)
    {
        obj_On_Sound.SetActive(isOn);
        obj_Off_Sound.SetActive(!isOn);
    }

    void ReloadUISettingMusic(bool isOn)
    {
        obj_On_Music.SetActive(isOn);
        obj_Off_Music.SetActive(!isOn);
    }

    void ReloadUISettingVibra(bool isOn)
    {
        obj_On_Vibra.SetActive(isOn);
        obj_Off_Vibra.SetActive(!isOn);
    }

    #endregion
    public void YouTobe_Button()
    {

    }
    public void FakeBook_Button()
    {

    }
}
