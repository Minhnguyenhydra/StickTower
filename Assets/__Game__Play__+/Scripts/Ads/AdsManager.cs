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

    void Start()
    {
        applovin.Init();
    }

    public void WatchRewardedAds(CallBackAds cbAds)
    {
        applovin.ShowRewardedAds(cbAds);
    }

    public void WatchInterstitialAds(CallBackAds cbAds)
    {
        applovin.ShowInterstitialAds(cbAds);
    }

}
