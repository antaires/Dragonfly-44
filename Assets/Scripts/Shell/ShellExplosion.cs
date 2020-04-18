using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    //public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;   


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other) //called whenever laser hits something
    {
        // Find all the tanks in an area around the shell and damage them.
		Collider[] colliders = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_TankMask); 

		for (int i = 0; i < colliders.Length; i ++){
			Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>(); 

			if (!targetRigidbody) 
				continue;

			//TEST - check for tags. if this.bullet tag != EnemyLaser and rigidbody.tag != enemy, do the following
			if (this.gameObject.tag == "EnemyLaser"){
				//Debug.Log ("fired laser is enemy laser");
				if (targetRigidbody.gameObject.tag == "Player") {
					//Debug.Log ("enemy hit player");
					// do the code
					targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius); 
					//add damange
					TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
					if (!targetHealth)
						continue; 
					//get position from rigidbody
					float damage = CalculateDamage (targetRigidbody.position);
					//apply damage
					targetHealth.TakeDamage(damage);

                    Explode(); 
				}
			}else if (this.gameObject.tag == "PlayerLaser"){
				if (targetRigidbody.gameObject.tag == "Enemy") {
					// do the code
					targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius); 
					//add damange
					TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
					if (!targetHealth)
						continue; 
					//get position from rigidbody
					float damage = CalculateDamage (targetRigidbody.position);
					//apply damage
					targetHealth.TakeDamage(damage);

                    Explode(); 
				}
			}


			//targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius); 
			//add damange
			//TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
			//if (!targetHealth)
			//	continue; 
			//get position from rigidbody
			//float damage = CalculateDamage (targetRigidbody.position);
			//apply damage
			//targetHealth.TakeDamage(damage); 
			
		}



			
    }

    void Explode() {

        //destroy laser but keep particles and explosion sound, which are children of laser
        //to do this we unparent them 
        m_ExplosionParticles.transform.parent = null;

        //play particle effect
        m_ExplosionParticles.Play();
        //play audio source
        //m_ExplosionAudio.Play();

        //destroy particles 
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
        //destroy explosion by destroying the laser itself
        Destroy(gameObject);

    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on position.

		//create vector from target to shell
		Vector3 explosionToTarget = targetPosition - transform.position; 

		//get how long (magnitude) vector is
		float explosionDistance = explosionToTarget.magnitude; 

		//relative ditance
		float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius; 

		float damage = relativeDistance * m_MaxDamage; 

		//make sure damage not negative (if negative, set it to 0)
		damage = Mathf.Max(0f, damage); 

		return damage; 

    }
}