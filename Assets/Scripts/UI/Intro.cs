using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

	SimpleGameManager GM; 

	void Awake(){
		GM = SimpleGameManager.Instance;
		GM.OnStateChange += HandleOnStateChange;

		Debug.Log ("Current game state when Awakes: " + GM.gameState);
	}

	void Start(){
		Debug.Log ("Current game state when Starts: " + GM.gameState);
	}

	public void HandleOnStateChange(){
		GM.SetGameState (GameState.MAIN_MENU);
		Debug.Log ("Handle state change to: " + GM.gameState);
		Invoke ("Loadlevel", 3f); 
	}

	public void LoadLevel(){
		//Application.LoadLevel ("Menu");
        SceneManager.LoadSceneAsync("Menu");

    }

}
