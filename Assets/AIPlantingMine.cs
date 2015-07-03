using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPlantingMine : MonoBehaviour {

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
			if (HasValidLayerDistance ())
				myPooledShooting.TriggerShoot (Vector3.zero);
		}
	}

	[SerializeField]
	List<LayerDistance> layerDistances;

	bool HasValidLayerDistance ()
	{
		return !IsOverlaping (this.transform.position);
	}

	bool IsOverlaping (Vector3 position)
	{
		for (int i = 0; i < layerDistances.Count; i++)
		{
			LayerDistance current = layerDistances[i];
			Collider2D overlappingCollider2d = Physics2D.OverlapCircle (position, current.distance, current.layerMask);
			if (overlappingCollider2d != null)
			{
				//				Debug.Log ("overlapping " + overlappingCollider2d.gameObject.ToString () + " @ " + overlappingCollider2d.transform.position);
#if UNITY_EDITOR 
				Debug.DrawLine (this.transform.position, overlappingCollider2d.transform.position, Color.red, 5f);
#endif 
				return true;
			}
		}
		return false;
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
