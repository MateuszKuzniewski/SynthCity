using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public int speed;

    public float distanceTraveled;
    private Vector3 startingPos;
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        distanceTraveled = startingPos.z - transform.position.z;

        if (distanceTraveled > boxCollider.size.z)
            transform.position = startingPos;
    }
}
