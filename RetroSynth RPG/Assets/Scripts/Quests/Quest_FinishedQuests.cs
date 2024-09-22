using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Quest", menuName = "Quest/Finished Quests")]

public class Quest_FinishedQuests : QuestBase
{
    [Space(2)]
    [Header("Objective")]
    public List<QuestBase> questsRequired = new List<QuestBase>();

    private int finishedQuestIndex;

    public override void DoQuest() 
    {
        finishedQuestIndex = 0;
        for(int i = 0; i < questsRequired.Count; i++)
        {
            if (questsRequired[i].IsQuestReturned())
                finishedQuestIndex++;
        }

        if (finishedQuestIndex >= questsRequired.Count)
            EndQuest();
    }

    public override void EndQuest() 
    {
        Debug.Log("Quest Completed | Quest ID: " + ID);
        QuestManager.instance.AddQuestToListOfFinished(this);
        QuestManager.instance.RemoveQuest(this);
        isCompleted = true;
        isStarted = false;

        if (reward != null)
        {
            FloatingText.DisplayFloatingText("Reward added to the inventory");
            Inventory.instance.AddItem(reward);
        }
    }
}
