using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PointsObject : CrystalQuestObjectScript {

	[SerializeField]
	protected float destroyScore;
	
	public float DestroyScore {
		get { return destroyScore; }
		set { destroyScore = value; }
	}

	#region UnityEvent
	// TODO static?
	public static UnityEvent releasePoint;
	public UnityEvent releasePoints;
	public UnityAction pointsAction;
	#endregion

	#region Delegate
	public delegate void ReleasePoints (float score);
	public static event ReleasePoints onReleasePoints;
	#endregion

	protected virtual void TriggerScore ()
	{
		if (onReleasePoints != null)
			onReleasePoints (destroyScore);
	}
}
