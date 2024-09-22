using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotate : MonoBehaviour
{
    public int rotationSpeed;
    public bool dayCycle;
 
    // Update is called once per frame
    void FixedUpdate()
    {
        if(dayCycle)
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
    }
}
