using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrystalQuestCrystalManager : CrystalQuestObjectScript {

	// sucht alle in der Scene instanziierten Crystalle
	// listen to neue Crystalinstanziierung
	// listen to destroy Crystal

	[SerializeField]
	List<CrystalObject> crystals;

	[SerializeField]
	float collectedCount = 0;

//	[SerializeField]
//	List<CollectableObject> crystals2;

	// listen to Crystal collected
	// remove from list, count elements
	// element count = 0 -> open gate


	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.CrystalsCollected, CrystalCollected);
		CrystalObject.onCrystalCreated += OnCrystalCreated;
		CrystalObject.onCrystalCollected += Collected;
//		CrystalObject.onCreated += RegisterObjectScript;
//		CrystalObject.onDestroyed += UnregisterObjectScript;
	}

	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.CrystalsCollected, CrystalCollected);
		CrystalObject.onCrystalCreated -= OnCrystalCreated;
		CrystalObject.onCrystalCollected -= Collected;
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

	void OnCrystalCreated (CrystalObject crystalScript)
	{
		crystals.Add (crystalScript);
	}

	void CrystalCollected ()
	{
		// crystals.Remove (crystalScript);
		collectedCount++;
		if (collectedCount >= crystals.Count)
		{
			NotifyAllCrystalsCollectedListener ();
		}
	}

	void Collected (CrystalObject crystalScript)
	{
		// crystals.Remove (crystalScript);
		collectedCount++;
		if (collectedCount >= crystals.Count)
		{
			NotifyAllCrystalsCollectedListener ();
		}
	}

	// portal gate öffnen
	public delegate void AllCrystalsCollected ();
	public static event AllCrystalsCollected onAllCrystalsCollected;

	void NotifyAllCrystalsCollectedListener ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.AllCrystalsCollected);

		if (onAllCrystalsCollected != null)
			onAllCrystalsCollected ();
		else
			Debug.LogError (this.ToString() + " no onAllCrystalsCollected listener");
	}

	public override void NextLevel (int level)
	{
		base.NextLevel (level);
		this.collectedCount = 0;
	}
}
