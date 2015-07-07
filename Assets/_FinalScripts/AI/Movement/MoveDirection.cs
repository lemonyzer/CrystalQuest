using UnityEngine;
using System.Collections;

public class MoveDirection {

	public static Vector2 RandomDirection ()
	{
		//float rand = Random.Range (0, 2 * Mathf.PI);
		float rand = Random.Range (0f, 360f);
		Vector2 moveDirection;
		// Returns the cosine of angle f in radians.
		moveDirection.x = Mathf.Cos (rand * Mathf.Deg2Rad);
		// Returns the sine of angle f in radians.
		moveDirection.y = Mathf.Sin (rand * Mathf.Deg2Rad);

		moveDirection.Normalize();
		return moveDirection;
	}

	public static Vector2 LimitedRandomDirection (float minAngle, float maxAngle)
	{
		float rand = 0;
		if (minAngle <= maxAngle)
		{
			rand = Random.Range (minAngle, maxAngle);
		}
		else
		{
			Debug.LogError("minAngle > maxAngle");
			rand = Random.Range (maxAngle, minAngle);
		}

		Vector2 moveDirection;
		// Returns the cosine of angle f in radians.
		moveDirection.x = Mathf.Cos (rand * Mathf.Deg2Rad);
		// Returns the sine of angle f in radians.
		moveDirection.y = Mathf.Sin (rand * Mathf.Deg2Rad);

		moveDirection.Normalize();
		return moveDirection;
	}

	public static Vector2 ShortestPath (Transform objetTransform, Transform targetTransform)
	{
		Vector2 moveDirection = -objetTransform.position + targetTransform.position;

		moveDirection.Normalize();
		return moveDirection;
	}
	
	public static Vector2 ShortestPath(Vector2 begin, Vector2 destination)
	{
		Vector2 moveDirection = -begin + destination;

		moveDirection.Normalize();
		return moveDirection;
	}

	public static int RandomSign()
	{
		// Random.value Returns a random number between 0.0 [inclusive] and 1.0 [inclusive] (Read Only).
		return Random.value < .5? 1 : -1;
	}

	public static Vector2 RandomDiagonalDirection(Transform targetTransform)
	{
		Vector2 moveDirection = Vector2.zero;

		moveDirection.x = RandomSign();
		moveDirection.y = RandomSign();

		moveDirection.Normalize();
		return moveDirection;
	}

//	// TODO achtung static's können nicht serialisiert werden!
//	// TODO @@@ 
//
//	// möglichkeit 1
//	public static float[] orthogonalAngles = new float[] {0,90,180,270};
//	public static float[] orthogonalRadiants = new float[] {0f,0.5f,1f,1.5f};
//
//	// möglichkeit 2 
//	public static Vector3[] orthogonalMoveVectors = new Vector3[] { Vector3.left, Vector3.right, Vector3.up, Vector3.down };
//
//	/// <summary>
//	/// Orthogonals the path.
//	/// Möglichkeiten: 4
//	/// rechts, links, hoch, runter
//	/// </summary>
//	/// <returns>The path.</returns>
//	/// <param name="targetTransform">Target transform.</param>
//	public static Vector2 OrthogonalPath (Transform targetTransform)
//	{
//		Vector2 moveDirection;
//		int rand = Random.Range (0, orthogonalMoveVectors.Length); 	// TODO wird Length auch eingeschlossen??
//																	// Random.Range(min,max)
//																	// Returns a random float number between and min [inclusive] and max [exclusive] (Read Only).
//		// Möglichkeit 1
//		//float rand = Random.Range (0,2*Mathf.PI);
//
//		// Möglichkeit 2
//		moveDirection = orthogonalMoveVectors[rand];
//
//		return moveDirection;
//	}

//	public static Vector2 OrthogonalPathDynamic (Transform targetTransform)
//	{
//		Vector2 moveDirection;
//
//		int randDirection = Random.Range (0, 4);	// Returns a random float number between and min [inclusive] and max [exclusive] (Read Only).
//
//		switch (randDirection)
//		{
//		case 0:
//			moveDirection = Vector3.right;
//		case 1:
//			moveDirection = Vector3.up;
//		case 2:
//			moveDirection = Vector3.left;
//		case 3:
//			moveDirection = Vector3.down;
//		}
//
//		return moveDirection;
//	}

	public static Vector2 RandomOrthogonalDirection (Transform targetTransform)
	{
		Vector2 moveDirection = Vector2.zero;
		
		int randDirection = Random.Range (0, 2);	// Returns a random float number between and min [inclusive] and max [exclusive] (Read Only).
		
		switch (randDirection)
		{
		case 0:
			moveDirection = Vector3.right;
			break;
		case 1:
			moveDirection = Vector3.up;
			break;
		default:
			moveDirection = Vector2.zero;
			Debug.LogError ("Incorrect random Direction (between 0 and 1 possibly)");
			break;
		}

		moveDirection.x *= RandomSign();
		moveDirection.y *= RandomSign();
		
		moveDirection.Normalize();
		return moveDirection;
	}

}
