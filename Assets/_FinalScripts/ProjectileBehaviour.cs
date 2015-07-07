using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {

	void OnEnable () {
//		DomainEventManager.StartGlobalListening (EventNames.AllCrystalsCollected, OnAllCrystalsCollected);
		DomainEventManager.StartGlobalListening (EventNames.SmartBombTriggered, DisableMe);
		DomainEventManager.StartGlobalListening (EventNames.RemoveAllProjectiles, DisableMe);
		DomainEventManager.StartGlobalListening (EventNames.WaveDestroyCurrent, DisableMe);
		DomainEventManager.StartGlobalListening (EventNames.WaveFailed, DisableMe);
		DomainEventManager.StartGlobalListening (EventNames.WaveComplete, DisableMe);
	}
	
	// Update is called once per frame
	void OnDisable () {
//		DomainEventManager.StopGlobalListening (EventNames.AllCrystalsCollected, OnAllCrystalsCollected);
		DomainEventManager.StopGlobalListening (EventNames.SmartBombTriggered, DisableMe);
		DomainEventManager.StopGlobalListening (EventNames.RemoveAllProjectiles, DisableMe);
		DomainEventManager.StopGlobalListening (EventNames.WaveDestroyCurrent, DisableMe);
		DomainEventManager.StopGlobalListening (EventNames.WaveFailed, DisableMe);
		DomainEventManager.StopGlobalListening (EventNames.WaveComplete, DisableMe);
	}

	void OnWaveDestroyCurrent ()
	{
		DisableMe ();
	}
	
	void OnRemoveAllProjectiles ()
	{
		DisableMe ();
	}
	
	void OnSmartBombTriggered ()
	{
		DisableMe ();
	}
	
	void DisableMe()
	{
		this.gameObject.SetActive (false);
	}
}
