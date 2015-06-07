using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CollectableObject : CollisionObject {

	[SerializeField]
	protected UnityEvent collected;
	
//	public override void Triggering2DHandling (Collider2D collider2D)
//	{
//		base.Triggering2DHandling (collider2D);
//	}

	public override void Die ()
	{
		base.Die ();
		if (collected != null)
			collected.Invoke ();
	}
}
