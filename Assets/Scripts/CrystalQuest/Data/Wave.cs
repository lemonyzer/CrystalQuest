using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WaveCrystal 
{
	[SerializeField]
	GameObject prefab;

	[SerializeField]
	int amount = 0;

	[SerializeField]
	bool enabled = false;
}

[System.Serializable]
public class Wave : ScriptableObject {

	[SerializeField]
	string waveName;

	[SerializeField]
	List<WaveEnemy> enemies;

	[SerializeField]
	WaveCrystal crystal;

	void OnEnable ()
	{
		this.waveName = this.name;
	}
}
