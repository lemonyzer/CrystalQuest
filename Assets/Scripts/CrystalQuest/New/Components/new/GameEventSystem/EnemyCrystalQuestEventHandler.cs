using UnityEngine;
using System.Collections;

public class EnemyCrystalQuestEventHandler : MonoBehaviour {

	void OnKillEnemies ()
	{
		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnReceiveFullDamage);
	}

	void OnPlayerDied ()
	{
		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnDie);				// TODO EventNames.Disable
	}

	void OnEnable ()
	{
//		CrystalQuestClassicEventManager.onPlayerDied += OnPlayerDied;
		DomainEventManager.StartGlobalListening (EventNames.PlayerDied, OnPlayerDied);
//		DomainEventManager.StartListening (DomainEventManager.instance, EventNames.KillAllEnemies, OnKillEnemy);
		DomainEventManager.StartGlobalListening (EventNames.KillAllEnemies, OnKillEnemies);
	}

	void OnDisable ()
	{
//		CrystalQuestClassicEventManager.onPlayerDied -= OnPlayerDied;
		DomainEventManager.StopGlobalListening (EventNames.PlayerDied, OnPlayerDied);
//		DomainEventManager.StopListening (DomainEventManager.instance, EventNames.KillAllEnemies, OnKillEnemy);
		DomainEventManager.StopGlobalListening (EventNames.KillAllEnemies, OnKillEnemies);
	}
}
