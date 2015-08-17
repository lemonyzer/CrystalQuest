using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ExplosionType
{
	small,
	middle,
	big,
	count
};

[System.Serializable]
public class Explosion 
{
	[SerializeField]
	private GameObject prefab;

	public GameObject Prefab {
		get {return prefab;}
		private set {prefab = value;}
	}

	[SerializeField]
	private ExplosionType type;

	public ExplosionType Type {
		get {return type;}
		private set {type = value;}
	}

	[SerializeField]
	private int poolAmount;
	
	public int PoolAmount {
		get {return poolAmount;}
		private set {poolAmount = value;}
	}

	[SerializeField]
	private int poolAmountMax;
	
	public int PoolAmountMax {
		get {return poolAmountMax;}
		private set {poolAmountMax = value;}
	}

	[SerializeField]
	private bool canGrow;

	public bool CanGrow {
		get {return canGrow;}
		private set {canGrow = value;}
	}

}

public class ExplosionPoolManager : MonoBehaviour {

	// Singleton
	private static ExplosionPoolManager m_instance;

	void Awake ()
	{
		m_instance = this;
	}

	public static ExplosionPoolManager Instance {
		get {return m_instance;}
		private set {m_instance = value;}
	}

	//[SerializeField]
	//private Dictionary<ExplosionTyp, Explosion> pooledExplosions;
	private Dictionary<ExplosionType, List<GameObject>> pooledExplosions;
//	private Dictionary<Explosion, List<GameObject>> pooledExplosionsOther;
//	private Dictionary<ExplodingObject, List<GameObject>> pooledExplosionsOther2;
	
	[SerializeField]
	private List<Explosion> explosionTypes;

	private Dictionary<ExplosionType, Explosion> explosionDataBase;

	void Start ()
	{
		InitExplosionDataBase ();
		InitExplosionsPool ();
	}

	void InitExplosionDataBase ()
	{
		explosionDataBase = new Dictionary<ExplosionType, Explosion>();
		for (int i=0; i< explosionTypes.Count; i++)
		{
			explosionDataBase.Add (((ExplosionType)i), explosionTypes[i]);
		}
	}

//	void InitExplosionsPool ()
//	{
//		pooledExplosions = new Dictionary<ExplosionType, List<GameObject>>();
//		for (int i=0; i< explosionTypes.Count; i++)
//		{
//			List<GameObject> poolList = new List<GameObject>();
//			Explosion explosion = null;
//			if (explosionDataBase.TryGetValue ((ExplosionType)i, out explosion))
//				GameObject currenPrefab = explosionTypes[i].Prefab;
//			ExplosionType currentType = explosionTypes[i].Type;
//			int currentAmount = explosionTypes[i].PoolAmount;
//			
//			for (int a = 0; a < currentAmount; a++)
//			{
//				AddNewToPool (poolList, );
//			}
//			
//		}
//	}

	void InitExplosionsPool ()
	{
		pooledExplosions = new Dictionary<ExplosionType, List<GameObject>>();
		for (int i=0; i< explosionTypes.Count; i++)
		{
			List<GameObject> poolList = new List<GameObject>();
			Explosion explosion = null;
			if (explosionDataBase.TryGetValue (((ExplosionType)i), out explosion))
			{
				GameObject currenPrefab = explosionTypes[i].Prefab;
				ExplosionType currentType = explosionTypes[i].Type;
				int currentAmount = explosionTypes[i].PoolAmount;

				for (int a = 0; a < currentAmount; a++)
				{
					AddNewObjectToPool (poolList, explosion);
//					Debug.Log ("instantiated");
				}
				pooledExplosions.Add((ExplosionType)i, poolList);		// add to PoolList to Dictionary
			}
			else
			{
				Debug.LogError (i + " (" + ((ExplosionType)i) + ") not found in DataBase");
			}
		}
	}

//	public GameObject GetPooledObject (Explosion explosion) {
//		
//		List<GameObject> explosions;
//		if (pooledExplosions.TryGetValue(explosion.Type, out explosions))
//		{
//			GetInactivObject (explosion, explosion.CanGrow);
//		}
//		
//		
//	}

	public GameObject GetPooledObject (ExplosionType type) {

		List<GameObject> explosions;

		Explosion explosion = null;
		if (explosionDataBase.TryGetValue (type, out explosion))
		{
			if (pooledExplosions.TryGetValue(type, out explosions))
			{
				GameObject inactivGO = GetInactivObject (explosions, explosion);
				if (inactivGO != null)
					return inactivGO;
				else
					Debug.LogError ("all Pooled Objects are activ, increase pool Amount of " + type.ToString ());
			}
			else
			{
				Debug.LogError (type.ToString() + " is not pooled");
			}
		}
		else
		{
			Debug.LogError (type.ToString () + " is not in explosionDataBase!");
		}
		return null;
	}

	public GameObject GetInactivObject (List<GameObject> list, Explosion explosion)
	{
		//		for (int i=0; i < projectilePoolAmount; i++)
		for (int i=0; i < list.Count; i++)
		{
			if(!list[i].activeInHierarchy)
			{
				return list[i];
			}
		}
		
		if (explosion.CanGrow)
		{
			if (list.Count < explosion.PoolAmountMax)
				return AddNewObjectToPool (list, explosion); 
		}
		
		return null;
	}

	GameObject AddNewObjectToPool (List<GameObject> poolList, Explosion explosion)
	{
		GameObject newExplosion = (GameObject) Instantiate (explosion.Prefab);
		newExplosion.SetActive (false);
		poolList.Add (newExplosion);
		return newExplosion;
	}
}
