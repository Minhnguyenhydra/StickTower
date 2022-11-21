using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/QuestData", order = 1)]
public class QuestData : ScriptableObject
{
    public List<QuestInfo> QuestInfos = new List<QuestInfo>();
}
