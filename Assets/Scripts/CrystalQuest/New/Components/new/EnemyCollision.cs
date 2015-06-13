using UnityEngine;
using System.Collections;

public class EnemyCollision : CollisionTrigger {
	
	protected override void OnTriggerEnter2D (Collider2D otherCollider2d)
	{
		if (otherCollider2d.gameObject.layer == LayerMask.NameToLayer ("Enemy"))
		{
			if (otherCollider2d.GetComponent <CollisionTrigger>			// TODO TODO TODO TODO TODO BRAINWORK
			Trigger ();
		}
	}
}
