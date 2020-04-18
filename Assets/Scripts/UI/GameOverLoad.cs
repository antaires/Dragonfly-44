using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLoad : MonoBehaviour {

	//public GameObject player; 

	// Update is called once per frame
	void Update () {

		if (GameObject.FindGameObjectsWithTag ("Player") == null) {
			Debug.Log ("LOAD GAME OVER LEVEL");
            SceneManager.LoadSceneAsync("GameOver");
            //Application.LoadLevel ("GameOver");
		}

		//TankHealth tankhealth = player.GetComponent<TankHealth> (); 

		//if (tankhealth.m_CurrentHealth <= 0) { 
		//	Debug.Log ("LOAD GAME OVER LEVEL"); 
		//	Application.LoadLevel ("Game_Over"); 
		//}

		
	}
}
