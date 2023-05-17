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

    public void UpdateQuestProgress(KillQuestTarget target, int amount)
    {
        if(target == currentQuest.target && !currentQuest.questCompleted)
        {
            currentQuest.currentKills += amount;
            
            if(currentQuest.currentKills >= currentQuest.neededAmountOfKills)
            {
                // other possible quest completed thingies here!!

                Debug.Log("Quest completed!");
            }
        }

        else
        {
            Debug.LogError("Current kill did not count for the quest progress.");
        }
    }
}

[Serializable]
public class Quest
{
    public KillQuestTarget target;
    public int neededAmountOfKills;
    public int currentKills;
    public bool questCompleted;
}

public enum KillQuestTarget
{
    broccoli,
    cabbage,
    carrot
}