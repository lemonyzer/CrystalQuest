using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AiMovementType
{
	Idle, Random, ShortestPath, Buzzing, AreaBuzzing, Diagonal, Orthogonal, Orbit 
};

public enum AiMoveState {
	Idle,
	Move
}

public class EnemyWithTarget : EnemyObjectScript {

	#region AI Movement
	[SerializeField]
	protected CrystalQuestObjectScript targetScript;

	[SerializeField]
	protected AiMovementType aiMovementType = AiMovementType.Idle;

	[SerializeField]
	protected AiMoveState aiMoveState = AiMoveState.Idle;

	[SerializeField]
	protected bool limitedDegreeChange = true;

//	[SerializeField]
//	protected bool useDegreeList = false;
//
//	[SerializeField]
//	protected List<float> degreeList;

	[SerializeField]
	protected Vector2 moveDirection;
	#endregion

	#region Level Dimension (maxima)
	[SerializeField]
	protected float levelLeft = -10f;		// 
	[SerializeField]
	protected float levelTop = 5f;		// 
	[SerializeField]
	protected float levelWidth = 20f;		// 
	[SerializeField]
	protected float levelHeight = 10f;		// 
	#endregion

	public void SetTargetScript(CrystalQuestObjectScript script)
	{
		targetScript = script;
	}

	#region Action
//	void FixedUpdate()
//	{
//		if(targetScript == null)
//			return;
//		else
//		{
//			if(targetScript.transform == null)
//				return;
//			else
//			{
//				moveDirection = AiMove(targetScript.transform);
//				moveDirection.Normalize();
//				rb2D.MovePosition(rb2D.position + (moveDirection*maxVelocity) * Time.fixedDeltaTime);
//			}
//		}
//	}

	[SerializeField]
	protected float deltaDistanceToReach = 0.1f;

	public bool ReachedTargetPosition ()
	{
		Vector2 distance = transform.position - targetScript.transform.position; 
		if (distance.sqrMagnitude < deltaDistanceToReach)
			return true;
		
		return false;
	}

//	public bool ReachedTargetPosition (float distanceToReach, float abweichung)
//	{
//		Vector2 distance = transform.position - targetScript.transform.position; 
//		if (distance.sqrMagnitude + abweichung < distanceToReach)
//			return true;
//		
//		return false;
//	}

	public bool ReachedTargetPosition (float distanceToReach)
	{
		Vector2 distance = transform.position - targetScript.transform.position; 
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

//	Vector2 AiMove (Transform targetTransform)
//	{
//		Vector2 currentMoveDirection = Vector2.zero;
//
//		if(aiMoveState == AiMoveState.MoveToTarget)
//		{
//			if(ReachedTargetPosition (targetTransform.position))
//			{
//				return Vector2.zero;
//			}
//			else
//			{
//
//			}
//		}
//		else if (aiMoveState == AiMoveState.Move)
//		{
//
//		}
//
//		switch(aiMovementType)
//		{
//		case AiMovementType.Buzzing:
//			currentMoveDirection = Buzzing(targetTransform);
//			break;
//		case AiMovementType.Random:
//			currentMoveDirection = RandomBuzzing();
//			break;
//		case AiMovementType.AreaBuzzing:
//			currentMoveDirection = AreaBuzzing(targetTransform);
//			break;
//		case AiMovementType.Diagonal:
//			currentMoveDirection = Diagonal(targetTransform);
//			break;
//		case AiMovementType.Following:
//			currentMoveDirection = Following(targetTransform);
//			break;
//		case AiMovementType.Orthogonal:
//			currentMoveDirection = Orthogonal(targetTransform);
//			break;
//		case AiMovementType.ShortestPath:
//			currentMoveDirection = ShortestPath(targetTransform);
//			break;
//		case AiMovementType.Orbit:
//			currentMoveDirection = Orbit(targetTransform.position, transform.position, orbitDistance, orbitSpeed);
//			break;
//		default:
//			// Default
//			break;
//		}
//
//		return currentMoveDirection;
//	}
	#endregion

	Vector2 RandomPosition (float left, float top, float width, float height)
	{
		Vector2 randomPosition = Vector2.zero;
		randomPosition.x = Random.Range(left,left + width); 
		randomPosition.y = Random.Range(top,top - height); 
		return randomPosition;
	}

}
