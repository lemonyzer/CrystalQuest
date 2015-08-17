using UnityEngine;
using System.Collections;

[System.Serializable]
public class MoveRandom : Movement {

	public MoveRandom (Transform myTransform) : base (myTransform)
	{
		
	}

	[SerializeField]
	float minAngle = 0f;

	public void SetAngles (float min, float max)
	{
		if (min > max)
		{
			minAngle = max;
			maxAngle = min;
		}
		else
		{
			minAngle = min;
			maxAngle = max;
		}
	}

	[SerializeField]
	float maxAngle = 359f;

	public Vector3 mDirection
	{
		get { return moveDirection; }
		set { moveDirection = value; }
	}

	public override Vector2 NextMoveDirection ()
	{
		if (nextMoveTimestamp <= Time.time)
		{
			nextMoveTimestamp = Time.time + RandomChangeDirectionInterval();
			moveDirection = GetMoveDirection (transform, minAngle, maxAngle, moveSpeed);
		}
		return moveDirection; 
	}

	public Vector2 GetMoveDirection (Transform myTransform, float minAngle, float maxAngle, float moveSpeed)
	{
		// myTransform
		return MoveDirection.LimitedRandomDirection (minAngle, maxAngle) * moveSpeed;
	}
}
