using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanel : PopUpProperties
{
    public void BtnNext()
    {
        DataParam.currentLevel++;
        Application.LoadLevel(Application.loadedLevelName);
        Debug.LogError("======= load next level");
    }
}
