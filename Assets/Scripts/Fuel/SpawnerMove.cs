using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMove : MonoBehaviour {

	public GameObject pyramid;
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
			Instantiate (pyramid, transform.position, Quaternion.identity);

			period = 0; 
		}

		period += UnityEngine.Time.deltaTime; 

	}

	void transformPosition (){
		//generate random numbers within the area of the arena
		//apply these numbers to x and y values of the transform of the object 
		transform.position = new Vector3(Random.Range(-80.0f, 80.0f), 10f, Random.Range(-40.0f, 40.0f)); 
		//Debug.Log ("pyramid position");
		//Debug.Log(transform.position); 
		//generate random numbers within the area of the arena
		//apply these numbers to x and y values of the transform of the object 
		//transform.position = new Vector3(Random.Range(-400.0f, 400.0f), 10f, Random.Range(-190.0f, 190.0f)); 
	}

//	IEnumerator waitTenSeconds(){
//		yield return new WaitForSeconds(10); 
//	}
		
}
