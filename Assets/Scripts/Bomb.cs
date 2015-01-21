using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public AudioClip bombExplode;
	public AudioClip bombHitEnemy;

//	StatsScript statsScript;
	GameControllerScript gScript;



	public bool triggered = false;

	Vector3 triggerPos;
	Vector3 startPos;
	Vector3 endPos;

	float startRadius = 0.1f;
	float radiusStepps = 0.2f;
	float endRadius = 4f;
	float currentRadius;

	float effectTime = 1.0f;

	// Use this for initialization
	void Start () {
//		statsScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<StatsScript> ();
		gScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameControllerScript> ();
		currentRadius = startRadius;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (triggered) {
			currentRadius += radiusStepps;
			Physics.CheckSphere (this.transform.position, currentRadius);
			DebugCircle();
			Destroy(this.gameObject, effectTime);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			if (triggered) {
				Debug.Log ("crystal bombed");
				Destroy (other.transform.gameObject);
//				statsScript.BombHitEnemy();
				gScript.BombHitEnemy();
			}
		}
	}


	void OnTriggerEnter(Collider other) {
		/**
		 * Layer 
		 **/
		if (other.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			if (triggered) {
				Debug.Log ("crystal bombed");
				Destroy (other.transform.gameObject);
//				statsScript.BombHitEnemy();
				gScript.BombHitEnemy();
			}
		}

		/**
		 * Tags 
		 **/
		if(other.tag == "Player")
		{
			if(!triggered)
				TriggerAction();
		}
		else if(other.tag == "Enemy")
		{
			if(triggered) {
				Debug.Log("Enemy bombed");
//				audio.PlayOneShot (bombHitEnemy);
				GameObject.FindGameObjectWithTag ("GameController").audio.PlayOneShot (bombHitEnemy);
				Destroy(other.transform.gameObject);
			}
		}
	}

//	void OnCollisionEnter(Collision coll) {
//		if(coll.gameObject.layer == LayerMask.NameToLayer("Enemy"))
//		{
//			if(triggered) {
//				Debug.Log("crystal bombed");
//				Destroy(coll.transform.gameObject);
//			}
//		}
//	}

	void TriggerAction()
	{
		triggered = true;
		triggerPos = this.transform.position;

//		audio.PlayOneShot (bombExplode);
		GameObject.FindGameObjectWithTag ("GameController").audio.PlayOneShot (bombExplode);

		rigidbody.velocity = Vector3.zero;
		this.transform.Find ("LevelStopper").gameObject.SetActive(false);

		this.GetComponent<Animator> ().SetTrigger ("explode");
	}

	void DebugCircle()
	{
//		for(int i=0; i< 360; i++)
//		{
			Debug.DrawLine(triggerPos,triggerPos+new Vector3(currentRadius,currentRadius,0),Color.green,2f);
//		}		

	}

}
