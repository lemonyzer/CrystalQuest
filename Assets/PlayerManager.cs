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

	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.PlayerDied, OnPlayerDied);
		DomainEventManager.StartGlobalListening (EventNames.WaveRetry, OnWaveRetry);
	}

	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.PlayerDied, OnPlayerDied);
		DomainEventManager.StopGlobalListening (EventNames.WaveRetry, OnWaveRetry);
	}

	void OnPlayerDied ()
	{
		Lifes--;
		if (respawnPossible)
			DomainEventManager.TriggerGlobalEvent (EventNames.PlayerWillRespawn);
		else 
			DomainEventManager.TriggerGlobalEvent (EventNames.PlayerGameOver);
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
