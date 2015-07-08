using UnityEngine;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;

public class RandomMoverWithAttackOnPlayerIdle : MonoBehaviour {

	new public Transform transform;

	[SerializeField]
	Vector2 moveDirection;

	[SerializeField]
	Rigidbody2D rb2d;

	[SerializeField]
	bool canMove = true;

	[SerializeField]
	protected Movement currentMovement;
	
	[SerializeField]
	protected IdleMovement idleMovement;
	
	//	[SerializeField]
	//	protected MoveToPositionMovement moveToPositionMovement;
	
	[SerializeField]
	protected MoveRandom moveRandom;

	[SerializeField]
	MoveToTargetTransform moveToTargetTransform;
	
	
	protected void Awake()
	{
		transform = this.GetComponent<Transform>();

		if (rb2d == null)
			rb2d = this.GetComponent<Rigidbody2D>();

		idleMovement.Transform = transform;
		//		moveToPositionMovement.Transform = transform;
		moveRandom.Transform = transform;

		moveToTargetTransform.Transform = transform;
		moveToTargetTransform.TargetTransform = PlayerSingleton.Instance.transform;
	}

	
	// Update is called once per frame
	void Update ()
	{
		currentMovement = SelectCurrentMovement();
	}
	
	void FixedUpdate()
	{
		if (!canMove)
			return;

		moveDirection = currentMovement.NextMoveDirection();
		Vector3 nextPosition = rb2d.position + moveDirection * Time.fixedDeltaTime;
		rb2d.MovePosition(nextPosition);
	}
	
	Movement SelectCurrentMovement()
	{
		// wenn Spieler sich nicht bewegt greife für x Sekunden an
		Vector2 userInput = Vector2.zero;
		// verwende eingabe um bewegung zu erkennen -> funktioniert da spieler nicht einfriert.
		// einzigster fall in dem der Input gesetzt spieler sich jedoch nicht bewegt ist wenn er am levelrand in richtung der levelbegrenzung steuert.

		// user input ist bei tastatursteuerung meist == 1 !!!

		//Enemy greift an wenn Spieler in entgegengesetze richtung steuert.
		userInput.x = CrossPlatformInputManager.GetAxis ("Horizontal");
		userInput.y = CrossPlatformInputManager.GetAxis ("Vertical");

		if (userInput.sqrMagnitude < 1)
			return moveToTargetTransform;

		return moveRandom;
	}

	public void StopMoving ()
	{
		canMove = false;
	}

	public void ResumMoving ()
	{
		canMove = true;
	}
}
