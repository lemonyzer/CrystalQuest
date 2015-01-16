using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrystalScript : MonoBehaviour {

	public GameObject crystalPrefab;


//	List<GameObject> crystalList;

	void Awake () {
//		crystalList = new List<GameObject>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RandomSpawn(int quantity)
	{
		for(int i=0; i<=quantity; i++)
		{
			GameObject randomPosCrystal = Instantiate (crystalPrefab,
			                                           LevelScript.RandomPosition (),
			                                           Quaternion.identity) as GameObject;
			
//			crystalList.Add (randomPosCrystal);
		}

	}
}
