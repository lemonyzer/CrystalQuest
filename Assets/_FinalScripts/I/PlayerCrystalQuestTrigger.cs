using UnityEngine;
using System.Collections;

public class PlayerCrystalQuestTrigger : MonoBehaviour {

//	void OnEnable ()
//	{
//		DomainEventManager.StartListening (this.gameObject, EventNames.OnDie, NotifyDie);
//	}
//
//	void OnDisable ()
//	{
//		DomainEventManager.StopListening (this.gameObject, EventNames.OnDie, NotifyDie);
//	}


//	// health, life update geht auch über UIScript an GameObject (um in richtiger Domain zu lauschen)
//	void NotifyHealthValueUpdate (float newValue)
//	{
//		
//	}
//
//	void NotifyLifeValueUpdate (float newValue)
//	{
//		
//	}

	public void NotifyPlayerDie ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.PlayerDied);
	}

	public void NotifyPlayerWillRespawn ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.PlayerWillRespawn);
	}

	public void NotifyPlayerIsGameOver ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.PlayerGameOver);
	}

	public void NotifyPlayerRespawned ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.PlayerRespawned);
	}

	public void Die ()
	{
//		if (onDied != null)
//			onDied ();
//		else
//			Debug.LogError ("no onDied listener");
	}
}
