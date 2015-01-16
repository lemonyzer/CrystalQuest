using UnityEngine;
using System.Collections;

public class SpawnPowerUp : MonoBehaviour {

	public GameObject[] prefabPowerUps;

	public bool SpawnPowerUpEnable = false;

	public float left;
	public float right;
	public float top;
	public float bottom;

	public float maxSpeed = 2.0f;
	public float maxForce = 100.0f;

	public float powerSpawnInterval = 3.0f;
	float currentInterval;

	// Use this for initialization
	void Start () {
		currentInterval = powerSpawnInterval;
	}
	
	// Update is called once per frame
	void Update () {

		if (!SpawnPowerUpEnable)
			return;

		currentInterval -= Time.deltaTime;

		if (currentInterval <= 0) {
			currentInterval = powerSpawnInterval;

			int randomPowerUpId = Random.Range(0,prefabPowerUps.Length);

			Vector3 randomSpawnPos = new Vector3(Random.Range(left,right),
			                                     Random.Range(bottom,top),
			                                     0);

			GameObject randomPowerUp = (GameObject) Instantiate(prefabPowerUps[randomPowerUpId], randomSpawnPos, Quaternion.identity);

//			randomPowerUp.rigidbody.velocity = new Vector3(Random.Range(-maxSpeed,maxSpeed),
//			                                               Random.Range(-maxSpeed,maxSpeed),
//			                                               0.0f);

			randomPowerUp.rigidbody.AddForce(new Vector3(Random.Range(-maxSpeed,maxSpeed),
			                                               Random.Range(-maxSpeed,maxSpeed),
			                                             0.0f) * maxForce);
		}

	}
}
