using UnityEngine;
using System.Collections;
using System.Collections.Generic;








public class SmartBombItemCreator : MonoBehaviour {
	
	[SerializeField]
	GameObject prefab;
	
	[SerializeField]
	List<GameObject> pooledGameObjects;
	
	[SerializeField]
	bool willGrow = true;
	
	[SerializeField]
	List<LayerDistance> distances;
	
	void Awake ()
	{
		if (pooledGameObjects == null)
			pooledGameObjects = new List<GameObject> ();
	}
	
	public GameObject GetObject ()
	{
		return GetInactiveFromPool ();
	}
	
	GameObject GetInactiveFromPool ()
	{
		for (int i=0; i < pooledGameObjects.Count; i++)
		{
			if (!pooledGameObjects[i].activeInHierarchy)
			{
				return pooledGameObjects[i];
			}
		}
		
		if (willGrow)
		{
			GameObject newOne = Instantiate (prefab);
			pooledGameObjects.Add (newOne);
			return newOne;
		}
		else {
			Debug.LogError ("Pool is completly active && willGrow == false");
			return null;
		}
	}
	
	public void Grow (int amount)
	{
		while (pooledGameObjects.Count < amount)
		{
			GameObject newOne = Instantiate (prefab);
			pooledGameObjects.Add (newOne);
			newOne.SetActive (false);
		}
	}
	
	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.WaveInit, OnWaveInit);
		DomainEventManager.StartGlobalListening (EventNames.ScoredExtraBonus, OnScoredExtraBonus);
		//		CrystalObject.onCrystalCreated += OnCrystalCreated;
		//		CrystalObject.onCrystalCollected += Collected;
		//		CrystalObject.onCreated += RegisterObjectScript;
		//		CrystalObject.onDestroyed += UnregisterObjectScript;
	}
	
	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.WaveInit, OnWaveInit);
		DomainEventManager.StopGlobalListening (EventNames.ScoredExtraBonus, OnScoredExtraBonus);
		//		CrystalObject.onCrystalCreated -= OnCrystalCreated;
		//		CrystalObject.onCrystalCollected -= Collected;
		//		CrystalObject.onCreated -= RegisterObjectScript;
		//		CrystalObject.onDestroyed -= UnregisterObjectScript;
	}

	[SerializeField]
	int extraItem = 0;

	void OnScoredExtraBonus ()
	{
		extraItem ++;
	}

	void OnWaveInit ()
	{
		Wave currentWave = CrystalQuestWaveManager.Instance.GetCurrentWave ();
		int smartBombsAmount = currentWave.GetAmountOfSmartBombs () + extraItem;
		extraItem = 0;
		int spawnedAmount = 0;
		
		Grow (smartBombsAmount);
		float startTime = Time.realtimeSinceStartup;
		bool broken = false;
		
		for (int i=0; i<smartBombsAmount; i++)
		{
			Vector3 spawnPosition = GetSpawnPosition ();
			if (spawnPosition != Vector3.zero)
			{
				GameObject pooledCrystal = GetObject ();
				pooledCrystal.SetActive (true);
				pooledCrystal.transform.position = spawnPosition;
				spawnedAmount++;
			}
			else
			{
				Debug.LogWarning (this.ToString () + " Crystal SpawnPosition failed");
			}
			
			if (Time.realtimeSinceStartup - startTime > 0.5f) {
				Debug.Log ("Time out placing Minion!");
				broken = true;
				break;
			}
		}
		
		if (broken)
			Debug.LogWarning ("Timelimit reached, cancle spawning: " + spawnedAmount + " of " + smartBombsAmount + " spawned");
		
	}
	
	Vector3 GetSpawnPosition () {
		Vector3 spawnPosition = new Vector3 ();
		float startTime = Time.realtimeSinceStartup;
		bool overlapping = true;
		int overlappingCount = 0;
		while (overlapping == true) {
			Vector2 spawnPositionRaw = CrystalQuestLevelManager.Instance.GetRandomLevelPositionWithoutPlayerSpawn();
			spawnPosition = spawnPositionRaw;
			//			overlapping = !Physics.CheckSphere (spawnPosition, 0.75f);
			overlapping = IsOverlaping (spawnPosition);
			if (overlapping)
				overlappingCount++;
			if (Time.realtimeSinceStartup - startTime > 0.5f) {
				Debug.Log ("Time out placing Minion!");
				return Vector3.zero;
			}
		}
		Debug.Log (this.ToString() + " overlappingCount = " + overlappingCount);
		return spawnPosition;
	}
	
	bool IsOverlaping (Vector3 position)
	{
		for (int i = 0; i < distances.Count; i++)
		{
			LayerDistance current = distances[i];
			Collider2D overlappingCollider2d = Physics2D.OverlapCircle (position, current.distance, current.layerMask);
			if (overlappingCollider2d != null)
			{
				//				Debug.Log ("overlapping " + overlappingCollider2d.gameObject.ToString () + " @ " + overlappingCollider2d.transform.position);
				return true;
			}
		}
		return false;
	}
}
