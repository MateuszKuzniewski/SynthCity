using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyManagers : MonoBehaviour
{
    public string ObjectTag;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(ObjectTag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
