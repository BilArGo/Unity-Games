using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    public float lifeTime;
    public float damage;
    public float x;

    void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
	}
	
	void Update ()
    {
        x += Time.deltaTime;
        rb.velocity = transform.up * speed * Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "DeadEnemy" && collision.gameObject.tag != "DeadShip" && x > 0.03f && collision.gameObject.tag != "Wrench" && collision.gameObject.tag != gameObject.tag && collision.gameObject.tag != "Grenade")
        {
            Destroy(gameObject);
            if (collision.gameObject.tag != "Bullet" && !GameManager.endGame)
            {
                collision.gameObject.GetComponent<Health>().health -= damage;
            }
        }
    }
}
