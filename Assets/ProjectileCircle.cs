using UnityEngine;
using System.Collections;

public class ProjectileCircle : MonoBehaviour {

	[SerializeField]
	GameObject projectilePrefab;

	[SerializeField]
	int amount;

	[SerializeField]
	float force;

	public void Shoot ()
	{
		if (projectilePrefab != null)
		{
			float angle = 360/amount;
			for (int i=0; i<amount; i++)
			{
				// calculate spawnpoints and moveDirection
				// instantiate / pool bullet
				GameObject projectile = (GameObject) Instantiate (projectilePrefab, this.transform.position, Quaternion.identity);

				Vector3 moveDireciton = Vector3.zero;
				moveDireciton.x = Mathf.Cos (i*angle);
				moveDireciton.y = Mathf.Sin (i*angle);

				// add force/velocity
				projectile.GetComponent<Projectile>().ReleasedWithVelocity(moveDireciton, force);
			}

		}
	}
}
