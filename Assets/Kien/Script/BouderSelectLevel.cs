using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BouderSelectLevel : MonoBehaviour
{
    [HideInInspector]
    public SelectLevelController mySelectLevel;
    [SerializeField]
     GameObject btnWatchAds, btnPlay;
    [SerializeField]
    Text levelText;
    public int index;
    public void Display()
    {
        levelText.text = "Level " + (index + 1);
    }
    public void BtnWatchAdsUnlock()
    {

    }
    public void BtnPlay()
    {
        DataParam.currentLevel = index;
        mySelectLevel.CloseMe();
    }
}
