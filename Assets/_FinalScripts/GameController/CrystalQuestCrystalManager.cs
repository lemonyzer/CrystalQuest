using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LayerDistance 
{
	public LayerMask layerMask;
	public float distance = 1f;
}

public class CrystalQuestCrystalManager : MonoBehaviour {

	[SerializeField]
	AudioSource audioSource;		// TODO weil crystals nach dem einsammeln deaktiviert werden -> audiosource componente kann nicht laufen wenn gameobject deaktiviert ist

	[SerializeField]
	AudioClip crystalCollectedClip;

	[SerializeField]
	AudioClip allCrystalCollectedClip;

	// sucht alle in der Scene instanziierten Crystalle
	// listen to neue Crystalinstanziierung
	// listen to destroy Crystal

//	[SerializeField]
//	List<CrystalObject> crystals;

	[SerializeField]
	float collectedCount = 0;

	[SerializeField]
	int collectedCrystals = 0;
	
	public int CollectedCrystals {
		get {return collectedCrystals;}
		set {
			collectedCrystals = value;
			
			if (value >= CrystalQuestWaveManager.Instance.GetCurrentWave ().GetAmountOfCrystals ())
				NotifyAllCrystalsCollectedListener ();
		}
	}

	[SerializeField]
	List<LayerDistance> distances;

//	[SerializeField]	LayerMask crystalls;
//	[SerializeField]	LayerMask mines;
//	[SerializeField]	float distanceBetweenCrystalls = 1f;
//	[SerializeField]	float distanceBetweenMines = 1f;

//	[SerializeField]
//	List<CollectableObject> crystals2;

	// listen to Crystal collected
	// remove from list, count elements
	// element count = 0 -> open gate


	void OnWaveInit ()
	{
		collectedCrystals = 0;
		Wave currentWave = CrystalQuestWaveManager.Instance.GetCurrentWave();
		int crystalAmount = currentWave.GetAmountOfCrystals ();
		int crystalSpawnedAmount = 0;

		CrystalPoolManager.Instance.Grow (crystalAmount);
		for (int i=0; i<crystalAmount; i++)
		{
			Vector3 spawnPosition = GetSpawnPosition ();
			if (spawnPosition != Vector3.zero)
			{
				GameObject pooledCrystal = CrystalPoolManager.Instance.GetObject ();
				pooledCrystal.SetActive (true);
				pooledCrystal.transform.position = spawnPosition;
				crystalSpawnedAmount++;
			}
			else
			{
				Debug.LogWarning (this.ToString () + " Crystal SpawnPosition failed");
			}
		}
		currentWave.SetAmountOfCrystals (crystalSpawnedAmount);
		if (crystalSpawnedAmount == 0)
		{
			Debug.LogError ("no Crystals in this Wave!");
			NotifyAllCrystalsCollectedListener ();
		}
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

	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.WaveInit, OnWaveInit);
		DomainEventManager.StartGlobalListening (EventNames.CrystalCollected, OnCrystalCollected);
//		CrystalObject.onCrystalCreated += OnCrystalCreated;
//		CrystalObject.onCrystalCollected += Collected;
//		CrystalObject.onCreated += RegisterObjectScript;
//		CrystalObject.onDestroyed += UnregisterObjectScript;
	}

	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.WaveInit, OnWaveInit);
		DomainEventManager.StopGlobalListening (EventNames.CrystalCollected, OnCrystalCollected);
//		CrystalObject.onCrystalCreated -= OnCrystalCreated;
//		CrystalObject.onCrystalCollected -= Collected;
//		CrystalObject.onCreated -= RegisterObjectScript;
//		CrystalObject.onDestroyed -= UnregisterObjectScript;
	}

//	void RegisterObjectScript (CrystalQuestObjectScript objectScript)
//	{
//		crystals2.Add (objectScript);
//	}
//	
//	void UnregisterObjectScript (CrystalQuestObjectScript objectScript)
//	{
//		crystals2.Remove (objectScript);
//	}

//	void Collected (CrystalQuestObjectScript crystalScript)
//	{
//		crystals.Remove (crystalScript);
//	}

//	void Collected (CollectableObject crystalScript)
//	{
//		crystals.Remove (crystalScript);
//	}

//	void OnCrystalCreated (CrystalObject crystalScript)
//	{
//		crystals.Add (crystalScript);
//	}

	void OnCrystalCollected ()
	{
		PlayerClip (crystalCollectedClip);

		// TODO DONE ersetzt:
//		if (audioSource != null)
//		{
//			if (crystalCollectedClip != null)
//				audioSource.PlayOneShot (crystalCollectedClip);
//			else
//				Debug.LogError (this.ToString () + " has no crystalCollectedClip set!!!");
//		}
//		else
//			Debug.LogError (this.ToString () + " has no audioSource set!!!");

		// crystals.Remove (crystalScript);
		CollectedCrystals++;
//		if (collectedCount >= crystals.Count)
//		{
//			NotifyAllCrystalsCollectedListener ();
//		}
	}

//	void Collected (CrystalObject crystalScript)
//	{
//		// crystals.Remove (crystalScript);
//		CollectedCrystals++;
//		if (collectedCount >= crystals.Count)
//		{
//			NotifyAllCrystalsCollectedListener ();
//		}
//	}

//	// portal gate öffnen
//	public delegate void AllCrystalsCollected ();
//	public static event AllCrystalsCollected onAllCrystalsCollected;
//
	void NotifyAllCrystalsCollectedListener ()
	{
		PlayerClip (allCrystalCollectedClip);
		DomainEventManager.TriggerGlobalEvent (EventNames.AllCrystalsCollected);

//		if (onAllCrystalsCollected != null)
//			onAllCrystalsCollected ();
//		else
//			Debug.LogError (this.ToString() + " no onAllCrystalsCollected listener");
	}
//
//	public override void NextLevel (int level)
//	{
//		base.NextLevel (level);
//		this.collectedCount = 0;
//	}

	void PlayerClip (AudioClip clip)
	{
		if (audioSource != null)
		{
			if (clip != null)
				audioSource.PlayOneShot (clip);
			else
				Debug.LogError (this.ToString () + " audioClip is not set!");
		}
		else
			Debug.LogError (this.ToString () + " audioSource is not set!");
			
	}
}
