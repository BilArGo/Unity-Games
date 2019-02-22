using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {

    
    public float spawnDistance;
    public float speed;
    Transform player;
    Rigidbody2D rb;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //transform.position = new Vector3(player.position.x + Random.Range(-spawnDistance, spawnDistance), player.position.y + Random.Range(-spawnDistance, spawnDistance), 0);
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        rb.AddForce(Vector3.up * speed * Time.deltaTime);   	
	}
}
