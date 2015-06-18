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

// Events auch durch Polymorphie und Interfaces realisierbar


///// <summary>
///// Start, Stop Listening kann durch count  und WithFloatValue automatisiert werden
///// 
///// </summary>
//public enum DomainHealthManager
//{
//	HealthValueUpdated,
//	ReceiveHealthDamage,
//	ReceiveLifeDamage,
//	LifeValueUpdated,
//	Count
//};
//
//public enum DomainHealthManagerWithIntValue
//{
//	ReceiveLifeDamage,
//	LifeValueUpdated,
//	Count
//};
//
//public enum DomainHealthManagerWithFloatValue
//{
//	HealthValueUpdated,
//	ReceiveHealthDamage,
//	ReceiveLifeDamage,
//	LifeValueUpdated,
//	Count
//};
//
//public enum DomainPlayer
//{
//	OnDie,
//	OnReceiveDamage,
//	OnRespawn,
//	OnGameOver,
//	On
//};

public class EventNames 
{
	
	/**
	 * 
	 * Local
	 * 
	 **/
	
	public const string OnCollisionDamage = "OnCollisionDamage";
	public const string OnReceiveDamage = "OnReceiveDamage";
	public const string OnReceiveFullDamage = "OnReceiveFullDamage";
	public const string OnCollision = "OnCollision";
	public const string OnDisableCollision = "OnDisableCollision";
	public const string OnEnableCollision = "OnEnableCollision";
	public const string OnInvincibleEnabled = "OnInvincibleEnabled";
	public const string OnInvincibleDisbled = "OnInvincibleDisbled";
	public const string OnHealthValueUpdate = "OnHealthValueUpdate";
	public const string OnLifeValueUpdate = "OnLifeValueUpdate";
	public const string OnDie = "OnDie";
	public const string OnLife = "OnLife";
	public const string RespawnTrigger = "RespawnTrigger";
	public const string OnDelayedReactivateRequest = "OnDelayedReactivateRequest";
	public const string OnRespawn = "OnRespawn";
	public const string OnRespawned = "OnRespawned";
	public const string OnExplode = "OnExplode";
	public const string OnExplosionFinish = "OnExplosionFinish";		// Pooled Object: unsafe Event-Trigger-System!!!
	public const string OnGameOver = "OnGameOver";
	public const string OnDisabled = "OnDisabled";						// inconsistent Start, Stop Listening in -> OnEnable () & OnDisable ()
	
	
	/**
	 * 
	 * Global
	 * 
	 **/

	public const string ScoredValue = "ScoredValue"; 	 
	

	public const string Pause = "Pause"; 								// Stop Spawning, Stop Moving, Stop Timer 
	public const string Resume = "Resume"; 								// Resum
	
	public const string SoundEffectsVolumeChange = "SoundEffectsVolumeChange"; 	// AudioSource -> slider.value update
	public const string MusicVolumeChange = "MusicVolumeChange"; 		// AudioSource -> slider.value update
	
	
	public const string SmartBombCollected = "SmartBombCollected"; 		// ++ numberOfSmartBombs
	public const string SmartBombTriggered = "SmartBombTriggered"; 		// -> kill all enemies, bonus items
	
	public const string RemoveAllItemTriggered = "RemoveAllItemTriggered";
	public const string RemoveAllEnemysTriggered = "RemoveAllEnemysTriggered";
	
	public const string RemoveAllItems = "RemoveAllItems"; 				// <-
	
	public const string RemoveAllEnemies = "RemoveAllEnemies"; 			// -> remove all enemies
	public const string KillAllEnemies = "KillAllEnemies"; 				// -> kill all enemies
	
	public const string DisableControlls = "DisableControlls"; 			// onDie
	public const string EnableControlls = "EnableControlls"; 			// onRespawn
	
	public const string PlayerHitted = "PlayerHitted"; 					// Animation ?? Overlayeffekt
	public const string PlayerDied = "PlayerDied"; 						// Respawner, instant respawn/ respawn with delay
	public const string PlayerRespawn = "PlayerRespawn"; 				// ->
	public const string PlayerRespawned = "PlayerRespawned"; 			// -> ControllsEnable ?
	public const string PlayerGameOver = "PlayerGameOver"; 				// -> TODO doppelter event DIE/GameOver
	
	public const string ShowHightScore = "ShowHightScore"; 
	
	public const string DestroyCurrentWave = "DestroyCurrentWave"; 
	public const string InitNextWave = "InitNextWave"; 					// Load new Wave Objects in Pool
	public const string StartWave = "StartWave"; 
	
	public const string AllCrystalsCollected = "AllCrystalsCollected"; 	// -> Open Portal
	public const string OpenLevelPortal = "OpenLevelPortal"; 			// -> Open Portal
	public const string LevelPortalOpened = "LevelPortalOpened"; 		// -> Enemy: Agressiv
	public const string CloseLevelPortal = "CloseLevelPortal"; 			// -> Close Portal
	public const string PortalReached = "PortalReached";	 			//
	public const string LevelMissionComplete = "LevelMissionComplete";	// -> Destroy(Disable)CurrentWave
	public const string WaveComplete = "WaveComplete"; 
	
	public const string StartEnemySpawning = "StartEnemySpawning"; 
	public const string StopEnemySpawning = "StopEnemySpawning"; 
}

public class DomainEventManager : MonoBehaviour {

	[SerializeField]
	private List<string> registeredDomains;

	bool IsAlreadyRegistered (string domain)
	{
		for (int i=0; i < registeredDomains.Count; i++)
		{
			if (registeredDomains[i].Equals (domain))
			{
				return true;
			}
		}
		return false;
	}

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

	public static string GetDomain (Object obj, string eventName)
	{
		string prefix = "";
		int id;
		if (obj != null)
		{
			prefix = "obj " + obj.ToString ();
			id = obj.GetInstanceID ();
		}
		else
		{
			prefix = "global";
			id = 0;
		}
		return prefix + id + "" + eventName;
	}

//	public static string GetDomain (MonoBehaviour monoBehaviour, string eventName)
//	{
//		int id = -1;
//		if (monoBehaviour != null)
//		{
//			id = monoBehaviour.GetInstanceID ();
//			return id + "" + eventName;
//		}
//		return id + "" + eventName;
//	}
//
//	public static string GetDomain (GameObject gameObject, string eventName)
//	{
//		int goId = -1;
//		if (gameObject != null)
//		{
//			goId = gameObject.GetInstanceID ();
//			return goId + "" + eventName;
//		}
//		return goId + "" + eventName;
//	}

	public static void ListDomain (string domain)
	{
		if (!instance.IsAlreadyRegistered (domain))
			instance.registeredDomains.Add (domain);
	}

	public static void UnlistDomain (string domain)
	{
		instance.registeredDomains.Remove (domain);
	}

	public static void StartGlobalListening (string globalEventName, UnityAction listener)
	{
		ListDomain (globalEventName);

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
		UnlistDomain (globalEventName);

		if (eventManager == null) return;
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (globalEventName, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}

	public static void StartGlobalListening (string globalEventName, UnityAction<float> listener)
	{
		ListDomain (globalEventName);

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
		UnlistDomain (globalEventName);

		if (eventManager == null) return;
		FloatEvent thisEvent = null;
		if (instance.floatEventDictionary.TryGetValue (globalEventName, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}

	#region UnityEvent
	public static void StartListening (Object obj, string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;
		string domain = GetDomain (obj, eventName);
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
	
	public static void StopListening (Object obj, string eventName, UnityAction listener)
	{
		if (eventManager == null) return;
		UnityEvent thisEvent = null;
		string domain = GetDomain (obj, eventName);
		if (instance.eventDictionary.TryGetValue (domain, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}
	#endregion

	#region UnityEvent<float>
	public static void StartListening (Object obj, string eventName, UnityAction<float> listener)
	{
		FloatEvent thisEvent = null;
		string domain = GetDomain (obj, eventName);
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
	
	public static void StopListening (Object obj, string eventName, UnityAction<float> listener)
	{
		if (eventManager == null) return;
		FloatEvent thisEvent = null;
		string domain = GetDomain (obj, eventName);
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

	public static void TriggerGlobalEvent (string globalEventName, float value)
	{
		FloatEvent thisEvent = null;
		if (instance.floatEventDictionary.TryGetValue (globalEventName, out thisEvent))
		{
			thisEvent.Invoke (value);
		}
	}

	public static void TriggerGlobalEvent (string globalEventName, bool value)
	{
		BoolEvent thisEvent = null;
		if (instance.boolEventDictionary.TryGetValue (globalEventName, out thisEvent))
		{
			thisEvent.Invoke (value);
		}
	}
	
	public static void TriggerEvent (Object obj, string eventName)
	{
		UnityEvent thisEvent = null;
		string domain = GetDomain (obj, eventName);
		if (instance.eventDictionary.TryGetValue (domain, out thisEvent))
		{
			thisEvent.Invoke ();
		}
	}

	public static void TriggerEvent (Object obj, string eventName, float value)
	{
		FloatEvent thisEvent = null;
		string domain = GetDomain (obj, eventName);
		if (instance.floatEventDictionary.TryGetValue (domain, out thisEvent))
		{
			thisEvent.Invoke (value);
		}
	}

	public static void TriggerEvent (Object obj, string eventName, bool value)
	{
		BoolEvent thisEvent = null;
		string domain = GetDomain (obj, eventName);
		if (instance.boolEventDictionary.TryGetValue (domain, out thisEvent))
		{
			thisEvent.Invoke (value);
		}
	}
}
