using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BurstShootingPooled : MonoBehaviour {

	[SerializeField]
	bool shootTrigger = false;

	[SerializeField]
	Vector2 m_ShootDirection;

	#region Burst Amount
	[SerializeField]
	int currentBurstProjectileAmount = 0;

	[SerializeField]
	int randBurstProjectileAmount = 0;

	[SerializeField]
	int minBurstProjectileAmount = 1;

	[SerializeField]
	int maxBurstProjectileAmount = 5;
	#endregion

	#region Projectiles
	[SerializeField]
	bool randomizeBurstProjectileIntervall = true;

	[SerializeField]
	float randTimeBetweenProjectiles = 0f;

	[SerializeField]
	float minTimeBetweenProjectiles = 0.1f;

	[SerializeField]
	float maxTimeBetweenProjectiles = 0.5f;
	#endregion 
	
	#region Burst
	[SerializeField]
	float randTimeBetweenBursts = 0f;

	[SerializeField]
	float minTimeBetweenBursts = 2f;

	[SerializeField]
	float maxTimeBetweenBursts = 6f;

	[SerializeField]
	float nextProjectileTimeStamp = 0f;

	[SerializeField]
	float nextBurstTimeStamp = 0f;
	#endregion 
	

	void Awake ()
	{
		CreateProjectilePool ();
	}

	bool CanUseWeapon ()
	{
		if(canUseWeapon)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	#region Projectile
	[SerializeField]
	protected GameObject projectilePrefab;

	[SerializeField]
	bool overrideProjectileMaxStayTimer = false;

	[SerializeField]
	float projectileMaxStayTimer = 1f;
	
	[SerializeField]
	protected Vector3 projectileSpawnPosition;
	#endregion
	
	#region Weapon
	[SerializeField]
	protected bool canUseWeapon = true;
	
	[SerializeField]
	protected float fireRateIntervall = 1f;
	
	[SerializeField]
	protected int numberOfProjectilesPerShoot = 1;
	
	[SerializeField]
	protected float reloadTime = 1.5f;
	
	[SerializeField]
	protected int magazineSize = 5;
	
	[SerializeField]
	protected int currentMagazineSize = 0;
	#endregion
	
	#region Shoot
	[SerializeField]
	protected float lastShootTimestamp = 0f;
	
	[SerializeField]
	protected float nextShootTimestamp = 0f;
	#endregion
	
	#region Action
	void FixedUpdate ()
	{
		Burst ();
//		if (CanShoot ())
//		{
//			if (shootTrigger)
//			{
//				shootTrigger = false;		// Trigger
//				Shoot ();
//			}
//		}
	}

	void InitNextRandBurstAmount ()
	{
		currentBurstProjectileAmount = 0;
		randTimeBetweenBursts = Random.Range (minTimeBetweenBursts, maxTimeBetweenBursts);
		nextBurstTimeStamp = Time.time + randTimeBetweenBursts;
		randBurstProjectileAmount = Random.Range (minBurstProjectileAmount, maxBurstProjectileAmount);

		if (!randomizeBurstProjectileIntervall)
			randTimeBetweenProjectiles = Random.Range (minTimeBetweenProjectiles, maxTimeBetweenProjectiles);
	}

	void Burst ()
	{
		if (Time.time >= nextBurstTimeStamp)
		{
			if (currentBurstProjectileAmount >= randBurstProjectileAmount)
			{
				// first, and if burst is over, init next burst
				InitNextRandBurstAmount ();
			}
			else
			{
				// burst is not over, rand time to next bullet
				if (Time.time >= nextProjectileTimeStamp)
				{
					m_ShootDirection = Random.onUnitSphere;	//TODO
					Shoot ();
					currentBurstProjectileAmount++;	// count up and spawn
					if (randomizeBurstProjectileIntervall)
					{
						// randomize every projectile spawnIntervall in Burst
						randTimeBetweenProjectiles = Random.Range (minTimeBetweenProjectiles, maxTimeBetweenProjectiles);
					}
					nextProjectileTimeStamp = Time.time + randTimeBetweenProjectiles;
				}
			}
		}
	}
	
	bool CanShoot()
	{
		if (!CanUseWeapon())
			return false;
		
		if (Time.time >= nextShootTimestamp)
		{
			nextShootTimestamp += fireRateIntervall;
			return true;
		}
		return false;
	}
	
	void Shoot ()
	{
		GameObject projectile = GetPooledObject ();
		if (projectile != null)
		{
			Projectile projectileScript = projectile.GetComponent<Projectile>();
			projectileScript.OwnerObject = this.gameObject;
			if (overrideProjectileMaxStayTimer)
				projectileScript.SetSelfDestroyTime (projectileMaxStayTimer);
			Vector3 shootDirection = m_ShootDirection;
			if (shootDirection == Vector3.zero)
			{
				shootDirection = transform.rotation.eulerAngles;
				if (shootDirection == Vector3.zero)
					shootDirection = Vector2.up;
			}
			projectileScript.transform.position = this.transform.position + shootDirection;
			projectile.SetActive (true);
			projectileScript.ReleasedWithVelocity (shootDirection, weaponForce);
		}
	}
	#endregion
	
	#region Weapon
	[SerializeField]
	float weaponForce = 10f;
	#endregion
	
	#region Projectile Pool

	[SerializeField]
	int projectilePoolAmount = 5;
	
	[SerializeField]
	bool projectilePoolWillGrow = false;
	
	[SerializeField]
	int projectilePoolAmountMax = 10;
	
	[SerializeField]
	List<GameObject> projectilePool;
	
//	[SerializeField]
//	GameObject projectilePrefab;
	
	void CreateProjectilePool ()
	{
		if (projectilePrefab != null)
		{
			if (projectilePool != null)
			{
				InitPool ();
			}
			else
			{
				projectilePool = new List<GameObject>();
				InitPool ();
			}
		}
	}
	
	void InitPool ()
	{
		for (int i=0; i < projectilePoolAmount; i++)
		{
			AddNewToPool ();
		}
	}
	
	GameObject AddNewToPool ()
	{
		GameObject obj = (GameObject) Instantiate (projectilePrefab);
		obj.SetActive (false);
		projectilePool.Add (obj);
		return obj;
	}
	
	// Update is called once per frame
	public GameObject GetPooledObject () {
		
		//		for (int i=0; i < projectilePoolAmount; i++)
		for (int i=0; i < projectilePool.Count; i++)
		{
			if(!projectilePool[i].activeInHierarchy)
			{
				return projectilePool[i];
			}
		}
		
		if (projectilePoolWillGrow)
		{
			if (projectilePool.Count < projectilePoolAmountMax)
				return AddNewToPool (); 
		}
		
		return null;
	}
	#endregion

	#region Input

//	Vector2 inputMoveDirection;
//	void Update()
//	{
//		inputMoveDirection.x = Input.GetAxis("Horizontal");
//		inputMoveDirection.y = Input.GetAxis("Vertical");
//		inputShoot = Input.GetButton("Fire1");
//
//		SetShootDirection (inputMoveDirection);
//	}

	void SetShootDirection (Vector2 direction)
	{
		if (direction != Vector2.zero)
		{
			m_ShootDirection = direction.normalized;
		}
		else
		{
			// use last shootDirection...
		}
	}

	public void TriggerShoot (Vector2 direction)
	{
		shootTrigger = true;
		SetShootDirection (direction);
	}
	#endregion
}
