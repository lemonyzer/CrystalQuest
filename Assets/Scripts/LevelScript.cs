using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

	public static float left = -9;
	public static float right= 9;
	public static float bottom = -4;
	public static float top = 4;

	public static int levelNumber = 0;
	public static int minCrystalQuantity = 2;
	public static int currentLevelCrystalQuantity;


//	CrystalScript crystalsScript;

	void Awake() {
//		crystalsScript = GetComponent<CrystalScript> ();
//		SpawnCrystals ();
	}

	// Use this for initialization
	void Start () {

	}


	
	// Update is called once per frame
	void Update () {
	
	}

//	public static Vector3 RandomPosition()
//	{
//		return new Vector3(Random.Range(left,right),
//		                                     Random.Range(bottom,top),
//		                                     0);
//	}


		 


}
