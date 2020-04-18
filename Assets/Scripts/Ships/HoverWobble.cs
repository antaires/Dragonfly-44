using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverWobble : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.Translate (0, Random.Range(0.0f,0.05f),0);

	}
}
