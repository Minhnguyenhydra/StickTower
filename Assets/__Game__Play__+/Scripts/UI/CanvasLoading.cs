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
            .SetEase(Ease.InOutQuad).OnComplete(() =>
                {
                    Close();
                    UIManager.Ins.OpenUI(UIID.UICMainMenu);

                    Debug.LogError(Datacontroller.instance.saveData.day);
                    if (Datacontroller.instance.saveData.session == 1)
                    {
                        EventController.PLAY_EVENT_DAY(Datacontroller.instance.saveData.day);
                        Datacontroller.instance.saveData.oldDay = System.DateTime.Today;
                    }
                    else
                    {
                        if (System.DateTime.Today != Datacontroller.instance.saveData.oldDay)
                        {
                            Datacontroller.instance.saveData.day++;
                            EventController.PLAY_EVENT_DAY(Datacontroller.instance.saveData.day);
                            Datacontroller.instance.saveData.oldDay = System.DateTime.Today;
                            if (Datacontroller.instance.saveData.day == 6)
                            {
                                EventController.PLAY_EVENT_WEEK(Datacontroller.instance.saveData.week);
                                Datacontroller.instance.saveData.week++;
                                Datacontroller.instance.saveData.day = 0;
                            }          
                        }
                    }
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
