using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectilePoolManager : MonoBehaviour {

	[SerializeField]
	Dictionary<GameObject, GameObject> projectileGameObjectDictionary;				// möglichkeit 1: ermöglicht keine speziellen pool settings

	[SerializeField]
	List<PooledProjectile> pooledProjectiles;	//TODO needed, dictionary kann nicht initialisiert werden

	[SerializeField]
	Dictionary<PooledProjectile, GameObject> pooledProjectileDictionary;			// möglichkeit 2: ermöglicht individuelle pool settings für projectile

																					// möglochkeit 3: pool an enemy go - ermöglicht individuelle pool settings, jedoch keine übergeordneten (jeder enemy kann dann zb. 15 projectile abfeuern)


																					// möglichkeit 2+3: sowohl auf scene betrachtet als auch auf enemy betrachtet limitierungsmöglichkeiten

			

	public static ProjectilePoolManager current;

	void Awake ()
	{
		current = this;
	}

//	[SerializeField]
//	List<GameObject> pooledProjectiles;
//	
//	[SerializeField]
//	int projectilePoolAmount = 5;
//	
//	[SerializeField]
//	bool projectilePoolWillGrow = false;
//	
//	[SerializeField]
//	int projectilePoolAmountMax = 10;
//	
//	[SerializeField]
//	List<GameObject> projectilePool;
//	
//	//	[SerializeField]
//	//	GameObject projectilePrefab;
//	
//	void CreateProjectilePool ()
//	{
//		if (projectilePrefab != null)
//		{
//			if (projectilePool != null)
//			{
//				InitPool ();
//			}
//			else
//			{
//				projectilePool = new List<GameObject>();
//				InitPool ();
//			}
//		}
//	}
//	
//	void InitPool ()
//	{
//		for (int i=0; i < projectilePoolAmount; i++)
//		{
//			AddNewToPool ();
//		}
//	}
//	
//	GameObject AddNewToPool ()
//	{
//		GameObject obj = (GameObject) Instantiate (projectilePrefab);
//		obj.SetActive (false);
//		projectilePool.Add (obj);
//		return obj;
//	}
//	
//	// Update is called once per frame
//	public GameObject GetPooledObject () {
//		
//		//		for (int i=0; i < projectilePoolAmount; i++)
//		for (int i=0; i < projectilePool.Count; i++)
//		{
//			if(!projectilePool[i].activeInHierarchy)
//			{
//				return projectilePool[i];
//			}
//		}
//		
//		if (projectilePoolWillGrow)
//		{
//			if (projectilePool.Count < projectilePoolAmountMax)
//				return AddNewToPool (); 
//		}
//		
//		return null;
//	}
}
