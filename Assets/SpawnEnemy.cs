using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public GameObject player;
	public GameObject[] spawnPositions;
	public GameObject prefabEnemy;
	public float spawnInterval = 2.0f;
	float currentInterval;

	// Use this for initialization
	void Start () {
		currentInterval = spawnInterval;
	}
	
	// Update is called once per frame
	void Update () {
		currentInterval -= Time.deltaTime;
		if (currentInterval <= 0) {
			currentInterval = spawnInterval;

			int currentSpawnPos = Random.Range(0,spawnPositions.Length);

			GameObject enemy = (GameObject) Instantiate(prefabEnemy, spawnPositions[currentSpawnPos].transform.position, Quaternion.identity);
			enemy.GetComponent<MoveToTarget>().target = player;
		}
	}


}
