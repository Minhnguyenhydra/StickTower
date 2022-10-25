using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasLoading : UICanvas
{
    public Image imgProgress;
    private void Start()
    {
        if (GameManager.isStarted)
        {
            UIManager.Ins.OpenUI(UIID.UICMainMenu);
            Close();
            return;
        }

        imgProgress.DOFillAmount(1, Constant.Time_Loading)
            .SetEase(Ease.InOutQuad).OnComplete(()=>
                {
                    Close();
                    UIManager.Ins.OpenUI(UIID.UICMainMenu);
                }
            );

        GameManager.isStarted = true;
    }
    public void SetPercent(float to, float time)
    {
        imgProgress.DOFillAmount(to, time)
            .SetEase(Ease.Linear);
    }
}
