using UnityEngine;
using System.Collections;

public class Movement {

	#region Move
	[SerializeField]
	protected Vector2 moveDirection;

	[SerializeField]
	protected float nextMoveTimestamp = 0f;	//hibernate?
	
	[SerializeField]
	protected float nextChangeDirectionTimestamp = 0f;
	
	[SerializeField]
	protected float changeDirectionIntervalMax = 0.5f;
	
	[SerializeField]
	protected float changeDirectionIntervalMin = 0.1f;
	#endregion

	float RandomChangeDirectionInterval()
	{
		return Random.Range(changeDirectionIntervalMin, changeDirectionIntervalMax);
	}

	public static Vector2 RandomBuzzing()
	{
		Vector2 moveDirection = this.moveDirection;
		if(Time.time >= nextChangeDirectionTimestamp)
		{
			nextChangeDirectionTimestamp = Time.time + RandomChangeDirectionInterval();
			moveDirection = Path.RandomPath();
		}
		
		return moveDirection;
	}

	Vector2 Buzzing(Transform targetTransform)
	{
		Vector2 moveDirection = this.moveDirection;
		if(Time.time >= nextChangeDirectionTimestamp)
		{
			nextChangeDirectionTimestamp = Time.time + RandomChangeDirectionInterval();
			moveDirection = Path.RandomPath();
		}
		
		return moveDirection;
	}
	
	Vector2 AreaBuzzing(Transform targetTransform)
	{
		Vector2 moveDirection = this.moveDirection;
		
		//		if(interruptMovingToAreaCenter)
		//		{
		//			if(Time.time >= nextChangeAreaTimestamp)
		//			{
		//				nextChangeAreaTimestamp = Time.time + changeAreaInterval;
		//			}
		//		}
		//		else
		//		{
		//			// ...
		//		}
		
		switch (currentAreaMoveState)
		{
		case AreaMoveState.MoveToAreaCenter:
			
			// check if area center position reached
			Vector2 distance = transform.position - areaCenterPosition;
			if(distance.sqrMagnitude < areaCenterMinDistanceToReachEnable)
			{
				// area center Position reached
				currentAreaMoveState = AreaMoveState.StayInArea;
				nextChangeAreaTimestamp = Time.time + changeAreaInterval;
				moveDirection = Vector2.zero;
			}
			else
			{
				// area center Position not reached, move forward to area center
				// TODO BuzzinPath (forwardAngle, currentPosition, destinationPosition);
				// TODO BuzzinPath (forwardAngle, startPosition, destinationPosition);
				moveDirection = ShortestPath(transform.position, areaCenterPosition);					// buzzing while moving to 
			}
			break;
		case AreaMoveState.StayInArea:
			// check if area stay time is over 
			if(Time.time >= nextChangeAreaTimestamp)
			{
				// time is over, move to new area 
				areaCenterPosition = RandomPosition(levelLeft, levelTop, levelWidth, levelHeight);
				
				// TODO, needed ? LIMIT moving to new area center 
				//nextChangeAreaTimestamp = Time.time + changeAreaInterval;
				
				// switch to move state
				currentAreaMoveState = AreaMoveState.MoveToAreaCenter;
				
			}
			break;
		}
		
		return moveDirection;
	}
}
