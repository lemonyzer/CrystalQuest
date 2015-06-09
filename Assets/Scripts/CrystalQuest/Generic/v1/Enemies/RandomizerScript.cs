using UnityEngine;
using System.Collections;

public class RandomizerScript : EnemyWithTarget {
	
	[SerializeField]
	protected Movement currentMovement;
	
	[SerializeField]
	protected IdleMovement idleMovement;
	
//	[SerializeField]
//	protected MoveToPositionMovement moveToPositionMovement;

	[SerializeField]
	protected MoveRandom moveRandom;
	
	
	protected override void Awake()
	{
		base.Awake ();
		idleMovement.Transform = transform;
//		moveToPositionMovement.Transform = transform;
		moveRandom.Transform = transform;
	}

//	public void SetNewDestination(Vector3 targetPosition)
//	{
//		moveToPositionMovement.Destination = targetPosition;
//	}
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
//		if (targetScript != null)
//		{
//			SetNewDestination (targetScript.transform.position);
//		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentMovement = SelectCurrentMovement();
	}
	
	void FixedUpdate()
	{
		moveDirection = currentMovement.NextMoveDirection();
		Vector3 nextPosition = rb2D.position + moveDirection * Time.fixedDeltaTime;
		rb2D.MovePosition(nextPosition);
	}
	
	Movement SelectCurrentMovement()
	{
		return moveRandom;
	}
}
