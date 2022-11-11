#if WatchADs
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppLovinAds : MonoBehaviour
{
    private const string MaxSdkKey = "7PspscCcbGd6ohttmPcZTwGmZCihCW-Jwr7nSJN2a_9Mg0ERPs0tmGdKTK1gs__nr6XHQvK0vTNaTb1uR1mCIN";

    [SerializeField]
    private string bannerAdUnitId = "a36b1af08986430c"; // Retrieve the ID from your account

    public delegate void CallBackAds();
    private CallBackAds callBackAds;
    private Coroutine corShowBanner;

    private void Start()
    {

        MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
        {
            // AppLovin SDK is initialized, configure and start loading ads.
            Debug.Log("MAX SDK Initialized");

            InitializeBannerAds();
            InitializeInterstitialAds();
            InitializeRewardedAds();

            //// Initialize Adjust SDK
            //AdjustConfig adjustConfig = new AdjustConfig("YourAppToken", AdjustEnvironment.Sandbox);
            //Adjust.start(adjustConfig);
        };

        MaxSdk.SetSdkKey(MaxSdkKey);
        MaxSdk.SetTestDeviceAdvertisingIdentifiers(new string[] { "2010f6b6-0585-4646-b7b4-e6291e3ea12b", "c23c7f01-c1c5-4c46-ae67-44b77b961672" });
        MaxSdk.InitializeSdk();
        ShowBannerAfter10S();
    }

    public void ShowBannerAfter10S()
    {
        if (corShowBanner != null)
            StopCoroutine(corShowBanner);

        corShowBanner = StartCoroutine(ShowBannerAd());
    }

#region Ads Banner
    public void InitializeBannerAds()
    {
        // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
        // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
        MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
        //MaxSdk.SetBannerExtraParameter(bannerAdUnitId, "adaptive_banner", "true");
        //Debug.Log(MaxSdkUtils.GetAdaptiveBannerHeight());

        // Set background or background color for banners to be fully functional
        MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, Color.white);

        MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdLoadFailedEvent;
        MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;
        MaxSdkCallbacks.Banner.OnAdExpandedEvent += OnBannerAdExpandedEvent;
        MaxSdkCallbacks.Banner.OnAdCollapsedEvent += OnBannerAdCollapsedEvent;
    }

    private IEnumerator ShowBannerAd()
    {
        yield return new WaitForSeconds(10f);
        MaxSdk.ShowBanner(bannerAdUnitId);
    }

    private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {
        Debug.Log("Banner Ad Loaded");
    }

    private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo) 
    {
        Debug.Log("Banner Ad Load Fail");
    }

    private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {
        Debug.Log("Banner Ad Clicked");
    }

    private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    { 

    }

    private void OnBannerAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    { 

    }

    private void OnBannerAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {

    }
#endregion

#region Ads Interstitials
    [SerializeField]
    private string adUnitId = "158fc1f844c580a1";
    int retryAttempt;

    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;
        //MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialRevenuePaidEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    

    private void LoadInterstitial()
    {
        Debug.Log("Interstitial Loading...");
        MaxSdk.LoadInterstitial(adUnitId);
    }

    public void ShowInterstitialAds(CallBackAds cbAds)
    {
        if (MaxSdk.IsInterstitialReady(adUnitId))
        {
            AppOpenAdManager.ResumeFromAds = true;
            callBackAds = cbAds;

            Debug.Log("Interstitial Showing...");
            MaxSdk.ShowInterstitial(adUnitId);
        }
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

        // Reset retry attempt
        retryAttempt = 0;
        Debug.Log("Interstitial Ad Loaded");
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));

        Invoke("LoadInterstitial", (float)retryDelay);
        Debug.Log("Interstitial Ad Load Fail");
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {
        Debug.Log("Interstitial Ad Displayed");
    }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        LoadInterstitial();
        Debug.Log("Interstitial Ad Display Fail");
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {
        Debug.Log("Interstitial Ad Clicked--------------");
    }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
        LoadInterstitial();

        AppOpenAdManager.ResumeFromAds = false;
        callBackAds?.Invoke();
        Debug.Log("Interstitial Ad Hidden");

    }
    private void OnInterstitialRevenuePaidEvent(string arg1, MaxSdkBase.AdInfo arg2)
    {
        Debug.Log("Interstitial Revenue Paid");
    }
#endregion

#region Ads Rewarded
    [SerializeField]
    private string adUnitRewardedId = "8152195bceed477e";
    int retryAttempt_Rewarded;

    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first rewarded ad
        LoadRewardedAd();
    }

    private void LoadRewardedAd()
    {
        Debug.Log("Rewarded Loading...");
        MaxSdk.LoadRewardedAd(adUnitRewardedId);
    }

    public void ShowRewardedAds(CallBackAds cbAds)
    {
        if (MaxSdk.IsRewardedAdReady(adUnitRewardedId))
        {
            callBackAds = cbAds;
            AppOpenAdManager.ResumeFromAds = true;
            Debug.Log("Rewarded Showing...");
            MaxSdk.ShowRewardedAd(adUnitRewardedId);
        }
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        retryAttempt_Rewarded = 0;
        Debug.Log("Rewarded Ad Loaded");
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        retryAttempt_Rewarded++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt_Rewarded));

        Invoke("LoadRewardedAd", (float)retryDelay);
        Debug.Log("Rewarded Ad Load Fail");
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {
        Debug.Log("Rewarded Ad Displayed");
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        LoadRewardedAd();
        Debug.Log("Rewarded Ad Display Fail");
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {
        Debug.Log("Rewarded Ad Clicked-------------");
    }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd();
        AppOpenAdManager.ResumeFromAds = false;
        Debug.Log("Rewarded Ad Hidden");
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // The rewarded ad displayed and the user should receive the reward.
        callBackAds?.Invoke();
        Debug.Log("Rewarded Ad ReceivedReward");
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Ad revenue paid. Use this callback to track user revenue.
    }
#endregion


}
#endif