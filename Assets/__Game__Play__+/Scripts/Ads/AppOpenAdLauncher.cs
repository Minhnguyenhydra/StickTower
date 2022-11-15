using System;
using GoogleMobileAds.Api;

public class AppOpenAdLauncher : Singleton<AppOpenAdLauncher>
{
    private static AppOpenAdLauncher Instance;

    protected void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;

        MobileAds.Initialize(status => { AppOpenAdManager.Instance.LoadAd(); });
    }


    private void OnApplicationPause(bool pause)
    {
        if (!pause && AppOpenAdManager.ConfigResumeApp && !AppOpenAdManager.ResumeFromAds)
        {
            AppOpenAdManager.Instance.ShowAdIfAvailable();
        }
    }
}