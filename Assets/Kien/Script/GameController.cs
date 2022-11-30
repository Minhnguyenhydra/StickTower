using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public SelectLevelController selectLevelController;
    ResultPanel resultPanel;
    [SerializeField]
    Transform canvasParent, canvasParent1;
    [SerializeField]
    GameObject effectWin;
    //[SerializeField]
    //LoadingPanel loading;
    public void BackToLoading()
    {
        //loading.OpenMe();
        Datacontroller.instance.ShowLoadingPanel(true, "Loading");
    }
    private void Awake()
    {
        instance = this;
    }
    public void ShowPopUpResult()
    {
        pathPopUp = "PopUp/ResultPanel";
        resultPanel = Instantiate(Resources.Load<ResultPanel>(pathPopUp));
        resultPanel.SetRect(canvasParent1);
        resultPanel.OpenMe();
        effectWin.SetActive(true);
        eraser.SetActive(false);
    }
    void ShowSelectLevelPanel()
    {
        pathPopUp = "PopUp/SelectLevelPanel";
        selectLevelController = Instantiate(Resources.Load<SelectLevelController>(pathPopUp));
        selectLevelController.SetRect(canvasParent);
        selectLevelController.OpenMe();
        selectLevelController.Display();
    }
    //  Vector2 originalPosErase;
    private void Start()
    {
        DataParam.canDelete = true;
        ShowSelectLevelPanel();
        Datacontroller.instance.ShowLoadingPanel(false, "");
        // originalPosErase = eraser.transform.position;
    }
    public void ShowPopUpWatchAds()
    {
        if (popUpWatchAds == null)
        {
            pathPopUp = "PopUp/PopUpWatchAds";
            popUpWatchAds = Instantiate(Resources.Load<PopUpWatchAds>(pathPopUp));
            popUpWatchAds.SetRect(canvasParent1);
            popUpWatchAds.OpenMe();

        }
        else
        {
            popUpWatchAds.OpenMe();
        }
        eraser.SetActive(false);
        DataParam.canDelete = false;
    }
    public void LoadLevel()
    {
        pathLevel = "LevelDelete/Level" + (DataParam.currentLevel + 1);
        Debug.LogError(pathLevel);
        level = Resources.Load<LevelController>(pathLevel);
        if (level != null)
        {
            levelController = Instantiate(level);
            //   levelController.DisplayLockOrUnlock();
        }
        else
        {
            DataParam.currentLevel = 0;
           // Application.LoadLevel(Application.loadedLevelName);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}

