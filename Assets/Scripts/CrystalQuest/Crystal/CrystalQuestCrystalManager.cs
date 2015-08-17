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

	[SerializeField]
	float creationStartTime = 0f;

	[SerializeField]
	float maxCreationTime = 0.2f;
	

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

	// listen to Crystal collected
	// remove from list, count elements
	// element count = 0 -> open gate


	void OnWaveInit ()
	{
		collectedCrystals = 0;
		Wave currentWave = CrystalQuestWaveManager.Instance.GetCurrentWave();
		int crystalAmount = currentWave.GetAmountOfCrystals ();
		int crystalSpawnedAmount = 0;

		creationStartTime = Time.realtimeSinceStartup;

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
				// break;
			}
			if (Time.realtimeSinceStartup > creationStartTime + maxCreationTime)
			{
				break;
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
		bool overlapping = true;
		int overlappingCount = 0;
		while (overlapping == true) {
			Vector2 spawnPositionRaw = CrystalQuestLevelManager.Instance.GetRandomLevelPositionWithoutPlayerSpawn();
			spawnPosition = spawnPositionRaw;
//			overlapping = !Physics.CheckSphere (spawnPosition, 0.75f);
			overlapping = IsOverlaping (spawnPosition);
			if (overlapping)
				overlappingCount++;
			if (Time.realtimeSinceStartup - creationStartTime > maxCreationTime) {
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
	}

	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.WaveInit, OnWaveInit);
		DomainEventManager.StopGlobalListening (EventNames.CrystalCollected, OnCrystalCollected);
	}

	void OnCrystalCollected ()
	{
		PlayerClip (crystalCollectedClip);
		CollectedCrystals++;
	}


	void NotifyAllCrystalsCollectedListener ()
	{
		PlayerClip (allCrystalCollectedClip);
		DomainEventManager.TriggerGlobalEvent (EventNames.AllCrystalsCollected);
	}


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
