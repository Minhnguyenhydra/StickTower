using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFade : UICanvas
{
    [Header("------Mr Me------")]
    public Animator anim_Fade;
    // Start is called before the first frame update
    //Fade
    public void Set_Fade_Out()
    {
        //StartCoroutine(IE_Fade_Out());
        ChangeScene.Ins.Fade();
    }
    public void Set_Fade_In()
    {
        //StartCoroutine(IE_Fade_In());
    }
    //
    IEnumerator IE_Fade_In()
    {
        anim_Fade.SetTrigger(Constant.Trigger_Fade_In);
        yield return Cache.GetWFS(Constant.Time_Fade);
        Close();
    }
    IEnumerator IE_Fade_Out()
    {
        anim_Fade.SetTrigger(Constant.Trigger_Fade_Out);
        yield return Cache.GetWFS(Constant.Time_Fade);
        Close();
    }
   
}
/*
yield return new WaitUntil(() => isDone);
 */