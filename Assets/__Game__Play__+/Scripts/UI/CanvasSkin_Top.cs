using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CanvasSkin_Top : UICanvas
{
    public Text txt_Gold;
    public Text txt_Gem;
    private void OnEnable()
    {
        Set_Refresh_Gold_Gem();
    }
    public void Homebutton()
    {
        SoundManager.Ins.PlayFx(FxID.click);

        StartCoroutine(LoadHomeScene());
    }
    private IEnumerator LoadHomeScene()
    {
        UIManager.Ins.OpenUI(UIID.UICFade);
        ((CanvasFade)UIManager.Ins.GetUI(UIID.UICFade)).Set_Fade_Out();

        AsyncOperation homeScene = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        while (!homeScene.isDone)
        {
            yield return null;
        }
    }
    public void Set_Refresh_Gold_Gem()
    {
        txt_Gold.text = PlayerPrefs_Manager.Get_Gold().ToString("N0");
        txt_Gem.text = PlayerPrefs_Manager.Get_Gem().ToString("N0");
    }
}
