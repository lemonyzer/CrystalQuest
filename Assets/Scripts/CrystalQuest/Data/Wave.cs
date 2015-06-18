using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave : ScriptableObject {
	
	[SerializeField]
	List<WaveEnemy> enemies;
}
