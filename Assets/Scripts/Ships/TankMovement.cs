using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;         
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;       
    public AudioSource m_MovementAudio;    
    public AudioClip m_EngineIdling;       
    public AudioClip m_EngineDriving;      
    public float m_PitchRange = 0.2f;

   	//this sets the horizontal1 or horizontal2 depending on m_PlayerNumber
    private string m_MovementAxisName;     
    private string m_TurnAxisName;         
    private Rigidbody m_Rigidbody;         
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;         


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
		//kills tank using kinematic, and this is called to birth a new tank and establish defaults
		// kinematic = want physics but no forces effect it
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
		//tank dies, no longer effected by physics
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
		//setting up the axis names (edit --> preferences --> input) 
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }
    

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName); 
		m_TurnInputValue = Input.GetAxis (m_TurnAxisName);

		//a function deals with the audio
		EngineAudio (); 
	}


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
    	
		// work out if ship moving
		if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f) {

			//check which audio clip, switch it if wrong, and change pitch within range
			if (m_MovementAudio.clip == m_EngineDriving) {
				m_MovementAudio.clip = m_EngineIdling; 
				m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange); 
				m_MovementAudio.Play (); 
			}

		} else { //idling
			if (m_MovementAudio.clip == m_EngineIdling) {
				m_MovementAudio.clip = m_EngineDriving; 
				m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange); 
				m_MovementAudio.Play (); 
			}
		}
	}


	private void FixedUpdate() //runs every physics step, rather than every rendered fram (like update)
    {
		// Move and turn the tank -- ie. apply the input obtained in Update()
		Move (); 
		Turn (); 
	}


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
		//transform.forward, positive for forward, negative for backward
		//time.delta time is smoothing out the movement 
		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime; 

		//apply movement to rigidbody
		//need to add current position to movement to it moves relative to itself and not the world
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement); 
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.

		//turn in degrees
		float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime; 

		//spin up or down with z...now set to 0
		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f); 

		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation); 
    }
}






















