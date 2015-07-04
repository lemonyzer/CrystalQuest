using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootingPooled : MonoBehaviour {

	public void Disable ()
	{
		shootTrigger = false;
		this.enabled = false;
	}

	public void Enable ()
	{
		shootTrigger = false;
		this.enabled = true;
	}

	[SerializeField]
	bool shootTrigger = false;

	[SerializeField]
	Vector2 m_ShootDirection;

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
	float projectileSpawnDistance = 0.5f;

//	[SerializeField]
//	protected Vector3 projectileSpawnPosition;
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
		if (CanShoot ())
		{
			if (shootTrigger)
			{
				shootTrigger = false;		// Trigger
				Shoot ();
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
			projectileScript.transform.position = this.transform.position + (shootDirection * projectileSpawnDistance);
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
