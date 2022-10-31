using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

public class UnityAds : MonoBehaviour
{
    [SerializeField]
    private string banner_androidAdUnitId;
    [SerializeField]
    private string banner_iosAdUnitId;

    [SerializeField]
    private string rewarded_androidAdUnitId;
    [SerializeField]
    private string rewarded_iosAdUnitId;

    [SerializeField]
    private string interstitial_androidAdUnitId;
    [SerializeField]
    private string interstitial_iosAdUnitId;

    private string iosGameId;
    private string androidGameId;

    [Header("Banner options")]
    public BannerAdAnchor bannerAdAnchor = BannerAdAnchor.TopCenter;

    public BannerAdPredefinedSize bannerSize = BannerAdPredefinedSize.Banner;

    IBannerAd m_BannerAd;
    IRewardedAd m_RewardedAd;
    IInterstitialAd m_InterstitialAd;


    public async void Init()
    {
        #region Init Banner Ads
        //try
        //{
        //    Debug.Log("Banner Initializing...");
        //    await UnityServices.InitializeAsync(GetGameId());
        //    Debug.Log("Banner Initialized!");

        //    Banner_InitializationComplete();
        //}
        //catch (Exception e)
        //{
        //    Banner_InitializationFailed(e);
        //}
        #endregion

        #region Init Reward Ads
        try
        {
            Debug.Log("Reward Initializing...");
            await UnityServices.InitializeAsync();
            Debug.Log("Reward Initialized!");
            Rewarded_InitializationComplete();
        }
        catch (Exception e)
        {
            Rewarded_InitializationFailed(e);
        }
        #endregion

        #region Init Interstitial Ads
        try
        {
            Debug.Log("Interstitial Initializing...");
            await UnityServices.InitializeAsync();
            Debug.Log(" Interstitial Initialized!");
            Interstitial_InitializationComplete();
        }
        catch (Exception e)
        {
            Interstitial_InitializationFailed(e);
        }
        #endregion
    }

    public void OnDestroy()
    {
        m_BannerAd?.Dispose();
        m_RewardedAd?.Dispose();
        m_InterstitialAd?.Dispose();
    }

    #region Banner Ads
    InitializationOptions GetGameId()
    {
        var initializationOptions = new InitializationOptions();

#if UNITY_IOS
                if (!string.IsNullOrEmpty(iosGameId))
                {
                    initializationOptions.SetGameId(iosGameId);
                }
#elif UNITY_ANDROID
        if (!string.IsNullOrEmpty(androidGameId))
        {
            initializationOptions.SetGameId(androidGameId);
        }
#endif

        return initializationOptions;
    }

    void Banner_InitializationComplete()
    {
        // Impression Event
        MediationService.Instance.ImpressionEventPublisher.OnImpression += Banner_ImpressionEvent;

        var bannerAdSize = bannerSize.ToBannerAdSize();
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                m_BannerAd = MediationService.Instance.CreateBannerAd(banner_androidAdUnitId, bannerAdSize, bannerAdAnchor);
                break;
            case RuntimePlatform.IPhonePlayer:
                m_BannerAd = MediationService.Instance.CreateBannerAd(banner_iosAdUnitId, bannerAdSize, bannerAdAnchor);
                break;
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.LinuxEditor:
                m_BannerAd = MediationService.Instance.CreateBannerAd(!string.IsNullOrEmpty(banner_androidAdUnitId) ? banner_androidAdUnitId : banner_iosAdUnitId, bannerAdSize, bannerAdAnchor);
                break;
            default:
                Debug.LogWarning("Mediation service is not available for this platform:" + Application.platform);
                return;
        }

        Debug.Log("Initialized On Start! Loading banner Ad...");
        Banner_LoadAd();
    }

    async void Banner_LoadAd()
    {
        try
        {
            await m_BannerAd.LoadAsync();
            Banner_AdLoaded();
        }
        catch (LoadFailedException e)
        {
            Banner_AdFailedLoad(e);
        }
    }

    void Banner_AdLoaded()
    {
        Debug.Log("Ad loaded");
    }

    void Banner_AdFailedLoad(LoadFailedException e)
    {
        Debug.Log("Failed to load ad");
        Debug.Log(e.Message);
    }

    void Banner_ImpressionEvent(object sender, ImpressionEventArgs args)
    {
        var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
        Debug.Log($"Impression event from ad unit id {args.AdUnitId} : {impressionData}");
    }

    void Banner_InitializationFailed(Exception error)
    {
        var initializationError = SdkInitializationError.Unknown;
        if (error is InitializeFailedException initializeFailedException)
        {
            initializationError = initializeFailedException.initializationError;
        }

        Debug.Log($"Initialization Failed: {initializationError}:{error.Message}");
    }
    #endregion

    #region Reward Ads
    public async void ShowRewarded()
    {
        if (m_RewardedAd?.AdState == AdState.Loaded)
        {
            try
            {
                RewardedAdShowOptions showOptions = new RewardedAdShowOptions();
                showOptions.AutoReload = true;
                await m_RewardedAd.ShowAsync(showOptions);
                Debug.Log("Rewarded Shown!");
            }
            catch (ShowFailedException e)
            {
                Debug.LogWarning($"Rewarded failed to show: {e.Message}");
            }
        }
    }

    public async void ShowRewardedWithOptions()
    {
        if (m_RewardedAd?.AdState == AdState.Loaded)
        {
            try
            {
                //Here we provide a user id and custom data for server to server validation.
                RewardedAdShowOptions showOptions = new RewardedAdShowOptions();
                showOptions.AutoReload = true;
                S2SRedeemData s2SData;
                s2SData.UserId = "my cool user id";
                s2SData.CustomData = "{\"reward\":\"Gems\",\"amount\":20}";
                showOptions.S2SData = s2SData;

                await m_RewardedAd.ShowAsync(showOptions);
                Debug.Log("Rewarded Shown!");
            }
            catch (ShowFailedException e)
            {
                Debug.LogWarning($"Rewarded failed to show: {e.Message}");
            }
        }
    }

    async void Rewarded_LoadAd()
    {
        try
        {
            await m_RewardedAd.LoadAsync();
        }
        catch (LoadFailedException)
        {
            // We will handle the failure in the OnFailedLoad callback
        }
    }

    void Rewarded_InitializationComplete()
    {
        // Impression Event
        MediationService.Instance.ImpressionEventPublisher.OnImpression += Rewarded_ImpressionEvent;

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                m_RewardedAd = MediationService.Instance.CreateRewardedAd(rewarded_androidAdUnitId);
                break;

            case RuntimePlatform.IPhonePlayer:
                m_RewardedAd = MediationService.Instance.CreateRewardedAd(rewarded_iosAdUnitId);
                break;
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.LinuxEditor:
                m_RewardedAd = MediationService.Instance.CreateRewardedAd(!string.IsNullOrEmpty(rewarded_androidAdUnitId) ? rewarded_androidAdUnitId : rewarded_iosAdUnitId);
                break;
            default:
                Debug.LogWarning("Mediation service is not available for this platform:" + Application.platform);
                return;
        }

        // Load Events
        m_RewardedAd.OnLoaded += Rewarded_AdLoaded;
        m_RewardedAd.OnFailedLoad += Rewarded_AdFailedLoad;

        // Show Events
        m_RewardedAd.OnUserRewarded += UserRewarded;
        m_RewardedAd.OnClosed += Rewarded_AdClosed;

        Debug.Log($"Initialized On Start. Loading Ad...");

        Rewarded_LoadAd();
    }

    void Rewarded_InitializationFailed(Exception error)
    {
        SdkInitializationError initializationError = SdkInitializationError.Unknown;
        if (error is InitializeFailedException initializeFailedException)
        {
            initializationError = initializeFailedException.initializationError;
        }
        Debug.Log($"Initialization Failed: {initializationError}:{error.Message}");
    }

    void UserRewarded(object sender, RewardEventArgs e)
    {
        Debug.Log($"User Rewarded! Type: {e.Type} Amount: {e.Amount}");
    }

    void Rewarded_AdClosed(object sender, EventArgs args)
    {
        Debug.Log("Rewarded Closed! Loading Ad...");
    }

    void Rewarded_AdLoaded(object sender, EventArgs e)
    {
        Debug.Log("Ad loaded");
    }

    void Rewarded_AdFailedLoad(object sender, LoadErrorEventArgs e)
    {
        Debug.Log("Failed to load ad");
        Debug.Log(e.Message);
    }

    void Rewarded_ImpressionEvent(object sender, ImpressionEventArgs args)
    {
        var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
        Debug.Log($"Impression event from ad unit id {args.AdUnitId} : {impressionData}");
    }
    #endregion

    #region Interstitial
    public async void ShowInterstitial()
    {
        if (m_InterstitialAd?.AdState == AdState.Loaded)
        {
            try
            {
                InterstitialAdShowOptions showOptions = new InterstitialAdShowOptions();
                showOptions.AutoReload = true;
                await m_InterstitialAd.ShowAsync(showOptions);
                Debug.Log("Interstitial Shown!");
            }
            catch (ShowFailedException e)
            {
                Debug.Log($"Interstitial failed to show : {e.Message}");
            }
        }
    }

    async void Interstitial_LoadAd()
    {
        try
        {
            await m_InterstitialAd.LoadAsync();
        }
        catch (LoadFailedException)
        {
            // We will handle the failure in the OnFailedLoad callback
        }
    }

    void Interstitial_InitializationComplete()
    {
        // Impression Event
        MediationService.Instance.ImpressionEventPublisher.OnImpression += Interstitial_ImpressionEvent;

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                m_InterstitialAd = MediationService.Instance.CreateInterstitialAd(interstitial_androidAdUnitId);
                break;

            case RuntimePlatform.IPhonePlayer:
                m_InterstitialAd = MediationService.Instance.CreateInterstitialAd(interstitial_iosAdUnitId);
                break;
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.LinuxEditor:
                m_InterstitialAd = MediationService.Instance.CreateInterstitialAd(!string.IsNullOrEmpty(interstitial_androidAdUnitId) ? interstitial_androidAdUnitId : interstitial_iosAdUnitId);
                break;
            default:
                Debug.LogWarning("Mediation service is not available for this platform:" + Application.platform);
                return;
        }

        // Load Events
        m_InterstitialAd.OnLoaded += Interstitial_AdLoaded;
        m_InterstitialAd.OnFailedLoad += Interstitial_AdFailedLoad;

        // Show Events
        m_InterstitialAd.OnClosed += Interstitial_AdClosed;

        Debug.Log("Initialized On Start! Loading Ad...");
        Interstitial_LoadAd();
    }

    void Interstitial_InitializationFailed(Exception error)
    {
        SdkInitializationError initializationError = SdkInitializationError.Unknown;
        if (error is InitializeFailedException initializeFailedException)
        {
            initializationError = initializeFailedException.initializationError;
        }
        Debug.Log($"Initialization Failed: {initializationError}:{error.Message}");
    }

    void Interstitial_AdClosed(object sender, EventArgs args)
    {
        Debug.Log("Interstitial Closed! Loading Ad...");
    }

    void Interstitial_AdLoaded(object sender, EventArgs e)
    {
        Debug.Log("Ad loaded");
    }

    void Interstitial_AdFailedLoad(object sender, LoadErrorEventArgs e)
    {
        Debug.Log("Failed to load ad");
        Debug.Log(e.Message);
    }

    void Interstitial_ImpressionEvent(object sender, ImpressionEventArgs args)
    {
        var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
        Debug.Log($"Impression event from ad unit id {args.AdUnitId} : {impressionData}");
    }
    #endregion

}