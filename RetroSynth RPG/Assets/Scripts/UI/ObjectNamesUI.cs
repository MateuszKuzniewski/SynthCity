using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectNamesUI : MonoBehaviour
{

    public static ObjectNamesUI instance;


    private TextMeshProUGUI nameText;
    public Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        nameText = GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void FollowCursor()
    {
        

    }

    public static void DisplayObjectName(string name)
    {
        Vector3 newPos = Input.mousePosition + instance.offset;
        newPos.z = 0f;

        //instance.gameObject.transform.position = newPos;

        instance.gameObject.SetActive(true);
        instance.nameText.text = name;
    }

    public static void HideObjectName()
    {
        instance.gameObject.SetActive(false);

    }

    public static void ChangeTextColour(Color colour)
    {
        instance.nameText.color = colour;
    }

    public static void ResetColour()
    {
        instance.nameText.color = Color.white;
    }
}
