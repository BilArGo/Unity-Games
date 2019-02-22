using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public ParticleSystem smoke;
    public GameObject fire;
    public Sprite DeadSprite;
    public GameObject child;
    public bool alive;
    public float Health;
    public float speed;
    public float rotationSpeed;
    public float Delay;
    public float shootDelay;
    float timeA;
    float timeB;
    public float range;
    public float stopRange;
    public float aimRange;
    bool enemyFounded;
    public Transform bullet;
    public Transform bulletOut;
    Rigidbody2D rb;
    public float x;
    float d;
    GameObject player;
    bool l;
    bool pAlive;
    Health h;
    Materials m;
    Animator anim;
    Animator fireAnim;
    float destroyTime;

    SpriteRenderer sr;



    void Start ()
    {
        alive = true;
        l = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        h = gameObject.GetComponent<Health>();
        m = gameObject.GetComponent<Materials>();
        anim = gameObject.GetComponent<Animator>();
        fireAnim = fire.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        pAlive = true;

        if (Vector3.Distance(transform.position, player.transform.position) < 20)
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {

        if (player != null)
        {
            if (player.tag == "DeadPlayer")
            {
                pAlive = false;
            }
     
        } 
        transform.localScale = new Vector3(10, 10, 1);

        if (h.health <= 0 && !l)
        {
            alive = false;
            gameObject.tag = "DeadEnemy";
            l = true;
            smoke.Play();
            sr.sprite = DeadSprite;
            child.GetComponent<SpriteRenderer>().sprite = DeadSprite;
            fire.SetActive(true);
            fireAnim.Play("New Animation");
            GameManager.elimination++;

        }

        if (destroyTime >  10)
        {
            Destroy(gameObject, 3);
            anim.Play("Enemy");
            smoke.Stop();
        }

        if (gameObject.tag == "LootedEnemy")
        {
            Destroy(gameObject, 3);
            anim.Play("Enemy");
            smoke.Stop();
        }

        float dis = Vector3.Distance(transform.position, player.transform.position);
        if (dis > 100)
        {
            Destroy(gameObject);
        }
        float disSpeed = Mathf.Clamp01((dis - stopRange) / 10);
        if (alive)
        {
            rb.velocity = transform.right * Time.deltaTime * speed * disSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
            destroyTime += Time.deltaTime;
        }
        timeA += Time.deltaTime;
        timeB += Time.deltaTime;

        if (dis < range && pAlive)
        {
            enemyFounded = true;
        }
        else
        {
            enemyFounded = false;
        }

        if (!enemyFounded && alive)
        {
            if (timeA > Delay)
            {
                timeA = 0;
                x = Random.Range(-100, 100);

            }
            transform.Rotate(Vector3.forward * x * 0.01f * rotationSpeed * Time.deltaTime);
        }

        else if(alive)
        {
            
            var dir = player.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (timeB > shootDelay && enemyFounded && alive && pAlive)
        {
            EnemyShoot();
            timeB = 0;

        }
        
	}

    void EnemyShoot()
    {
        Vector3 aim = bulletOut.rotation.eulerAngles + new Vector3(0,0,Random.Range(-aimRange,aimRange));
        Instantiate(bullet, bulletOut.position, Quaternion.Euler(aim.x, aim.y, aim.z));
    }

}
