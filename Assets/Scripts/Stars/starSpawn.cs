using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starSpawn : MonoBehaviour {

	public GameObject star1;
	public GameObject star2;
	public GameObject star3;
	public GameObject star4;
	public GameObject star5; 

	public float period = 0.0f; 

	// Use this for initialization
	void Start () {

		transform.position = new Vector3(0.0f, 10f, 0.0f);

	}

	// Update is called once per frame
	void Update () {

		//StartCoroutine ("waitTenSeconds");

		if (period > 5) {


			transformPosition (); 

			//need to make it wait a random number of seconds between a range before birth
			//birth pyramid
			GameObject temp = star1;
			int num = Random.Range(1,6); 
			if (num <= 1) {
				temp = star1;
			} else if (num <= 2) {
				temp = star2;
			} else if (num <= 3) {
				temp = star3;
			} else if (num <= 4) {
				temp = star4;
			} else {
				temp = star5; 
			}
				
			Instantiate (temp, transform.position, Quaternion.identity);

			period = 0; 
		}

		period += UnityEngine.Time.deltaTime; 

	}

	void transformPosition (){
		//generate random numbers within the area of the arena
		//apply these numbers to x and y values of the transform of the object 
		transform.position = new Vector3(Random.Range(-80.0f, 80.0f), 10f, Random.Range(-80.0f, 80.0f)); 

	}

}
