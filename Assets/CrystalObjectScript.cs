using UnityEngine;
using System.Collections;

public class CrystalObjectScript : MonoBehaviour {

//	void OnEnable ()
//	{
		//		SmartBombManager.onSmartBombing += SmartBombTriggered;
//		DomainEventManager.StartGlobalListening (EventNames.CrystalsCollected, OnLevelCompleted);
//	}
	
//	void OnDisable ()
//	{
		//		SmartBombManager.onSmartBombing -= SmartBombTriggered;
//		DomainEventManager.StopGlobalListening (EventNames.CrystalsCollected, OnLevelCompleted);
//	}
	
//	[SerializeField]
//	public static MyEvent collected;
	
	public void TriggerCollected ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.CrystalsCollected);
	}

}
