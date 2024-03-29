﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : NPCBaseSFM {

	public GameObject fuel; 

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo,layerIndex);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//I greyed this out for navmesh
		//rotate away from target
		var direction = (NPC.transform.position - opponent.transform.position);
		NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, 
			Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
		NPC.transform.Translate(0,0, Time.deltaTime * speed);
	
		//turn away player
		//agent.SetDestination(-opponent.transform.position);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
