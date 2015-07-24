using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[System.Serializable]
//public class WaveCrystal 
//{
//	[SerializeField]
//	public GameObject prefab;
//
//	[SerializeField]
//	public int amount = 0;
//
//	[SerializeField]
//	public bool enabled = false;
//}

[System.Serializable]
public class Wave : ScriptableObject {

	[SerializeField]
	public string waveName;

//	[SerializeField]
//	WaveCrystal crystal;

	[SerializeField]
	public int crystalAmount;
	
	[SerializeField]
	public int mineAmount;
	
	[SerializeField]
	public int smartBombs;
	
	[SerializeField]
	public int bonusCrystals;
	
	[SerializeField]
	public int bonusPoints;

	[SerializeField]
	public float bonusTimeLimit;

	[SerializeField]
	public float timeBonus = 1000f;

	[SerializeField]
	public float minSpawnTime = 5f;
	[SerializeField]
	public float randSpawnTimeDelay = 2f;

	[SerializeField]
	public List<WaveEnemy> enemies;


	public void SetAmountOfCrystals (int value)
	{
		crystalAmount = value;
	}

	public int GetAmountOfCrystals ()
	{
		return crystalAmount;
	}

	public int GetAmountOfMines ()
	{
		return mineAmount;
	}

	public int GetAmountOfSmartBombs ()
	{
		return smartBombs;
	}

	void OnEnable ()
	{
		this.waveName = this.name;
#if UNITY_EDITOR 
		ValidateEnemies ();
//		crystalAmount = crystal.amount;
#endif 
	}

	public void ValidateEnemies ()
	{
		if (enemies != null)
		{
			for (int i=0; i< enemies.Count; i++)
			{
				if (enemies[i].Amount <= 0)
					Debug.LogError (this.ToString () + " Enemy at Position " + i + " has no valid Amount!", this);

				if (enemies[i].Enemy == null)
					Debug.LogError (this.ToString () + " Enemy at Position " + i + " has no valid Enemy set!", this);
				else
				{
					if (enemies[i].Enemy.Prefab == null)
						Debug.LogError (this.ToString () + " Enemy at Position " + i + " has no valid Enemy Prefab set!", this);
				}

			}
		}
		else
		{
			Debug.LogError (this.ToString () + " EnemyList not initialized!", this);
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
