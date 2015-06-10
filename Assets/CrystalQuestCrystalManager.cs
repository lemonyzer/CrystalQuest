using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrystalQuestCrystalManager : MonoBehaviour {

	// sucht alle in der Scene instanziierten Crystalle
	// listen to neue Crystalinstanziierung
	// listen to destroy Crystal

	[SerializeField]
	List<CrystalObject> crystals;

//	[SerializeField]
//	List<CollectableObject> crystals2;

	// listen to Crystal collected
	// remove from list, count elements
	// element count = 0 -> open gate


	void OnEnable ()
	{
		CrystalObject.onCrystalCreated += Created;
		CrystalObject.onCrystalCollected += Collected;
//		CrystalObject.onCreated += RegisterObjectScript;
//		CrystalObject.onDestroyed += UnregisterObjectScript;
	}

	void OnDisable ()
	{
		CrystalObject.onCrystalCreated -= Created;
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

	void Created (CrystalObject crystalScript)
	{
		crystals.Add (crystalScript);
	}

	void Collected (CrystalObject crystalScript)
	{
		crystals.Remove (crystalScript);
	}
}
