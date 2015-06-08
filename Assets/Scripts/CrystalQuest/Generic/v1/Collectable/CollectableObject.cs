using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CollectableObject : MovingObject {

	// CollectableObject
	// -> kein Damage
	// A: 	manuell CollisionSendDamge bei allen Objecten auf 0 setzen
	// B: 	Awake () CollisionSendDamge = 0 setzen
	// C: 	Collision behaviour ändern, problem : aktuelles Bahviour liest CollisionSendDamge von anderem Collisions Script... () 
	//	  	Collisions behaviour müsste dann in der Form von Collision.SendDamge (myCollisionSendDamageValue)
	//		TODO Nachteil: in Unity Inspector nicht direkt ersichtlich das Collision keine Damage übertragt!!!!!!! TODO
			//TODO CollectableObject : HarmlessCollisionObject (no SendDamage ()) TODO 
	// D:	boolen SendDamgeEnabled -> nachteil muss für alle eingestellt werden 

	protected override void Awake ()
	{
		base.Awake ();
//		CollisionSendDamageValue = 0f;		// kein Damage -> Methode B
	}

	/// <summary>
	/// kein Damage -> Methode C
	/// Sends the collision damage. 
	/// </summary>
	/// <param name="otherObjectScript">Other object script.</param>
	public override void SendCollisionDamage (CollisionObject otherObjectScript)
	{
		// empty, no Damage sent
		//base.SendCollisionDamage (otherObjectScript);
		
	}

	/// <summary>
	/// wird bei Collision zertört, egal welchen SendCollisionWert der sender hat 
	/// und egal wie hoch Collectable Health
	/// Applies the collision damage.
	/// </summary>
	/// <param name="otherObjectScript">Other object script.</param>
	public override void ApplyCollisionDamage (CollisionObject otherObjectScript)
	{
		//base.ApplyCollisionDamage (otherObjectScript);
		this.ReceiveDamage (Health);
	}

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
