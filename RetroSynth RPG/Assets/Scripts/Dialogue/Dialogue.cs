
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour
{
    private Quests q;

    [TextArea(3, 10)]
    public string[] Sentences;
    public string RepeatableSentence;

    private GameObject player;
    private Quaternion defaultRotation;

    public bool rotateTowards;
    public bool resetRotation;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        defaultRotation = transform.rotation;
        q = GetComponent<Quests>();
    }

    void Update()
    {
        if (rotateTowards)
            RotateTowards();

        if (resetRotation)
            ResetRotation();
    }

    public void OnMouseOver()
    {
        ObjectNamesUI.ChangeTextColour(Color.white);
        ObjectNamesUI.DisplayObjectName(name);
        if (Input.GetMouseButtonDown(0))
        {

            if(q.quests.Count != 0 || q.OnlyDialogue)
                DialogueManager.instance.StartDialogue(gameObject, true);

        }
    }

    public void OnMouseExit()
    {
        ObjectNamesUI.HideObjectName();
    }

    private void RotateTowards()
    {
  
        Vector3 targetDirection = player.transform.position - transform.position;
        float singleStep = 5 * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        this.transform.rotation = Quaternion.LookRotation(newDir);
 
    }

    public void ResetRotation()
    {
      
        
        transform.rotation = defaultRotation;
    
    }
}
