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
        imgProgress.DOFillAmount(1, Constant.Time_Loading)
            .SetEase(Ease.InOutQuad).OnComplete(()=>
                {
                    Close();
                    UIManager.Ins.OpenUI(UIID.UICMainMenu);
                }
            );
    }
    public void SetPercent(float to, float time)
    {
        imgProgress.DOFillAmount(to, time)
            .SetEase(Ease.Linear);
    }
}
