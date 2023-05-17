using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;

    public Quest currentQuest;
    public List<Quest> possibleQuests;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one Quest Manager in the scene!");
        }

        instance = this;
    }

    public static QuestManager GetInstance()
    {
        return instance;
    }

    public void SetNewQuest(Quest newQuest)
    {
        currentQuest = newQuest;
        currentQuest.currentKills = 0;
    }
}

[Serializable]
public class Quest
{
    public KillQuestTarget target;
    public int neededAmountOfKills;
    public int currentKills;
}

public enum KillQuestTarget
{
    broccoli,
    cabbage,
    carrot
}