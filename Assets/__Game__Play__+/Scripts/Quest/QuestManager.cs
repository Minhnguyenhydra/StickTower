using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestManager;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    #region Id Quests
    public enum QuestID
    {
        Quest01 = 1,
        Quest02 = 2,
        Quest03 = 3,
        Quest04 = 4,
        Quest05 = 5,
        Quest06 = 6,
        Quest07 = 7,
        Quest08 = 8,
        Quest09 = 9,
        Quest10 = 10,
        Quest11 = 11,
        Quest12 = 12,
    }
    #endregion

    [SerializeField]
    private QuestData questData;
    [SerializeField]
    public Quest[] quests;
    [SerializeField]
    private Animator anim;

    public static List<int> lsQuestId = new List<int> ();

    void OnEnable()
    {
        int rand = Random.Range(0, questData.QuestInfos.Count);
        int lastRand = PlayerPrefs_Manager.GetDay(DateTime.Now.DayOfWeek);

        int lastDay = PlayerPrefs_Manager.GetLastDay();
        int curDay = (int)DateTime.Now.DayOfWeek;
        if (lastDay == curDay)
            rand = lastRand;
        else
        {
            while (lastDay == rand)
                rand = Random.Range(0, questData.QuestInfos.Count);

            lastRand = rand;
        }

        lsQuestId.Clear();
        for (int i = 0; i < quests.Length; i++)
        {
            QuestInfo questInfo = questData.QuestInfos[rand];
            lsQuestId.Add(questInfo.id);
            rand++;
            if (rand >= questData.QuestInfos.Count)
                rand = 0;

            int value = PlayerPrefs_Manager.GetQuest(Constant.Quest + questInfo.id);
            quests[i].SetData(questInfo.name, questInfo.id, value, questInfo.number, questInfo.goldRewarded, questInfo.gemRewarded);
        }

        PlayerPrefs_Manager.SetDay(DateTime.Now.DayOfWeek, lastRand);
    }

    public void CloseQuestClicked()
    {
        anim.SetTrigger("Close");
    }

    public void UnactiveObject()
    {
        gameObject.SetActive(false);
    }

    #region Callbacks
    public void RegisterCallbacks()
    {
        this.RegisterCallback(QuestID.Quest01, CollectKeys);
        this.RegisterCallback(QuestID.Quest02, RescuePrincess);
        this.RegisterCallback(QuestID.Quest03, KillBoss);
        this.RegisterCallback(QuestID.Quest04, CollectTreasures);
        this.RegisterCallback(QuestID.Quest05, KillEnemy);
        this.RegisterCallback(QuestID.Quest06, PassChallenge);
        this.RegisterCallback(QuestID.Quest07, PassArena);
        this.RegisterCallback(QuestID.Quest08, PassLevel);
        this.RegisterCallback(QuestID.Quest09, UnlockAnySkin);
        this.RegisterCallback(QuestID.Quest10, CollectSpecialPieces);
        this.RegisterCallback(QuestID.Quest11, CollectNormalPieces);
        this.RegisterCallback(QuestID.Quest12, OpenFiveTreasures);
    }

    private void OpenFiveTreasures(int obj)
    {
        for (int i = 0; i < lsQuestId.Count; i++)
        {
            if (lsQuestId[i] == (int)QuestID.Quest12)
            {
                obj += PlayerPrefs_Manager.GetQuest(Constant.Quest + (int)QuestID.Quest12);
                Debug.Log($"Open {obj} treasures");
                PlayerPrefs_Manager.SetQuest(Constant.Quest + (int)QuestID.Quest12, obj);
            }
        }

    }

    private void CollectNormalPieces(int obj)
    {
        for (int i = 0; i < lsQuestId.Count; i++)
        {
            if (lsQuestId[i] == (int)QuestID.Quest11)
            {
                obj += PlayerPrefs_Manager.GetQuest(Constant.Quest + (int)QuestID.Quest11);
                Debug.Log($"Collected {obj} normal pieces");
                PlayerPrefs_Manager.SetQuest(Constant.Quest + (int)QuestID.Quest11, obj);
            }
        }
    }

    private void CollectSpecialPieces(int obj)
    {
        for (int i = 0; i < lsQuestId.Count; i++)
        {
            if (lsQuestId[i] == (int)QuestID.Quest10)
            {
                obj += PlayerPrefs_Manager.GetQuest(Constant.Quest + (int)QuestID.Quest10);
                Debug.Log($"Collected {obj} special pieces");
                PlayerPrefs_Manager.SetQuest(Constant.Quest + (int)QuestID.Quest10, obj);
            }
        }
    }

    private void UnlockAnySkin(int obj)
    {
        for (int i = 0; i < lsQuestId.Count; i++)
        {
            if (lsQuestId[i] == (int)QuestID.Quest09)
            {
                obj += PlayerPrefs_Manager.GetQuest(Constant.Quest + (int)QuestID.Quest09);
                Debug.Log($"Unclocked {obj} skin");
                PlayerPrefs_Manager.SetQuest(Constant.Quest + (int)QuestID.Quest09, obj);
            }
        }
    }

    private void PassLevel(int obj)
    {
        for (int i = 0; i < lsQuestId.Count; i++)
        {
            if (lsQuestId[i] == (int)QuestID.Quest08)
            {
                obj += PlayerPrefs_Manager.GetQuest(Constant.Quest + (int)QuestID.Quest08);
                Debug.Log($"Passed {obj} level");
                PlayerPrefs_Manager.SetQuest(Constant.Quest + (int)QuestID.Quest08, obj);
            }
        }
    }

    private void PassArena(int obj)
    {
        for (int i = 0; i < lsQuestId.Count; i++)
        {
            if (lsQuestId[i] == (int)QuestID.Quest07)
            {
                obj += PlayerPrefs_Manager.GetQuest(Constant.Quest + (int)QuestID.Quest07);
                Debug.Log($"Passed {obj} arena");
                PlayerPrefs_Manager.SetQuest(Constant.Quest + (int)QuestID.Quest07, obj);
            }
        }
    }

    private void PassChallenge(int obj)
    {
        for (int i = 0; i < lsQuestId.Count; i++)
        {
            if (lsQuestId[i] == (int)QuestID.Quest06)
            {
                obj += PlayerPrefs_Manager.GetQuest(Constant.Quest + (int)QuestID.Quest06);
                Debug.Log($"Passed {obj} challenge");
                PlayerPrefs_Manager.SetQuest(Constant.Quest + (int)QuestID.Quest06, obj);
            }
        }
    }

    private void KillEnemy(int obj)
    {
        for (int i = 0; i < lsQuestId.Count; i++)
        {
            if (lsQuestId[i] == (int)QuestID.Quest05)
            {
                obj += PlayerPrefs_Manager.GetQuest(Constant.Quest + (int)QuestID.Quest05);
                Debug.Log($"Killed {obj} enemies");
                PlayerPrefs_Manager.SetQuest(Constant.Quest + (int)QuestID.Quest05, obj);
            }
        }
    }

    private void CollectTreasures(int obj)
    {
        for (int i = 0; i < lsQuestId.Count; i++)
        {
            if (lsQuestId[i] == (int)QuestID.Quest04)
            {
                obj += PlayerPrefs_Manager.GetQuest(Constant.Quest + (int)QuestID.Quest04);
                Debug.Log($"Collected {obj} treasures");
                PlayerPrefs_Manager.SetQuest(Constant.Quest + (int)QuestID.Quest04, obj);
            }
        }
    }

    private void KillBoss(int obj)
    {
        for (int i = 0; i < lsQuestId.Count; i++)
        {
            if (lsQuestId[i] == (int)QuestID.Quest03)
            {
                obj += PlayerPrefs_Manager.GetQuest(Constant.Quest + (int)QuestID.Quest03);
                Debug.Log($"Killed {obj} bosses");
                PlayerPrefs_Manager.SetQuest(Constant.Quest + (int)QuestID.Quest03, obj);
            }
        }
    }

    private void RescuePrincess(int obj)
    {
        for (int i = 0; i < lsQuestId.Count; i++)
        {
            if (lsQuestId[i] == (int)QuestID.Quest02)
            {
                obj += PlayerPrefs_Manager.GetQuest(Constant.Quest + (int)QuestID.Quest02);
                Debug.Log($"Rescued {obj} princesses");
                PlayerPrefs_Manager.SetQuest(Constant.Quest + (int)QuestID.Quest02, obj);
            }
        }
    }

    private void CollectKeys(int obj)
    {
        for (int i = 0; i < lsQuestId.Count; i++)
        {
            if (lsQuestId[i] == (int)QuestID.Quest01)
            {
                obj += PlayerPrefs_Manager.GetQuest(Constant.Quest + (int)QuestID.Quest01);
                Debug.Log($"Collected {obj} Keys");
                PlayerPrefs_Manager.SetQuest(Constant.Quest + (int)QuestID.Quest01, obj);
            }
        }
    }
    #endregion
}

[Serializable]
public static class Events
{
    private static Dictionary<QuestID, Action<int>> callbacks = new Dictionary<QuestID, Action<int>>();
    public static void RegisterCallback(this MonoBehaviour mono, QuestID questId, Action<int> callback)
    {
        if (!callbacks.ContainsKey(questId))
        {
            callbacks.Add(questId, callback);
        }
        else
            Debug.Log($"{questId} is duplicate");
    }

    public static void UnregisterAllCallbacks(this MonoBehaviour mono)
    {
        Debug.Log($"UnregisterAllCallbacks");

        if (callbacks != null && callbacks.Count > 0)
            callbacks.Clear();
    }

    public static void PostEvent(this MonoBehaviour mono, QuestID questId, int value)
    {
        if (callbacks.ContainsKey(questId))
            callbacks[questId].Invoke(value);
        else
            Debug.Log($"{questId} have not any callback");
    }
}
