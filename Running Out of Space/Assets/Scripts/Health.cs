using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float health;
    bool zero;

    void Start ()
    {
        zero = false;	
	}
	
	void Update ()
    {
        if (health == 0)
        {
            zero = true;
        }

        if (!zero)
        {
            health += Time.deltaTime;
        }
        
        health = Mathf.Clamp(health, 0, 100);
    }
}
