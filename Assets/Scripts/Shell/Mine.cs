using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

	public int explosionForce = 50000; 
	public int explosionRadius = 10; 
	public float timer = 10.0f; 
	public int jumpForce = 200; 
	public float jumpTimer = 0.2f;
    public float damage = 25.0f;

    public Rigidbody rb;
    public GameObject visiblePyramid; 

	public ParticleSystem ExplosionParticles;

    //audio
    public AudioSource ExplosionAudio;
    public AudioClip explosionClip;

    public GameObject target;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody> ();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		timer -= Time.deltaTime; 
		if (timer <= 0) {
            timer = 50f; 

			//apply upward force to rigidbody
			while (jumpTimer > 0) {
				jumpTimer -= Time.deltaTime;
				rb.AddForce (transform.forward * jumpForce);
			}

			if (this.gameObject.tag == "Mine"){
				//get player rigidbody and reduce player health if in range
				//find gameobject with tag "player"
				target = GameObject.FindGameObjectWithTag ("Player"); 
				//if player.position - mine postion <= explosion radius:
				float distance = Vector3.Distance (target.transform.position, transform.position);
				if (distance <= explosionRadius) {
					//do damage
					Rigidbody targetRigidbody = target.GetComponent<Rigidbody> (); 
					//move player with explosion (can improve this to push player away from mine)
					targetRigidbody.AddForce (transform.up * explosionForce);
					//add damange
					TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth> (); 
					//apply damage
					targetHealth.TakeDamage (damage); 
				}
			}

			//need to make a list of all enemy positions to check if they are in range when timer runs out
			if (this.gameObject.tag == "PlayerMine"){
				//get list of all enemies
				GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy"); 
				foreach (GameObject enemy in enemyList) {

					float distance = Vector3.Distance (enemy.transform.position, transform.position);
					if (distance <= explosionRadius) {
						//do damage
						Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody> (); 
						//move player with explosion (can improve this to push player away from mine)
						enemyRigidbody.AddForce (transform.up * explosionForce);
						//add damange
						TankHealth enemyHealth = enemyRigidbody.GetComponent<TankHealth> (); 
						//apply damage
						enemyHealth.TakeDamage (damage); 
					}
					

				}

			}

            Explode();

        }
	}

	//if player hits mine, explode and damage player
	private void OnTriggerEnter(Collider other){

		if (this.gameObject.tag == "Mine") {
			if (other.gameObject.tag == "Player") {
				Rigidbody targetRigidbody = other.GetComponent<Rigidbody> (); 
				//move player with explosion (can improve this to push player away from mine)
				targetRigidbody.AddForce (transform.forward * explosionForce);
				//add damange
				TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth> (); 
				//apply damage
				targetHealth.TakeDamage (damage); 

				Explode ();  
			} else if (other.gameObject.tag == "PlayerLaser") {
				Explode (); 
			}
		}

		if (this.gameObject.tag == "PlayerMine") {
			if (other.gameObject.tag == "Enemy") {
				Rigidbody targetRigidbody = other.GetComponent<Rigidbody> (); 
				//move player with explosion (can improve this to push player away from mine)
				targetRigidbody.AddForce (transform.forward * explosionForce);
				//add damange
				TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth> (); 
				//apply damage
				targetHealth.TakeDamage (damage); 

				Explode ();  
			} //else if (other.gameObject.tag == "enemy_laser") {
			//	Explode (); 
			//}
		}

	}

	void Explode () {
        visiblePyramid.SetActive(false);

		ExplosionParticles.Play();

        ExplosionAudio.clip = explosionClip;
        ExplosionAudio.Play();

        //destroy gameobject after timer
        StartCoroutine(ExplosionTimer ());
		}

	IEnumerator ExplosionTimer(){
		yield return new WaitForSecondsRealtime (0.5f);
        Destroy (ExplosionParticles); 
		Destroy (gameObject); 
	}
}
