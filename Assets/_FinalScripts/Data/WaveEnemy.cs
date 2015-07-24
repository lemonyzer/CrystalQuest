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

	[SerializeField]
	int frequenzy = 1;
	
	void OnEnable ()
	{
		if (enemy != null)
		{
			if (enemy.Prefab != null)
				this.enemyName = this.enemy.Prefab.name;
		}
	}

	public Enemy Enemy {
		get {return enemy;}
	}

	public int Amount {
		get {return amount;}
	}

	public int Frequenzy {
		get {return frequenzy;}
	}
}
