using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour {

	Animator anim;
	public GameObject player;
	public GameObject bullet;  
	public GameObject FireTransform; 

	//mine
	public GameObject enemy;
    //GameObject player = GameObject.FindGameObjectWithTag ("Player"); 

    //audio
    //public AudioSource enemySound;
    //public AudioClip enemyShootClip; 

	public GameObject GetPlayer(){
		return player; 
	}

	//Penny's Fire code : I might need to add code to the bullet itself (separate enemy bullet)
	//this code will make the bullet explode when it hits something
	void Fire (){
		GameObject b = Instantiate (bullet, FireTransform.transform.position, FireTransform.transform.rotation);
		b.GetComponent<Rigidbody> ().AddForce (FireTransform.transform.forward*3000);

        //enemySound.clip = enemyShootClip;
        //enemySound.Play();
        //Debug.Log("enemy sound played");
    }

	public void StopFiring(){
		CancelInvoke ("Fire");
	}

	public void StartFiring(){
		//fire frequency
		InvokeRepeating ("Fire", 0.5f, 0.5f);


    }

	public void MineStopFiring(){
		CancelInvoke ("Fire");
	}

	public void MineStartFiring(){
		//fire frequency
		InvokeRepeating ("Fire", 0.5f, 0.5f);
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//calculate distance to player
		anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));


		TankHealth tankhealth = enemy.GetComponent<TankHealth> ();
		//update enemy fuel level float in animator
		anim.SetFloat("fuel_level", tankhealth.m_CurrentHealth); //want to access m_CurrentHealth

		//is_alive
		//I think this one doesn't work
		anim.SetBool("is_alive", tankhealth.is_alive); 
		//Debug.Log ("IS_ALIVE value test");
		//Debug.Log (tankhealth.is_alive); 


	}
		
}
