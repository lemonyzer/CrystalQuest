using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public GameObject player;
	public GameObject[] spawnPositions;
	public GameObject prefabEnemy;
	public float spawnInterval = 2.0f;
	float currentInterval;

	public bool SpawnEnemyEnable = true; 

	// Use this for initialization
	void Start () {
		currentInterval = spawnInterval;
	}
	
	// Update is called once per frame
	void Update () {
		if (!SpawnEnemyEnable)
			return;

		currentInterval -= Time.deltaTime;
		if (currentInterval <= 0) {
			currentInterval = spawnInterval;

			int currentSpawnPos = Random.Range(0,spawnPositions.Length);
			int secondSpawnPos = currentSpawnPos;
			do {
				secondSpawnPos = Random.Range(0, spawnPositions.Length);
			} while (secondSpawnPos == currentSpawnPos);

			//Spawnposition
			//RANDOM
			//Abwechselnd
			//Von Spielerposition abhängig machen
			//	schwerer: in der nähe 
			//	leichter: weiter weg

			GameObject enemy = (GameObject) Instantiate(prefabEnemy, spawnPositions[currentSpawnPos].transform.position, Quaternion.identity);
			enemy.GetComponent<MoveToTarget>().target = player;

			enemy = (GameObject) Instantiate(prefabEnemy, spawnPositions[secondSpawnPos].transform.position, Quaternion.identity);
			enemy.GetComponent<MoveToTarget>().target = player;
		}
	}


}
