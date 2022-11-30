﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.Networking;
using static GetMoreGame;
using System.Threading;
using System.Globalization;
using System;
using static SaveData;
using static SaveDelete;


[System.Serializable]
public class SaveData
{
    public int session, day = 0, week = 1;
    public DateTime oldDay = System.DateTime.Now;
    public SaveDelete saveDelete = new SaveDelete();
}
[System.Serializable]
public class SaveDelete
{
    public List<InfoSaveDelete> infoSaveDelete = new List<InfoSaveDelete>();
    [System.Serializable]
    public class InfoSaveDelete
    {
        public List<bool> takeSprite = new List<bool>();
        public bool unlock;
        public int currentStep = 0;
    }
}
[System.Serializable]
public class SaveMoreGame
{
    public List<int> indexClicked = new List<int>();
}
[System.Serializable]
public class GetMoreGame
{
    public List<InfoMoreGame> infoMoreGame = new List<InfoMoreGame>();
    [System.Serializable]
    public class InfoMoreGame
    {
        public int index;
        public string nameGame;
        public string link;
        public string linkIcon;
        public string packageName;
        public Texture2D myTexture;
    }
}
public class Datacontroller : MonoBehaviour
{
    public bool debug;
    public SaveData saveData;

    public DeleteData deleteData;
    public static Datacontroller instance;
    string urlMoreGame;
    string urlLevel;

    public string devAndroid, devkeyIos;
    public string urlLevelAndroid, urlLevelIOS;
    public string appIDIos;
    [SerializeField]
    LoadingPanel loadingPanel;
    string pathPopUp;

   public void ShowLoadingPanel(bool openScene, string Scene)
    {
        if (openScene)
        {
            if (loadingPanel == null)
            {
                pathPopUp = "PopUp/Loading";
                loadingPanel = Instantiate(Resources.Load<LoadingPanel>(pathPopUp));
                loadingPanel.transform.parent = transform;
                loadingPanel.OpenMe(Scene);
            }
            else
            {
                loadingPanel.OpenMe(Scene);
            }
        }
        else
        {
            if (loadingPanel != null)
                loadingPanel.gameObject.SetActive(false);
        }
    }
    public void TakePartDelete(int indexSource, int indexPart)
    {
        saveData.saveDelete.infoSaveDelete[indexSource].takeSprite[indexPart] = true;
    }
    private void Awake()
    {
        if (instance == null)
        {
              Application.targetFrameRate = 60;
            Debug.unityLogger.logEnabled = debug;
            //    Input.multiTouchEnabled = false;
            CultureInfo ci = new CultureInfo("en-us");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAllData();
        }
        else
        {
            DestroyImmediate(gameObject);
        }

        //#if UNITY_IOS
        //       // urlMoreGame = urlIos;
        //        urlLevel = urlLevelIOS;
        //#else
        //        //  urlMoreGame = urlAdroid;
        //        urlLevel = urlLevelAndroid;
        //#endif

        //        wwwLevel = UnityWebRequest.Get(urlLevel);

        Debug.LogError("============== zo day phai ko");
    }
    UnityWebRequest wwwLevel;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        ShowLoadingPanel("DeleteScene");
    //    }
    //}


    void ReadPlayer(string value)
    {

        strDataLoadPref = null;
        strDataLoadPref = value;
        if (!string.IsNullOrEmpty(strDataLoadPref) && strDataLoadPref != "" && strDataLoadPref != "[]")
        {
            saveData = JsonMapper.ToObject<SaveData>(strDataLoadPref);
        }
    }

    void LoadData(string value)
    {
        saveData = new SaveData();
        saveData.saveDelete = new SaveDelete();
        ReadPlayer(value);

    }
    void LoadAllData()
    {
        LoadData(PlayerPrefs.GetString(DataParam.SAVEDATA));
        //  LoadSaveMoreGame(PlayerPrefs.GetString(DataParam.SAVEMOREGAME));
        saveData.session++;


    }
    void CreateDataBegin()
    {
        Debug.LogError(saveData + ":" + saveData.saveDelete);
        for (int i = saveData.saveDelete.infoSaveDelete.Count; i < deleteData.infoDelete.Length; i++)
        {
            InfoSaveDelete _infoDelete = new InfoSaveDelete();
            if (i <= 1)
            {
                _infoDelete.unlock = true;
            }
            for (int j = 0; j < deleteData.infoDelete[i].resourceSprite.sp.Length; j++)
            {
                bool takeSprite = new bool();
                _infoDelete.takeSprite.Add(takeSprite);
            }
            saveData.saveDelete.infoSaveDelete.Add(_infoDelete);
        }
    }

    void Start()
    {

        CreateDataBegin();

        //Debug.LogError("=======" + urlMoreGame);
        //WWW www = new WWW(urlMoreGame);

        //StartCoroutine(WaitForRequest(www));
        //  StartCoroutine(WaitForRequestLevel(wwwLevel));
        //  LoadSaveMoreGame(PlayerPrefs.GetString(DataParam.SAVEMOREGAME));

    }
    //IEnumerator WaitForRequest(WWW www)
    //{
    //    yield return www;

    //    // check for errors

    //    if (www.error == null)
    //    {
    //        //  Debug.LogError("=====WWW MoreGame!: " + www.text);
    //        //jData = JsonMapper.ToObject(www.text);
    //        //if (jData != null)
    //        //{
    //        getMoreGame = JsonMapper.ToObject<GetMoreGame>(www.text/*jData.ToJson()*/);
    //        //}
    //    }
    //    else
    //    {
    //        //   Debug.Log("======WWW Error MoreGame: " + www.error);
    //    }

    //    for (int i = 0; i < getMoreGame.infoMoreGame.Count; i++)
    //    {
    //        SetImage(getMoreGame.infoMoreGame[i].linkIcon, i);
    //    }
    //}

    //IEnumerator WaitForRequestLevel(UnityWebRequest www)
    //{
    //    yield return www.SendWebRequest();

    //    if (www.isNetworkError || www.isHttpError)
    //    {
    //        Debug.Log("======WWW Error Level: " + www.error);
    //    }
    //    else
    //    {
    //        //  DataParam.wwwLevel = www.downloadHandler.text;
    //        Debug.LogError("=====WWW Level!: " + www.downloadHandler.text);
    //    }


    //}

    static bool calculate;
    GetMoreGame getMoreGame = new GetMoreGame();
    public void CalculateListMoreGameBegin()
    {
        if (calculate)
            return;
        for (int i = 0; i < getMoreGame.infoMoreGame.Count; i++)
        {
            if (!saveMoreGame.indexClicked.Contains(getMoreGame.infoMoreGame[i].index) && getMoreGame.infoMoreGame[i].packageName != Application.identifier && getMoreGame.infoMoreGame[i].myTexture != null)
            {
                moreGameLst.infoMoreGame.Add(getMoreGame.infoMoreGame[i]);
            }
            if (getMoreGame.infoMoreGame[i].packageName != Application.identifier && getMoreGame.infoMoreGame[i].myTexture != null)
            {
                moreGameLstToComingSoon.infoMoreGame.Add(getMoreGame.infoMoreGame[i]);
            }
        }
        calculate = true;
    }
    //public void SetImage(string url, int i)
    //{
    //    StartCoroutine(DownloadImage(url, i));
    //}
    //Texture2D texture;
    //IEnumerator DownloadImage(string url, int i)
    //{
    //    UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
    //    yield return request.SendWebRequest();
    //    if (request.isNetworkError || request.isHttpError)
    //        Debug.LogError("======" + request.error);
    //    else
    //    {
    //        texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    //        getMoreGame.infoMoreGame[i].myTexture = texture;
    //        Debug.LogError("======" + "texture ok");
    //    }
    //}
    void CreateTestMoreGame()
    {
        getMoreGame.infoMoreGame.Clear();
        for (int i = 0; i < 3; i++)
        {
            InfoMoreGame _getMoreGame = new InfoMoreGame();
            _getMoreGame.index = i;
            _getMoreGame.nameGame = "Game" + (i + 1);
            getMoreGame.infoMoreGame.Add(_getMoreGame);
        }
        string moregame = JsonMapper.ToJson(getMoreGame);
        Debug.LogError(moregame);
    }
    void LoadSaveMoreGame(string value)
    {
        ReadSaveMoreGame(value);
    }
    string strDataLoadPref;
    void ReadSaveMoreGame(string value)
    {
        strDataLoadPref = null;
        strDataLoadPref = value;

        if (!string.IsNullOrEmpty(strDataLoadPref) && strDataLoadPref != "" && strDataLoadPref != "[]")
        {
            // Debug.LogError(strDataLoadPref);
            //  jData = JsonMapper.ToObject(strDataLoadPref);
            //   if (jData != null)
            saveMoreGame = JsonMapper.ToObject<SaveMoreGame>(strDataLoadPref/*jData.ToJson()*/);
        }
    }


    public GetMoreGame moreGameLst = new GetMoreGame();
    public GetMoreGame moreGameLstToComingSoon = new GetMoreGame();
    public SaveMoreGame saveMoreGame = new SaveMoreGame();
    public string urlAdroid, urlIos;

    JsonData jData;
    string tempsaveData, tempsaveMoreGame;
    void SetSaveTemp()
    {
        tempsaveData = JsonMapper.ToJson(saveData);
        tempsaveMoreGame = JsonMapper.ToJson(saveMoreGame);
    }
    void SaveData()
    {
        SetSaveTemp();
        PlayerPrefs.SetString(DataParam.SAVEDATA, tempsaveData);
        //   PlayerPrefs.SetString(DataParam.SAVEMOREGAME, tempsaveMoreGame);
        PlayerPrefs.Save();
        // Debug.LogError("===save data===");
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveData();
            //  Debug.LogError("======!focus");
        }
        //else
        //{
        //    Debug.LogError("======focus");
        //    if (DataParam.afterShowAds)
        //    {
        //        DataParam.afterShowAds = false;
        //        Debug.LogError("======after show ads");
        //    }
        //    else
        //    {
        //      //  Debug.LogError("======can show ad here");
        //        if (AdsController.instance != null)
        //            AdsController.instance.ShowOpenAdsAfterChangeApp();
        //        // ShowInter();
        //    }
        //}
    }

    //public void ShowInter()
    //{


    //    if (saveData.removeAds)
    //        return;

    //    DataParam.lastShowInter = System.DateTime.Now;
    //    if ((DataParam.lastShowInter - DataParam.beginShowInter).TotalSeconds > DataParam.timeDelayShowAds)
    //    {
    //        if (AdsController.instance != null)
    //            AdsController.instance.ShowInter();
    //        EventController.AF_INTERS_AD_ELIGIBLE();
    //    }

    //}
}
