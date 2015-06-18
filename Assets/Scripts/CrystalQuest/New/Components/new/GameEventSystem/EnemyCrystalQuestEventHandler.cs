using UnityEngine;
using System.Collections;

public class EnemyCrystalQuestEventHandler : MonoBehaviour {

	void OnKillEnemy ()
	{

	}

	void OnPlayerDied ()
	{
		this.gameObject.SetActive (false);
	}

	void OnEnable ()
	{
		CrystalQuestClassicEventManager.onPlayerDied += OnPlayerDied;
//		DomainEventManager.StartListening (DomainEventManager.instance, EventNames.KillAllEnemies, OnKillEnemy);
		DomainEventManager.StartGlobalListening (EventNames.KillAllEnemies, OnKillEnemy);
		DomainEventManager.StartGlobalListening (EventNames.PlayerDied, OnPlayerDied);
	}

	void OnDisable ()
	{
		CrystalQuestClassicEventManager.onPlayerDied -= OnPlayerDied;
//		DomainEventManager.StopListening (DomainEventManager.instance, EventNames.KillAllEnemies, OnKillEnemy);
		DomainEventManager.StopGlobalListening (EventNames.KillAllEnemies, OnKillEnemy);
	}
}
