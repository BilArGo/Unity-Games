using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour {

    public float targetRot;
    public float z;
    public float zl;


    void Start () {
		
	}
	
	void Update ()
    {
        z = transform.eulerAngles.z;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");




        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetRot), Time.deltaTime * 3);

        if (Mathf.Round(h) == 1 && Mathf.Round(v) == 1) { targetRot = 315.0f; }
        if (Mathf.Round(h) == 1 && Mathf.Round(v) == -1) { targetRot = 225.0f; }
        if (Mathf.Round(h) == -1 && Mathf.Round(v) == 1) { targetRot = 45.0f; }
        if (Mathf.Round(h) == -1 && Mathf.Round(v) == -1) { targetRot = 135.0f; }

        if (Mathf.Round(h) == 1 && Mathf.Round(v) == 0) { targetRot = 270.0f; }
        if (Mathf.Round(h) == 0 && Mathf.Round(v) == 1) { targetRot = 0.0f; }
        if (Mathf.Round(h) == -1 && Mathf.Round(v) == 0) { targetRot = 90.0f; }
        if (Mathf.Round(h) == 0 && Mathf.Round(v) == -1) { targetRot = 180.0f; }



    }
}
