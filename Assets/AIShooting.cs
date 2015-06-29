using UnityEngine;
using System.Collections;

public class AIShooting : MonoBehaviour {

	[SerializeField]
	ShootingPooled myPooledShooting;

	[SerializeField]
	bool aiShooting = true;

	[SerializeField]
	bool sprayProjectiles = false;

	float nextTriggerTime = 0f;

	[SerializeField]
	float timeBetweenTrigger = 0f;

	[SerializeField]
	float minTimeBetweenTrigger = 0.5f;

	[SerializeField]
	float maxTimeBetweenTrigger = 4f;

	[SerializeField]
	bool useWeaponSettings = true;

	ShootingPooled InitMyPooledShooting (ShootingPooled current)
	{
		if (current == null)
			current = this.GetComponent<ShootingPooled> ();
//		else
//			return current;

		if (current == null)
		{
			Debug.LogError (this.ToString () + " needs ShootingPooled Script attached!");
			this.enabled = false;
		}
		else
		{
			if (useWeaponSettings)
				ReadWeaponSettings (current);
		}

		return current;
	}

	void ReadWeaponSettings (ShootingPooled current)
	{
//		minTimeBetweenTrigger = current.
//		maxTimeBetweenTrigger = current.
	}

	void Awake ()
	{
		myPooledShooting = InitMyPooledShooting (myPooledShooting);
	}

	void Update ()
	{
		if (aiShooting)
			RandomShootAi ();
	}

	float RandomFireRate ()
	{
		return Random.Range (minTimeBetweenTrigger, maxTimeBetweenTrigger);
	}

	void RandomShootAi ()
	{
		if (Time.time >= nextTriggerTime)
		{
			timeBetweenTrigger = RandomFireRate ();
			nextTriggerTime = Time.time + timeBetweenTrigger;
			myPooledShooting.TriggerShoot (Random.onUnitSphere);
		}
	}

	public void StopShooting ()
	{
		aiShooting = false;
	}

	public void ResumeShooting ()
	{
		aiShooting = true;
	}

	public void StartShooting ()
	{
		aiShooting = true;
		nextTriggerTime = Time.time;
	}

	public void RestartShooting ()
	{
		aiShooting = true;
		nextTriggerTime = Time.time;
	}
}
