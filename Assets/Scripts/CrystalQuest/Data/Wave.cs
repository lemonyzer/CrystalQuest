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
	WaveCrystal crystal;

	[SerializeField]
	public int mineAmount;

	public void SetAmountOfCrystals (int value)
	{
		crystal.amount = value;
	}

	public int GetAmountOfCrystals ()
	{
		return crystal.amount;
	}

	public int GetAmountOfMines ()
	{
		return mineAmount;
	}

	void OnEnable ()
	{
		this.waveName = this.name;
	}
}
