using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Collisions : MonoBehaviour {

	public AudioClip crystalCollectedClip;
	public AudioClip playerGetHitByBulletClip;
	public AudioClip playerGetHitClip;
	public AudioClip playerDiedClip;

	public static string layerDamage = "Damage";
	public static string layerEnemy = "Enemy";
	public static string layerBullet = "Bullet";
	public static string layerCrystal = "Crystal";

	public int startingHealth = 100;
	public int levelWallDamageValue = 100;
	public int currentHealth;
	public Slider healthSlider; 
	public int points = 0;

	public Text pointsText;

//	StatsScript statsScript;
	GameControllerScript gScript;

	void Awake()
	{
//		statsScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<StatsScript> ();
		gScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameControllerScript> ();
		
		currentHealth = startingHealth;
		healthSlider.value = currentHealth;
	}



//	void OnCollisionEnter(Collision coll)
//	{
//
//		if (coll.gameObject.layer == LayerMask.NameToLayer (layerDamage)) {
//			LevelDamageHit();
//		}
//		else if(coll.gameObject.layer == LayerMask.NameToLayer (layerEnemy))
//		{
//			//EnemyHit(); <-- kein Zugriff auf Kollidierendes GameObject!
//			//EnemyHit(coll); <-- mit Zugriff auf Kollidierendes GameObject
// 			Debug.Log ("Enemy Hit");
//			Destroy(coll.gameObject);
//		}
//		else if(coll.gameObject.layer == LayerMask.NameToLayer (layerCrystal))
//		{
//			CrystalHit();
//		}
//	}

	void OnTriggerStay(Collider other)
	{

		}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.layer == LayerMask.NameToLayer (layerDamage)) {
			LevelDamageHit();
		}
		else if(other.gameObject.layer == LayerMask.NameToLayer (layerBullet))
		{
			//EnemyHit(); <-- kein Zugriff auf Kollidierendes GameObject!
			//EnemyHit(coll); <-- mit Zugriff auf Kollidierendes GameObject
			Debug.Log ("Bullet Hit");
			//Destroy(other.gameObject);
			
			points -= 10;
			
			Destroy(other.gameObject);
			
			currentHealth -= 10;
			if(currentHealth >=0)
			{
				healthSlider.value = currentHealth;
			}
			if(currentHealth <= 0)
			{
				gScript.PlayerDied();
				audio.PlayOneShot(playerDiedClip);
				//				Application.LoadLevel(Application.loadedLevel);
			}
			else
			{
				audio.PlayOneShot(playerGetHitByBulletClip);
			}
		}
		else if(other.gameObject.layer == LayerMask.NameToLayer (layerEnemy))
		{
			//EnemyHit(); <-- kein Zugriff auf Kollidierendes GameObject!
			//EnemyHit(coll); <-- mit Zugriff auf Kollidierendes GameObject
			Debug.Log ("Enemy Hit");
			//Destroy(other.gameObject);
			
			points -= 100;

			Destroy(other.gameObject);

			currentHealth -= 50;
			if(currentHealth >=0)
			{
				healthSlider.value = currentHealth;
			}
			if(currentHealth <= 0)
			{
				gScript.PlayerDied();
				audio.PlayOneShot(playerDiedClip);
//				Application.LoadLevel(Application.loadedLevel);
			}
			else
			{
				audio.PlayOneShot(playerGetHitClip);
			}
		}
		else if(other.gameObject.layer == LayerMask.NameToLayer (layerCrystal))
		{
//			statsScript.PlayerHitCrystal();
			gScript.PlayerHitCrystal();
			audio.PlayOneShot(crystalCollectedClip);
			Destroy (other.gameObject);
		}
	}

	void EnemyHit(Collider other)
	{
		Debug.Log ("Enemy Hit");
		
	}

	void EnemyHit(Collision coll)
	{
		Debug.Log ("Enemy Hit");
		
	}

	void CrystalHit()
	{
		Debug.Log ("Crystal Hit");
		
		points++;
//		pointsText.text = "Points " + points;
//		Destroy(other.gameObject);
		//animation?
	}


	void LevelDamageHit()
	{
		Debug.Log ("LevelDamage Hit");
		
		points = 0;
		currentHealth -= levelWallDamageValue;
		if(currentHealth >=0)
		{
			healthSlider.value = currentHealth;
			audio.PlayOneShot(playerGetHitClip);
		}
		if(currentHealth <= 0)
		{
			audio.PlayOneShot(playerDiedClip);
			gScript.PlayerDied();
		}
	}


//	void OnGUI()
//	{
//		GUILayout.Box("Points: " + points.ToString());
//	}
}
