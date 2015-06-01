using UnityEngine;
using System.Collections;

public class ParasiteScript : EnemyWithTarget {

	[SerializeField]
	protected Movement currentMovement;

	[SerializeField]
	protected IdleMovement idleMovement;

	[SerializeField]
	protected MoveToTargetTransform moveToTargetTransform;


	protected override void Awake()
	{
		base.Awake();
		idleMovement.Transform = transform;
		moveToTargetTransform.Transform = transform;
	}

	public void SetNewTarget(Transform targetTransform)
	{
		moveToTargetTransform.TargetTransform = targetTransform;
	}

	// Use this for initialization
	void Start () {
		if (targetScript != null)
		{
			SetNewTarget (targetScript.transform);
		}
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
		if (moveToTargetTransform.TargetTransform != null)
		{
			if (ReachedTargetPosition (moveToTargetTransform.TargetTransform.position, 0.1f))
				return idleMovement;

			return moveToTargetTransform;
		}
		return idleMovement;
	}
}
