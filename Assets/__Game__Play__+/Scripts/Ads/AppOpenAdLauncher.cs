using System;
using GoogleMobileAds.Api;

public class AppOpenAdLauncher : Singleton<AppOpenAdLauncher>
{

    private bool isAwake;
    protected void Awake()
    {
        //base.Awake();

        MobileAds.Initialize(status => { AppOpenAdManager.Instance.LoadAd(); });
        isAwake = true;
    }


    private void OnApplicationPause(bool pause)
    {
        if (isAwake == true)
        {
            isAwake = false;
            return;
        }

        if (!pause && AppOpenAdManager.ConfigResumeApp && !AppOpenAdManager.ResumeFromAds)
        {
            AppOpenAdManager.Instance.ShowAdIfAvailable();
        }
    }
}