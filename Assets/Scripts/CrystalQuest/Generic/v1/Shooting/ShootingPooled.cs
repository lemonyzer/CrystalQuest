using UnityEngine;
using System.Collections;

public class ShootingPooled : MonoBehaviour {

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
	protected Vector3 projectileSpawnPosition;
	#endregion
	
	#region Weapon
	[SerializeField]
	protected bool canUseWeapon = true;
	
	[SerializeField]
	protected float fireRate = 1f;
	
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
			Shoot ();
		}
	}
	
	bool CanShoot()
	{
		if (!CanUseWeapon())
			return false;
		
		if (Time.time >= nextShootTimestamp)
		{
			nextShootTimestamp += fireRate;
			return true;
		}
		return false;
	}
	
	void Shoot ()
	{
		if(projectilePrefab != null)
		{
			GameObject projectile = GameObject.Instantiate(projectilePrefab, projectileSpawnPosition, Quaternion.identity) as GameObject;
			ProjectileObjectScript projectileScript = projectile.GetComponent<ProjectileObjectScript>();
			// Projectile Enemyspezifisch initialisieren
//			projectileScript.OwnerObjectScript = this;
		}
	}
	#endregion
}
