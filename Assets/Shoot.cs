using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	Accelerator acc;
	public GameObject prefabBullet;
	public int magazinCapacity = 3;
	public int currentCapacity = 0;
	public float magazinReloadTime = 2f;
	public float bulletStayTime = 2f;
	public float bulletSpeed = 20;
	public float timeBetweenBullets = 0.2f;
	public float lastBulletTime = 0f;

	public AudioClip enemyShootClip;

	// Use this for initialization
	void Start () {
		currentCapacity = magazinCapacity;
		acc = GetComponent<Accelerator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0)
		{
			DoShoot();
		}
	}

	void DoShoot()
	{
		if(currentCapacity >= 1)
		{
			if(Time.time > lastBulletTime + timeBetweenBullets)
			{
				lastBulletTime = Time.time;
				Vector3 direction = acc.dir;
				direction.Normalize();
				
				GameObject bullet = Instantiate (prefabBullet, this.transform.position + direction, Quaternion.identity) as GameObject;
				Destroy (bullet, bulletStayTime);
				
				bullet.GetComponent<Rigidbody> ().velocity = direction * bulletSpeed; 
				GameObject.FindGameObjectWithTag ("GameController").audio.PlayOneShot (enemyShootClip);

				currentCapacity--;
			}
		}
		else
		{
			if(!reloading)
			{
				StartCoroutine(ReloadMagazin(magazinReloadTime));
			}
		}
	}

	bool reloading = false;

	IEnumerator ReloadMagazin(float waitTime) {
		reloading = true;
		yield return new WaitForSeconds(waitTime);
		RealoadMagazonDone ();
		print("WaitAndPrint " + Time.time);
	}

	public void RealoadMagazonDone()
	{
		currentCapacity = magazinCapacity;
		reloading = false;
	}
}
