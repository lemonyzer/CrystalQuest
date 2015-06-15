using UnityEngine;
using System.Collections;



public class ExplosionScript : MonoBehaviour {

	// Ohne Explosion Pool Manager
	// Script Instantiert explosionPrefab an gewünschter stelle, oder an aktuelle GO position

	// Mit Explosion PoolManager
	// Script sitzt am explodierenden Object und kommuniziert mit Explosion PoolManager

//	[SerializeField]
//	private GameObject explosionPrefab;
//
//	[SerializeField]
//	private GameObject explosionGo;
//
//	[SerializeField]
//	private string explosionTypName;

	[SerializeField]
	private ExplosionType explosionTyp;

//	[SerializeField]
//	private Explosion explosion;				// scriptable Object... kann verglichen werden
	
	public void Explode ()
	{
		GameObject go = ExplosionPoolManager.Instance.GetPooledObject(explosionTyp);
		if (go != null)
		{
			go.transform.position = this.transform.position;
			go.SetActive (true);
		}
		else
			Debug.LogError ("pooling failed");
	}

	public void Explode (Vector3 position)
	{
		
	}
}
