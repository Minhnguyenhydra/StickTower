using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppOpenManager : MonoBehaviour
{
    public static AppOpenManager Instance;
    public static bool ResumeFromAds = false;
    private const string AppOpenAdUnitId = "89f77459501f3b6d";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowAdIfReady()
    {
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
            MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
        }
        else
        {
            MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
            while (!MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId))
            {
                yield return null;
            }
            MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
        }
    }
}
