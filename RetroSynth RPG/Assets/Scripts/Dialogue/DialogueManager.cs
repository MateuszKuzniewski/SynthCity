using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public MainCamera cam;
    public GameObject dialogueBox;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueOptionsPanel;
    public TextMeshProUGUI dialogueOption1;
    public TextMeshProUGUI dialogueOption2;

    private bool isFinished = true;
    private int sentenceNumber;
    private PlayerMovement player;
    private GameObject playerGameObject;
    private GameObject targetSpeaker;

    // Start is called before the first frame update
    private void Start()
    {
        if(instance == null)
            instance = this;

        dialogueBox.SetActive(false);
        dialogueOptionsPanel.SetActive(false);
        sentenceNumber = 0;

        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        player = playerGameObject.GetComponent<PlayerMovement>();
       
    }

    void Update()
    {
        // TO DO: Move this code to separate function
        if (!IsDialogueFinished())
        {
            player.Speed = 0;

            if (Input.GetKeyDown(KeyCode.Space))
               NextSentence(targetSpeaker);

           
        }
    }

    public void StartDialogue(GameObject speaker, bool cameraZoom)
    {
        if(cameraZoom)
            cam.Zoom(true);

        Quests npcQuests = speaker.GetComponent<Quests>();
        Dialogue npcDialogue = speaker.GetComponent<Dialogue>();
        Player.instance.playerState = PlayerStates.TALKING;
        npcDialogue.resetRotation = false;
        npcDialogue.rotateTowards = true;
        targetSpeaker = speaker;
        sentenceNumber = 0;
        dialogueBox.SetActive(true);
        isFinished = false;
        speakerName.text = speaker.name;

        if (speaker.GetComponent<Quests>().OnlyDialogue)
            dialogueText.text = npcDialogue.Sentences[sentenceNumber];
        else
            dialogueText.text = npcQuests.quests[0].Sentences[sentenceNumber];
    }

    public void NextSentence(GameObject speaker)
    {
        Quests npcQuests = speaker.GetComponent<Quests>();
        Dialogue npcDialogue = speaker.GetComponent<Dialogue>();

        if (dialogueBox.activeSelf == true)
        {
            sentenceNumber++;
            // If only dialogues
            if (speaker.GetComponent<Quests>().OnlyDialogue)
            {
                if (sentenceNumber <= npcDialogue.Sentences.Length - 1)
                {
                    dialogueText.text = npcDialogue.Sentences[sentenceNumber];

                }
                else
                {
                    if ((targetSpeaker.GetComponent<Vendor>() as Vendor) != null)
                    {
                        Player.instance.playerState = PlayerStates.SELLING;
                        targetSpeaker.GetComponent<Vendor>().UpdateVendorSlots();
                        OpenDialogueOptions();
                    }
                    else
                    {
                        StopDialogue();
                    }
                }
            }
            // If quests 
            else
            {
                if (sentenceNumber <= npcQuests.quests[0].Sentences.Length - 1)
                {
                    dialogueText.text = npcQuests.quests[0].Sentences[sentenceNumber];

                }
                else
                {
                    if (npcQuests.quests.Count != 0 && !npcQuests.quests[0].IsQuestStarted() && !npcQuests.quests[0].IsQuestCompleted())
                        npcQuests.quests[0].StartQuest();

                    if ((targetSpeaker.GetComponent<Vendor>() as Vendor) != null)
                    {
                        Player.instance.playerState = PlayerStates.SELLING;
                        targetSpeaker.GetComponent<Vendor>().UpdateVendorSlots();
                        OpenDialogueOptions();
                    }
                    else
                    {
                        StopDialogue();                       
                    }
                }
            }
        }
    }


    public void StopDialogue()
    {
        dialogueBox.SetActive(false);
        dialogueOptionsPanel.SetActive(false);
        sentenceNumber = 0;
        isFinished = true;
        cam.Zoom(false);
        player.Speed = 10;
        Player.instance.playerState = PlayerStates.MOVING;
        targetSpeaker.GetComponent<Dialogue>().rotateTowards = false;
        targetSpeaker.GetComponent<Dialogue>().resetRotation = true;
    }

    public void OpenDialogueOptions()
    {
        dialogueBox.SetActive(false);
        dialogueOptionsPanel.SetActive(true);
    }

    public bool IsDialogueFinished()
    {
        if (instance.isFinished)
        {
            return true;
        }
        return false;
    }
}