using UnityEngine;
using System.Collections;

public class ProjectileObjectScript : MovingObject {

	[SerializeField]
	protected CrystalQuestObjectScript ownerObjectScript;

	public CrystalQuestObjectScript OwnerObjectScript {
		get { return ownerObjectScript; }
		set { ownerObjectScript = value; }
	}

	[SerializeField]
	protected float selfDestroyTime = 5f;

	protected override void Start ()
	{
		base.Start ();
//		Destroy (this, selfDestroyTime);
		Invoke ("AutoDestroy", selfDestroyTime);
	}

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
		rb2D.AddForce (Vector2.right * releaseForce);	// force geht nur wenn input nicht abgefragt wird bzw. movement disabled ist 
	}
	
	public void ReleasedWithForce (Vector3 direction, float force)
	{
		//		rb2D.AddForce (Vector2.right * force);
	}

	public void ReleasedWithVelocity (Vector3 direction, float force)
	{
		rb2D.velocity = direction * force;
	}

	

}
