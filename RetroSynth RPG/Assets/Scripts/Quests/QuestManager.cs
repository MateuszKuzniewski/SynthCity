using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public GameObject questPrefab;
    public GameObject questPanel;
    public QuestBase startingQuest;
    public List<QuestBase> questList = new List<QuestBase>();
    public List<QuestBase> finishedQuestList = new List<QuestBase>();
    private List<GameObject> questListUI = new List<GameObject>();


    private GameObject clone;
    private TextMeshProUGUI qName;
    private TextMeshProUGUI qDesc;

    private StringBuilder builder;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        questPrefab.SetActive(false);
        startingQuest.Reset();
        startingQuest.StartQuest();

        UpdateQuestLogUI();
    }

    private void Update()
    {
        if (questList.Count != 0)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].IsQuestStarted() == true && questList[i].IsQuestCompleted() == false && questList[i].IsQuestReturned() == false)
                    questList[i].DoQuest();
            }
        }

        UpdateQuestLogUI();
    }

    public void AddQuest(QuestBase q)
    {
        clone = Instantiate(questPrefab, questPanel.transform);
        questListUI.Add(clone);
        questList.Add(q);
        clone.SetActive(true);
        qName = clone.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        qDesc = clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        StringBuilder builder = new StringBuilder();
        builder.Append(q.questDescription).Append(" ").Append(q.GetObjectiveStatus()).Append("/").Append(q.GetObjectiveTarget());

        qName.text = q.questName;
        qDesc.text = builder.ToString();
    }
    public void AddQuestToListOfFinished(QuestBase q)
    {
        finishedQuestList.Add(q);
    }

    public void UpdateQuestLogUI()
    {
        for(int i = 0; i < questListUI.Count; i++)
        {
            if (!questList[i].IsQuestCompleted())
            {
                builder = new StringBuilder();
                if (questList[i].GetObjectiveTarget() == 0)
                {
                    builder.Append(questList[i].questDescription);
                }
                else
                {
                    builder.Append(questList[i].questDescription).Append(" ").Append(questList[i].GetObjectiveStatus()).Append("/").Append(questList    [i].GetObjectiveTarget());
                }

                questListUI[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = builder.ToString();
            }
            else
            {
                builder = new StringBuilder();
                builder.Append(questList[i].questName).Append(" | V");
                questListUI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = builder.ToString();
                builder = new StringBuilder();
                builder.Append(questList[i].questCompletedDescription);
                questListUI[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = builder.ToString();
               
            }
        }
    }

    public void RemoveQuest(QuestBase q)
    {
        int index = 0;
        index = questList.IndexOf(q);
        questListUI[index].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Completed!";
        questListUI[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";

        // Delete Header
        StartCoroutine(DeleteTextAnimation(questListUI[index],
            questListUI[index].transform.GetChild(0).GetComponent<TextMeshProUGUI>()));

        // Delete Description
        StartCoroutine(DeleteTextAnimation(questListUI[index],
            questListUI[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>()));

        questList.RemoveAt(index);
        questListUI.RemoveAt(index);
        
    }

    private IEnumerator DeleteTextAnimation(GameObject gm, TextMeshProUGUI text)
    {
        float duration = 0.8f; 
        float currentTime = 0f;
        
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gm);
        yield break;
    }
}
