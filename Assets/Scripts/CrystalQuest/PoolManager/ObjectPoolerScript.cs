using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolerScript : MonoBehaviour {

	public static ObjectPoolerScript current;
	public GameObject pooledObject;
	public int pooledAmount = 20;
	public bool willGrow = true;

	List<GameObject> pooledObjects;

	void Awake ()
	{
		current = this;
	}

	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject>();

		if (pooledObject != null)
		{
			for (int i=0; i < pooledAmount; i++)
			{
				AddNewToPool ();
			}
		}
	}

	GameObject AddNewToPool ()
	{
		GameObject obj = (GameObject) Instantiate (pooledObject);
		obj.SetActive (false);
		pooledObjects.Add (obj);
		return obj;
	}

	// Update is called once per frame
	public GameObject GetPooledObject () {
		for (int i=0; i < pooledAmount; i++)
		{
			if(!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}

		if (willGrow)
		{
			return AddNewToPool (); 
		}

		return null;
	}
}
