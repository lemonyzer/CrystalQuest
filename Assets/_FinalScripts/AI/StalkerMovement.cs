using UnityEngine;
using System.Collections;

public class StalkerMovement : MonoBehaviour {

	// a) muss Crystale kennen 
	// b) von einem Object gemanaged werden das alle Crystale kennt
	// 
	
	[SerializeField]
	Transform targetTransform;

	[SerializeField]
	Vector2 moveDirection;

	[SerializeField]
	Rigidbody2D rb2d;

	[SerializeField]
	protected bool useTargetPosition = true;	// useTargetPosition = managed
	[SerializeField]
	protected Vector3 targetPosition = Vector3.one;
	
	Vector3 targetPosNotInitialized = Vector3.one;

	[SerializeField]
	protected bool targetReached = false;
	
	[SerializeField]
	protected Movement currentMovement;

	[SerializeField]
	protected MoveToTargetTransform moveToTargetTransform;
	
	// List<Movement> movement;
	
	 void Awake()
	{
		rb2d = this.GetComponent<Rigidbody2D>();
		moveToTargetTransform.Transform = transform;

		if (targetTransform == null)
		{
			SetTarget ();
		}
		moveToTargetTransform.TargetTransform = targetTransform;
		
	}
	
	void Start () {
		SetTarget();
		currentMovement = moveToTargetTransform;
	}
	
	void SetTarget()
	{
		this.targetTransform = PlayerSingleton.Instance.transform;
	}
	
	void FixedUpdate()
	{
		moveDirection = currentMovement.NextMoveDirection();
		Vector3 nextPosition = rb2d.position + moveDirection * Time.fixedDeltaTime;
		rb2d.MovePosition(nextPosition);
	}

	public bool ReachedTargetPosition (float distanceToReach)
	{
		Vector2 distance = transform.position - targetTransform.position; 
		if (distance.sqrMagnitude < distanceToReach)
			return true;
		
		return false;
	}
	
	public bool ReachedTargetPosition (Vector3 targetPos, float distanceToReach)
	{
		Vector2 distance = transform.position - targetPos; 
		if (distance.sqrMagnitude < distanceToReach)
			return true;
		
		return false;
	}
}
