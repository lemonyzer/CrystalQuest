using UnityEngine;
using System.Collections;

[System.Serializable]
public class PooledProjectile : ScriptableObject {

	[SerializeField]
	public int amount;

	[SerializeField]
	public GameObject prefab;

	[SerializeField]
	public int amountPerShip;
}
