using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrift : MonoBehaviour {

    public float pitch = 0f;
    public float roll = 0f; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //player move
        if (Input.GetKey(KeyCode.W))
        {
            if (pitch >= -20)
            {
                pitch -= 2 * Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.W)) {
            pitch = 0; 
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (pitch <= 20)
            {
                pitch += 2 * Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            pitch = 0;
        }


        if (Input.GetKey(KeyCode.A))
        {
            if (roll <= 20)
            {
                roll += 2 * Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            roll = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (roll >= -20)
            {
                roll -= 2 * Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            roll = 0;
        }


        //apply pitch / roll
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(pitch, 0f, roll), 10.0f * Time.deltaTime);

    }
}
