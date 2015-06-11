using UnityEngine;
using System.Collections;

public class EnemyObjectScript : MovingObject {


	public override void Die ()
	{
		base.Die ();
		TriggerScore ();
	}

	public override void RestartLevel ()
	{
		base.RestartLevel ();
		this.gameObject.SetActive (false);
#if UNITY_EDITOR
//		Debug.Log (this.ToString() + " RestartLevel ()");
#endif
	}

}
