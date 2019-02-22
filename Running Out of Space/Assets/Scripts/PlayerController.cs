using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Transform cursor;
    public Transform gunPivot;
    public Transform bulletOut;
    public Transform grenade;
    public Transform bullet;
    public SpriteRenderer gun;
    public float oxygen = 100;
    public int greAmo;
    public int fuel;
    public int material;
    public bool wrench;
    float x;
    float a;
    public float c;
    public GameObject GameOverPanel;
    public TextMesh shiptext;
    bool speedDecrease;
    float sd = 1;


    Rigidbody2D rb;
    Health h;
    Animator anim;
    GameManager gm;



    void Start ()
    {
        h = gameObject.GetComponent<Health>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        oxygen = 100;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        
	}
	
	void Update ()
    {


        if (Mathf.Sign(transform.localScale.x) == 1)
        {
            gun.flipY = true;
        }
        else
        {
            gun.flipY = false;
        }

        if (h.health == 0)
        {
            Dead();
        }
        float ho = Input.GetAxis("Horizontal");
        float ve = Input.GetAxis("Vertical");

        if (ho != 0 || ve != 0)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
        if(!GameManager.endGame)
        {
            oxygen -= Time.deltaTime * 3;
        }
        oxygen = Mathf.Clamp(oxygen, 0, 100);
        if (oxygen <= 0)
        {
            h.health -= Time.deltaTime * 15; 
        }
        
        x = -Mathf.Sign(cursor.position.x - transform.position.x);
        rb.velocity = new Vector3(ho * speed * Time.deltaTime, ve * speed * Time.deltaTime, 0);
        Vector3 camPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        cursor.transform.position = new Vector3(camPoint.x, camPoint.y, 0);
        gunPivot.LookAt(cursor);

        if (PointSlope.contains == 1 || PointSlope.contains == -1) { transform.localScale = new Vector3(PointSlope.contains * c * 0.3f, transform.localScale.y, transform.localScale.z); }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1) && greAmo > 0)
        {
            Grenade();
            greAmo--;
        }
        if (Mathf.Round(transform.eulerAngles.z) < 180 && Mathf.Round(transform.eulerAngles.z) > 0)
        {
            c = -1;
        }
        else
        {
            c = 1;
        }
        if (speedDecrease)
        {
            rb.velocity = Vector3.zero;
        }


    }

    void Shoot()
    {
        Instantiate(bullet.gameObject, bulletOut.position, bulletOut.transform.rotation);
    }

    void Grenade()
    {
        Instantiate(grenade.gameObject, bulletOut.position, bulletOut.transform.rotation);
    }

    void Dead()
    {
        anim.SetFloat("Health", 0.0f);
        gameObject.tag = "DeadPlayer";
        GameOverPanel.SetActive(true);
        rb.gravityScale = 0;  
        gameObject.GetComponent<PlayerController>().enabled = false;
        speedDecrease = true;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) && collision.gameObject.tag == "DeadEnemy")
        {
            a += Time.deltaTime;
            Materials mE = collision.gameObject.GetComponent<Materials>();

            mE.text.text = "looting...";

            if (a > 0.5f)
            {
                oxygen += mE.oxygen;
                fuel += mE.fuel;
                material += mE.material;

                int gre = Random.Range(0, 100);
                if (gre > 65)
                {
                    mE.text.text = (Mathf.Round(mE.oxygen) + " Oxygen, " + mE.fuel + " fuel, " + mE.material + " material, 1 grenade.");
                    greAmo++;
                }
                else
                {
                    mE.text.text = (Mathf.Round(mE.oxygen) + " Oxygen, " + mE.fuel + " fuel, " + mE.material + " material.");
                }

                collision.gameObject.tag = "LootedEnemy";
                a = 0;
            }
        }

        else if (collision.gameObject.tag == "DeadEnemy")
        {
            Materials mE = collision.gameObject.GetComponent<Materials>();

            mE.text.text = "Hold 'E' to loot.";
        }


        if (collision.gameObject.tag == "DeadShip")
        {
            if (gm.neededFuel > fuel || gm.neededMaterial > material || gm.wrenchToggle.isOn != true)
            {
                int v;
                if (wrench)
                {
                    v = 0;
                }

                else
                {
                    v = 1;
                }
                shiptext.text = (gm.neededFuel - fuel) + " fuel, " + (gm.neededMaterial - material) + " material and " + v + "  wrench left";
            }

            else
            {
                gm.Victory();
            }
        }

        if (collision.gameObject.tag == "Wrench")
        {
            Destroy(collision.gameObject);
            wrench = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeadEnemy")
        {
            Materials mE = collision.gameObject.GetComponent<Materials>();
            mE.text.text = null;
        }

        if (collision.gameObject.tag == "DeadShip")
        {
            shiptext.text = null;
        }
    }

   
}
