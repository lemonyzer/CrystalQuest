using UnityEngine;
using System.Collections;

public class EnemyObjectScript : MovingObject {


	public override void Die ()
	{
		base.Die ();
		TriggerScore ();
	}

}
