using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	[SerializeField]
	ScoreDataModell score;

	void Awake ()
	{
		if (score == null)
			score = new ScoreDataModell ();

		onScoring = new UnityAction<float> (OnScoring);
	}

	// Use this for initialization
	void OnEnable () {
		DomainEventManager.StartGlobalListening (EventNames.ScoredValue, onScoring);
	}
	
	// Update is called once per frame
	void OnDisable () {
		DomainEventManager.StopGlobalListening (EventNames.ScoredValue, onScoring);
	}

	UnityAction<float> onScoring;// = new UnityAction<float> (OnScoring);

	void OnScoring (float scoreValue)
	{
		this.score.AddPoints (scoreValue);
		DomainEventManager.TriggerGlobalEvent (EventNames.ScoreUpdate, this.score.ScoreValue);
	}
}
