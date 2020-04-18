using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public Button play; 
	public Button quit; 

	SimpleGameManager GM; 

	public 

	void Awake() {
		GM = SimpleGameManager.Instance; 
		GM.OnStateChange += HandleOnStateChange;
	}

	public void HandleOnStateChange(){
		Debug.Log ("OnStateChange!");
	}

	public void OnGUI(){
		//menu layout
	
	}
}
