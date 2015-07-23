using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	[SerializeField]
	AudioClip extraLifeClip;

	[SerializeField]
	AudioSource myAudioSource;

	[SerializeField]
	ScoreDataModell score;

	[SerializeField]
	int pointsForExtraLive = 10000;

	[SerializeField]
	int extraLiveStep = 1;

	void Awake ()
	{
		if (score == null)
			score = new ScoreDataModell ();

		onScoring = new UnityAction<float> (OnScoring);

		myAudioSource = this.GetComponent<AudioSource> ();
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

		if ( 0 <= (this.score.ScoreValue - pointsForExtraLive * extraLiveStep))
		{
			extraLiveStep++;
			DomainEventManager.TriggerGlobalEvent (EventNames.ExtraLifeGained);
			myAudioSource.PlayOneShot (extraLifeClip);
		}
	}


	public float GetScore ()	
	{
		return score.ScoreValue;
	}


}
