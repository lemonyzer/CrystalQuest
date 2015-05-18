using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AiMovementType
{
	ShortestPath, Following, Buzzing, Diagonal, Orthogonal 
};

public class EnemyWithTarget : EnemyObjectScript {

	#region AI Movement
	[SerializeField]
	protected CrystalQuestObjectScript targetScript;

	[SerializeField]
	protected AiMovementType aiMovementType = AiMovementType.Diagonal;

	[SerializeField]
	protected bool limitedDegreeChange = true;

	[SerializeField]
	protected bool useDegreeList = false;

	[SerializeField]
	protected List<float> degreeList;

	[SerializeField]
	protected Vector2 moveDirection;
	#endregion

	#region Move
	[SerializeField]
	protected float nextMoveTimestamp = 0f;	//hibernate?

	[SerializeField]
	protected float nextChangeDirectionTimestamp = 0f;

	[SerializeField]
	protected float changeDirectionInterval = 0.5f;
	#endregion

//	[SerializeField]
//	protected GameObject targetGO;
//	
//	public void SetTargetGO(GameObject go)
//	{
//		targetGO = go;
//	}

	public void SetTargetScript(CrystalQuestObjectScript script)
	{
		targetScript = script;
	}
	#region Action
	void FixedUpdate()
	{
		if(targetScript == null)
			return;
		if(targetScript.transform == null)
			return;

		moveDirection = AiMove(targetScript.transform);
		moveDirection.Normalize();
		rb2D.MovePosition(rb2D.position + (moveDirection*maxVelocity) * Time.fixedDeltaTime);
	}

	Vector2 AiMove(Transform targetTransform)
	{
		Vector2 currentMoveDirection = Vector2.zero;

		Vector2 distance = transform.position - targetTransform.position; 
		if (distance.sqrMagnitude < 0.1f)
			return Vector2.zero;

		switch(aiMovementType)
		{
		case AiMovementType.Buzzing:
			currentMoveDirection = Buzzing(targetTransform);
			break;
		case AiMovementType.Diagonal:
			currentMoveDirection = Diagonal(targetTransform);
			break;
		case AiMovementType.Following:
			currentMoveDirection = Following(targetTransform);
			break;
		case AiMovementType.Orthogonal:
			currentMoveDirection = Orthogonal(targetTransform);
			break;
		case AiMovementType.ShortestPath:
			currentMoveDirection = ShortestPath(targetTransform);
			break;
		default:
			// Default
			break;
		}

		return currentMoveDirection;
	}
	#endregion

	Vector2 Buzzing(Transform targetTransform)
	{
		Vector2 moveDirection = this.moveDirection;
		if(Time.time >= nextChangeDirectionTimestamp)
		{
			nextChangeDirectionTimestamp = Time.time + changeDirectionInterval;
			moveDirection = ShortestPath(targetTransform);
		}

		return moveDirection;
	}

	Vector2 Diagonal(Transform targetTransform)
	{
		Vector2 moveDirection = Vector2.zero;

		return moveDirection;
	}

	Vector2 Following(Transform targetTransform)
	{
		Vector2 moveDirection = Vector2.zero;
		
		return moveDirection;
	}

	Vector2 Orthogonal(Transform targetTransform)
	{
		Vector2 moveDirection = Vector2.zero;
		
		return moveDirection;
	}

	Vector2 ShortestPath(Transform targetTransform)
	{
		Vector2 moveDirection = -transform.position + targetTransform.position;
		return moveDirection;
	}
}
