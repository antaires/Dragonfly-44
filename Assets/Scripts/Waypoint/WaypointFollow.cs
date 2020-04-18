using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour {

	//array of waypoints
	//public GameObject[] waypoints;
	public UnityStandardAssets.Utility.WaypointCircuit circuit; 

	int currentWP = 0; 

	private float speed = 5f; 
	private float accuracy = 0.5f; 
	private float rotSpeed = 5f; 

	// Use this for initialization
	void Start () {
		//waypoints = GameObject.FindGameObjectsWithTag ("Waypoint"); 

	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (circuit.Waypoints.Length == 0)
			return; 

		Vector3 lookAtGoal = new Vector3 (circuit.Waypoints[currentWP].position.x, this.transform.position.y, circuit.Waypoints[currentWP].position.z);
		Vector3 direction = lookAtGoal - this.transform.position; 
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

		if (direction.magnitude < accuracy) {
			currentWP++;
			if (currentWP >= circuit.Waypoints.Length) {
				currentWP = 0;
			}
		}
		this.transform.Translate (0, 0, speed * Time.deltaTime);
	}
}
