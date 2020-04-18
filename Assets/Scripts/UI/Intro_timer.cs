using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro_timer : MonoBehaviour {

	// Use this for initialization
	void Start () {

		StartCoroutine (SceneChangeTimer ());
	}

	IEnumerator SceneChangeTimer(){
		yield return new WaitForSecondsRealtime (2.5f);
		//Application.LoadLevel ("Menu");
        SceneManager.LoadScene("Menu");
    }

}
