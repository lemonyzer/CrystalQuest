using UnityEngine;
using System.Collections;

[System.Serializable]
public class MoveToPositionMovement : Movement {

	public MoveToPositionMovement (Transform myTransform) : base (myTransform)
	{
		
	}

	[SerializeField]
	Vector3 destination;

	public Vector3 Destination
	{
		get { return destination; }
		set { destination = value; }
	}

	public override Vector2 NextMoveDirection ()
	{
		return GetMoveDirection (transform.position, destination, moveSpeed);
	}

	public Vector2 GetMoveDirection (Vector3 currentPosition, Vector3 destination, float moveSpeed)
	{
		return MoveDirection.ShortestPath(currentPosition, destination) * moveSpeed;
	}
}
