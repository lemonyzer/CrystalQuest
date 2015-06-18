using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScoreDataModell {

	[SerializeField]
	float m_scoreValue;

	public float ScoreValue {
		get {return m_scoreValue;}
		set {m_scoreValue = value;}
	}

	public void AddPoints (float value)
	{
		Debug.Log ("AddPoints " + m_scoreValue + " + " + value); 
		m_scoreValue += value;
	}

	public void AddPoints (ScoreDataModell addingScoreData)
	{
		Debug.Log ("AddPoints " + m_scoreValue + " + " + addingScoreData.ScoreValue); 
		m_scoreValue += addingScoreData.ScoreValue;
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
		DomainEventManager.TriggerGlobalEvent (EventNames.ScoredValue, this.scoreData.ScoreValue);

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
