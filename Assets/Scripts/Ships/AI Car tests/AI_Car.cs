using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Car : MonoBehaviour {

	public Transform goal;
	float acceleration = 5f;
	float deceleration = 5f;
	float minSpeed = 0.0f;
	float maxSpeed = 100.0f;
	float brakeAngle = 20.0f;	//determines the angle for which a a car will start braking 
	public float rotSpeed = 1.0f;
	float speed = 0;
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		Vector3 lookAtGoal = new Vector3 (goal.position.x, this.transform.position.y, goal.position.z);
		Vector3 direction = lookAtGoal - this.transform.position; 

		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime * rotSpeed);

		//constant speed
		//speed = Mathf.Clamp (speed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);

		//speed changes based on angle between the forward vector of vehicle and goal 
		if (Vector3.Angle (goal.forward, this.transform.forward) > brakeAngle) {
			speed = Mathf.Clamp (speed - (deceleration * Time.deltaTime), minSpeed, maxSpeed);
		} else {
			speed = Mathf.Clamp (speed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);

		} 	

		this.transform.Translate (0, 0, speed);

	}
}
