using UnityEngine;
using System.Collections;

public class TriggerPlayerExitReached : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
	
	}
	
	// Update is called once per frame
	void OnDisable () {
	
	}

	public void NotifyPlayerExitReachedListener ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.PortalReached);
	}
}
