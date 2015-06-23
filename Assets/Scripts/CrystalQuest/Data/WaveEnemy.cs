using UnityEngine;
using System.Collections;

[System.Serializable]
public class WaveEnemy {

	[SerializeField]
	string enemyName;

	[SerializeField]
	bool enabled = true;
	
	[SerializeField]
	Enemy enemy;
	
	[SerializeField]
	int amount;
	
	void OnEnable ()
	{
		if (enemy != null)
		{
			if (enemy.Prefab != null)
				this.enemyName = this.enemy.Prefab.name;
		}
	}
}
