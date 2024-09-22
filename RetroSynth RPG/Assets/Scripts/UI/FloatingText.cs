using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public static FloatingText instance;

    public GameObject floatingTextPrefab;
    public GameObject Canvas;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

      
    }

    public static void DisplayFloatingText(string data)
    {
        instance.StartCoroutine(instance.InstantiateText(data));
    }

    private IEnumerator InstantiateText(string data)
    {
        GameObject clone = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        clone.transform.parent = Canvas.transform;
        clone.GetComponentInChildren<TextMeshProUGUI>().text = data;
        clone.GetComponentInChildren<Animator>().SetTrigger("Trigger");

        yield return new WaitForSeconds(3.0f);
        clone.transform.SetParent(null, false);
        Destroy(clone);
    }
}
