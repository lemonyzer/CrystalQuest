using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LayerCollision
{
	[SerializeField]
	string layerName;

	[SerializeField]
	LayerMask layerMask;

	[SerializeField]
	bool ignoreCollision;

	[SerializeField]
	MyEvent collisionEvent = null;

	[SerializeField]
	FloatEvent collisionDamageEvent = null;

	public void TriggerCollisionEvent ()
	{
		collisionEvent.Invoke ();
	}

	public void TriggerCollisionEvent (float damageValue)
	{
		collisionDamageEvent.Invoke (damageValue);
	}

	public LayerMask LayerMask {
		get {return layerMask;}
		set {layerMask = value;}
	}

	public string LayerName {
		get {return layerName;}
		set {layerName = value;}
	}

	public bool IgnoreCollision {
		get {return ignoreCollision;}
		set {ignoreCollision = value;}
	}
}

// ScriptableObject List<LayerCollision> ... TODO

public class CollisionDamageTrigger : MonoBehaviour {

//	[SerializeField]
//	private HealthManager healthManager;
//
//	// TODO REFERENCE BUG !!!??!!! CALL BY REF ?!!
////	void InitHealthManager (HealthManager myHealthManager)
////	{
////		if (myHealthManager != null)
////			return;
////		else
////		{
////			Debug.Log ("trying auto. setting Healthmanager...");
////			myHealthManager = this.GetComponent<HealthManager>();
////		}
////
////		if (myHealthManager == null)
////			Debug.LogError (this.ToString() + " needs a HealthManager to send Damage");
////	}
//
//	HealthManager InitHealthManager (HealthManager currentHealthManager)
//	{
//		if (currentHealthManager != null)
//			return currentHealthManager;
//		else
//		{
//			Debug.Log ("trying auto. setting Healthmanager...");
//			HealthManager myHealthManager = this.GetComponent<HealthManager>();
//			if (myHealthManager == null)
//				Debug.LogError ("Auto. setting Failed " + this.ToString() + " needs a HealthManager to send Damage!");
//			return myHealthManager;
//		}
//	}

	[SerializeField]
	private bool sendDamageEnabled = true;
	
	[SerializeField]
	private float sendDamageValue = 100f;
	
//	[SerializeField]
	private float sendDamageMulti = 1f;
	
	[SerializeField]
	private bool receiveDamageEnabled = true;
	
//	[SerializeField]
	private float receiveDamageMulti = 1f;

//	[SerializeField]
//	LayerMask layerMask;

	[SerializeField]
	private FloatEvent myCollisionDamageEvent;					// Event for my Event-Domain
	
	[SerializeField]
	private UnityEvent myCollisionEvent;								// Event for my Event-Domain

	[SerializeField]
	private bool IgnoreCollisionIfLayerNotInList = false;

	[SerializeField]
	private List<LayerCollision> layerCollision;// = new List<LayerCollision>();	//TODO -.-  BUG! not visible in Inspector!

	private bool CheckIfCollisionShouldBeIgnoredWithLayerMaskCompare (Collider2D otherCollider2D)
	{
		int otherLayerMask = 1 << otherCollider2D.gameObject.layer;
		for (int i=0; i<layerCollision.Count; i++)
		{
			if ( (layerCollision[i].LayerMask & otherLayerMask) != 0)
			{
				return layerCollision[i].IgnoreCollision;					// TODO Problem LayerMask könnte mehrere angeklickt werden, jedoch wird nur das erste gefundene verwendet! 
			}
		}
		return IgnoreCollisionIfLayerNotInList;
	}

	private bool CheckIfCollisionShouldBeIgnored (Collider2D otherCollider2D)
	{
		for (int i=0; i<layerCollision.Count; i++)
		{
			if (layerCollision[i].LayerName == LayerMask.LayerToName (otherCollider2D.gameObject.layer))
			{
				return layerCollision[i].IgnoreCollision;
			}
		}
		return IgnoreCollisionIfLayerNotInList;
	}

	private LayerCollision GetLayerCollision (Collider2D otherCollider2D)
	{
		for (int i=0; i<layerCollision.Count; i++)
		{
			if (layerCollision[i].LayerName == LayerMask.LayerToName (otherCollider2D.gameObject.layer))
			{
				return layerCollision[i];
			}
		}
		return null;
	}

	void OnEnable ()
	{
		EnableCollisionDetection ();
		DomainEventManager.StartGlobalListening (EventNames.PlayerDied, OnPlayerDied);
		DomainEventManager.StartGlobalListening (EventNames.WaveStart, EnableCollisionDetection);
	}

	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.PlayerDied, OnPlayerDied);
		DomainEventManager.StopGlobalListening (EventNames.WaveStart, EnableCollisionDetection);
	}

	[SerializeField]
	bool collisionDetectionEnabled = true;

	void StopCollisionDetection ()
	{
		collisionDetectionEnabled = false;
	}

	void OnPlayerDied ()
	{
		StopCollisionDetection ();
	}

	void EnableCollisionDetection ()
	{
		collisionDetectionEnabled = true;
	}

	private void OnTriggerEnter2D (Collider2D otherCollider2d)
	{
		if (!collisionDetectionEnabled)
		{
			Debug.LogWarning (this.ToString () + " Collision Detection disabled!");
		}

//		if (CheckIfCollisionShouldBeIgnored (otherCollider2d))
		if (CheckIfCollisionShouldBeIgnoredWithLayerMaskCompare (otherCollider2d))
		{
			// generic, alle Collisionen werden gleich behandelt/verarbeitet
			#if UNITY_EDITOR
//								Debug.LogError (this.ToString () + " ignore Collision with " + otherCollider2d.gameObject.name + " in Layer "  + LayerMask.LayerToName(otherCollider2d.gameObject.layer));
			#endif 
		}
		else
		{
			// Collision is not Ignored -> Start Triggering
			CollisionDamageTrigger otherCollisionTrigger = otherCollider2d.GetComponent <CollisionDamageTrigger>(); 
			if (otherCollisionTrigger != null)
			{
				float otherSendDamageValue = otherCollisionTrigger.GetDamageValue ();
				float myDamageValue = sendDamageValue * sendDamageMulti;
				float receiveDamageValue = receiveDamageMulti * otherSendDamageValue;
				#if UNITY_EDITOR  
//								Debug.Log (this.ToString() + " Collision is not Ignored -> Start Triggering ");
//								Debug.Log (this.ToString() + " otherSendDamageValue " + otherSendDamageValue);
//								Debug.Log (this.ToString() + " myDamageValue " + myDamageValue);
//								Debug.Log (this.ToString() + " receiveDamageValue " + receiveDamageValue);
				#endif


				// de-coupling

				LayerCollision currentLayerCollision = GetLayerCollision (otherCollider2d);
				if (currentLayerCollision != null)
				{
					// Kollisionpartner-Ebene ist in layerCollision aufgelistet
					if (!currentLayerCollision.IgnoreCollision)
					{
						// Trigger Layer specific Collision Event
						currentLayerCollision.TriggerCollisionEvent ();

						if (receiveDamageEnabled)
							// Trigger Layer spzefic Collision Damage Event
							currentLayerCollision.TriggerCollisionEvent (receiveDamageValue);
					}
				}
				else
				{
					// Kollisionpartner-Ebene ist NICHT in layerCollision aufgelistet
				}

				// Trigger global Collision Event
				NotifyCollision ();

				// Trigger global CollisionDamage Event
				if (receiveDamageEnabled)
					NotifyCollisionDamage (receiveDamageValue);
	//				else
	//					NotifyCollisionDamage (0f);

	//				// coupling
	//				healthManager.ReceiveHealthDamage (otherSendDamageValue);
			}
			else
			{
				#if UNITY_EDITOR
//								Debug.LogError (this.ToString () + " can't find  CollisionTriggerScript @ " + otherCollider2d.gameObject.name + " in Layer " + LayerMask.LayerToName(otherCollider2d.gameObject.layer));
				#endif
			}

		}

//			// additional collisionbehaviour
//			if (otherCollider2d.gameObject.layer == LayerMask.NameToLayer ("Enemy"))
//			{
//				// to this...
//				
//			}
//			else if (otherCollider2d.gameObject.layer == LayerMask.NameToLayer ("Player"))
//			{
//				// do that..
//			}
//
//			// problem htis is a generic model, can
//			// additional collisionbehaviour with seperation if this object is player or enemy or... (better with Polymorphism 
//			// CollisionDamageTrigger <|- EnemyCollisionDamageTrigger
//			// CollisionDamageTrigger <|- PlayerCollisionDmageTrigger
//			if (this.gameObject.layer == "Player")
//			{
//				if (otherCollider2d.gameObject.layer == LayerMask.NameToLayer ("Enemy"))
//				{
//					// to this...
//
//			    }
//				else if (otherCollider2d.gameObject.layer == LayerMask.NameToLayer ("Player"))
//				{
//					// do that..
//				}
//			}
//			else if ("Enemy")
//			{
//				if (otherCollider2d.gameObject.layer == LayerMask.NameToLayer ("Enemy"))
//				{
//					// to this...
//					
//				}
//				else if (otherCollider2d.gameObject.layer == LayerMask.NameToLayer ("Player"))
//				{
//					// do that..
//				}
//			}
	}

	public float GetDamageValue ()
	{
		if (sendDamageEnabled)
			return sendDamageValue * sendDamageMulti;
		else
			return 0f;
	}
	


	void CheckInit ()
	{
		for (int i=0; i < layerCollision.Count; i++)
		{
			if ( (layerCollision[i].LayerMask | 0) == 0 )
			{
				Debug.LogError (this.ToString() + " layerCollision " + i + " not initialized!!!");
			}
		}
	}

	void Awake ()
	{
		CheckInit();
//		healthManager = InitHealthManager (healthManager);
//		myCollisionDamageEvent = new MyFloatEvent();
	}



//	public void StartListening (UnityAction newListener)
//	{
//		myCollisionEvent.AddListener (newListener);
//	}
//
//	public void StopListening (UnityAction newListener)
//	{
//		myCollisionEvent.RemoveListener (newListener);
//	}

//	[SerializeField]
//	private UnityAction<float> collisionDamage;

//	void OnEnable ()
//	{
////		DomainEventManager.StartListening (this.gameObject, "Collision", collisionDamage);
//	}

//	void OnDisable ()
//	{
////		DomainEventManager.StopListening (this.gameObject, "Collision", collisionDamage);
//	}

	void OnDestroy ()
	{
//		DomainEventManager.StopListening (this.gameObject, "Collision", collisionDamage);
	}
	
	public void StartListening (UnityAction<float> newFloatListener)
	{
		myCollisionDamageEvent.AddListener (newFloatListener);
	}
	
	public void StopListening (UnityAction<float> newFloatListener)
	{
		myCollisionDamageEvent.RemoveListener (newFloatListener);
	}

	public void NotifyCollision ()
	{
		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnCollision);

		if (myCollisionEvent != null)
			myCollisionEvent.Invoke ();
		else
			Debug.LogError (this.ToString () + " myCollisionEvent == NULL");
	}

	public void NotifyCollisionDamage (float value)
	{
		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnCollisionDamage, value);
	
		if (myCollisionDamageEvent != null)
			myCollisionDamageEvent.Invoke (value);
		else
			Debug.LogError (this.ToString () + " myCollisionDamageEvent == NULL");
	}

	// use either Send Damage or Reveice Damage
	// dont use both!
	public void ReceiveCollisionDamage (float damageValue)
	{
		if (receiveDamageEnabled)
			NotifyCollisionDamage (damageValue);
	}

	// use either Send Damage or Reveice Damage
	// dont use both!
	public void SendDamage (CollisionDamageTrigger otherCollisionTrigger, float damageValue)
	{
		if (sendDamageEnabled)
			otherCollisionTrigger.ReceiveCollisionDamage (damageValue);
	}
}
