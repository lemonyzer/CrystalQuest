using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	[SerializeField]
	MyEvent onWavePreInit;

	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.WavePreInit, OnWavePreInit);
//		DomainEventManager.StartGlobalListening (EventNames.WaveInit, OnWaveInit);
	}
	
	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.WavePreInit, OnWavePreInit);
//		DomainEventManager.StopGlobalListening (EventNames.WaveInit, OnWaveInit);
    }

	void OnWavePreInit ()
	{
		this.gameObject.transform.position = Vector3.zero;
		onWavePreInit.Invoke ();
	}

	void OnWaveInit ()
	{
        
    }
}
