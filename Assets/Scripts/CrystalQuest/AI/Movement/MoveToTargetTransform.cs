using UnityEngine;
using System.Collections;

[System.Serializable]
public class MoveToTargetTransform : Movement {

	public MoveToTargetTransform (Transform myTransform) : base (myTransform)
	{
		
	}

	[SerializeField]
	Transform targetTransform;

	public Transform TargetTransform
	{
		get { return targetTransform; }
		set { targetTransform = value; }
	}

	public override Vector2 NextMoveDirection ()
	{
		return GetMoveDirection (transform.position, targetTransform, moveSpeed);
	}

	public Vector2 GetMoveDirection (Vector3 currentPosition, Transform targetTransform, float moveSpeed)
	{
		return MoveDirection.ShortestPath(currentPosition, targetTransform.position) * moveSpeed;
	}
}
