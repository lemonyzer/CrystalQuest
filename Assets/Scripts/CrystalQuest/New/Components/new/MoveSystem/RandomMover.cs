using UnityEngine;
using System.Collections;

public class RandomMover : MonoBehaviour {

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
	
	
	protected void Awake()
	{
		transform = this.GetComponent<Transform>();

		if (rb2d == null)
			rb2d = this.GetComponent<Rigidbody2D>();

		idleMovement.Transform = transform;
		//		moveToPositionMovement.Transform = transform;
		moveRandom.Transform = transform;
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
