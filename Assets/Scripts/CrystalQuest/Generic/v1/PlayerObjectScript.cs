using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerObjectScript : MovingObject {

	#region Initialization
	protected override void Start ()
	{
		base.Start ();
		NotifyLifeListener (lifes);

		CreateProjectilePool ();
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

	[SerializeField]
	GameObject projectilePrefab;

	void CreateProjectilePool ()
	{
		if (projectilePrefab != null)
		{
			for (int i=0; i < projectilePoolAmount; i++)
			{
				AddNewToPool ();
			}
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
		for (int i=0; i < projectilePoolAmount; i++)
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
	void Update()
	{
		if(CanUseInput())
		{
			inputMoveDirection.x = Input.GetAxis("Horizontal");
			inputMoveDirection.y = Input.GetAxis("Vertical");
			
			inputFire = Input.GetButton("Fire1");
			inputUseItem = Input.GetButton("UseItem");
		}
		else
		{

		}

		if (inputFire)
		{

			GameObject projectile = GetPooledObject ();
			if (projectile != null)
			{
				ProjectileObjectScript projectileScript = projectile.GetComponent<ProjectileObjectScript>();
				projectileScript.OwnerObjectScript = this;
				Vector3 shootDirection = inputMoveDirection;
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

			// no Objectpooling, bad performance
//			if (projectilePrefab != null)
//			{
//				// http://answers.unity3d.com/questions/40379/how-is-the-rotation-of-a-transform-converted-into.html
//				GameObject projectile = (GameObject) Instantiate (projectilePrefab, transform.position, Quaternion.identity);
//				ProjectileObjectScript projectileScript = projectile.GetComponent<ProjectileObjectScript>();
//				projectileScript.OwnerObjectScript = this;
////				Vector3 shootDirection = this.transform.rotation.eulerAngles;
////				Vector3 shootDirection = this.rb2D.velocity.normalized;
//				Vector3 shootDirection = inputMoveDirection;
//				if (shootDirection == Vector3.zero)
//				{
//					shootDirection = transform.rotation.eulerAngles;
//					if (shootDirection == Vector3.zero)
//						shootDirection = Vector2.up;
//				}
////				Debug.Log (shootDirection);
//				projectileScript.ReleasedWithVelocity (shootDirection, weaponForce);
//			}
		}
	}
	#endregion

	#region Movement
	void FixedUpdate()
	{
		// inputDirection abs [-1;+1]
		Mathf.Clamp(inputMoveDirection.x, -1f, +1f);
		Mathf.Clamp(inputMoveDirection.y, -1f, +1f);
		
		// Better
		//Vector3 normalized = Vector3.Normalize(moveDirection);
		
		// Movement
		if(CanMove())
			rb2D.MovePosition(rb2D.position + (inputMoveDirection*maxVelocity) * Time.fixedDeltaTime);
	}
	#endregion

	#region customs
	protected override void HealthUpdated ()
	{
		base.HealthUpdated ();
		NotifyHealthListener(Health); 
	}

	public override void Die ()
	{
		base.Die ();
		DecreaseLifeCount (1);
		NotifyDiedListener (Lifes);
	}

	public override void RestartLevel ()
	{
		base.RestartLevel ();
		isDead = false;
		this.gameObject.SetActive (true);
		this.transform.position = Vector3.zero;
	}
	#endregion
}
