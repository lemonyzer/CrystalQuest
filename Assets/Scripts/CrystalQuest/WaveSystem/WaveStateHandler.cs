using UnityEngine;
using System.Collections;

public class WaveStateHandler : MonoBehaviour {

	void OnEnable()
	{
		DomainEventManager.StartGlobalListening (EventNames.WaveInit, OnWaveInit);
		DomainEventManager.StartGlobalListening (EventNames.WaveStart, OnWaveStarted);
		DomainEventManager.StartGlobalListening (EventNames.WaveComplete, OnWaveCompleted);
	}
	
	void OnDisable()
	{
		DomainEventManager.StopGlobalListening (EventNames.WaveInit, OnWaveInit);
		DomainEventManager.StopGlobalListening (EventNames.WaveStart, OnWaveStarted);
		DomainEventManager.StopGlobalListening (EventNames.WaveComplete, OnWaveCompleted);
	}

	void NotExecuted ()
	{
		waveStarted = null;
		waveCompleted = null;
		waveFailed = null;
		waveNextTry = null;
		waveLoading = null;
		waveInit = null;
	}

	[SerializeField]
	MyEvent waveStarted;

	[SerializeField]
	MyEvent waveCompleted;

	[SerializeField]
	MyEvent waveFailed;

	[SerializeField]
	MyEvent waveNextTry;

	[SerializeField]
	MyEvent waveLoading;

	[SerializeField]
	MyEvent waveInit;

	void OnWaveStarted ()
	{
		waveStarted.Invoke ();
	}

	void OnWaveCompleted ()
	{
		waveCompleted.Invoke ();
	}

	void OnWaveFailed ()
	{
		waveFailed.Invoke ();
	}

	void OnWaveNextTry ()
	{
		waveNextTry.Invoke ();
	}

	void OnWaveLoading ()
	{
		waveLoading.Invoke ();
	}

	void OnWaveInit ()
	{
		waveInit.Invoke ();
	}
}
