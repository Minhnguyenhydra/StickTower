using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject eraser;
    [HideInInspector]
    public LevelController levelController;
    LevelController level;
    string pathLevel = "LevelDelete/Level";
    string pathPopUp = "PopUp/";
    PopUpWatchAds popUpWatchAds;
    SelectLevelController selectLevelController;
    ResultPanel resultPanel;
    [SerializeField]
    Transform canvasParent;
    [SerializeField]
    GameObject effectWin;
    private void Awake()
    {
        instance = this;
    }
    public void ShowPopUpWin()
    {
        pathPopUp = "PopUp/ResultPanl";
        resultPanel = Instantiate(Resources.Load<ResultPanel>(pathPopUp));
        resultPanel.SetRect(canvasParent);
        resultPanel.OpenMe();
        effectWin.SetActive(true);
    }
    void ShowSelectLevelPanel()
    {
        pathPopUp = "PopUp/SelectLevelPanel";
        selectLevelController = Instantiate(Resources.Load<SelectLevelController>(pathPopUp));
        selectLevelController.SetRect(canvasParent);
        selectLevelController.OpenMe();
    }
    private void Start()
    {
        DataParam.canDelete = true;
        ShowSelectLevelPanel();
    }
    public void ShowPopUpWatchAds()
    {
        if (popUpWatchAds == null)
        {
            pathPopUp = "PopUp/PopUpWatchAds";
            popUpWatchAds = Instantiate(Resources.Load<PopUpWatchAds>(pathPopUp));
            popUpWatchAds.SetRect(canvasParent);
            popUpWatchAds.OpenMe();
        }
        else
        {
            popUpWatchAds.OpenMe();
        }
        DataParam.canDelete = false;
    }
    public void LoadLevel()
    {
        pathLevel += (DataParam.currentLevel + 1);
        Debug.LogError(pathLevel);
        level = Resources.Load<LevelController>(pathLevel);
        if (level != null)
        {
            levelController = Instantiate(level);
        }
        else
        {
            DataParam.currentLevel = 0;
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
}

