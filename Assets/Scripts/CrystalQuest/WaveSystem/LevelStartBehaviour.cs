using UnityEngine;
using System.Collections;

public class LevelStartBehaviour : MonoBehaviour {

	void NotExecuted ()
	{
		startWave = null;
	}

	void OnEnable () {
		DomainEventManager.StartGlobalListening (EventNames.WaveStart, OnStartWave);
	}
	
	void OnDisable () {
		DomainEventManager.StopGlobalListening (EventNames.WaveStart, OnStartWave);
	}

	[SerializeField]
	MyEvent startWave;

	void OnStartWave ()
	{
		startWave.Invoke ();
	}
}
