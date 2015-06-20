using UnityEngine;
using System.Collections;



public class ExplosionScript : MonoBehaviour {

	[SerializeField]
	MyEvent explosionTriggered;

	void NotExecuted ()
	{
		explosionTriggered = null;
	}

	void NotifyExplosionTriggered ()
	{
		if (explosionTriggered != null)
		{
			explosionTriggered.Invoke ();
		}
		else
		{
			#if UNITY_EDITOR 
			Debug.LogError ("no \"explosionTriggered\" observer");
			#endif
		}
	}

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

//	void OnEnable ()
//	{
//		DomainEventManager.StartListening (this.gameObject, EventNames.OnDie, Explode);
//	}
//
//	void OnDisable ()
//	{
//		DomainEventManager.StopListening (this.gameObject, EventNames.OnDie, Explode);
//	}

	void OnExplode ()
	{
		NotifyExplosionTriggered ();
	}

	public void Explode ()
	{
		OnExplode ();
		GameObject go = ExplosionPoolManager.Instance.GetPooledObject(explosionTyp);
		if (go != null)
		{
			go.transform.position = this.transform.position;
			go.SetActive (true);
			ExplosionAnimation explosionAnimation = go.GetComponent<ExplosionAnimation>();		// pooled object can be used for different objects, if event is listened so the domain needs to be the same
			if (explosionAnimation != null)
				explosionAnimation.EventDomain = this.gameObject;
			else
				Debug.LogError (go.name + " has no explosionAnimation Script attached, cant set EventDomain to recognize explosion animation end");
		}
		else
			Debug.LogError ("pooling failed");
	}

	public void ExplodeAt (Vector3 position)
	{
		
	}

//	public void NotifyExplodeListener ()
//	{
//		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnExplode);
//		
//	}
}
