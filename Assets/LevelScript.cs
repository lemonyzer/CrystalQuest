using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

	public static float left = -9;
	public static float right= 9;
	public static float bottom = -4;
	public static float top = 4;

	public static int levelNumber = 0;
	public int minCrystalQuantity = 2;
	public static int currentLevelCrystalQuantity;


	CrystalScript crystalsScript;

	void Awake() {
		crystalsScript = GetComponent<CrystalScript> ();
		SpawnCrystals ();
	}

	// Use this for initialization
	void Start () {

	}

	void SpawnCrystals() {
		currentLevelCrystalQuantity = levelNumber + minCrystalQuantity;
		crystalsScript.RandomSpawn (currentLevelCrystalQuantity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static Vector3 RandomPosition()
	{
		return new Vector3(Random.Range(left,right),
		                                     Random.Range(bottom,top),
		                                     0);
	}

	public void RemoveEnemys()
	{
		GameObject[] enemys = GameObject.FindGameObjectsWithTag ("Enemy");
		
		int currentEnemyCount = enemys.Length;
		
		for(int i=0; i<currentEnemyCount; i++)
		{
			Destroy(enemys[i]);
		}

//		Destroy (enemys);
	}
		 

	public void nextLevel()
	{
		RemoveEnemys ();

		levelNumber++;
		SpawnCrystals ();

		GameObject.FindGameObjectWithTag ("Player").transform.position = Vector3.zero;
		GameObject gate = GameObject.FindGameObjectWithTag("Gate");
		gate.GetComponents<BoxCollider>()[0].enabled = true;
		gate.GetComponents<BoxCollider>()[1].enabled = true;
		gate.GetComponent<MeshRenderer>().enabled = true;
		// animation (levelübergang)
		// alle gegner entfernen
		// crystals spawnen
		// player auf startPos setzen
		// gegner nach freeztime/cooldown spawnen
	}
}
