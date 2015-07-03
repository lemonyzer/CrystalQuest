using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	[SerializeField]
	Rigidbody2D rb2d;

//	[SerializeField]
//	protected CrystalQuestObjectScript ownerObjectScript;
//	
//	public CrystalQuestObjectScript OwnerObjectScript {
//		get { return ownerObjectScript; }
//		set { ownerObjectScript = value; }
//	}

	[SerializeField]
	GameObject ownerObject;
	
	public GameObject OwnerObject {
		get { return ownerObject; }
		set { ownerObject = value; }
	}

	[SerializeField]
	protected bool selfDestroyEnabled = true;

	[SerializeField]
	protected float selfDestroyTime = 5f;

	void Awake ()
	{
		rb2d = this.GetComponent<Rigidbody2D> ();
	}
	
//	void OnEnable ()
//	{
//		Invoke ("AutoDestroy", selfDestroyTime);
//	}
	
	void OnDisable ()
	{
		CancelInvoke ();
	}
	
	void AutoDestroy ()
	{
		this.gameObject.SetActive (false);
	}
	
	[SerializeField]
	protected float releaseForce;
	
	public void ReleasedWithForce (Vector3 direction)
	{
		rb2d.AddForce (Vector2.right * releaseForce);	// force geht nur wenn input nicht abgefragt wird bzw. movement disabled ist 
		Release ();
	}
	
	public void ReleasedWithForce (Vector3 direction, float force)
	{
		//		rb2D.AddForce (Vector2.right * force);
		Release ();
	}

	public void SetSelfDestroyTime (float duration)
	{
		selfDestroyTime = duration;
	}
	
	public void ReleasedWithVelocity (Vector3 direction, float force)
	{
		if (rb2d != null)
			rb2d.velocity = direction * force;
		else
		{
			#if UNITY_EDITOR 
				Debug.LogWarning (this.ToString() + " has no rigidbody2d, releasing without movement!");
			#endif 
		}
		Release ();
	}

	public void ReleasedWithoutMovement ()
	{
		Release ();
	}

	void Release ()
	{
		if (selfDestroyEnabled)
			Invoke ("AutoDestroy", selfDestroyTime);
	}
	
	public void RestartLevel ()
	{
		DisableCachedGameObject();		// dont destroy, this object is cached. just disable it
		// manager could disable it !!! consistent?
	}
	
	public void DisableCachedGameObject ()
	{
		this.gameObject.SetActive (false);		// dont destroy, this object is cached. just disable it
		// manager could disable it !!! consistent?
	}
	
}
