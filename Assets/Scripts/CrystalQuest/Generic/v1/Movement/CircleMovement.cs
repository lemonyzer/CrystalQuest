using UnityEngine;
using System.Collections;

[System.Serializable]
public class CircleMovement : Movement {

	public CircleMovement (Transform myTransform) : base (myTransform)
	{

	}

	#region Orbit
//	[SerializeField]
//	protected OrbitMoveState currentOrbitMoveState = OrbitMoveState.Idle;

//	[SerializeField]
//	protected Transform transform = null;		// 

	[SerializeField]
	protected Vector3 orbitCenterPosition = Vector3.zero;		// 

	public Vector3 OrbitCenterPosition
	{
		get { return orbitCenterPosition; }
		set { orbitCenterPosition = value; }
	}

	[SerializeField]
	protected float orbitDistance = 1f;		//

	public float OrbitDistance
	{
		get { return orbitDistance; }
		set { orbitDistance = value; }
	}

	[SerializeField]
	protected float winkelGeschwindigkeit = 20f;

//	[SerializeField]
//	protected float orbitCurrentAnglePosition = 0f;	// winkel
//
//	public float OrbitCurrentAnglePosition
//	{
//		get { return orbitCurrentAnglePosition; }
//		set { orbitCurrentAnglePosition = value; }
//	}
	
//	[SerializeField]
//	protected float orbitSpeed = 2f;	// winkel
	#endregion

	float CalculateAngle(Vector3 position, Vector3 center)
	{
		// The magnitude of any number is usually called its "absolute value" or "modulus", denoted by |x|.
		//float r = (center - position).magnitude;
		float r = orbitDistance;
		
		float h = Mathf.Abs ( (center.y + r) - position.y );
		
//		float s = 2 * Mathf.Sqrt ( 2*r*h - h*h  );

		// Mathf.Asin (f)
		// Returns the arc-sine of f - the angle in radians whose sine is f.
		float radiant = 2 * Mathf.Acos (1 - h/r);
//		float radiant = 2 * Mathf.Asin (s / (2*r));

		float alpha = radiant * Mathf.Rad2Deg;
		return alpha;
	}

//	public Vector2 GetMoveDirection (Vector3 center, Vector3 currentPosition, float distance, float speed)
//	{
//		Vector2 moveDirection = Vector2.zero;
//		
//		// TODO Problem: speed zu hoch, keine Kreisbewegung (eckig)
//		// TODO currentAngle wieder über transform.Position und Center Position berechnen
//		// http://de.wikipedia.org/wiki/Kreiswinkel
//		// http://en.wikipedia.org/wiki/Inscribed_angle
//
//		orbitCurrentAnglePosition = CalculateAngle (currentPosition, center);
//		orbitCurrentAnglePosition -= speed;
//		
//		Vector3 nextPosition = Vector3.zero;
//		// Mathf.Cos (f)
//		// Returns the cosine of angle f in radians.
//		nextPosition.x = Mathf.Cos (orbitCurrentAnglePosition);
//		// Returns the sine of angle f in radians.
//		nextPosition.y = Mathf.Sin (orbitCurrentAnglePosition);
//
////		nextPosition.Normalize();
//
//		nextPosition += center;
//		
//		// 
//		moveDirection = MoveDirection.ShortestPath (currentPosition, nextPosition);
//		
//		return moveDirection;
//	}

	public Vector2 GetMoveDirection (Vector3 center, Vector3 currentPosition, float distance, float speed)
	{
		// transform.RotateAround				
		// Rotates the transform about axis passing through point in world coordinates by angle degrees.
		transform.RotateAround (center, Vector3.forward, winkelGeschwindigkeit * Time.deltaTime);

		// eigene Rotation aufheben
		transform.rotation = Quaternion.identity;

		Vector3 nextPosition = transform.position;

		// Bewegungsrichtung berechnen
		moveDirection = MoveDirection.ShortestPath (currentPosition, nextPosition) * speed;

		// position zurücksetzen
		transform.position = currentPosition;
		return moveDirection;
	}

	public override Vector2 NextMoveDirection ()
	{
		return GetMoveDirection(orbitCenterPosition, transform.position, orbitDistance, moveSpeed);
	}

}
