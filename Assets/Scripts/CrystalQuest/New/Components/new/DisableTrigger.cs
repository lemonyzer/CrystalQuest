using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public abstract class DisableTrigger : MonoBehaviour {
	
	[SerializeField]
	protected UnityEvent myEvent;					// Event for my Event-Domain

	public void StartListening (UnityAction newListener)
	{
		myEvent.AddListener (newListener);
	}
	
	public void StopListening (UnityAction newListener)
	{
		myEvent.RemoveListener (newListener);
	}

	protected void onDisable ()
	{
		myEvent.Invoke ();
	}

}
