using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitUserData_Gamplay : Singleton<InitUserData_Gamplay>
{
    //mỗi scene kéo vào 1 bản
    public UserData userData;
    private void Awake()
    {
        userData?.OnInitData();
        DontDestroyOnLoad(gameObject);
    }
}
