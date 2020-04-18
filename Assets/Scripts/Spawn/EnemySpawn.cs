using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {


	public GameObject player; 
	public GameObject spawnPrefab1;
    public GameObject bigEnemy;
    public GameObject bigEnemy2; 

	public float minSecondsBetweenSpawning = 3.0f;
	public float maxSecondsBetweenSpawning = 6.0f;

	public Transform chaseTarget;

	private float savedTime;
	private float secondsBetweenSpawning;

	private int totalEnemies; 
	private int numEnemies = 0;
    private bool spawnBigEnemy = false;
    private bool spawnBigenemy2 = false;

    private int currentScore;

    private float time; 

	// Use this for initialization
	void Start () {
		savedTime = Time.time;
		secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
        //reset enemy total 
        totalEnemies = 10;
        spawnBigEnemy = false;
        spawnBigenemy2 = false;
	}

	// Update is called once per frame
	void Update () {
		numEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;

        time = Time.timeSinceLevelLoad; 

        if (time > 20.0f) {
            spawnBigEnemy = true;
        }
        if (time > 50.0f) {
            spawnBigenemy2 = true;
        }

        //get current score
        TwinStickPlayerMove twinStickPlayerMove = player.GetComponent<TwinStickPlayerMove>();
        currentScore = twinStickPlayerMove.points;
        //update total num enemies based on score
        if (currentScore > 200)
        {
            totalEnemies = 12;
        }
        else if (currentScore > 400)
        {
            totalEnemies = 14;
        }
        else if (currentScore > 500)
        {
            totalEnemies = 16;
        }
        else if (currentScore > 600)
        {
            totalEnemies = 18;
        }
        else if (currentScore > 800)
        {
            totalEnemies = 20;
        }
        else if (currentScore > 900)
        {
            totalEnemies = 25;
        }
        else if (currentScore > 1000)
        {
            totalEnemies = 30;
        }
        else if (currentScore > 1500)
        {
            totalEnemies = 40;
        }
        else if (currentScore > 2000) {
            totalEnemies = 50; 
        }


        if (Time.time - savedTime >= secondsBetweenSpawning) // is it time to spawn again?
		{

            if (numEnemies < totalEnemies){

                if (spawnBigEnemy == true)
                {
                    MakeBigEnemyToSpawn();
                }

                if (spawnBigenemy2 == true) {
                    MakeBigEnemyToSpawn2();
                }

                MakeThingToSpawn();
				savedTime = Time.time; // store for next spawn
				secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);

            }
		}	
	}

	void MakeThingToSpawn()
	{
		// create a new gameObject
		GameObject clone = Instantiate(spawnPrefab1, transform.position, transform.rotation) as GameObject;

		//set references to player
		clone.gameObject.GetComponent<TankAI> ().player = player; 
		clone.gameObject.GetComponent<TankHealth> ().m_player = player; 

	}

    void MakeBigEnemyToSpawn()
    {
        // create a new gameObject
        GameObject clone = Instantiate(bigEnemy, transform.position, transform.rotation) as GameObject;

        //set references to player
        clone.gameObject.GetComponent<TankAI>().player = player;
        clone.gameObject.GetComponent<TankHealth>().m_player = player;

    }

    void MakeBigEnemyToSpawn2()
    {
        // create a new gameObject
        GameObject clone = Instantiate(bigEnemy2, transform.position, transform.rotation) as GameObject;

        //set references to player
        clone.gameObject.GetComponent<TankAI>().player = player;
        clone.gameObject.GetComponent<TankHealth>().m_player = player;

    }
}