using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MyEvent : UnityEvent
{
	
}

[System.Serializable]
public class ItemEvent : UnityEvent<ItemScript>
{
	
}

[System.Serializable]
public class BoolEvent : UnityEvent<bool>
{
	
}

[System.Serializable]
public class IntEvent : UnityEvent<int>
{
	
}

[System.Serializable]
public class FloatEvent : UnityEvent<float>
{
	
}

//[System.Serializable]
//public class MyFloatEvent : UnityEvent<float>
//{
//	
//}

[System.Serializable]
public class WaveEvent : UnityEvent<Wave>
{
	
}

[System.Serializable]
public class MyVector3Event : UnityEvent<Vector3>
{
	
}

[System.Serializable]
public class MyDataEvent : UnityEvent<MyDataClass>
{
	
}

[System.Serializable]
public class MyDataClass
{

}

public class EventTrigger : MonoBehaviour {

	// Parameter / arguments in UnityEvent
	// http://forum.unity3d.com/threads/how-to-addlistener-featuring-an-argument.266934/
	// http://gamedev.stackexchange.com/questions/83027/unity-new-ui-dynamically-change-the-functions-called-by-gui-elements

	public static UnityEvent myStaticEvent;			// For GameManager 1 - n partnership

	[SerializeField]
	protected UnityEvent myEvent;					// Event for my Event-Domain

	[SerializeField]
	protected UnityAction myTriggerListener;		// listener for the Triggers (Trigger-Domain)

	[SerializeField]
	protected List<EventTrigger> triggerScripts;

	void Awake ()
	{
		myTriggerListener = new UnityAction (Action);
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
			for (int i = 0; i < triggerScripts.Count; i++)
			{
				triggerScripts[i].StartListening (myTriggerListener);
			}
		}
	}

	void StopTriggerListening ()
	{
		Debug.LogError (this.ToString () + " StopTriggerListening");
		if (triggerScripts != null)
		{
			for (int i = 0; i < triggerScripts.Count; i++)
			{
				triggerScripts[i].StopListening (myTriggerListener);
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
