using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

// wenn mit objecten gearbeitet wird müsste das selbe object benutzt werden und nicht mit wie bei string nur die values übereinstimmen!
// also müsste ein DomainManager exisieren der das Domain Object cached, sucht und zurückgibt. dieses object könnte dann wiederum im EventManager verwendet werden um das richtige Event zu identifizieren 
// TODO zwischen DomainManager unnötig, Domain Klasse ebenfalls.
// Domain ist definiet durch: GameObject.GetInstanceId () + eventName
//public class Domain {
//
//	GameObject gameObject;
//	string eventName;
//
//	public Domain (GameObject go, string eventName)
//	{
//		gameObject = go;
//		this.eventName = eventName;
//	}
//}

public class DomainEventManager : MonoBehaviour {

	private Dictionary <string, UnityEvent> eventDictionary;
	private Dictionary <string, FloatEvent> floatEventDictionary;
	private Dictionary <string, MyIntEvent> IntEventDictionary;
	private Dictionary <string, BoolEvent> boolEventDictionary;
	private Dictionary <string, ItemEvent> ItemEventDictionary;
	
	private static DomainEventManager eventManager;
	
	public static DomainEventManager instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType (typeof (DomainEventManager)) as DomainEventManager;
				
				if (!eventManager)
				{
					Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
				}
				else
				{
					eventManager.Init (); 
				}
			}
			
			return eventManager;
		}
	}
	
	void Init ()
	{
		// UnityEvent
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<string, UnityEvent>();
		}
		// FloatEvent
		if (floatEventDictionary == null)
		{
			floatEventDictionary = new Dictionary<string, FloatEvent>();
		}
		// BoolEvent
		if (boolEventDictionary == null)
		{
			boolEventDictionary = new Dictionary<string, BoolEvent>();
		}
		// IntEvent
		if (IntEventDictionary == null)
		{
			IntEventDictionary = new Dictionary<string, MyIntEvent>();
		}
	}

	public static string GetDomain (MonoBehaviour monoBehaviour, string eventName)
	{
		int id = -1;
		if (monoBehaviour != null)
		{
			id = monoBehaviour.GetInstanceID ();
			return id + "" + eventName;
		}
		return id + "" + eventName;
	}

	public static string GetDomain (GameObject gameObject, string eventName)
	{
		int goId = -1;
		if (gameObject != null)
		{
			goId = gameObject.GetInstanceID ();
			return goId + "" + eventName;
		}
		return goId + "" + eventName;
	}

	public static void StartGlobalListening (string globalEventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (globalEventName, out thisEvent))
		{
			thisEvent.AddListener (listener);
		} 
		else
		{
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (globalEventName, thisEvent);
		}
	}

	public static void StopGlobalListening (string globalEventName, UnityAction listener)
	{
		if (eventManager == null) return;
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (globalEventName, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}

	public static void StartGlobalListening (string globalEventName, UnityAction<float> listener)
	{
		FloatEvent thisEvent = null;
		if (instance.floatEventDictionary.TryGetValue (globalEventName, out thisEvent))
		{
			thisEvent.AddListener (listener);
		} 
		else
		{
			thisEvent = new FloatEvent ();
			thisEvent.AddListener (listener);
			instance.floatEventDictionary.Add (globalEventName, thisEvent);
		}
	}
	
	public static void StopGlobalListening (string globalEventName, UnityAction<float> listener)
	{
		if (eventManager == null) return;
		FloatEvent thisEvent = null;
		if (instance.floatEventDictionary.TryGetValue (globalEventName, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}

	#region UnityEvent
	public static void StartListening (GameObject go, string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;
		string domain = GetDomain (go, eventName);
		if (instance.eventDictionary.TryGetValue (domain, out thisEvent))
		{
			thisEvent.AddListener (listener);
		} 
		else
		{
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (domain, thisEvent);
		}
	}
	
	public static void StopListening (GameObject go, string eventName, UnityAction listener)
	{
		if (eventManager == null) return;
		UnityEvent thisEvent = null;
		string domain = GetDomain (go, eventName);
		if (instance.eventDictionary.TryGetValue (domain, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}
	#endregion

	#region UnityEvent<float>
	public static void StartListening (GameObject go, string eventName, UnityAction<float> listener)
	{
		FloatEvent thisEvent = null;
		string domain = GetDomain (go, eventName);
		if (instance.floatEventDictionary.TryGetValue (domain, out thisEvent))
		{
			thisEvent.AddListener (listener);
		} 
		else
		{
			thisEvent = new FloatEvent ();
			thisEvent.AddListener (listener);
			instance.floatEventDictionary.Add (domain, thisEvent);
		}
	}
	
	public static void StopListening (GameObject go, string eventName, UnityAction<float> listener)
	{
		if (eventManager == null) return;
		FloatEvent thisEvent = null;
		string domain = GetDomain (go, eventName);
		if (instance.floatEventDictionary.TryGetValue (domain, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}
	#endregion

	public static void TriggerGlobalEvent (string globalEventName)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (globalEventName, out thisEvent))
		{
			thisEvent.Invoke ();
		}
	}
	
	public static void TriggerEvent (GameObject go, string eventName)
	{
		UnityEvent thisEvent = null;
		string domain = GetDomain (go, eventName);
		if (instance.eventDictionary.TryGetValue (domain, out thisEvent))
		{
			thisEvent.Invoke ();
		}
	}
}
