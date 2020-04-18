using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour {

	public int value = 10;
	//public GameObject explosionPrefab;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			//if (GameManager.gm!=null)

			//add fuel value PUT CODE HERE

				
				// tell the game manager to Collect
				//GameManager.gm.Collect (value);
			

			// explode if specified
			//if (explosionPrefab != null) {
			//	Instantiate (explosionPrefab, transform.position, Quaternion.identity);


			// destroy after collection
			Destroy (gameObject);
		}
	}
}