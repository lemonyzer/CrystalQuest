using UnityEngine;


[System.Serializable]
public class CollisionObject : DamageAbleObject
{

	#region CollisionObject

	[SerializeField]
	protected bool useCollisionEvents = false;

	[SerializeField]
	private float collisionSendDamageValue = float.PositiveInfinity;
	public float CollisionSendDamageValue {
		get { return collisionSendDamageValue; }
		set { collisionSendDamageValue = value; }
	}

	public float GetCollisionDamageValue ()
	{
		return collisionSendDamageValue;
	}

	public void ApplyCollisionDamage (CollisionObject otherObjectScript)
	{
		ReceiveDamage(otherObjectScript.collisionSendDamageValue);
	}

	#endregion

	#region Collision 2D
	/**
	 * Collision's
	 **/
	
	// 2D
	//public delegate void OnTriggered2D(CollisionScript collisionScript, CollisionObject collisionObject, Collider2D otherCollider);
	//public delegate void OnTriggered2D(CollisionScript collisionScript, Collider2D otherCollider);
	//	public delegate void TriggerEnter2D(CollisionScript collisionScript, CollisionScript otherCollisionScript);
	//	public static event TriggerEnter2D onTriggerEnter2D;
	//	public delegate void CollisionEnter2D(CollisionScript collisionScript, CollisionScript otherCollisionScript);
	//	public static event CollisionEnter2D onCollisionEnter2D;
	public delegate void CollisionEnter2D(CrystalQuestObjectScript objectScript, CrystalQuestObjectScript otherObjectScript);
	public static event CollisionEnter2D onCollisionEnter2D;
	
	// 2D
	void OnCollisionEnter2D(Collision2D collision)
	{
		Collision2DHandling(collision);
	}
	
	// 2D
	void Collision2DHandling(Collision2D collision2D)
	{
		//		CollisionHandling(collision2D.gameObject);
	}
	
	#endregion
	#region Triggering 2D
	/**
	 * Triggering 2D
	 **/
	
	public delegate void TriggerEnter2D (CrystalQuestObjectScript objectScript, CrystalQuestObjectScript otherObjectScript);
	public static event TriggerEnter2D onTriggerEnter2D;
	
	/// <summary>
	/// Raises the trigger enter 2D event.
	/// </summary>
	/// <param name="otherCollider">Other collider.</param>
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		Triggering2DHandling(otherCollider);
	}
	
	// 2D
	public virtual void Triggering2DHandling(Collider2D collider2D)
	{
		TriggerHandling(collider2D.gameObject);

		CollisionObject otherScript = collider2D.GetComponent<CollisionObject>();
		if (otherScript != null)
		{
			this.ApplyCollisionDamage (otherScript);
		}
		else
			Debug.LogError ( collider2D.gameObject.name + " hat kein CrystalQuestObjectScript");
	}
	#endregion
	
	#region Unity 2D Event Handling
	void CollisionHandling(GameObject otherGameObject)
	{
//		NotifyCollisionEnter2DListener (this, otherGameObject.GetComponent<CrystalQuestObjectScript>());
		NotifyCollisionEnter2DListener (this, otherGameObject.GetComponent<CollisionObject>());
		//		if(otherGameObject.layer == LayerMask.NameToLayer(Globals.layerStringEnemy))
		//		{
		//			// Enemy
		//			// Schaden zufügen (wie viel)
		//			EnemyScript enemyScript = otherGameObject.GetComponent<EnemyScript>();
		//			if(enemyScript != null)
		//			{
		//				ReceiveDamage(enemyScript.GetCollisionDamageValue());
		//			}
		//			else
		//				Debug.LogError(otherGameObject.name + " hat kein EnemyScript -> vergessen anzufügen oder GO ist in der falschen Layer!");
		//		}
		//		else if(otherGameObject.layer == LayerMask.NameToLayer(Globals.layerStringEnemyProjectile))
		//		{
		//			// Enemy Projectile
		//			// Schaden zufügen
		//		}
		//		else if(otherGameObject.layer == LayerMask.NameToLayer(Globals.layerStringItem))
		//		{
		//			// Item
		//			
		//			// welches Item
		//			// Collect
		//		}
	}
	void TriggerHandling(GameObject otherGameObject)
	{
		//NotifyTriggerEnter2DListener (this, otherGameObject.GetComponent<CrystalQuestObjectScript>());
		NotifyTriggerEnter2DListener (this, otherGameObject.GetComponent<CollisionObject>());
		//		if(otherGameObject.layer == LayerMask.NameToLayer(Globals.layerStringEnemy))
		//		{
		//			// Enemy
		//			// Schaden zufügen (wie viel)
		//			EnemyScript enemyScript = otherGameObject.GetComponent<EnemyScript>();
		//			if(enemyScript != null)
		//			{
		//				ReceiveDamage(enemyScript.GetCollisionDamageValue());
		//			}
		//			else
		//				Debug.LogError(otherGameObject.name + " hat kein EnemyScript -> vergessen anzufügen oder GO ist in der falschen Layer!");
		//		}
		//		else if(otherGameObject.layer == LayerMask.NameToLayer(Globals.layerStringEnemyProjectile))
		//		{
		//			// Enemy Projectile
		//			// Schaden zufügen
		//		}
		//		else if(otherGameObject.layer == LayerMask.NameToLayer(Globals.layerStringItem))
		//		{
		//			// Item
		//			
		//			// welches Item
		//			// Collect
		//		}
	}
//	void NotifyTriggerEnter2DListener (CrystalQuestObjectScript detectorObjectScript, CrystalQuestObjectScript otherObjectScript)
//	{
//		if(onTriggerEnter2D != null)
//		{
//			onTriggerEnter2D (detectorObjectScript, otherObjectScript);
//		}
//		else
//		{
//			Debug.LogError("no \"onTriggerEnter2D\" Listener");
//		}
//	}
//	void NotifyCollisionEnter2DListener (CrystalQuestObjectScript detectorObjectScript, CrystalQuestObjectScript otherObjectScript)
//	{
//		if(onCollisionEnter2D != null)
//		{
//			onCollisionEnter2D (detectorObjectScript, otherObjectScript);
//		}
//		else
//		{
//			Debug.LogError("no \"onCollisionEnter2D\" Listener");
//		}
//	}
	void NotifyTriggerEnter2DListener (CollisionObject detectorObjectScript, CollisionObject otherObjectScript)
	{
		if (!useCollisionEvents)
			return;

		if(onTriggerEnter2D != null)
		{
			onTriggerEnter2D (detectorObjectScript, otherObjectScript);
		}
		else
		{
			Debug.LogError("no \"onTriggerEnter2D\" Listener");
		}
	}
	void NotifyCollisionEnter2DListener (CollisionObject detectorObjectScript, CollisionObject otherObjectScript)
	{
		if (!useCollisionEvents)
			return;

		if(onCollisionEnter2D != null)
		{
			onCollisionEnter2D (detectorObjectScript, otherObjectScript);
		}
		else
		{
			Debug.LogError("no \"onCollisionEnter2D\" Listener");
		}
	}
	#endregion

}
