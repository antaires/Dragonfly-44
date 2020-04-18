using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseSFM : StateMachineBehaviour {

	public GameObject NPC;
	public GameObject opponent; 
	public UnityEngine.AI.NavMeshAgent agent;
	public float speed = 2.0f;
	public float rotSpeed = 1.0f;
	public float accuracy = 3.0f;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		NPC = animator.gameObject;
		opponent = NPC.GetComponent<TankAI>().GetPlayer(); 
		agent = NPC.GetComponent<UnityEngine.AI.NavMeshAgent> (); 
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
