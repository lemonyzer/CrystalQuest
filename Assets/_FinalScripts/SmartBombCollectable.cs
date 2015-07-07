using UnityEngine;
using System.Collections;

public class SmartBombCollectable : MonoBehaviour {

	public void TriggerCollected ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.SmartBombCollected);
	}
}
