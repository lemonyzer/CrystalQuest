using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScoreDataModell {

	[SerializeField]
	float scoreValue;

	public float ScoreValue {
		get {return scoreValue;}
		set {scoreValue = value;}
	}

	public void AddPoints (float value)
	{
		Debug.Log ("AddPoints " + scoreValue + " + " + value); 
		scoreValue += value;
	}

	public void AddPoints (ScoreDataModell scoreData)
	{
		Debug.Log ("AddPoints " + scoreValue + " + " + scoreData.scoreValue); 
		scoreValue += scoreData.ScoreValue;
	}
}

public class ScoreableObjectScript : MonoBehaviour {

	[SerializeField]
	ScoreDataModell scoreData;// = new ScoreDataModell();

	[SerializeField]
	MyFloatEvent scored;// = new MyFloatEvent();	// kann direkt im Unity Editor (Inspector gesetzt werden)

	[SerializeField]
	public static MyFloatEvent gScored = new MyFloatEvent ();	//TODO static needs to be instantiated, not serializable in unity and no therefore not visible in inspector

	public delegate void Scored (ScoreDataModell scoreData);
	public static event Scored onScored;

	void Awake ()
	{
//		gScored = new MyFloatEvent ();		// TODO check what happens if multiple objects with this static event, it will be overwritten??
	}

	// public Methode: kann im Inspector als UnityEvent action gesetzt werden
	public void ReleaseScore ()
	{
		Debug.Log (this.ToString () + " Releasing Score " + scoreData.ScoreValue);
		NotifyScoreListener ();
		NotifyGlobalScoreListener ();
		TriggerOnScored ();
	}

	void TriggerOnScored ()
	{
		if (onScored != null)
		{
			onScored (this.scoreData);
		}
	}

	void NotifyScoreListener ()
	{
		if (scored != null)
			scored.Invoke (scoreData.ScoreValue);
		else
			Debug.LogError (this.ToString () + " scored == NULL");
	}

	void NotifyGlobalScoreListener ()
	{
		if (gScored != null)
			gScored.Invoke (scoreData.ScoreValue);
		else
			Debug.LogError (this.ToString () + " gScored == NULL");
	}
}
