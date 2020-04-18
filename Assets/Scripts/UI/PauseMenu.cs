using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PauseMenu : MonoBehaviour {

    public GameObject mc;
    public GameObject pc;

    private Canvas miniC;
    private Canvas pauseC;

    private bool isPaused; 

	// Use this for initialization
	void Start () {
        isPaused = false;
        Time.timeScale = 1;

        miniC = mc.GetComponent<Canvas>();
        pauseC = pc.GetComponent<Canvas>();
        //disable pause on start
        pauseC.enabled = false;


    }
	
	// Update is called once per frame
	void Update () {
        //get list of all enemies to freeze rigidbodies
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        //get gameobject player to freeze rigidbody
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (Input.GetKeyDown(KeyCode.P)) { 

            //pause game
            if (isPaused == false){
                isPaused = true;
                Time.timeScale = 0;
                //Time.fixedDeltaTime = 0; 
                //disable hover wobble script enemy
                foreach (GameObject enemy in enemyList) {
                    enemy.GetComponent<HoverWobble>().enabled = false;
                }
                //disable player hover wobble
                player.GetComponent<HoverWobble>().enabled = false;

                //deactivate minimap canvas
                miniC.enabled = false;
                //activiate pause canvas
                pauseC.enabled = true; 

                //pause music


                // resume game
            } else if (isPaused == true)
            {
                isPaused = false;
                Time.timeScale = 1;
                //Time.fixedDeltaTime = 1; 

                //deactivate pause canvas
                //activate minimap canvas
                miniC.enabled = true;
                pauseC.enabled = false;

                //enable hover wobble script
                foreach (GameObject enemy in enemyList)
                {
                    enemy.GetComponent<HoverWobble>().enabled = true;
                }
                player.GetComponent<HoverWobble>().enabled = true;

                //play music
            }
        }

    }
}
