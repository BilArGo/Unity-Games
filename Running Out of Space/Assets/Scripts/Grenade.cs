using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    public float explosionTime;
    public float damage;
    public float x;
    public GameObject fire;
    public List<GameObject> victims;
    bool explode;
    bool active;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed * Time.deltaTime);
        rb.AddTorque(3000 * Time.deltaTime);
        active = true;
        explode = false;
    }

    void Update()
    {
        x += Time.deltaTime;
        
        if (x > explosionTime)
        {
            explode = true;
            fire.SetActive(true);
            fire.transform.parent = null;
            fire.gameObject.GetComponent<Animator>().Play("New Animation");
            Destroy(gameObject, 1);
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && explode && !victims.Contains(collision.gameObject))
        {
            victims.Add(collision.gameObject);           
            float dis = Vector3.Distance(transform.position, collision.transform.position);
            float d = damage / dis * 1.3f;

            collision.gameObject.GetComponent<Health>().health -= d;
            
           
        }
    }
}
