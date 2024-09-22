using UnityEngine;

[CreateAssetMenu(fileName = "new Quest", menuName = "Quest/Collect")]

public class Quest_Collect : QuestBase
{
    [Space(2)]
    [Header("Objective")]
    public uint targetCollectedItems;
    public Item targetItem;

    private uint collectedItems;

    public override void DoQuest() 
    {
        collectedItems = Inventory.instance.CheckForItemAmountByID(targetItem.ID);
        if (collectedItems >= targetCollectedItems)
            ReturnQuest();
    }

    public override void EndQuest()
    {
        isStarted = false;
        isReturned = true;
        Debug.Log("Quest Completed | Quest ID: " + ID);
        QuestManager.instance.AddQuestToListOfFinished(this);
        QuestManager.instance.RemoveQuest(this);
        Inventory.instance.RemoveItemByID(targetItem.ID, targetCollectedItems);

        if(reward != null)
        {
            FloatingText.DisplayFloatingText("Reward added to the inventory");
            Inventory.instance.AddItem(reward);
        }
    }

    public override void ReturnQuest()
    {
        isCompleted = true;
    }

    public override int GetObjectiveStatus()
    {
        return (int)collectedItems;
    }

    public override int GetObjectiveTarget()
    {
        return (int)targetCollectedItems;
    }
}
