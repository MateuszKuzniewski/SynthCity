using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarFight : MonoBehaviour
{
    private bool eventTriggered;

    public GameObject dialogueNPC;
    public GameObject cameraZoomPoint;
    public MainCamera cam;
    public GameObject[] enemies;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && eventTriggered == false)
        {
            StartCoroutine(StartEvent());
        }
    }

    private IEnumerator StartEvent()
    {
        cam.ChangeFollowSpeed(0.02f);
        cam.ChangeTarget(cameraZoomPoint);
        DialogueManager.instance.StartDialogue(dialogueNPC, true);
        yield return new WaitUntil(() => DialogueManager.instance.IsDialogueFinished() == true);
        eventTriggered = true;
        cam.ResetTarget();
        cam.ChangeFollowSpeed(0.125f);
        GetComponent<BoxCollider>().enabled = false;

    
        foreach (GameObject m_enemy in enemies)
            m_enemy.GetComponent<Enemy>().enemyState = Enemy.EnemyStates.COMBAT;
    }
}

