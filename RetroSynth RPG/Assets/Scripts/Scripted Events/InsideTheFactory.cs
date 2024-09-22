using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InsideTheFactory : MonoBehaviour
{

    private bool eventTriggered = false; 
    private bool enableInput = false;

    public GameObject phone;
    public GameObject phoneText;
    public GameObject MaxVector;
    public Animator doorsAnim;
    public QuestBase questToComplete;
    public QuestBase questToGive;


    // Update is called once per frame
    void Update()
    {
        if (enableInput)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                phone.SetActive(false);
                phoneText.SetActive(false);
                StartCoroutine(AnswerCall());
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && eventTriggered == false)
        {
            questToComplete.EndQuest();
            phone.SetActive(true);
            phoneText.SetActive(true);
            enableInput = true;
            eventTriggered = true;
        }
    }

    private IEnumerator AnswerCall()
    {
        enableInput = false;
        DialogueManager.instance.StartDialogue(MaxVector, true);
        yield return new WaitUntil(() => DialogueManager.instance.IsDialogueFinished() == true);
        doorsAnim.SetTrigger("closeDoors");
        questToGive.StartQuest();
    }

}
