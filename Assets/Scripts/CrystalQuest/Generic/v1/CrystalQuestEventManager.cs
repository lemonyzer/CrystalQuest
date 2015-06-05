using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public enum CrystalQuestEvent {
	PlayerCollisionWithEnemy,
	PlayerCollisionWithProjectile,
	PlayerTakesDamage,
	PlayerLosesLife,
	EnemyTakesDamage,
	EnemyCollisionWithEnemy,
	SmartBomb,
	NextLevel
}

public class CrystalQuestEventManager : MonoBehaviour {
	
	private Dictionary <CrystalQuestEvent, UnityEvent> eventDictionary;
	
	private static CrystalQuestEventManager eventManager;
	
	public static CrystalQuestEventManager instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType (typeof (CrystalQuestEventManager)) as CrystalQuestEventManager;
				
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
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<CrystalQuestEvent, UnityEvent>();
		}
	}
	
	public static void StartListening (CrystalQuestEvent eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.AddListener (listener);
		} 
		else
		{
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (eventName, thisEvent);
		}
	}
	
	public static void StopListening (CrystalQuestEvent eventName, UnityAction listener)
	{
		if (eventManager == null) return;
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}
	
	public static void TriggerEvent (CrystalQuestEvent eventName)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.Invoke ();
		}
	}
}
