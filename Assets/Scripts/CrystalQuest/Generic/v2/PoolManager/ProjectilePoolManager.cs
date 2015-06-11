using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectilePoolManager : MonoBehaviour {

//	[SerializeField]
//	Dictionary<CrystalQuestObjectScript, GameObject> projectileDictionary;

	public static ProjectilePoolManager current;

	void Awake ()
	{
		current = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
