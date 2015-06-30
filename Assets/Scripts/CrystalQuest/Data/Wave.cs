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
#if UNITY_EDITOR 
		ValidateEnemies ();
#endif 
	}

	public void ValidateEnemies ()
	{
		for (int i=0; i< enemies.Count; i++)
		{
			if (enemies[i].Amount <= 0)
				Debug.LogError (this.ToString () + " Enemy at Position " + i + " has no valid Amount!");

			if (enemies[i].Enemy == null)
				Debug.LogError (this.ToString () + " Enemy at Position " + i + " has no valid Enemy set!");
			else
			{
				if (enemies[i].Enemy.Prefab == null)
					Debug.LogError (this.ToString () + " Enemy at Position " + i + " has no valid Enemy Prefab set!");
			}

		}
	}

	public WaveEnemy GetRandomWaveEnemy ()
	{
		if (enemies == null)
			return null;

		int random = Random.Range (0, enemies.Count);
		return enemies[random];
	}
}
