using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quests : MonoBehaviour
{
    public List<QuestBase> quests = new List<QuestBase>();
    public GameObject markerGameObject;
    public bool OnlyDialogue;

    private TextMeshPro marker;

    // Start is called before the first frame update
    void Start()
    {

        foreach (QuestBase q in quests)
            q.Reset(); // Scriptable objects don't have start method so I have to do it here

        marker = markerGameObject.GetComponentInChildren<TextMeshPro>();
    }

    private void Update()
    {
        if (!OnlyDialogue)
        {
            if (quests.Count != 0)
            {
                if (!quests[0].IsQuestStarted() && !quests[0].IsQuestCompleted() && !quests[0].IsQuestReturned())
                    marker.text = "!";
                else if (quests[0].IsQuestStarted() && !quests[0].IsQuestCompleted() && !quests[0].IsQuestReturned())
                    marker.text = " ";
                else if (quests[0].IsQuestStarted() && quests[0].IsQuestCompleted() && !quests[0].IsQuestReturned())
                    marker.text = "?";
            }
            else
                marker.text = " ";
        }
        else
            marker.text = " ";
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
           // marker.gameObject.SetActive(false);

            if(quests.Count != 0)
            { 
                if (quests[0].IsQuestCompleted())
                {
                    quests[0].EndQuest();
                    quests.RemoveAt(0);
                }
            }
        }
    }
}
