using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CollisionMatrix : ScriptableObject {

	[SerializeField]
	private bool[] row;

	[SerializeField]
	private List<bool> column;

}
