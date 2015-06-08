using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PointsObject : CrystalQuestObjectScript {

	[SerializeField]
	protected int points;
	
	public int Points {
		get { return points; }
		set { points = value; }
	}

	#region UnityEvent
	// TODO static?
	public static UnityEvent releasePoint;
	public UnityEvent releasePoints;
	public UnityAction pointsAction;
	#endregion

	#region Delegate
	public delegate void ReleasePoints (int score);
	public static event ReleasePoints onReleasePoints;
	#endregion

	protected virtual void Score ()
	{
		if (onReleasePoints != null)
			onReleasePoints (points);
	}
}
