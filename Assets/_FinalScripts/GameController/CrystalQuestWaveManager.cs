using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[System.Serializable]
//public class WaveTask {
//	public string TaskName;
//	public bool completed = false;
//}

public class CrystalQuestWaveManager : MonoBehaviour {

//	[SerializeField]
//	List<WaveTask> waveTasks;

	[SerializeField]
	AudioSource audioSource;

	[SerializeField]
	AudioClip waveCompleteClip;

	[SerializeField]
	WaveDataBase waveDB;

	static CrystalQuestWaveManager m_instance;

	public static CrystalQuestWaveManager Instance {
		get {return m_instance;}
	}

	void Awake ()
	{
		if (m_instance != null)
			Debug.LogWarning (this.ToString() + " there is already a CrystakQestWaveManager.");
		else
		{
			m_instance = this;
		}

		if (waveDB == null)
			waveDB = this.GetComponent<WaveDataBase>();

		if (waveDB == null)
		{
			Debug.LogError (this.ToString () + " WaveDataBase missing");
			this.enabled = false;
		}
	}

	void Start ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.WaveInit);
		DomainEventManager.TriggerInitWave (GetCurrentWave ());
		DomainEventManager.TriggerGlobalEvent (EventNames.WaveStart);
		
	}

	[SerializeField]
	int currentWave = 0;

//	[SerializeField]
//	Wave wave;

	[SerializeField]
	Wave wave;


	
	public int CurrentWave {
		get {return currentWave;}
		set {
			if (value >= waveDB.GetNumberOfWaves())
			{
				OnNoMoreWaves ();
				return;
			}
			if (value >=0 && value < waveDB.GetNumberOfWaves())
			{
				currentWave = value;
				OnNextWaveExists ();
			}
			else
				currentWave = 0;


		}
	}
	
	public Wave GetCurrentWave ()
	{
		return waveDB.GetWave(currentWave);
	}

	void OnWaveComplete ()
	{
		CurrentWave++;
	}
	
	void OnNoMoreWaves ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.AllWavesCompleted);
	}

	void OnNextWaveExists ()
	{
		// tell everone next wave is coming
		DomainEventManager.TriggerGlobalEvent (EventNames.WaveNext);		// für scripts die selbst mitzählen
		DomainEventManager.TriggerGlobalEvent (EventNames.WavePreInit);		// loading: pooling
		DomainEventManager.TriggerGlobalEvent (EventNames.WaveInit);		// loading: pooling
		DomainEventManager.TriggerInitWave (GetCurrentWave ());		// loading: pooling
		DomainEventManager.TriggerGlobalEvent (EventNames.WaveStart);
	}

	void OnWaveInit ()
	{
		wave = GetCurrentWave();		// TODO, check if crystal spawning is before this OnWaveInit ()
	}

	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.WaveInit, OnWaveInit);
//		DomainEventManager.StartGlobalListening (EventNames.WaveStart, OnWaveStart);
		DomainEventManager.StartGlobalListening (EventNames.PlayerDied, OnPlayerDied);
		DomainEventManager.StartGlobalListening (EventNames.PlayerWillRespawn, OnPlayerWillRespawn);
//		DomainEventManager.StartGlobalListening (EventNames.PlayerRespawned, OnPlayerRespawned);
		DomainEventManager.StartGlobalListening (EventNames.AllCrystalsCollected, OnAllCrystallsCollected);
//		DomainEventManager.StartGlobalListening (EventNames.CrystalsCollected, OnCrystalCollected);
		DomainEventManager.StartGlobalListening (EventNames.PortalReached, OnPlayerTriggerWaveExit);
//		DomainEventManager.StartGlobalListening (EventNames.WaveDestroyCurrent, OnWaveDestroyCurrent);
	}

	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.WaveInit, OnWaveInit);
//		DomainEventManager.StopGlobalListening (EventNames.WaveStart, OnWaveStart);
		DomainEventManager.StopGlobalListening (EventNames.PlayerDied, OnPlayerDied);
		DomainEventManager.StopGlobalListening (EventNames.PlayerWillRespawn, OnPlayerWillRespawn);
//		DomainEventManager.StopGlobalListening (EventNames.PlayerRespawned, OnPlayerRespawned);
		DomainEventManager.StopGlobalListening (EventNames.AllCrystalsCollected, OnAllCrystallsCollected);
//		DomainEventManager.StopGlobalListening (EventNames.CrystalsCollected, OnCrystalCollected);
		DomainEventManager.StopGlobalListening (EventNames.PortalReached, OnPlayerTriggerWaveExit);
//		DomainEventManager.StopGlobalListening (EventNames.WaveDestroyCurrent, OnWaveDestroyCurrent);
	}

	void TriggerAllCrystalsCollected ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.AllCrystalsCollected);
	}

//	void OnDisable ()
//	{
//		DomainEventManager.StopGlobalListening (EventNames.PlayerDied, OnPlayerDied);
//	}

	void OnPlayerWillRespawn ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.WaveRetry);
		DomainEventManager.TriggerGlobalEvent (EventNames.WaveStart);
	}

	void OnPlayerDied ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.WaveFailed);
	}

//	void OnPlayerRespawned ()
//	{
//		DomainEventManager.TriggerGlobalEvent (EventNames.WaveStart);
//	}

	void OnAllCrystallsCollected ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.WaveTaskCompleted);
	}

//	float currentWaveTime = 0;
//	float gameTime = 0;
//
//	void Update ()
//	{
//		currentWaveTime += Time.deltaTime;
//		gameTime += Time.deltaTime;
//	}
//
//	void OnWaveStart ()
//	{
//		currentWaveTime = 0f;
//	}
//
//	void OnGUI ()
//	{
//		GUILayout.TextField ("GameTime: " + gameTime.ToString ("F3"));
//		GUILayout.TextField ("WaveTime: " + currentWaveTime.ToString ("F2"));
//		GUILayout.TextField ("TimeLimit: " + GetCurrentWave().bonusTimeLimit.ToString ("F2"));
//	}
//	
//	void CalculateBonusPoints ()
//	{
//		Debug.Log ("current Wave Time = " + currentWaveTime + ", bonus time limit = " + GetCurrentWave ().bonusTimeLimit);
//
//		float timeDiff = currentWaveTime - GetCurrentWave ().bonusTimeLimit;
//
//		if (timeDiff > 0f)
//		{
//			float scoreBonus = timeDiff * GetCurrentWave ().timeBonus;
//			DomainEventManager.TriggerGlobalEvent (EventNames.ScoredValue, scoreBonus);
//		}
//	}

	void OnPlayerTriggerWaveExit ()
	{
		// player must not collid with anything No Bullet no Enemy...
		// remove all objects
		// set player back to 0,0,0 (PreInit)
		DomainEventManager.TriggerGlobalEvent (EventNames.PlayerInvincibleEnable);

//		CalculateBonusPoints ();

		// tell everyone wave completed
		audioSource.PlayOneShot (waveCompleteClip);
		DomainEventManager.TriggerGlobalEvent (EventNames.WaveComplete);


		// TODO show wave stats
		//

		// disable current wave
		DomainEventManager.TriggerGlobalEvent (EventNames.WaveDestroyCurrent);

		// TODO show wave stats
		//

		// count up
		CurrentWave++;		// <-- trigger't NoMoreWaves left oder NextWaveExists



//		// tell everone next wave is coming
//		DomainEventManager.TriggerGlobalEvent (EventNames.WaveNext);
//		DomainEventManager.TriggerGlobalEvent (EventNames.WaveInit);
//		DomainEventManager.TriggerGlobalEvent (EventNames.WaveStart);
	}

	// TODO DONE Dependency Inversion
//	void OnWaveDestroyCurrent ()
//	{
//		DomainEventManager.TriggerGlobalEvent (EventNames.RemoveAllProjectiles);
//		DomainEventManager.TriggerGlobalEvent (EventNames.RemoveAllProjectiles);
//	}
}
