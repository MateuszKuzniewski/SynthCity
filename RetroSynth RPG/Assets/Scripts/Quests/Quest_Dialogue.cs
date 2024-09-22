
using UnityEngine;

[CreateAssetMenu(fileName = "new Quest", menuName = "Quest/Dialogue")]

public class Quest_Dialogue : QuestBase
{
    [Space(2)]
    [Header("Objective")]
    public GameObject targetPerson;
    public override void DoQuest()
    {
        if (InputManager.instance.GetSelectedObject().name == targetPerson.name)
        { 
            EndQuest();
        }
    }

    public override void EndQuest()
    {
        
        Debug.Log("Quest Completed | Quest ID: " + ID);
        FloatingText.DisplayFloatingText("Quest Completed");
        QuestManager.instance.AddQuestToListOfFinished(this);
        QuestManager.instance.RemoveQuest(this);
        isCompleted = true;
        isStarted = false;
        isReturned = true;

        if (reward != null)
        {
            FloatingText.DisplayFloatingText("Reward added to the inventory");
            Inventory.instance.AddItem(reward);
        }
    }
}
