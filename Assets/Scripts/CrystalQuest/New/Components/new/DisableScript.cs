using UnityEngine;
using System.Collections;

public class DisableScript : MonoBehaviour {



//	void OnEnable ()
//	{
//		DomainEventManager.StartListening (this.gameObject, EventNames.OnDie, Disable);
//	}
//
//	void OnDisable ()
//	{
//		DomainEventManager.StopListening (this.gameObject, EventNames.OnDie, Disable);
//	}

	public void Disable ()
	{
		this.gameObject.SetActive (false);
	}
}
