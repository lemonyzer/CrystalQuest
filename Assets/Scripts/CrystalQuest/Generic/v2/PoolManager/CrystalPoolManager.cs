using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrystalPoolManager : MonoBehaviour {

	static CrystalPoolManager m_instance;

	public static CrystalPoolManager Instance {
		get {return m_instance;}
	}

	[SerializeField]
	GameObject prefab;

	[SerializeField]
	List<GameObject> crystalls;

	[SerializeField]
	bool willGrow = true;

	void Awake ()
	{
		if (m_instance != null)
			Debug.LogWarning (this.ToString () + " m_instance already exists");
		else
			m_instance = this;

		if (prefab == null)
			Debug.LogError (this.ToString () + " prefab not set");

		if (crystalls == null)
			crystalls = new List<GameObject> ();
	}

	public GameObject GetObject ()
	{
		return GetInactiveFromPool ();
	}

	GameObject GetInactiveFromPool ()
	{
		for (int i=0; i < crystalls.Count; i++)
		{
			if (!crystalls[i].activeInHierarchy)
			{
				return crystalls[i];
			}
		}

		if (willGrow)
		{
			GameObject newOne = Instantiate (prefab);
			crystalls.Add (newOne);
			return newOne;
		}
		else {
			Debug.LogError ("Pool is completly active && willGrow == false");
			return null;
		}
	}

	public void Grow (int amount)
	{
		while (crystalls.Count < amount)
		{
			GameObject newOne = Instantiate (prefab);
			crystalls.Add (newOne);
			newOne.SetActive (false);
		}
	}
}
