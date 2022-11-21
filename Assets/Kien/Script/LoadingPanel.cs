﻿//using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Spine.Unity;
using Spine;

public class LoadingPanel : MonoBehaviour
{
    public Image fillImage;
    public AsyncOperation currentLoadingOperation = null;
    public string scenes;
    public Canvas myCanvas;
   // Datacontroller dataController;

    //  public Text countText;

    // public SkeletonGraphic sg;
    string pathLoadResources;

    public void OpenMe(string name)
    {
        //  dataController = Datacontroller.instance;
        myCanvas.worldCamera = Camera.main;
        fillImage.fillAmount = 0;
        SetUp(name);
        gameObject.SetActive(true);

    }
    public void SetUp(string name)
    {
        scenes = name;
        currentLoadingOperation = null;
        currentLoadingOperation = SceneManager.LoadSceneAsync(scenes);
        currentLoadingOperation.allowSceneActivation = false;

        //     Debug.LogError("========== fuck off");
    }

    private void FixedUpdate()
    {
        var deltaTime = Time.deltaTime;

        fillImage.fillAmount += deltaTime / 1.1f;
        //  countText.text = Mathf.RoundToInt(fillImage.fillAmount * 100) + "%";

        if (fillImage.fillAmount >= 1 /*&& DataParam.loadDoneLevel*//* && AdsController.checkLoadOpenAds*/)
        {
            if (!currentLoadingOperation.allowSceneActivation)
            {
                currentLoadingOperation.allowSceneActivation = true;
                //gameObject.SetActive(false);
            }
        }
    }

    //    private void Complete(TrackEntry trackEntry)
    //    {

    //        currentLoadingOperation.allowSceneActivation = true;

    //#if !UNITY_EDITOR
    //                        if (!showOpenAds)
    //                        {
    //                            AdsController.instance.ShowOpenAdsAfterLoading();
    //                            showOpenAds = true;
    //                        }
    //#endif

    //    }
}