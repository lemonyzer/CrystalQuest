using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingObject : CollisionObject {

	#region Initialisation
	protected override void Awake()
	{
		base.Awake();
//		Debug.Log(this.name + " Awake() " + this.ToString());
		InitializeRigidbody2D ();
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
			Debug.LogError(this.ToString() + " Rigidbody2D fehlt");
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
	
}
