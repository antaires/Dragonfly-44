using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol2 : NPCBaseSFM {

	GameObject[] waypoints;
	int currentWP; 

	void Awake(){
		//find all gameobjects with tag "waypoints"
		waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
	}

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator,stateInfo,layerIndex);
		currentWP = 0; 

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//patrolling and moving 
		if(waypoints.Length == 0) return;
		if (Vector3.Distance (waypoints [currentWP].transform.position,
			NPC.transform.position) < accuracy) {
			currentWP++;
			Debug.Log ("current waypoing");
			Debug.Log (currentWP); 
			if (currentWP >= waypoints.Length) {
				currentWP = 0;
			}
		}

		agent.SetDestination (waypoints[currentWP].transform.position); 
		//Greyed this out with Navmesh
		//rotate torwards target //move 
		//var direction = waypoints[currentWP].transform.position - NPC.transform.position;
		//NPC.transform.rotation = Quaternion.Slerp (NPC.transform.rotation, 
		//	Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
		//NPC.transform.Translate (0,0,Time.deltaTime * speed); //float value == speed of NPC
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}

	//BELOW IS USED FOR ANIMATION

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
