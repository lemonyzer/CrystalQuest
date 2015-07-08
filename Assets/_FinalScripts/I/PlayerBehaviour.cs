using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	[SerializeField]
	MyEvent onWavePreInit;

	[SerializeField]
	MyEvent onWaveStart;

	[SerializeField]
	MyEvent onExtraLifeGained;

	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.WavePreInit, OnWavePreInit);
		DomainEventManager.StartGlobalListening (EventNames.ExtraLifeGained, OnExtraLifeGained);
		DomainEventManager.StartGlobalListening (EventNames.WaveStart, OnWaveStart);
//		DomainEventManager.StartGlobalListening (EventNames.WaveInit, OnWaveInit);
	}
	
	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.WavePreInit, OnWavePreInit);
		DomainEventManager.StopGlobalListening (EventNames.ExtraLifeGained, OnExtraLifeGained);
		DomainEventManager.StartGlobalListening (EventNames.WaveStart, OnWaveStart);
//		DomainEventManager.StopGlobalListening (EventNames.WaveInit, OnWaveInit);
    }

	void OnWavePreInit ()
	{
		this.gameObject.transform.position = Vector3.zero;
		// disable input
		onWavePreInit.Invoke ();
	}

	void OnWaveInit ()
	{
		
	}

	void OnWaveStart ()
	{
		onWaveStart.Invoke ();
	}

	void OnExtraLifeGained ()
	{
		onExtraLifeGained.Invoke ();
	}
}
