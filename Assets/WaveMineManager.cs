using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveMineManager : MonoBehaviour {

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
		//		CrystalObject.onCrystalCreated += OnCrystalCreated;
		//		CrystalObject.onCrystalCollected += Collected;
		//		CrystalObject.onCreated += RegisterObjectScript;
		//		CrystalObject.onDestroyed += UnregisterObjectScript;
	}
	
	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.WaveInit, OnWaveInit);
		//		CrystalObject.onCrystalCreated -= OnCrystalCreated;
		//		CrystalObject.onCrystalCollected -= Collected;
		//		CrystalObject.onCreated -= RegisterObjectScript;
		//		CrystalObject.onDestroyed -= UnregisterObjectScript;
	}

	void OnWaveInit ()
	{
		Wave currentWave = CrystalQuestWaveManager.Instance.GetCurrentWave ();
		int minesAmount = currentWave.GetAmountOfMines ();
		int spawnedAmount = 0;
		
		Grow (minesAmount);
		float startTime = Time.realtimeSinceStartup;
		bool broken = false;
		
		for (int i=0; i<minesAmount; i++)
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
			Debug.LogWarning ("Timelimit reached, cancle spawning: " + spawnedAmount + " of " + minesAmount + " spawned");

	}

	Vector3 GetSpawnPosition () {
		Vector3 spawnPosition = new Vector3 ();
		float startTime = Time.realtimeSinceStartup;
		bool overlapping = true;
		while (overlapping == true) {
			Vector2 spawnPositionRaw = CrystalQuestLevelManager.Instance.GetRandomLevelPositionWithoutPlayerSpawn();
			spawnPosition = spawnPositionRaw;
			//			overlapping = !Physics.CheckSphere (spawnPosition, 0.75f);
			overlapping = IsOverlaping (spawnPosition);
			if (Time.realtimeSinceStartup - startTime > 0.5f) {
				Debug.Log ("Time out placing Minion!");
				return Vector3.zero;
			}
		}
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
				Debug.Log ("overlapping " + overlappingCollider2d.gameObject.ToString () + " @ " + overlappingCollider2d.transform.position);
				return true;
			}
		}
		return false;
	}
}
