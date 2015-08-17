using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpawnPosition
{
	[SerializeField]
	Transform transform;

	[SerializeField]
	Vector3 offset;

	void NotExecuted ()
	{
		transform = null;
		offset = Vector3.zero;
	}

	public Vector3 Position {
		get {
			if (transform != null)
				return transform.position + offset;
			else
			{
				Debug.LogError (this.ToString() + " SpawnPosition has no transform set!");
				return Vector3.zero;
			}
		}
	}
}

public class EnemyManager : MonoBehaviour {


	[SerializeField]
	AudioSource audioSource;

	[SerializeField]
	AudioClip spawnClip;


	/**
	 * 
	 * Enemy Pooler included
	 * 
	 * Dictionary <GameObject, List<GameObject>> objectsPool;
	 * 
	 * 
	 * Key: Prefab (from Assets, not touched - referenced in a ScriptableObject Enemy)
	 * 
	 * 
	 **/

//	public List<GameObject> enemyPrefabs;

	[SerializeField]
	List<GameObject> currentWaveEnemyFrequency;

	[SerializeField]
	Dictionary<GameObject, List<GameObject>> objectsPool;

	void Awake ()
	{
//		CheckPrefabList ();
		objectsPool = new Dictionary<GameObject, List<GameObject>>();
		if (audioSource == null)
			audioSource = this.GetComponent<AudioSource> ();
	}

//	void CheckPrefabList ()
//	{
//		for (int i = 0; i < enemyPrefabs.Count; i++)
//		{
//			if (enemyPrefabs[i] == null)
//				Debug.LogError (this.ToString () + " enemyPrefabs " + i + " not initialized!");
//		}
//	}


	[SerializeField]
	private List<SpawnPosition> spawnPositions;

	[SerializeField]
	private int spawnCount = 0;

	[SerializeField]
	private int spawnCountMax = 10;

	public int SpawnCount {
		get {
			return spawnCount;
		}
		set {
			if (value >= 0)
				spawnCount = value;
		}
	}

	public int SpawnCountMax {
		get {
			return spawnCountMax;
		}
		set {
			if (value >= 0)
				spawnCountMax = value;
		}
	}

	[SerializeField]
	private int nextSpawningEnemyId = -1;

	[SerializeField]
	private float nextEnemySpawnTimestamp = 0f;
	public float NextEnemySpawnTimestamp {
		get
		{
			return nextEnemySpawnTimestamp;
		}
		set
		{
			nextEnemySpawnTimestamp = value;
		}
	}

	[SerializeField]
	private float enemySpawnIntervallMin = 0.1f;
	[SerializeField]
	private float enemySpawnIntervallMax = 2f;

	int RandomEnemyId()
	{
		if (currentWaveEnemyFrequency == null)
			return -1;
		if (currentWaveEnemyFrequency.Count < 1)
			return -1;

		return Random.Range(0, currentWaveEnemyFrequency.Count);
	}

//	int RandomEnemyId()
//	{
//		if (enemyPrefabs == null)
//			return -1;
//		if (enemyPrefabs.Count < 1)
//			return -1;
//
//		return Random.Range(0, enemyPrefabs.Count);
//	}

	float RandomNextSpawnTimestamp()
	{
		return Random.Range (enemySpawnIntervallMin, enemySpawnIntervallMax);
	}

	[SerializeField]
	bool spawningEnabled = false;

	// Update is called once per frame
	void Update () {

		if (Activ())
		{

		}

	}

	bool Activ()
	{
		if (!spawningEnabled)
			return false;

		if (currentWaveEnemyFrequency != null && currentWaveEnemyFrequency.Count > 0)
		{
			if (!SpawnCountMaxReached ())
			{
				if (Time.time >= nextEnemySpawnTimestamp)
				{
					nextEnemySpawnTimestamp = Time.time + RandomNextSpawnTimestamp();
					Spawn();
				}
			}
		}
		else
		{
			Debug.LogError ("current Wave has no Enemy Frequency, stop spawning");
			spawningEnabled = false;
		}
		return false;
	}

	bool SpawnCountMaxReached ()
	{
		if (spawnCount >= spawnCountMax)
			return true;
		else
			return false;
	}

//	bool ValidNextSpawningEnemy()
//	{
//		nextSpawningEnemyId = RandomEnemyId();
//		if (nextSpawningEnemyId > -1)
//		{
//			if (enemyPrefabs[nextSpawningEnemyId] != null)
//				return true;
//			else
//			{
//				Debug.LogError ("EnemyID: " + nextSpawningEnemyId + " is not valid");
//				return false;
//			}
//		}
//		return false;
//	}

// TOTAL RANDOM
//	void Spawn() 
//	{
////		if (ValidNextSpawningEnemy())
////		{	
////			nextEnemySpawnTimestamp = Time.time + RandomNextSpawnTimestamp();
//			spawnCount++;
//			int randomSpawn = Random.Range (0, spawnPositions.Count);
//			SpawnPosition randomSpawnPosition = spawnPositions[randomSpawn];
//			WaveEnemy randomWaveEnemy = CrystalQuestWaveManager.Instance.GetCurrentWave ().GetRandomWaveEnemy ();
//			GameObject newEnemy = InstantiateEnemy (randomWaveEnemy, randomSpawnPosition);
//			if (newEnemy != null)
//			{
//				newEnemy.SetActive (true);
//				newEnemy.GetComponent<HealthManager>().Revive ();
//				//			newEnemy.GetComponent<PooledObject>().Reuse ();		// TODO
//				//			EnemyObjectScript enemyScript = newEnemy.GetComponent<EnemyObjectScript>();
//				if (audioSource != null && spawnClip != null)
//					audioSource.PlayOneShot (spawnClip);
//				else
//					Debug.LogError ("AudioSource or SpawnClip not set");
//			}
////		}
//	}

	/// <summary>
	/// Spawn Enemy with considering frequency.
	/// </summary>
	void Spawn()
	{
		//		if (ValidNextSpawningEnemy())
		//		{	
		//			nextEnemySpawnTimestamp = Time.time + RandomNextSpawnTimestamp();
		spawnCount++;
		int randomSpawn = Random.Range (0, spawnPositions.Count);
		SpawnPosition randomSpawnPosition = spawnPositions[randomSpawn];
//		WaveEnemy randomWaveEnemy = CrystalQuestWaveManager.Instance.GetCurrentWave ().GetRandomWaveEnemy ();
		int randomConsideringFrequency = RandomEnemyId ();
		GameObject enemyPrefab = currentWaveEnemyFrequency[randomConsideringFrequency];
		GameObject newEnemy = InstantiateEnemy (enemyPrefab, randomSpawnPosition);
		if (newEnemy != null)
		{
			newEnemy.SetActive (true);
			newEnemy.GetComponent<HealthManager>().Revive ();
			//			newEnemy.GetComponent<PooledObject>().Reuse ();		// TODO
			//			EnemyObjectScript enemyScript = newEnemy.GetComponent<EnemyObjectScript>();
			if (audioSource != null && spawnClip != null)
				audioSource.PlayOneShot (spawnClip);
			else
				Debug.LogError ("AudioSource or SpawnClip not set");
		}
		//		}
	}

//	GameObject InstantiateEnemy (WaveEnemy waveEnemy, SpawnPosition spawnPos)
//	{
//		GameObject enemy = GetObject (waveEnemy);
//		if (enemy != null)
//		{
//			enemy.transform.position = spawnPos.Position;
//		}
//		
//		return enemy;
//	}

	GameObject InstantiateEnemy (GameObject enemyPrefab, SpawnPosition spawnPos)
	{
		GameObject enemy = GetObject (enemyPrefab);
		if (enemy != null)
		{
			enemy.transform.position = spawnPos.Position;
		}
		
		return enemy;
	}

	public void StartSpawning ()
	{
		spawningEnabled = true;
	}

	public void ResetSpawning ()
	{
		nextEnemySpawnTimestamp = Time.time + RandomNextSpawnTimestamp();
		spawnCount = 0;
	}

	public void RestartSpawning ()
	{
		ResetSpawning ();
		StartSpawning ();
	}

	public void StopSpawning ()
	{
		spawningEnabled = false;
	}

	void NextWave ()
	{

	}

//	GameObject GetObject (WaveEnemy waveEnemy)
//	{
//		if (waveEnemy == null)
//			return null;
//
//		if (waveEnemy.Enemy == null)
//			return null;
//
//		if (waveEnemy.Enemy.Prefab == null)
//			return null;
//
//		List<GameObject> currentEnemyPool = null;
//
//		if (objectsPool.TryGetValue (waveEnemy.Enemy.Prefab, out currentEnemyPool))
//		{
//			return GetInactive (currentEnemyPool);
//		}
//		else
//			return null;
//	}

	GameObject GetObject (GameObject enemyPrefab)
	{
		if (enemyPrefab == null)
			return null;
		
		List<GameObject> currentEnemyPool = null;
		
		if (objectsPool.TryGetValue (enemyPrefab, out currentEnemyPool))
		{
			return GetInactive (currentEnemyPool);
		}
		else
			return null;
	}

	GameObject GetInactive (List<GameObject> pool)
	{
		for (int i=0; i<pool.Count; i++)
		{
			if (!pool[i].activeInHierarchy)
				return pool[i];
		}

		return null;
	}

	void OnWaveInit ()
	{
		// Pooling Enemys

		// aktuelle Welle auslesen
		Wave currentWave = CrystalQuestWaveManager.Instance.GetCurrentWave ();

		// wahrscheinlichkeit
		ReadEnemyFrequenzyList (currentWave);

		// Create Wave Pool
		CreateWavePool (currentWave);

		// Pooling Projectiles

		// Reset all
		ResetSpawning ();
	}

	void ReadEnemyFrequenzyList (Wave currentWave)
	{
		currentWaveEnemyFrequency.Clear ();

		for (int i=0; i < currentWave.enemies.Count; i++)
		{
			// für jeden gegnertyp in wave, füge anzahl in liste um später mit random eine wahrscheinlichkeit zu erreichen

			if (currentWave.enemies[i] == null)
			{
				Debug.LogError ("currentWave enemy " + i + " not initialized!", currentWave);
				continue;
			}
			if (currentWave.enemies[i].Enemy == null)
			{
				Debug.LogError ("currentWave enemy " + i + " has no enemy object  initialized!", currentWave);
				continue;
			}
			if (currentWave.enemies[i].Enemy.Prefab == null)
			{
				Debug.LogError ("currentWave enemy " + i + " -> enemy object has no prefab!", currentWave.enemies[i].Enemy);
				continue;
			}

			for (int x = 0; x < currentWave.enemies[i].Frequenzy; x++)
			{
				currentWaveEnemyFrequency.Add (currentWave.enemies[i].Enemy.Prefab);
			}
		}
	}

	void CreateWavePool (Wave currentWave)
	{
		for (int i=0; i< currentWave.enemies.Count; i++)
		{
			// für jeden Enemy in WaveDB
			WaveEnemy currentWaveEnemy = currentWave.enemies[i];
			if (currentWaveEnemy != null)
			{
				Enemy currentEnemy = currentWaveEnemy.Enemy;
				if (currentEnemy != null)
				{
					if (currentWaveEnemy.Amount <= 0)
					{
						// überspringen
						Debug.Log (this.ToString () + " " + currentWaveEnemy.Enemy.Prefab.name + " wave Amount =" + currentWaveEnemy.Amount + ". dont need to create pool.");
						continue;
					}
					List<GameObject> currentEnemyPool = null;
					int initAmount = 0;
					
					if (objectsPool.TryGetValue (currentEnemy.Prefab, out currentEnemyPool))
					{
						// bereits vorhanden, anzahl überprüfen und ggf. neue hinzufügen
						initAmount = currentWaveEnemy.Amount - currentEnemyPool.Count;
					}
					else
					{
						// noch nicht in pool db vorhanden
						initAmount = currentWaveEnemy.Amount;
						// liste erzeugen
						currentEnemyPool = new List<GameObject> ();
						// liste in dictionary eintragen
						objectsPool.Add (currentEnemy.Prefab, currentEnemyPool);
					}
					
					// erzeuge falls notwendig restliche GameObject instancen von Prefab, deaktivieren und in pool laden
					if (initAmount > 0)
					{
						for (int a = 0; a < initAmount; a++)
						{
							GameObject newPooledObject = Instantiate (currentEnemy.Prefab);
							newPooledObject.SetActive (false);
							currentEnemyPool.Add (newPooledObject);
						}
					}
					Debug.Log (this.ToString () + " " + initAmount + " " + currentWaveEnemy.Enemy.Prefab.name + " pooled.");
				}
				else
					Debug.LogError ("WaveEnemy " + currentWaveEnemy.ToString () + " has no Enemy set");
			}
			else
				Debug.LogError ("error in currentWave " + currentWave.waveName + " @ EnemyField " + i);
		}
	}

	void OnWaveRetry ()
	{
		StopSpawning ();
	}

	void OnWaveStart ()
	{
		// Spawning enabled
		ResetSpawning ();
		StartSpawning ();
		nextEnemySpawnTimestamp = Time.time + CrystalQuestWaveManager.Instance.GetCurrentWave ().minSpawnTime + Random.Range (0f,  CrystalQuestWaveManager.Instance.GetCurrentWave ().randSpawnTimeDelay);
	}

	void OnWaveFailed ()
	{
		StopSpawning ();
	}

	void OnWaveComplete ()
	{
		StopSpawning ();
	}

	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.WaveInit, OnWaveInit);
		DomainEventManager.StartGlobalListening (EventNames.WaveStart, OnWaveStart);
		DomainEventManager.StartGlobalListening (EventNames.WaveRetry, OnWaveRetry);
		DomainEventManager.StartGlobalListening (EventNames.WaveFailed, OnWaveFailed);
		DomainEventManager.StartGlobalListening (EventNames.WaveComplete, OnWaveComplete);
		// TODO DONE: Dependency Inversion
		// Gedanke dahinter war Componenten unabhängig vom Spiel zu machen, dann muss aber eine Componente alle Componenten kennen und deren Events einzeln aufrufen
//		DomainEventManager.StartGlobalListening (EventNames.StartEnemySpawning, StartSpawning);
//		DomainEventManager.StartGlobalListening (EventNames.StopEnemySpawning, StopSpawning);
	}

	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.WaveInit, OnWaveInit);
		DomainEventManager.StopGlobalListening (EventNames.WaveStart, OnWaveStart);
		DomainEventManager.StopGlobalListening (EventNames.WaveRetry, OnWaveRetry);
		DomainEventManager.StopGlobalListening (EventNames.WaveFailed, OnWaveFailed);
		DomainEventManager.StopGlobalListening (EventNames.WaveComplete, OnWaveComplete);
//		DomainEventManager.StopGlobalListening (EventNames.StartEnemySpawning, StartSpawning);
//		DomainEventManager.StopGlobalListening (EventNames.StopEnemySpawning, StopSpawning);
	}
}
