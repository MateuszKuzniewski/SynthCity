using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DoorPortal : MonoBehaviour
{
    public Transform teleportTo;
    public Animator sceneAnim;

    private GameObject player;
    private SpriteRenderer spriteRend;
    private TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRend = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();
        spriteRend.enabled = false;
        text.enabled = false;
    }

   

    void OnMouseEnter()
    {
        spriteRend.enabled = true;
        text.enabled = true;
    }
    void OnMouseExit()
    {
        spriteRend.enabled = false;
        text.enabled = false;
    }

    private void OnMouseDown()
    {
        StartCoroutine(StartTransition());
    }

    private IEnumerator StartTransition()
    {
        Player.instance.GetComponent<PlayerMovement>().enableInput = false;
        sceneAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        player.transform.position = teleportTo.position;
        yield return new WaitForSeconds(1);
        sceneAnim.SetTrigger("FadeOut");
        Player.instance.GetComponent<PlayerMovement>().enableInput = true;
    }
}
