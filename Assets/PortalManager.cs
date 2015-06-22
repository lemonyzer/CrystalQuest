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
		DomainEventManager.StartGlobalListening (EventNames.CloseLevelPortal, OnClosingPortal);
	}
	
	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.OpenLevelPortal, OnOpeningPortal);
		DomainEventManager.StopGlobalListening (EventNames.CloseLevelPortal, OnClosingPortal);
	}
	
	[SerializeField]
	MyEvent openingPortal;

	[SerializeField]
	MyEvent closingPortal;
	
	void OnOpeningPortal ()
	{
		openingPortal.Invoke ();
	}

	void OnClosingPortal ()
	{
		closingPortal.Invoke ();
	}


}
