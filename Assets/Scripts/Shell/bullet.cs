using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	public float bulletSpeed = 50.0f; 
	public float maxDistance; 
	//public GameObject player; 
	
	// Update is called once per frame
	void Update () {

		//works but bullet in one direction
		transform.Translate (Vector3.forward * Time.deltaTime * bulletSpeed); 
		maxDistance += 1 * Time.deltaTime; 

		if (maxDistance >= 5){
			Destroy (this.gameObject);
		}
	}
}
