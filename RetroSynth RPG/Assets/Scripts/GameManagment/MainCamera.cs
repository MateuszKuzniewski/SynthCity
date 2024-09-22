using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Animator anim;
    private Transform target;
    private Transform defaultTarget;


    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        defaultTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

   
    void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }

    public void ChangeFollowSpeed(float newSpeed)
    {
        smoothSpeed = newSpeed;
    }

    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget.transform;
    }
    public void ResetTarget()
    {
        target = defaultTarget;
    }

    public void Zoom(bool condition)
    {
        if (condition)
        {
            anim.SetTrigger("ZoomIn");
        }
        else
        {

            anim.SetTrigger("ZoomOut");
        }
    }
}

