using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalExplosion : MonoBehaviour {

	public ParticleSystem ExplosionParticles;       
	//public AudioSource ExplosionAudio; 


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "PlayerLaser") {

			Explode (); 
		}
	}

	void Explode () {
		ExplosionParticles.Play();
		//ExplosionAudio.Play(); 

		//destroy gameobject after timer

		Debug.Log ("Crystal hit"); 

		//detach particles
		DetachParticles(); 

		Destroy (gameObject);
		//StartCoroutine (ExplosionTimer ());
	}

	IEnumerator ExplosionTimer(){
		yield return new WaitForSecondsRealtime (.2f);
		Destroy (gameObject); 
	}

	public void DetachParticles(){
		ExplosionParticles.transform.parent = null;

		//ExplosionParticles.emission = 0;

		//ExplosionParticles.GetComponent<ParticleSystem> ().= true; 
	
	}

	void Invisible(){

		//get list of children
		//GameObject[] allChildren = GetComponentsInChildren<GameObject>();
		//foreach (GameObject child in allChildren) {
			//iterate over child list and disable
		//	rend = child.GetComponent<MeshRenderer>();
		//	rend.enabled = false; 
		//}

	

	}
}
