using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	[SerializeField]
	ScoreDataModell score;

	void Awake ()
	{
		if (score == null)
			score = new ScoreDataModell ();
	}

	// Use this for initialization
	void OnEnable () {
		DomainEventManager.StartGlobalListening (EventNames.ScoredValue, OnScoring);
	}
	
	// Update is called once per frame
	void OnDisable () {
		DomainEventManager.StopGlobalListening (EventNames.ScoredValue, OnScoring);
	}

	void OnScoring (float scoreValue)
	{
		this.score.AddPoints (scoreValue);
		DomainEventManager.TriggerGlobalEvent (EventNames.ScoreUpdate, this.score.ScoreValue);
	}
}
