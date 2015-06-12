using UnityEngine;
using System.Collections;

public class CrystalQuestScoreManager : MonoBehaviour {

	[SerializeField]
	private float currentLevelScore = 0;

	[SerializeField]
	private float currentSessionPoints;

	public float CurrentLevelScore {
		get {return currentLevelScore;}
		set {
			currentLevelScore = value;
			NotifyScoreListener (currentLevelScore);
		}
	}

	void OnEnable ()
	{
		PointsObject.onReleasePoints += AddPoints;
	}

	void OnDisable ()
	{
		PointsObject.onReleasePoints -= AddPoints;
	}

	void AddPoints (float value)
	{
		CurrentLevelScore += value;
	}

	#region Delegate
	public delegate void ScoreUpdate (float score);
	public static event ScoreUpdate onScoreUpdate;
	#endregion


	void NotifyScoreListener (float notifyScore)
	{
		if (onScoreUpdate != null)
		{
			onScoreUpdate (notifyScore);
		}
		else
		{
#if UNITY_EDITOR
			Debug.LogError (this.ToString () + " no onScoreUpdate listener!");
#endif
		}
	}

	#region Initialisation
	void Start ()
	{
		NotifyScoreListener (currentLevelScore);
	}
	#endregion
}
