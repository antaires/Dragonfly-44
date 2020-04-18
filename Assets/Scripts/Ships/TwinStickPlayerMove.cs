using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwinStickPlayerMove : MonoBehaviour
{

    //variables
    public float moveSpeed = 5;
    public float jumpForce = 5;


    //public float heightLimit = 10.0f;
    //public GameObject camera; 
    //public GameObject playerObj; 

    //shooting variables
    public GameObject fireTransform;
    public GameObject bullet;
    public float waitTime = 0.2f;

    //mine variables
    //if want to add mine pickups, they can access this variable and set it to 0, then 10 etc for a time limit
    public int mineLimit = 10;
    public GameObject mineTransform;
    public GameObject mine;
    public float mineWaitTime = 0.2f;

    //score
    public int points;
    //add connection to score text here and update it with points
    public Text scoreText;

    //audio for stars and shooting
    public AudioSource ShipAudio;
    public AudioClip shootingClip;
    public AudioClip coinCollectClip;
    public AudioClip mineDropClip;
    public AudioClip shipJumpClip;

    //geometry to tilt on move
    public GameObject shipGeometry; 

    void Start()
    {
        points = 0;
        SetScore();

    }

    //methods
    void FixedUpdate()
    {
        //player faces mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;
        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;

            //if use playerObj for global movement:
            //playerObj.transform.rotation = Quaternion.Slerp (playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        //player move
        if (Input.GetKey(KeyCode.W))
        {
            //move by local axis
            //transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime); 

            //move by global axis
            transform.position += (Vector3.forward * moveSpeed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.S))
        {
            //move by local
            //transform.Translate (Vector3.back * moveSpeed * Time.deltaTime); 

            //move by global
            transform.position += (Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A)) 
        {
            //transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);

            //move by global
            transform.position += (Vector3.left * moveSpeed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.D)) 
        {
            //transform.Translate (Vector3.right * moveSpeed * Time.deltaTime); 

            //move by global
            transform.position += (Vector3.right * moveSpeed * Time.deltaTime);

        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            //play shooting audio
            ShipAudio.clip = shootingClip;
            ShipAudio.Play();
        }

        if (Input.GetMouseButtonDown(1))
        {
            //count all mines
            int numPlayerMines = GameObject.FindGameObjectsWithTag("PlayerMine").Length;
            //keep total player mines to below 10
            if (numPlayerMines < mineLimit)
            {
                DropMine();
                //play audio
                ShipAudio.clip = mineDropClip;
                ShipAudio.Play();
            }
        }

        //jump
        if (Input.GetKey(KeyCode.Space))
        {

            transform.position += (Vector3.up * jumpForce * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //jump audio
            ShipAudio.clip = shipJumpClip;
            ShipAudio.Play();
        }

        //keep player from flying away on enemy hit
        //float playerPosY = transform.position.y;  
        //if (playerPosY > heightLimit) {
        //    transform.position += (Vector3.down * Time.deltaTime); 
        //}


        SetScore();

    }

    //MINE add fuel for pyramid collection
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Star_One")
        {
            points += 1;
            //play audio
            ShipAudio.clip = coinCollectClip;
            ShipAudio.Play();

        }
        else if (other.gameObject.tag == "Star_Five")
        {
            points += 5;
            //play audio
            ShipAudio.clip = coinCollectClip;
            ShipAudio.Play();
        }
        SetScore();
    }



    void SetScore()
    {
        //update points on UI
        scoreText.text = points.ToString();
    }


    void DropMine()
    {

        //test
        Vector3 playerPos = this.transform.position;
        Vector3 playerDirection = this.transform.forward;
        Quaternion playerRotation = this.transform.rotation;
        //float spawnDistance = 10; 

        //Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

        Instantiate(mine.transform, mineTransform.transform.position, playerRotation);

    }

    void Shoot()
    {

        //test
        Vector3 playerPos = this.transform.position;
        Vector3 playerDirection = this.transform.forward;
        Quaternion playerRotation = this.transform.rotation;
        //float spawnDistance = 10; 

        //Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

        Instantiate(bullet.transform, fireTransform.transform.position, playerRotation);

    }

    void drift() {


    }

    void Tilt() {
        Vector3 tiltRot = new Vector3(20f, 0f, 0f);
        Quaternion tiltRotation = Quaternion.LookRotation(tiltRot);
        //tiltRotation.x = 0;
        //tiltRotation.z = 0;
        shipGeometry.transform.rotation = Quaternion.Slerp(transform.rotation, tiltRotation, 7f * Time.deltaTime);

        //Vector3 wRotation = 

        //Quaternion geoRotation = shipGeometry.transform.rotation;
        //transform.rotation += Vector3;
    }

}