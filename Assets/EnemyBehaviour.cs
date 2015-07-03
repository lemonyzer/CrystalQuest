using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	[SerializeField]
	HealthManager healthManager;

	[SerializeField]
	ExplosionScript explosionScript;

	void Awake ()
	{
		if (healthManager == null)
		{
			healthManager = this.GetComponent<HealthManager>();
			Debug.LogError (this.ToString() + " no HealthManager attached");
		}

		if (explosionScript == null)
		{
			explosionScript = this.GetComponent<ExplosionScript>();
			Debug.LogError (this.ToString() + " no ExplosionScript attached");
		}
	}

	// Use this for initialization
	void OnEnable () {
//		DomainEventManager.StartGlobalListening (EventNames.AllCrystalsCollected, OnAllCrystalsCollected);
		DomainEventManager.StartGlobalListening (EventNames.SmartBombTriggered, OnSmartBombTriggered);
		DomainEventManager.StartGlobalListening (EventNames.KillAllEnemies, OnKillAllEnemies);
		DomainEventManager.StartGlobalListening (EventNames.WaveDestroyCurrent, OnWaveDestroyCurrent);
		DomainEventManager.StartGlobalListening (EventNames.WaveFailed, DisableMe);
		DomainEventManager.StartGlobalListening (EventNames.WaveComplete, DisableMe);
	}

	// Update is called once per frame
	void OnDisable () {
//		DomainEventManager.StopGlobalListening (EventNames.AllCrystalsCollected, OnAllCrystalsCollected);
		DomainEventManager.StopGlobalListening (EventNames.SmartBombTriggered, OnSmartBombTriggered);
		DomainEventManager.StopGlobalListening (EventNames.KillAllEnemies, OnKillAllEnemies);
		DomainEventManager.StopGlobalListening (EventNames.WaveDestroyCurrent, OnWaveDestroyCurrent);
		DomainEventManager.StopGlobalListening (EventNames.WaveFailed, DisableMe);
		DomainEventManager.StopGlobalListening (EventNames.WaveComplete, DisableMe);
	}

	void OnWaveDestroyCurrent ()
	{
		DisableMe ();
	}

	void OnKillAllEnemies ()
	{
		ReceiveFullDamage ();
	}

//	void OnAllCrystalsCollected ()
//	{
//		DisableMe ();
//	}

	void OnSmartBombTriggered ()
	{
		ReceiveFullDamage ();
	}

	void OnRemoveAllEnemies ()
	{
		DisableMe ();
	}

	void DisableMe()
	{
		this.gameObject.SetActive (false);
	}

	void ReceiveFullDamage ()
	{
		if (healthManager != null)
			healthManager.ReceiveFullDamageIgnoreInvincible ();
		else
			DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnReceiveFullDamage);
	}
}
