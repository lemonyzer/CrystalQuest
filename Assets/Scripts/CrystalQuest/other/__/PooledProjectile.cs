using UnityEngine;
using System.Collections;

public enum ProjectileType
{
	PlayerBullet,
	EnemyBullet,
	BouncingBomb,
	HailStormBullet,
	HighSpeedBullet,
	Mine,
	Laser
}

[System.Serializable]
public class PooledProjectile {

	[SerializeField]
	public int amount;

	[SerializeField]
	public GameObject prefab;

	[SerializeField]
	public ProjectileType type;
}
