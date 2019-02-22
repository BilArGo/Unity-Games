using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public static int elimination;
    public static float time;
    public int neededFuel;
    public int neededMaterial;
    public int enemyAmount;
    public float spawnableArea;
    public GameObject enemy;
    public Image hp;
    public Image o2;
    public Text fuelText;
    public Text materialText;
    public Toggle wrenchToggle;
    public Text eliminationT;
    public Text eliminationTV;
    public Text timeT;
    public Text timeVT;
    public GameObject VictoryPanel;
    public GameObject wrenchG;
    public GameObject Arrow;
    public Transform ship;
    public float score;
    public Text scoreT;
    public Text scoreVT;
    public Text grenade;
    public GameObject Warning;

    public static bool endGame;

    public GameObject[] curEnemies;
    Transform player;
    PlayerController pc;
    int curFuel;
    int curMaterial;
    bool wrench;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        pc = player.GetComponent<PlayerController>();
        endGame = false;
        WrenchSpawn();
    }

    void Update()
    {

        if (curFuel > neededFuel && curMaterial > neededMaterial && wrench)
        {
            Arrow.GetComponent<Animator>().Play("ArrowBlink");
        }

        if (Vector3.Distance(ship.position, player.position) > 240 && !endGame)
        {
            Warning.SetActive(true);
        }
        else
        {
            Warning.SetActive(false);
        }

        if (Vector3.Distance(ship.position, player.position) > 300)
        {
            player.GetComponent<Health>().health = 0;
        }

        grenade.text = "x" + player.GetComponent<PlayerController>().greAmo;

        eliminationT.text = elimination.ToString();
        eliminationTV.text = elimination.ToString();

        timeT.text = (Mathf.FloorToInt(time / 60) + "m " + Mathf.Floor(time - Mathf.FloorToInt(time / 60) * 60)) + " second";
        timeVT.text = (Mathf.FloorToInt(time / 60) + "m " + Mathf.Floor(time - Mathf.FloorToInt(time / 60) * 60)) + " second";

        scoreT.text = "Score:  " + Mathf.Round(score).ToString();

        scoreVT.text = "Score:  " + Mathf.Round(score).ToString();

        if (!endGame)
        {
            time += Time.deltaTime;

        }
        curFuel = pc.fuel;
        curMaterial = pc.material;
        wrench = pc.wrench;
        if (player.tag == "DeadPlayer")
        {
            GameOver();
        }

        hp.rectTransform.sizeDelta = new Vector2(player.GetComponent<Health>().health, 20);
        o2.rectTransform.sizeDelta = new Vector2(pc.oxygen, 20);
        fuelText.text = pc.fuel + "/" + neekkkkkkorkmakorkma sondedFuel;
        materialText.text = pc.material + "/" + neededMaterial;
        wrenchToggle.isOn = pc.wrench;

        if (pc.fuel > neededFuel)
        {
            fuelText.color = Color.green;
        }
        if (pc.material > neededMaterial)
        {
            materialText.color = Color.green;

        }



        curEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (curEnemies.Length < enemyAmount && !endGame)
        {

            Vector3 pos = new Vector3(player.position.x + Random.Range(-spawnableArea, spawnableArea), player.position.y + Random.Range(-spawnableArea, spawnableArea), 0);
            if (Vector3.Distance(pos, player.position) > 60)
            {
                SpawnEnemy(pos);         
            }

        }

    }

    void GameOver()
    {
        endGame = true;
    }

    void SpawnEnemy (Vector3 p)
    {
        Instantiate(enemy, p, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }

    public void FadeOut()
    {
        SceneManager.LoadScene("Game");


    }

    public void Victory()
    {
        endGame = true;
        VictoryPanel.SetActive(true);

        score = elimination * 100 - time * 3;

        for (int i = 0; i < curEnemies.Length; i++)
        {
            Destroy(curEnemies[i]);
        }
    }


    void WrenchSpawn()
    {
        GameObject w = Instantiate(wrenchG, new Vector3(Random.Range(70, -70), Random.Range(70, -70), 0), Quaternion.identity);
        if (Vector3.Distance(player.position, w.transform.position) < 70)
        {
            Destroy(w);
            WrenchSpawn();

        }
    }


}
