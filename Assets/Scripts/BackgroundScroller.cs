using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float speed = 0.1f;
    public float clampPos;
    public Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        float newPosition = Mathf.Repeat(Time.time * speed, clampPos);
        transform.position = startPosition + Vector3.right * newPosition;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    
}
