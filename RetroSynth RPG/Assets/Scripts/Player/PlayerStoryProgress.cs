using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStoryProgress : MonoBehaviour
{
    public static PlayerStoryProgress instance;


    public static bool tutorial_TalkedWithDealer;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
    }

 
}
