using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisableOnHealth : DisableTrigger {

//	protected override void onDisable ()
//	{
//		throw new System.NotImplementedException ();
//	}

	[SerializeField]
	List<HealthTrigger> healthTrigger;
}
