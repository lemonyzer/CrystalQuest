using UnityEngine;
using System.Collections;

public class CrystalQuestObject : MonoBehaviour {
	
	#region Initialisation
	void Awake()
	{
		InitializeRigidbody2D();
	}
	#endregion
	
	#region Input
	/**
	 * Input
	 * Player Controls
	 * AI Algorithm
	 **/
	[SerializeField]
	private bool enableInput = true;
	[SerializeField]
	private Vector2 inputMoveDirection = Vector2.zero;
	[SerializeField]
	private bool inputFire = false;
	[SerializeField]
	private bool inputUseItem = false;
	
//	// Input reading
//	void InputReading(){
//		if(enableInput)
//		{
//			// Input aktiv
//			inputMoveDirection.x = Input.GetAxis("Horizontal");
//			inputMoveDirection.y = Input.GetAxis("Vertical");
//			inputFire = Input.GetButton("Fire1");
//			inputUseItem = Input.GetButton("Fire2");
//		}
//		else
//		{
//			// Input deaktiviert
//			inputMoveDirection = Vector2.zero;
//			inputFire = false;
//			inputUseItem = false;
//		}
//	}
	
	public void OverrideInput(Vector2 direction, bool shoot, bool useItem)
	{
		this.inputFire = shoot;
		this.inputUseItem = useItem;
		this.inputMoveDirection = direction;
	}
	
	public void InputSetActive(bool enable)
	{
		enableInput = enable;
	}
	#endregion
	
	#region Movement
	/**
	 * Movement
	 **/
	[SerializeField]
	private bool canMove = true;
	//	[SerializeField]
	//	private bool moveWithoutInput = false;	// macht kein sinn, movement wird über MovePosition und nicht über Kräfte realisiert
	[SerializeField]
	private float maxVelocity = 5f;
	[SerializeField]
	private Rigidbody2D rb2D;
	// -> moved to globals
	//	void SetupRigidbody2D(Rigidbody2D rigidbody2D)
	//	{
	//		// rb2D einstellen
	//		// Gravitation deaktivieren
	//		rigidbody2D.gravityScale = 0;
	//		// Collisionen auch bei hohen geschwindigkeiten erkennen
	//		rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
	//	}
	
	void InitializeRigidbody2D()
	{
		rb2D = this.GetComponent<Rigidbody2D>();
		if(rb2D == null)
		{
			Debug.LogError("Rigidbody2D fehlt");
		}
		else
		{
			// rb2D einstellen
			Globals.SetupRigidbody2D(rb2D);
		}
	}
	
	public void MovementSetActive(bool enable)
	{
		canMove = enable;
	}
	
	void FixedUpdate()
	{
		// inputDirection abs [-1;+1]
		Mathf.Clamp(inputMoveDirection.x, -1f, +1f);
		Mathf.Clamp(inputMoveDirection.y, -1f, +1f);
		
		// Better
		Vector3.Normalize(inputMoveDirection);
		
		// Movement
		if(canMove)
			rb2D.MovePosition(rb2D.position + (inputMoveDirection*maxVelocity) * Time.fixedDeltaTime);
	}
	#endregion
	
	#region Health
	/**
	 * Health
	 **/
	public delegate void LostLife(int numberOfRemainingLifes);
	public static event LostLife onLostLife;
	public delegate void UpdateHealth(float currentHealthValue);
	public static event UpdateHealth onUpdateHealth;
	
	[SerializeField]
	private float collisionSendDamageValue = float.PositiveInfinity;
	[SerializeField]
	private bool invincible = false;
	[SerializeField]
	private bool projectileInvincible = false;
	[SerializeField]
	private bool collisionInvincible = false;
	[SerializeField]
	private float health = 100f;
	[SerializeField]
	private int lifes = 3;
	[SerializeField]
	private bool dead = false;
	
	public float GetCollisionDamageValue()
	{
		return collisionSendDamageValue;
	}
	
	public void ReceiveEnemyCollisionDamage(float damageValue)
	{
		if(invincible)
			return;
		
		if(collisionInvincible)
		{
			return;
		}
		else
		{
			ReceiveDamage(damageValue);
		}
	}
	
	public void ReceiveProjectileCollisionDamage(float damageValue)
	{
		if(invincible)
			return;
		
		if(projectileInvincible)
		{
			return;
		}
		else
		{
			ReceiveDamage(damageValue);
		}
	}
	
	public void ReceiveDamage(float damageValue)
	{
		if(invincible)
			return;
		
		float newHealth = health - damageValue;
		if (newHealth <= 0f)
		{
			// damageValue > health -> lebensabzug
			health = 0;
			int newLifeCount = lifes-1;
			if (newLifeCount <= 0)
			{
				// GameOver
				lifes = 0;
			}
			else
			{
				// Keep Trying
				lifes = newLifeCount;
			}
			NotifyLostLifeListener(lifes);
		}
		else
		{
			// not dead, gogogo
			health = newHealth;
		}
		NotifyHealthListener(health);
	}
	
	void NotifyHealthListener(float currentHealth)
	{
		if(onUpdateHealth != null)
		{
			onUpdateHealth(currentHealth);
		}
		else
		{
			Debug.LogError("no \"onUpdateHealth\" Listener");
		}
	}
	void NotifyLostLifeListener(int numberOfLifes)
	{
		if(onLostLife != null)
		{
			onLostLife(numberOfLifes);
		}
		else
		{
			Debug.LogError("no \"onLostLife\" Listener");
		}
	}
	#endregion
	
	#region Collision
	/**
	 * Collision's
	 **/
	// 3D
	//public delegate void OnTriggered(CollisionScript collisionScript, CollisionObject collisionObject, Collider otherCollider);
	//public delegate void OnTriggered(CollisionScript collisionScript, Collider otherCollider);
	public delegate void TriggerEnter(CollisionScript collisionScript, CollisionScript otherCollisionScript);
	public static event TriggerEnter onTriggerEnter;
	public delegate void CollisionEnter(CollisionScript collisionScript, CollisionScript otherCollisionScript);
	public static event CollisionEnter onCollisionEnter;
	
	// 2D
	//public delegate void OnTriggered2D(CollisionScript collisionScript, CollisionObject collisionObject, Collider2D otherCollider);
	//public delegate void OnTriggered2D(CollisionScript collisionScript, Collider2D otherCollider);
	public delegate void TriggerEnter2D(CollisionScript collisionScript, CollisionScript otherCollisionScript);
	public static event TriggerEnter2D onTriggerEnter2D;
	public delegate void CollisionEnter2D(CollisionScript collisionScript, CollisionScript otherCollisionScript);
	public static event CollisionEnter2D onCollisionEnter2D;
	
	// 3D
	void OnCollisionEnter(Collision collision)
	{
		PlayerCollisionHandling(collision);
	}
	
	// 2D
	void OnCollisionEnter2D(Collision2D collision)
	{
		PlayerCollision2DHandling(collision);
	}
	
	// 3D
	void PlayerCollisionHandling(Collision collision)
	{
		PlayerCollisionHandling(collision.gameObject);
	}
	
	// 2D
	void PlayerCollision2DHandling(Collision2D collision2D)
	{
		PlayerCollisionHandling(collision2D.gameObject);
	}
	
	void PlayerCollisionHandling(GameObject otherGameObject)
	{
		if(otherGameObject.layer == LayerMask.NameToLayer(Globals.layerStringEnemy))
		{
			// Enemy
			// Schaden zufügen (wie viel)
			EnemyScript enemyScript = otherGameObject.GetComponent<EnemyScript>();
			if(enemyScript != null)
			{
				ReceiveDamage(enemyScript.GetCollisionDamageValue());
			}
			else
				Debug.LogError(otherGameObject.name + " hat kein EnemyScript -> vergessen anzufügen oder GO ist in der falschen Layer!");
		}
		else if(otherGameObject.layer == LayerMask.NameToLayer(Globals.layerStringEnemyProjectile))
		{
			// Enemy Projectile
			// Schaden zufügen
		}
		else if(otherGameObject.layer == LayerMask.NameToLayer(Globals.layerStringItem))
		{
			// Item
			
			// welches Item
			// Collect
		}
	}
	
	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="otherCollider">Other collider.</param>
	void OnTriggerEnter(Collider otherCollider)
	{
		PlayerTriggeringHandling(otherCollider);
	}
	
	/// <summary>
	/// Raises the trigger enter 2D event.
	/// </summary>
	/// <param name="otherCollider">Other collider.</param>
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		PlayerTriggering2DHandling(otherCollider);
	}
	
	// 3D
	void PlayerTriggeringHandling(Collider collider)
	{
		PlayerCollisionHandling(collider.gameObject);
	}
	
	// 2D
	void PlayerTriggering2DHandling(Collider2D collider2D)
	{
		PlayerCollisionHandling(collider2D.gameObject);
	}
	#endregion
}
