using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WaveCrystal 
{
	[SerializeField]
	public GameObject prefab;

	[SerializeField]
	public int amount = 0;

	[SerializeField]
	public bool enabled = false;
}

[System.Serializable]
public class Wave : ScriptableObject {

	[SerializeField]
	public string waveName;

	[SerializeField]
	public List<WaveEnemy> enemies;

	[SerializeField]
	public WaveCrystal crystal;

	void OnEnable ()
	{
		this.waveName = this.name;
	}
}
