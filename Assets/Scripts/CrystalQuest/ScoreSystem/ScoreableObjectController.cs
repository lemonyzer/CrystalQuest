using UnityEngine;
using System.Collections;

public class ScoreableObjectController : MonoBehaviour {

	[SerializeField]
	ScoreDataModell scoreData;// = new ScoreDataModell();

	[SerializeField]
	ScoreDataModell scoreDataDelegated;// = new ScoreDataModell();

	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.ScoredValue, AddPoints);

		// delegate Listening
		ScoreableObjectScript.onScored += AddPoints;
		// Unity Event listening
		ScoreableObjectScript.gScored.AddListener (AddPoints);
	}

	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.ScoredValue, AddPoints);
		
		ScoreableObjectScript.onScored -= AddPoints;
		ScoreableObjectScript.gScored.RemoveListener (AddPoints);
	}

	void AddPoints (ScoreDataModell scoreData)
	{
		scoreDataDelegated.AddPoints (scoreData);
	}

	public void AddPoints (float value)
	{
		scoreData.AddPoints (value);
	}

}
