using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataParam
{
    static WaitForSeconds waitDeleteCheck = new WaitForSeconds(2f);
    public static bool canDelete = false;
    public static int currentLevel = 0;
    public static float timeDelayShowAds;
    public static string SAVEDATA = "kiensavedata";
    public static WaitForSeconds WAITDELETECHECK
    {
        get
        {
            return waitDeleteCheck;
        }
    }
}

