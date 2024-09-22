using UnityEngine;

public class QuestBase : ScriptableObject
{
    [Header("Quest")]
    public uint ID;
    public string questName;
    public string questDescription;
    public string questCompletedDescription;
    public Item reward;
    [SerializeField] protected bool isCompleted;
    [SerializeField] protected bool isStarted;
    [SerializeField] protected bool isReturned;

    [TextArea(3, 10)]
    public string[] Sentences;

    public void StartQuest() 
    {
        isCompleted = false;
        isReturned = false;
        isStarted = true;
        Debug.Log("Quest Started | Quest ID: " + ID);
        QuestManager.instance.AddQuest(this);

    }

    public void Reset()
    {
        isCompleted = false;
        isReturned = false;
        isStarted = false;
    }

    public bool IsQuestCompleted()
    {
        if(isCompleted)
        {
            return true;
        }

        return false;
    }

    public bool IsQuestStarted()
    {
        if (isStarted)
        {
            return true;
        }

        return false;
    }

    public bool IsQuestReturned()
    {
        if(isReturned)
        {
            return true;
        }

        return false;
    }

    public virtual void EndQuest() { }
    public virtual void ReturnQuest() { }
    public virtual void DoQuest() { }
    public virtual int GetObjectiveStatus() { return 0; }
    public virtual int GetObjectiveTarget() { return 0; }
}
