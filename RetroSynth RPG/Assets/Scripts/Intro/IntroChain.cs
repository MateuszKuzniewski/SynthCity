using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class IntroChain : MonoBehaviour
{
    private bool enableInput = false;


    public GameObject text;
    public Animator ScreenTransition;
    public Animator PhoneTransition;
    public GameObject MaxVectorDialogue;
    public GameObject DrivingAssistantDialogue;

    private void Start()
    {
        text.SetActive(false);
        StartCoroutine(Intro());
    }

    private void Update()
    {
        if(enableInput)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                text.SetActive(false);
                StartCoroutine(AnswerCallTransition());
            }

        }
    }

    private IEnumerator Intro()
    {
        yield return new WaitForSeconds(4);
        enableInput = true;
        text.SetActive(true);
    }

    private IEnumerator AnswerCallTransition()
    {
        yield return new WaitForSeconds(1);
        PhoneTransition.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        ScreenTransition.SetTrigger("FadeOut");
        enableInput = false;
        yield return new WaitForSeconds(2);
        DialogueManager.instance.StartDialogue(MaxVectorDialogue, false);
        yield return new WaitUntil(() => DialogueManager.instance.IsDialogueFinished() == true);
        StartCoroutine(IntroTransitionToGame());

    }
    private IEnumerator IntroTransitionToGame()
    {
        DialogueManager.instance.StartDialogue(DrivingAssistantDialogue, false);
        yield return new WaitUntil(() => DialogueManager.instance.IsDialogueFinished() == true);
        ScreenTransition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }

}
