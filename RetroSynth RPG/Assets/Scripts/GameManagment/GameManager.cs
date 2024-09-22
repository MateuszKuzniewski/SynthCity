using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public List<GameObject> itemBoxList = new List<GameObject>();
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject itemBox in GameObject.FindGameObjectsWithTag("ItemBox"))
            itemBoxList.Add(itemBox);
        
    }

    // Find index on an object in a list
    public int FindIndex(GameObject tarObj, List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == tarObj)
            {
                index = i;
                return index;
            }
        }
        return -1;
    }
}
