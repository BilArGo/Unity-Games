using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelChanger : MonoBehaviour {



    void Start () {
		
	}
	
	void Update () {
		
	}



    public void FadeIn ()
    {
        gameObject.SetActive(false);
    }

    


    public void ReloadLevel()
    {
        SceneManager.LoadScene("Game");
    }
}
