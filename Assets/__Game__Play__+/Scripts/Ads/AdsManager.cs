using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AppLovinAds;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;


    [SerializeField]
    private AppLovinAds applovin;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    public void WatchRewardedAds(CallBackAds cbAds)
    {
        Debug.Log("Clicked Rewarded Ads");
        applovin.ShowRewardedAds(cbAds);
        Debug.Log("Rewarded Ads Displayed");
    }

    public void WatchInterstitialAds(CallBackAds cbAds)
    {
        Debug.Log("Clicked Interstitial Ads");
        applovin.ShowInterstitialAds(cbAds);
        Debug.Log("Interstitial Ads Displayed");
    }

}
