using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelExitZone : CollisionObject {
	
	[SerializeField]
	protected bool sendCollisionDamageEnabled = false;

	protected override void Awake ()
	{
		base.Awake ();
		useCollisionEvents = true;	// Method A:
									// Require: GO in Layer die nur mit Player GO reagiert.
									// SendDamage = false
	}

	// Method B
	// override OnTriggerEnter2D
	// override OnCollisionEnter2D

	// Method C
	// override CollisionHandling2D
	// override TriggerHandling2D

	public override void ApplyCollisionDamage (CollisionObject otherObjectScript)
	{
		//base.ApplyCollisionDamage (otherObjectScript);
		// no Damage
	}

	public override void SendCollisionDamage (CollisionObject otherObjectScript)
	{
		if (sendCollisionDamageEnabled)
			base.SendCollisionDamage (otherObjectScript);
	}


}
