using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBackdoor : MonoBehaviour
{

    public QuestBase quest;
    public GameObject door;
    public GameObject guard;
    public Animator sceneAnim;

    private bool isEventTriggered;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && quest.IsQuestReturned() && isEventTriggered == false)
            StartCoroutine(StartEvent());
    }

    private IEnumerator StartEvent()
    {
        isEventTriggered = true;
        yield return new WaitUntil(() => DialogueManager.instance.IsDialogueFinished() == true);
        sceneAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        guard.transform.position = new Vector3(guard.transform.position.x, guard.transform.position.y, guard.transform.position.z + 4);
        door.transform.rotation = Quaternion.Euler(-90, 0, 0);
        sceneAnim.SetTrigger("FadeOut");

    }
}
