using UnityEngine;
using System.Collections;

public class PlayerProjectileScript : MonoBehaviour {
	
	#region SendDamage
	[SerializeField]
	private float damageValue = 100f;
	#endregion

	#region Health
	[SerializeField]
	private float health = 100f;
	[SerializeField]
	private bool invincible = false;
	[SerializeField]
	private bool destroyOnAnyCollision = true;
	[SerializeField]
	private bool destroyOnLevelCollision = true;
	#endregion
	
	#region Movement
	/**
	 * Movement
	 **/
	public void StartImpulse(Vector2 moveDirection, float maxVelocity)
	{

	}
	#endregion
}
