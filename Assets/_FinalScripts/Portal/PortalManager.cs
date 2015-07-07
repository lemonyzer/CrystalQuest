using UnityEngine;
using System.Collections;

public class PortalManager : MonoBehaviour {

	void NotExecuted ()
	{
		openingPortal = null;
		closingPortal = null;
	}

	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.OpenLevelPortal, OnOpeningPortal);
		DomainEventManager.StartGlobalListening (EventNames.WaveTaskCompleted, OnOpeningPortal);

		DomainEventManager.StartGlobalListening (EventNames.CloseLevelPortal, OnClosingPortal);
		DomainEventManager.StartGlobalListening (EventNames.WaveInit, OnClosingPortal);
	}
	
	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.WaveTaskCompleted, OnOpeningPortal);
		DomainEventManager.StopGlobalListening (EventNames.OpenLevelPortal, OnOpeningPortal);

		DomainEventManager.StopGlobalListening (EventNames.WaveInit, OnClosingPortal);
		DomainEventManager.StopGlobalListening (EventNames.CloseLevelPortal, OnClosingPortal);
	}
	
	[SerializeField]
	MyEvent openingPortal;

	[SerializeField]
	MyEvent closingPortal;
	
	void OnOpeningPortal ()
	{
		Debug.Log (this.ToString () + " open Portal");
		openingPortal.Invoke ();
	}

	void OnClosingPortal ()
	{
		Debug.Log (this.ToString () + " close Portal");
		closingPortal.Invoke ();
	}


}
