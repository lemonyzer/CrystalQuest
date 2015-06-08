using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : CrystalQuestObjectScript {

	public delegate void EnemySpawned(CrystalQuestObjectScript enemyScript);
	public static event EnemySpawned onEnemySpawned;

	public List<GameObject> enemyPrefabs;

	[SerializeField]
	private Vector3 spawnPositionOffset;

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



	// Use this for initialization
	void Start () {
	
	}

	int RandomEnemyId()
	{
		if (enemyPrefabs == null)
			return -1;
		if (enemyPrefabs.Count < 1)
			return -1;

		return Random.Range(0, enemyPrefabs.Count);
	}

	float RandomNextSpawnTimestamp()
	{
		return Random.Range (enemySpawnIntervallMin, enemySpawnIntervallMax);
	}
	
	// Update is called once per frame
	void Update () {

		if (Activ())
		{

		}

	}

	bool Activ()
	{
		if (enemyPrefabs != null && enemyPrefabs.Count > 0)
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
		return false;
	}

	bool SpawnCountMaxReached ()
	{
		if (spawnCount >= spawnCountMax)
			return true;
		else
			return false;
	}

	bool ValidNextSpawningEnemy()
	{
		nextSpawningEnemyId = RandomEnemyId();
		if (nextSpawningEnemyId > -1)
		{
			if (enemyPrefabs[nextSpawningEnemyId] != null)
				return true;
			else
				return false;
		}
		return false;
	}

	void Spawn()
	{
		if (ValidNextSpawningEnemy())
		{	
//			nextEnemySpawnTimestamp = Time.time + RandomNextSpawnTimestamp();
			spawnCount++;
			GameObject newEnemy = InstantiateEnemy (enemyPrefabs[nextSpawningEnemyId], transform.position);
			EnemyObjectScript enemyScript = newEnemy.GetComponent<EnemyObjectScript>();
			if (onEnemySpawned != null)
			{
				onEnemySpawned (enemyScript);
			}
			else
			{
#if UNITY_EDITOR
				Debug.LogWarning (this.ToString() + " no onEnemySpawned listener");
#endif
			}
		}
	}

	GameObject InstantiateEnemy (GameObject prefab, Vector3 position)
	{
		return GameObject.Instantiate (prefab, position + spawnPositionOffset, Quaternion.identity) as GameObject;
	}

	#region Customs
	public override void RestartLevel ()
	{
		base.RestartLevel ();
		this.gameObject.SetActive (true);
		this.SpawnCount = 0;
	}
	#endregion
}
