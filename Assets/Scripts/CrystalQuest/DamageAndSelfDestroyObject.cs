using UnityEngine;
using System.Collections;

[System.Serializable]
public class DamageAndSelfDestroyObject : DamageObject {

	public override void Collided ()
	{
		base.Collided ();
	}
}
