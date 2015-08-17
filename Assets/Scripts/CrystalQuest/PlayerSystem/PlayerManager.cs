using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	[SerializeField]
	int lifes = 3;

	[SerializeField]
	GameObject playerGO;

	[SerializeField]
	RespawnScript player;

	[SerializeField]
	bool respawnPossible = true;

	public int Lifes {
		get {return lifes;}
		set {
			if (value >=0)
			{
				lifes = value;
				respawnPossible = true;
			}
			else if (value < 0)
			{
				lifes = 0;
				respawnPossible = false;
			}
		}
	}

	public GameObject PlayerGO {
		get {return playerGO;}
	}

	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.PlayerDied, OnPlayerDied);
		DomainEventManager.StartGlobalListening (EventNames.WaveRetry, OnWaveRetry);
		DomainEventManager.StartGlobalListening (EventNames.ExtraLifeGained, OnExtraLifeGained);
	}

	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.PlayerDied, OnPlayerDied);
		DomainEventManager.StopGlobalListening (EventNames.WaveRetry, OnWaveRetry);
		DomainEventManager.StopGlobalListening (EventNames.ExtraLifeGained, OnExtraLifeGained);
	}

	void OnPlayerDied ()
	{
		Lifes--;
		if (respawnPossible)
			DomainEventManager.TriggerGlobalEvent (EventNames.PlayerWillRespawn);
		else 
			DomainEventManager.TriggerGlobalEvent (EventNames.PlayerGameOver);
	}

	void OnExtraLifeGained ()
	{
		lifes++;
	}

	void OnWaveRetry ()
	{
		Respawn ();
	}

	void Respawn ()
	{
		player.Activate ();
	}
}
