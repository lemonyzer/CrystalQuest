using UnityEngine;
using System.Collections;

public class MoveToTargetAndShoot : MonoBehaviour {

	public AudioClip enemyShootClip;
	public GameObject target;

	public float aliveTime = 0f;

	public int magazin = 2;

	public float waitForShooting = 2f;
	public float currentWaiting = 0f;
	public float timeBetweenBullets = 1f;
	public float shootCoolDownTime = 1f;
	public GameObject prefabBullet;
	public float bulletSpeed = 5f;
	public bool shootEnable = false;
	public float bulletStayTime = 2f;

	// Use this for initialization
	void Start () {
	
	}

	void Shoot()
	{

		//differenz vektor
		Vector3 direction = target.transform.position - this.transform.position;
		direction.Normalize();

		GameObject bullet = Instantiate (prefabBullet, this.transform.position + direction, Quaternion.identity) as GameObject;
		Destroy (bullet, bulletStayTime);

		bullet.GetComponent<Rigidbody> ().velocity = direction * bulletSpeed; 
		GameObject.FindGameObjectWithTag ("GameController").audio.PlayOneShot (enemyShootClip);
	}

//	IEnumerable ShootInterval()
//	{
//
//	}

	// Update is called once per frame
	void Update () {

		if(!shootEnable)
		{
			currentWaiting += Time.deltaTime;
			if(currentWaiting >= waitForShooting)
			{
				shootEnable = true;
			}
		}
		else
		{
			currentWaiting += Time.deltaTime;
			if(currentWaiting >= timeBetweenBullets)
			{
				currentWaiting = 0f;
				Shoot();
			}
		}




		//differenz vektor
		Vector3 direction = target.transform.position - this.transform.position;

		aliveTime += Time.deltaTime;

		if(aliveTime < 0.6f)
		{
			// clamp acceleration vector to unit sphere
			if (direction.sqrMagnitude > 1)
			{
				direction.Normalize();
			}
			
//			direction = direction * (aliveTime/2f);
		}
		else if(aliveTime > 0f && aliveTime < 10f)
		{
			// clamp acceleration vector to unit sphere
			if (direction.sqrMagnitude > 1)
			{
				direction.Normalize();
			}

//			direction = direction * (aliveTime/2f);
		}
		else
		{
			if (direction.sqrMagnitude > 1)
			{
				direction.Normalize();
			}
			direction = direction * (3f);
		}

		transform.Translate (direction * Time.deltaTime);
	}

//	void OnCollisionEnter(Collision coll)
//	{
//		
//		if (coll.gameObject.layer == LayerMask.NameToLayer ("Player")) {
//			LevelDamageHit();
//		}
//	}
}
