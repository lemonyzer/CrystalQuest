using UnityEngine;
using System.Collections;

public class CircleMovement : Movement {

	#region Orbit
	[SerializeField]
	protected OrbitMoveState currentOrbitMoveState = OrbitMoveState.Idle;
	
	[SerializeField]
	protected Vector3 orbitCenterPosition = Vector3.zero;		// 
	
	[SerializeField]
	protected float orbitDistance = 1f;		//
	
	[SerializeField]
	protected float orbitCurrentRadialPosition = 0f;	// winkel
	
	[SerializeField]
	protected float orbitSpeed = 2f;	// winkel
	#endregion

	Vector2 Orbit (Vector3 center, Vector3 currentPosition, float distance, float speed)
	{
		Vector2 moveDirection = Vector2.zero;
		
		// TODO Problem
		orbitCurrentRadialPosition += speed;
		
		Vector3 nextPosition = Vector3.zero;
		// Returns the cosine of angle f in radians.
		nextPosition.x = Mathf.Cos(orbitCurrentRadialPosition);
		// Returns the sine of angle f in radians.
		nextPosition.y = Mathf.Sin(orbitCurrentRadialPosition);
		
		nextPosition = nextPosition * distance;
		// 
		moveDirection = currentPosition - nextPosition;
		
		return moveDirection;
	}


}
