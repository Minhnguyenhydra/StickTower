using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    [SerializeField]
    private UnityAds unityAds;
   
    void Start()
    {
        unityAds.Init();

        DontDestroyOnLoad(this);
    }

    private void OnDisable()
    {
        unityAds.OnDestroy();
    }

    public void WatchAds()
    {
        unityAds.ShowRewarded();
    }

}
