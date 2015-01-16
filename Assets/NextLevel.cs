using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	LevelScript levelScript;

	// Use this for initialization
	void Start () {
		levelScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<LevelScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			levelScript.nextLevel();
		}
	}
}
