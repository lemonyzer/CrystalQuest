using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProtectorScript : EnemyWithTarget {

	// a) muss Crystale kennen 
	// b) von einem Object gemanaged werden das alle Crystale kennt
	// 
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
	protected IdleMovement idleMovement;
	[SerializeField]
	protected CircleMovement orbitMovement;
	[SerializeField]
	protected MoveToPositionMovement moveToPositionMovement;

	// List<Movement> movement;

	protected override void Awake()
	{
		base.Awake();
//		idleMovement = new IdleMovement(transform);
//		orbitMovement = new CircleMovement(transform);
//		moveToPositionMovement = new MoveToPositionMovement(transform);

		idleMovement.Transform = transform;
		orbitMovement.Transform = transform;
		moveToPositionMovement.Transform = transform;

	}
		
	void Start ()
	{
		SetTarget();
	}

	void SetTarget()
	{
		orbitMovement.OrbitCenterPosition = targetPosition;
		moveToPositionMovement.Destination = targetPosition;
	}

	public void NewTarget(CrystalQuestObjectScript newTargetScript)
	{
		this.targetScript = newTargetScript;
		Reset();
	}

	public void NewTarget(Vector3 newTargetPosition)
	{
		this.targetPosition = newTargetPosition;
		Reset();
		SetTarget();
	}

	public void Reset()
	{
		targetReached = false;
	}

	float PredictCircleAngle(Vector3 position, Vector3 center)
	{
		if (position.y > center.y)
		{
			// 0° < angle < 180°

			if (position.x > center.x)
			{
				// 0° < angle < 90°
				// 1. Quadtrant
				return 45f;
			}
			else
			{
				// 90° < angle < 180°
				// 2. Quadtrant
				return 45f+90f;
			}
		}
		else
		{
			if (position.x < center.x)
			{
				// 180° < angle < 270°
				// 3. Quadrant
				return 45f+90f+90f;
			}
			else
			{
				// 270° < angle < 360°
				// 4. Quadrant
				return 45f+90f+90f+90f;
			}
		}
	}



	bool TargetReached()
	{
		if (ReachedTargetPosition(targetPosition, orbitMovement.OrbitDistance))
		{
			targetReached = true;
//			orbitMovement.OrbitCurrentAnglePosition = CalculateAngle(transform.position, orbitMovement.OrbitCenterPosition);
			return true;
		}
		else
		{
			return false;
		}
	}

	bool TargetScriptReached()
	{
		if (ReachedTargetPosition(orbitMovement.OrbitDistance))
		{
			targetReached = true;
			return true;
		}
		else
		{
			return false;
		}
	}
	
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
		if (useTargetPosition)
		{
			// managed bekommt nur die Position angegeben die es bewachen soll, wenn Bewacendes-Object zerstört wird kann der Manager entscheiden was passiert
			if (targetReached || TargetReached())
			{
				// Flag als LOCK, Distance kann leicht abweichen und zB. OrbitDistance überschreiten bei Berechnungen und Kreisbewegungen
				return orbitMovement;
			}
			else
			{
				// Target wurde noch nie erreicht
				return moveToPositionMovement;
			}
		}
//		else
//		{
//			// self-Managed: arbeitet mit targetScript (muss dieses Kennen...)
//			if (targetScript != null)
//			{
//				if (targetReached || TargetScriptReached())
//				{
//					return orbitMovement;
//				}
//				else
//				{
//					return moveToPositionMovement;
//				}
//			}
//		}

		return idleMovement;

	}
	
	
}
