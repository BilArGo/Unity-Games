using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public Transform Ship;
    public Transform player;
    public GameObject c;

    void Start () {
		
	}
	
	void Update ()
    {
        transform.position = player.position;
        transform.LookAt(Ship);

        if (Vector3.Distance(Ship.position, player.position) < 40)
        {
            c.SetActive(false);

        }

        else
        {
            c.SetActive(true);

        }
    }
}
