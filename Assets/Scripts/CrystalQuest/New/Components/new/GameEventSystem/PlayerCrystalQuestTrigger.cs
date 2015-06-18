using UnityEngine;
using System.Collections;

public class PlayerCrystalQuestTrigger : MonoBehaviour {

//	public delegate void Died ();
//	public static event Died onDied;

	void OnEnable ()
	{
		DomainEventManager.StartListening (this.gameObject, EventNames.OnHealthValueUpdate, NotifyHealthValueUpdate);
		DomainEventManager.StartListening (this.gameObject, EventNames.OnLifeValueUpdate, NotifyLifeValueUpdate);
		DomainEventManager.StartListening (this.gameObject, EventNames.OnDie, NotifyDie);
		
	}

	void OnDisable ()
	{
		DomainEventManager.StopListening (this.gameObject, EventNames.OnHealthValueUpdate, NotifyHealthValueUpdate);
		DomainEventManager.StopListening (this.gameObject, EventNames.OnLifeValueUpdate, NotifyLifeValueUpdate);
		DomainEventManager.StopListening (this.gameObject, EventNames.OnDie, NotifyDie);
		
	}


	// health, life update geht auch über UIScript an GameObject (um in richtiger Domain zu lauschen)
	void NotifyHealthValueUpdate (float newValue)
	{
		
	}

	void NotifyLifeValueUpdate (float newValue)
	{
		
	}

	void NotifyDie ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.PlayerDied);
	}

	public void Die ()
	{
//		if (onDied != null)
//			onDied ();
//		else
//			Debug.LogError ("no onDied listener");
	}
}
