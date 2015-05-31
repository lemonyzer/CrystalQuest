using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class CrystalQuestObjectScript : MonoBehaviour {

	#region Override
	new public Transform transform;
	#endregion

	#region Initialisation
	protected virtual void Awake()
	{
		Debug.Log(this.name + " Awake() " + this.ToString());
		transform = this.GetComponent<Transform>();
		InitializeRigidbody2D();
	}
	#endregion

	#region Flag Collection
	public bool CanMove()
	{
		if(isDead)
			return false;

		return canMove;
	}
	public bool CanUseInput()
	{
		if(enableInput)
			return true;
		else
			return false;
	}
	#endregion
	
	#region Input
	/**
	 * Input
	 * Player Controls
	 * AI Algorithm
	 **/
	[SerializeField]
	protected bool enableInput = true;

	[SerializeField]
	protected Vector2 inputMoveDirection = Vector2.zero;

	[SerializeField]
	protected bool inputFire = false;

	[SerializeField]
	protected bool inputUseItem = false;
	
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
	protected float maxVelocity = 5f;
	
	[SerializeField]
	protected Rigidbody2D rb2D;
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
	private bool selfAttack = false;
	public bool SelfAttack {
		get { return selfAttack; }
		set { selfAttack = value; }
	}

	[SerializeField]
	private bool teamAttack = false;
	public bool TeamAttack {
		get { return teamAttack; }
		set { teamAttack = value; }
	}

	[SerializeField]
	private float collisionSendDamageValue = float.PositiveInfinity;
	public float CollisionSendDamageValue {
		get { return collisionSendDamageValue; }
		set { collisionSendDamageValue = value; }
	}

	[SerializeField]
	private bool invincible = false;

//	[SerializeField]
//	private bool projectileInvincible = false;
//	[SerializeField]
//	private bool collisionInvincible = false;

	[SerializeField]
	private float minHealth = 0f;

	[SerializeField]
	private float maxHealth = 100f;

	[SerializeField]
	private float currentHealth = 100f;

	public float Health {
		get {return currentHealth;}
		set
		{
			if (currentHealth >= minHealth)
				currentHealth = value;
			else
				currentHealth = minHealth;
		}
	}

	[SerializeField]
	private int lifes = 3;

	[SerializeField]
	private int minLifes = 0;
	public int Lifes {
		get {return lifes;}
		set
		{
			if (lifes >= minLifes)
				lifes = value;
			else
				lifes = minLifes;
		}
	}

	[SerializeField]
	private bool isDead = false;
	
	public float GetCollisionDamageValue()
	{
		return collisionSendDamageValue;
	}
	
	public void ApplyCollisionDamage(CrystalQuestObjectScript otherObjectScript)
	{
		ReceiveDamage(otherObjectScript.collisionSendDamageValue);
	}

	public void ReceiveDamage(float damageValue)
	{
		if(invincible)
			return;
		
		Health = Health - damageValue;
		if (Health == minHealth)
		{
			// damageValue > health -> lebensabzug
			DecreaseLifeCount(-1);
		}
		else
		{
			// not dead, gogogo
		}
		NotifyHealthListener(Health);
	}

	void DecreaseLifeCount(int decreaseAmount)
	{
		Lifes = Lifes - decreaseAmount;
		if (Lifes == minLifes)
		{
			// GameOver
		}
		else
		{
			// not GameOver, keep trying
		}
		NotifyLostLifeListener(Lifes);
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
		CollisionHandling(collision2D.gameObject);
	}

	#endregion
	#region Triggering 2D
	/**
	 * Triggering 2D
	 **/

	public delegate void TriggerEnter2D(CrystalQuestObjectScript objectScript, CrystalQuestObjectScript otherObjectScript);
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
	void Triggering2DHandling(Collider2D collider2D)
	{
		TriggerHandling(collider2D.gameObject);
	}
	#endregion

	#region Unity 2D Event Handling
	void CollisionHandling(GameObject otherGameObject)
	{
		NotifyCollisionEnter2DListener(this, otherGameObject.GetComponent<CrystalQuestObjectScript>());
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
		NotifyTriggerEnter2DListener(this, otherGameObject.GetComponent<CrystalQuestObjectScript>());
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
	void NotifyTriggerEnter2DListener(CrystalQuestObjectScript detectorObjectScript, CrystalQuestObjectScript otherObjectScript)
	{
		if(onTriggerEnter2D != null)
		{
			onTriggerEnter2D (detectorObjectScript, otherObjectScript);
		}
		else
		{
			Debug.LogError("no \"onTriggerEnter2D\" Listener");
		}
	}
	void NotifyCollisionEnter2DListener(CrystalQuestObjectScript detectorObjectScript, CrystalQuestObjectScript otherObjectScript)
	{
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
