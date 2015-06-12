using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrystalQuestGameManager : MonoBehaviour {

	[SerializeField]
	private GameObject playerGo;
	[SerializeField]
	private PlayerObjectScript playerScript;
	[SerializeField]
	private List<CrystalQuestObjectScript> cachedScripts;

	void Awake ()
	{
//		cachedScripts = new List<CrystalQuestObjectScript>();
	}

	void RegisterObjectScript (CrystalQuestObjectScript objectScript)
	{
		cachedScripts.Add (objectScript);
	}

	void UnregisterObjectScript (CrystalQuestObjectScript objectScript)
	{
		cachedScripts.Remove (objectScript);
	}

    void OnEnable()
    {
		CrystalQuestObjectScript.onCreated += RegisterObjectScript;
		CrystalQuestObjectScript.onDestroyed += UnregisterObjectScript;
		PlayerObjectScript.onDied += PlayerDied;
    }

    void OnDisable()
    {
		CrystalQuestObjectScript.onCreated -= RegisterObjectScript;
		CrystalQuestObjectScript.onDestroyed -= UnregisterObjectScript;
		PlayerObjectScript.onDied -= PlayerDied;
    }

	void PlayerDied (int numberOfRemainingLifes)
	{
		if (numberOfRemainingLifes > 0)
			TriggerRestartLevel ();
		else
		{
			GameOver ();
		}
	}

	void TriggerRestartLevel ()
	{
		foreach (CrystalQuestObjectScript script in cachedScripts)
		{
			script.RestartLevel ();
		}
	}

	void RestartLevelNotCached ()
	{
		// Methode 1
		// Variante A
		// TODO: PROBLEM TODO nur Aktive GameObjecte/Scripte werden gefunden!!!!
		// TODO cachen:
		// A) alle Objecte CrystalGameObjectScript haben ein OnInstantiated Event und Manager hört auf Event und trägt GO/Script in Liste ein
		// B) alle Objecte CrystalGameObjectScript haben ein OnInstantiated Event und registrieren sich bei einem InstantiatedManager (PoolManager)
		CrystalQuestObjectScript[] objects = GameObject.FindObjectsOfType<CrystalQuestObjectScript>();
		foreach (CrystalQuestObjectScript script in objects)
		{
			script.RestartLevel ();
		}
		
		// Variante B
		//
//		CrystalQuestObjectManagerScript[] managers = GameObject.FindObjectsOfType<CrystalQuestObjectManagerScript>();
//		foreach (CrystalQuestObjectManagerScript manager in managers)
//		{
//			manager.RestartLevel ();
//		}
		
		// Variante C
		//
//		EnemyObjectScript[] enemies = GameObject.FindObjectsOfType<EnemyObjectScript>();
//		ProjectileObjectScript[] projectiles = GameObject.FindObjectsOfType<ProjectileObjectScript>();
//		
//		foreach (EnemyObjectScript script in enemies)
//			GameObject.Destroy (script.gameObject);
//		
//		foreach (ProjectileObjectScript script in projectiles)
//			GameObject.Destroy (script.gameObject);
//		
//		PlayerObjectScript player = GameObject.FindObjectOfType<PlayerObjectScript>();
//		player.transform.position = Vector3.zero;
//		player.gameObject.SetActive (true);
	}

	// Methode 2
	//
//	public delegate void RestartLevel ();
//	public static event RestartLevel onRestartLevel;
//
//	void RestartLevelDelegate ()
//	{
//		// Methode 2
//		if (onRestartLevel != null)
//			onRestartLevel ();
//		else
//			Debug.LogError (this.ToString() + " no onRestartLevel listener");
//	}


	// Methode 3 
	//
//	EventManager.listen ("RestartLevel");

	void GameOver ()
	{

	}

}
