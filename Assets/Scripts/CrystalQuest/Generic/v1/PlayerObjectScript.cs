using UnityEngine;
using System.Collections;

public class PlayerObjectScript : MovingObject {

	#region Initialization
	protected override void Start ()
	{
		base.Start ();
		NotifyLifeListener (lifes);
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
