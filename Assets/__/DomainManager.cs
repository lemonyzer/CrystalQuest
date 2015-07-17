using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//// wenn mit objecten gearbeitet wird müsste das selbe object benutzt werden und nicht mit wie bei string nur die values übereinstimmen!
//// also müsste ein DomainManager exisieren der das Domain Object cached, sucht und zurückgibt. dieses object könnte dann wiederum im EventManager verwendet werden um das richtige Event zu identifizieren 
//// TODO zwischen DomainManager unnötig, Domain Klasse ebenfalls.
//// Domain ist definiet durch: GameObject.GetInstanceId () + eventName
////public class Domain {
////
////	GameObject gameObject;
////	string eventName;
////
////	public Domain (GameObject go, string eventName)
////	{
////		gameObject = go;
////		this.eventName = eventName;
////	}
////}
//
//public class DomainManager : MonoBehaviour {
//
//	private Dictionary <string, Domain> eventDictionary;
//	
//	private static DomainManager eventManager;
//	
//	public static DomainManager instance
//	{
//		get
//		{
//			if (!eventManager)
//			{
//				eventManager = FindObjectOfType (typeof (DomainManager)) as DomainManager;
//				
//				if (!eventManager)
//				{
//					Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
//				}
//				else
//				{
//					eventManager.Init (); 
//				}
//			}
//			
//			return eventManager;
//		}
//	}
//	
//	void Init ()
//	{
//		if (eventDictionary == null)
//		{
//			eventDictionary = new Dictionary<Domain, Domain>();
//		}
//	}
//
//
//
//}
