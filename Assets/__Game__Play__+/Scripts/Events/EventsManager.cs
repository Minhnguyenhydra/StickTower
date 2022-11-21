using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;
    private QuestManager _questManager;


    private void Start()
    {

        if (Instance != null)
            return;

        _questManager = new QuestManager();
        _questManager.RegisterCallbacks();
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
