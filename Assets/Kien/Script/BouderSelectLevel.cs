using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BouderSelectLevel : MonoBehaviour
{
    [HideInInspector]
    public SelectLevelController mySelectLevel;
    [SerializeField]
    GameObject lockObj, unlockObj;

    public int index;
    public void Display()
    {
        if (Datacontroller.instance.saveData.saveDelete.infoSaveDelete[index].unlock)
        {
            lockObj.SetActive(false);
            unlockObj.SetActive(true);
        }
        else
        {
            lockObj.SetActive(true);
            unlockObj.SetActive(false);
        }
    }
    public void BtnWatchAdsUnlock()
    {
#if UNITY_EDITOR
        Reward();
#else
        AdsManager.Instance.WatchRewardedAds(Reward, "video_show_unlock_delete_" + (index + 1));
#endif
    }
    void Reward()
    {
        Datacontroller.instance.saveData.saveDelete.infoSaveDelete[index].unlock = true;
        Display();
    }
    public void BtnPlay()
    {


        if (DataParam.currentLevel == index)
        {
            if (GameController.instance.levelController != null)
            {
                GameController.instance.levelController.gameObject.SetActive(true);
                Debug.LogError("================= hien thi lai level hien tai");
            }
            else
            {
                DataParam.currentLevel = index;
                GameController.instance.LoadLevel();
                Debug.LogError("================= load level moi len");
            }

        }
        else
        {
            if (GameController.instance.levelController != null)
            {
                Destroy(GameController.instance.levelController.gameObject);
                GameController.instance.levelController = null;
                Debug.LogError("================= xoa level hien tai di");
            }
            DataParam.currentLevel = index;
            GameController.instance.LoadLevel();
        }

        mySelectLevel.CloseMe();
    }
}
