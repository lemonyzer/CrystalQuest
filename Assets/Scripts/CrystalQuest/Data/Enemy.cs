using UnityEngine;
using System.Collections;

[System.Serializable]
public class Enemy : ScriptableObject {
	
	[SerializeField]
	GameObject prefab;

	public GameObject Prefab {
		get {return prefab;}
		private set {prefab = value;}
	}
	
}
