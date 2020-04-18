using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TankHealth : MonoBehaviour
{
	public float m_StartingHealth = 100f;          
	public Slider m_Slider;                        
	public Image m_FillImage;                      
	public Color m_FullHealthColor = Color.green;  
	public Color m_ZeroHealthColor = Color.red;    
	public GameObject m_ExplosionPrefab;


	private AudioSource m_ExplosionAudio;
    public AudioSource ShipAudio;
    public AudioClip healthClip;
    //public AudioClip lowHealthWarningClip;

    private ParticleSystem m_ExplosionParticles;   
	public float m_CurrentHealth; 
	private bool m_Dead;  

	//my own variables for fuel
	public GameObject m_FuelPrefab; 
	public float m_fuel_value = 50f; 
	//my own additions for accessing player number of tanks
	public GameObject m_TankPrefab; 
	public GameObject m_player;
	public GameObject m_enemy; 
	//public TankManager[] m_Tanks; 
	//public float m_PlayerFuelAdd; 
	//camera
	public GameObject camera_rig; 

	//mine for enemy FSM is_alive bool parameter
	public bool is_alive = true; 
	public int enemyDeathPoints = 50;

    //highscore
    private int tempScore = 0;
    private string tempName; 

	private void Awake()
	{
		m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
		m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

		m_ExplosionParticles.gameObject.SetActive(false);
	}


	private void OnEnable()
	{
		m_CurrentHealth = m_StartingHealth;
		m_Dead = false;

		SetHealthUI();
	}


	//my addition FUEL DECREMENT
	public void FuelDecrease(){
		//decrease
		m_CurrentHealth -= 1f * Time.deltaTime;
		//set health
		SetHealthUI();
	}

	//my addition FUEL INCREASE (enemy)
	public void FuelIncrease(){
		//increase
		if (m_CurrentHealth < 100){
			m_CurrentHealth += 1f * Time.deltaTime; 
			//set health
			SetHealthUI(); 
		}
	}
		

	void Update(){

		// increase enemy health with time
		if (this.gameObject.tag == "Enemy") {
			//Debug.Log ("enemy on Tank health");
			FuelIncrease (); 

			//if(this.gameObject.activeSelf == true){
			//	Debug.Log ("ENEMY IS ACTIVE"); 
			//}

			//if (this.gameObject.activeSelf == false) {
			//	is_alive = false; 
			//	Debug.Log ("ENEMY IS INACTIVE, is_alive = False"); 
			//} 

			//check if health at 0, set is_alive to false
			//This never gets set to False for some reason...
			//if (m_CurrentHealth < 5) {
			//	is_alive = false; 
			//	if (is_alive == false) {
			//		Debug.Log("IS ALIVE = FALSE"); 
			//	}
			//}
		}

		//reduce player health with time
		if (this.gameObject.tag == "Player") {
			//Debug.Log ("player on tank health");
			FuelDecrease();

            //low health warning
            //if (m_CurrentHealth <= 15) {
             //   ShipAudio.clip = lowHealthWarningClip;
            //    ShipAudio.Play();
            //}


        }


		if (m_CurrentHealth <= 0f && !m_Dead) {
			OnDeath (); 
		}
			
	}


	public void TakeDamage(float amount)
	{
		// Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
		//subtract damage amount (amount varies depending on how close the laser is)
		m_CurrentHealth -= amount; 

		//update health UI 
		SetHealthUI (); 

		//check if dead
		if (m_CurrentHealth <= 0f && !m_Dead){
            //give player points for death
            if (this.gameObject.tag == "Enemy")
            {
                TwinStickPlayerMove twinStickPlayerMove = m_player.GetComponent<TwinStickPlayerMove>();
                twinStickPlayerMove.points += enemyDeathPoints;

                //give player health for killing enemy
                //access tankHealth script of player
                TankHealth tankhealth = m_player.GetComponent<TankHealth>();
                //add points to player
                if (tankhealth.m_CurrentHealth < 100)
                {
                    //change this HIGHER = easier game, LOWER = harder
                    tankhealth.m_CurrentHealth += 5;
                }
                //set health
                SetHealthUI();

                OnDeath();
            }
		}

		//if damaged gameObject == enemy, add points to player
		//if (this.gameObject.tag == "Enemy"){
			//access tankHealth script of player
			//TankHealth tankhealth = m_player.GetComponent<TankHealth> ();
			//add points to player
			//if (tankhealth.m_CurrentHealth < 100) {
				//change this HIGHER = easier game, LOWER = harder
			//	tankhealth.m_CurrentHealth += 5;
			//}
			//set health
			//SetHealthUI();
		//}
	}


	private void SetHealthUI()
	{
		// Adjust the value and colour of the slider.
		m_Slider.value = m_CurrentHealth; 
		m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
	}


	private void OnDeath()
	{
		// Play the effects for the death of the tank and deactivate it.
		m_Dead = true;

		//move paticles to ship location
		m_ExplosionParticles.transform.position = transform.position;

		//turn particles back on
		m_ExplosionParticles.gameObject.SetActive (true); 

		//play particles and explosion audio
		m_ExplosionParticles.Play();
		m_ExplosionAudio.Play (); 

		//leave attack FSM
		//if (this.gameObject.tag == "Player"){
		//	is_alive = false; 
		//Debug.Log("IS_ALIVE = FALSE"); 
		//}

		//load game over if player dies
		if (this.gameObject.tag == "Player"){
			//get access to points
			TwinStickPlayerMove twinStickPlayerMove = m_player.GetComponent<TwinStickPlayerMove> ();
			int score = twinStickPlayerMove.points;

            //if score is greater than highscore1, replace it (do for all 5 highscores) 
            if (score > PlayerPrefs.GetInt("Highscore1"))
            {
                SetCurrentName("name1");
                //move all other scores down
                //5 = 4
                tempScore = PlayerPrefs.GetInt("Highscore4");
                PlayerPrefs.SetInt("Highscore5", tempScore);
                tempName = PlayerPrefs.GetString("name4");
                PlayerPrefs.SetString("name5", tempName);
                //4 = 3
                tempScore = PlayerPrefs.GetInt("Highscore3");
                PlayerPrefs.SetInt("Highscore4", tempScore);
                tempName = PlayerPrefs.GetString("name3");
                PlayerPrefs.SetString("name4", tempName);
                //3 = 2
                tempScore = PlayerPrefs.GetInt("Highscore2");
                PlayerPrefs.SetInt("Highscore3", tempScore);
                tempName = PlayerPrefs.GetString("name2");
                PlayerPrefs.SetString("name3", tempName);
                //get 1, set as 2
                tempScore = PlayerPrefs.GetInt("Highscore1");
                PlayerPrefs.SetInt("Highscore2", tempScore);
                tempName = PlayerPrefs.GetString("name1");
                PlayerPrefs.SetString("name2", tempName);
                //set score
                PlayerPrefs.SetInt("Highscore1", score);
            }
            else if (score > PlayerPrefs.GetInt("Highscore2"))
            {
                SetCurrentName("name2");
                //move scores down
                //5 = 4
                tempScore = PlayerPrefs.GetInt("Highscore4");
                PlayerPrefs.SetInt("Highscore5", tempScore);
                tempName = PlayerPrefs.GetString("name4");
                PlayerPrefs.SetString("name5", tempName);
                //4 = 3
                tempScore = PlayerPrefs.GetInt("Highscore3");
                PlayerPrefs.SetInt("Highscore4", tempScore);
                tempName = PlayerPrefs.GetString("name3");
                PlayerPrefs.SetString("name4", tempName);
                //3 = 2
                tempScore = PlayerPrefs.GetInt("Highscore2");
                PlayerPrefs.SetInt("Highscore3", tempScore);
                tempName = PlayerPrefs.GetString("name2");
                PlayerPrefs.SetString("name3", tempName);

                //set score
                PlayerPrefs.SetInt("Highscore2", score);
            }
            else if (score > PlayerPrefs.GetInt("Highscore3"))
            {
                SetCurrentName("name3");
                //5 = 4
                tempScore = PlayerPrefs.GetInt("Highscore4");
                PlayerPrefs.SetInt("Highscore5", tempScore);
                tempName = PlayerPrefs.GetString("name4");
                PlayerPrefs.SetString("name5", tempName);
                //4 = 3
                tempScore = PlayerPrefs.GetInt("Highscore3");
                PlayerPrefs.SetInt("Highscore4", tempScore);
                tempName = PlayerPrefs.GetString("name3");
                PlayerPrefs.SetString("name4", tempName);

                PlayerPrefs.SetInt("Highscore3", score);
            }
            else if (score > PlayerPrefs.GetInt("Highscore4"))
            {
                SetCurrentName("name4");
                tempScore = PlayerPrefs.GetInt("Highscore4");
                PlayerPrefs.SetInt("Highscore5", tempScore);
                tempName = PlayerPrefs.GetString("name4");
                PlayerPrefs.SetString("name5", tempName);

                PlayerPrefs.SetInt("Highscore4", score);
            }
            else if (score > PlayerPrefs.GetInt("Highscore5"))
            {
                SetCurrentName("name5");
                PlayerPrefs.SetInt("Highscore5", score);
            }
            else {
                SetCurrentName("none");
            }
            //save
            PlayerPrefs.Save();
            SceneManager.LoadScene("GameOver");
            //Application.LoadLevel ("GameOver"); 
		}

		Destroy (this.gameObject); 
		//turn ship off
		gameObject.SetActive(false); 
	}

    void SetCurrentName(string name) {
        PlayerPrefs.SetString("currentName", name);
    }


	//MINE add fuel for pyramid collection
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Fuel") {
			if (m_CurrentHealth < 100) {
				m_CurrentHealth += 20; 
			}
            //play health sound
            ShipAudio.clip = healthClip;
            ShipAudio.Play();

            //set health
            SetHealthUI();
		}
	}




}