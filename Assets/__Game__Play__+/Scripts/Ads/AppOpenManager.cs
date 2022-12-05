using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppOpenManager : MonoBehaviour
{
    public static AppOpenManager Instance;
    public static bool ResumeFromAds = false;
    private const string AppOpenAdUnitId = "969c7707cd170d45";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void LoadAOA()
    {
        MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
    }

    public void ShowAdIfReady()
    {
        if (Datacontroller.instance.noAdsTest)
            return;
        if (MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId))
        {
            MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
        }
        else
        {
            MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
        }
    }

    public IEnumerator ShowAtStart()
    {
        if (MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId))
        {
            if (!Datacontroller.instance.noAdsTest)
            {
                MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
            }
        }
        else
        {
            while (!MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId))
            {
                yield return null;
            }
            if (!Datacontroller.instance.noAdsTest)
            {
                MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
            }
        }
    }
}
