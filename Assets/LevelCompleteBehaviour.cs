using UnityEngine;
using System.Collections;

public class LevelCompleteBehaviour : MonoBehaviour {

	void NotExecuted ()
	{
		levelCompleted = null;
	}

	void OnEnable ()
	{
		//		SmartBombManager.onSmartBombing += SmartBombTriggered;
		DomainEventManager.StartGlobalListening (EventNames.LevelMissionComplete, OnLevelCompleted);
	}
	
	void OnDisable ()
	{
		//		SmartBombManager.onSmartBombing -= SmartBombTriggered;
		DomainEventManager.StopGlobalListening (EventNames.LevelMissionComplete, OnLevelCompleted);
	}
	
	[SerializeField]
	MyEvent levelCompleted;

//	[SerializeField]
//	MyIntEvent levelCompletedEvent;
	
	void OnLevelCompleted ()
	{
		levelCompleted.Invoke ();
//		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnReceiveFullDamage);
	}

//	void OnLevelCompleted (int level)
//	{
//
//	}
}
