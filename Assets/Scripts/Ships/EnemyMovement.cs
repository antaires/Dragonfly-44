using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public Transform goal;

	private float speed = 5f; 
	private float accuracy = 0.5f; 
	private float rotSpeed = 5f; 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 lookAtGoal = new Vector3 (goal.position.x, this.transform.position.y, goal.position.z);

		Vector3 direction = lookAtGoal - this.transform.position; 

		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation(direction),Time.deltaTime * rotSpeed);
		//this.transform.LookAt (lookAtGoal);
		//Vector3 direction = goal.position - this.transform.position; 
		//Debug.DrawRay (this.transform.position, direction, Color.red);
		if (direction.magnitude > accuracy) {
			this.transform.Translate (direction.normalized * speed * Time.deltaTime, Space.World); 
		}
	}
}
