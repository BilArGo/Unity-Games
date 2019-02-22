using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour {

    public float smoothSpeed = 0.125f;
    Transform p;
    Vector3 offset;

    void Start ()
    {
        p = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3(0, 0, -10);
	}
	
	void FixedUpdate ()
    {
        Vector3 desiredPosition = p.position + offset;
        Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPostion;
	}
}
