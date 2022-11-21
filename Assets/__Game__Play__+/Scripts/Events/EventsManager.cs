using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    private QuestManager _questManager;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _questManager = new QuestManager();
        _questManager.RegisterCallbacks();
    }

    private void OnDisable()
    {
        this.UnregisterAllCallbacks();
    }
}
