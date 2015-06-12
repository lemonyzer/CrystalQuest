using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventTrigger : MonoBehaviour {

	public static UnityEvent myStaticEvent;	// For GameManager 1 - n partnership

	[SerializeField]
	protected UnityEvent myEvent;			// Event for my Domain

	[SerializeField]
	protected UnityAction myListener;		// listener for the TriggerDomain

	[SerializeField]
	protected List<EventTrigger> triggerScripts;

	void Awake ()
	{
		myListener = new UnityAction (Action);
	}

	public virtual void Action ()
	{
		Debug.LogError (this.ToString () + " Action!!!");
	}

	void StartTriggerListening ()
	{
		Debug.LogError (this.ToString () + " StartTriggerListening");
		if (triggerScripts != null)
		{
			for (int i=0; i < triggerScripts.Count; i++)
			{
				triggerScripts[i].StartListening (myListener);
			}
		}
	}

	void StopTriggerListening ()
	{
		Debug.LogError (this.ToString () + " StopTriggerListening");
		if (triggerScripts != null)
		{
			for (int i=0; i < triggerScripts.Count; i++)
			{
				triggerScripts[i].StopListening (myListener);
			}
		}
	}

	void OnEnable () {
		StartTriggerListening ();
	}

	void OnDisable ()
	{
		StopTriggerListening ();
	}

	public void StartListening (UnityAction listener)
	{
		myEvent.AddListener (listener);
	}

	public void StopListening (UnityAction listener)
	{
		myEvent.RemoveListener (listener);
	}

	public void TriggerEvent ()
	{
		Debug.LogError (this.ToString () + " TriggerEvent");
		if (myEvent != null)
			myEvent.Invoke ();
		else
			Debug.LogError (this.ToString() + " myEvent == NULL");
	}


}
