using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitUserData_Gamplay : Singleton<InitUserData_Gamplay>
{
    private static InitUserData_Gamplay Instance;
    //mỗi scene kéo vào 1 bản
    public UserData userData;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        userData?.OnInitData();
    }
}
