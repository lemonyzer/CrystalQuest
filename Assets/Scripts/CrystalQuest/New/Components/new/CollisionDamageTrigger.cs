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
	bool ignoreCollision;

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

	[SerializeField]
	private HealthManager healthManager;

//	void InitHealthManager (HealthManager myHealthManager)
//	{
//		if (myHealthManager != null)
//			return;
//		else
//		{
//			Debug.Log ("trying auto. setting Healthmanager...");
//			myHealthManager = this.GetComponent<HealthManager>();
//		}
//
//		if (myHealthManager == null)
//			Debug.LogError (this.ToString() + " needs a HealthManager to send Damage");
//	}

	HealthManager InitHealthManager (HealthManager currentHealthManager)
	{
		if (currentHealthManager != null)
			return currentHealthManager;
		else
		{
			Debug.Log ("trying auto. setting Healthmanager...");
			HealthManager myHealthManager = this.GetComponent<HealthManager>();
			if (myHealthManager == null)
				Debug.LogError ("Auto. setting Failed " + this.ToString() + " needs a HealthManager to send Damage!");
			return myHealthManager;
		}
	}

	[SerializeField]
	private bool sendDamageEnabled = true;
	
	[SerializeField]
	private float sendDamageValue = 100f;
	
	[SerializeField]
	private float sendDamageMulti = 1f;
	
	[SerializeField]
	private bool receiveDamageEnabled = true;
	
	[SerializeField]
	private float receiveDamageMulti = 1f;

	[SerializeField]
	private bool IgnoreCollisionIfLayerNotInList = false;

	[SerializeField]
	private List<LayerCollision> layerCollision;// = new List<LayerCollision>();	//TODO -.-  BUG! not visible in Inspector!

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

	private void OnTriggerEnter2D (Collider2D otherCollider2d)
	{
		CollisionDamageTrigger otherCollisionTrigger = otherCollider2d.GetComponent <CollisionDamageTrigger>(); 
		if (otherCollisionTrigger != null)
		{

			if (CheckIfCollisionShouldBeIgnored (otherCollider2d))
			{
				// generic, alle Collisionen werden gleich behandelt/verarbeitet
			}
			else
			{
				// Collision is not Ignored -> Start Triggering
				Debug.Log (this.ToString() + " Collision is not Ignored -> Start Triggering ");
				float otherSendDamageValue = otherCollisionTrigger.GetDamageValue ();
				float myDamageValue = sendDamageValue * sendDamageMulti;
				Debug.Log (this.ToString() + " otherSendDamageValue " + otherSendDamageValue);
				Debug.Log (this.ToString() + " myDamageValue " + myDamageValue);

				// de-coupling

				// Trigger Collision Event
				Trigger ();

				// Trigger CollisionDamage Event
				if (receiveDamageEnabled)
					Trigger (otherSendDamageValue);
				else
					Trigger (0f);

				// coupling
//				healthManager.ReceiveHealthDamage (otherSendDamageValue);

			}

//			if (otherCollider2d.gameObject.layer == LayerMask.NameToLayer ("Enemy"))
//			{
//				// to this...
//
//		    }
//			else if (otherCollider2d.gameObject.layer == LayerMask.NameToLayer ("Player"))
//			{
//				// do that..
//			}
		}
		else
		{
			#if UNITY_EDITOR
			Debug.LogError (otherCollider2d.gameObject.name + " has no CollisionTrigger Script attached!");
			#endif
		}
	}

	public float GetDamageValue ()
	{
		if (sendDamageEnabled)
			return sendDamageValue * sendDamageMulti;
		else
			return 0f;
	}
	
	[SerializeField]
	private MyFloatEvent myCollisionDamageEvent;					// Event for my Event-Domain

	void Awake ()
	{
		healthManager = InitHealthManager (healthManager);
//		myCollisionDamageEvent = new MyFloatEvent();
	}

	[SerializeField]
	private UnityEvent myCollisionEvent;								// Event for my Event-Domain

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
	private UnityAction<float> collisionDamage;

	void OnEnable ()
	{
		DomainEventManager.StartListening (this.gameObject, "Collision", collisionDamage);
	}

	void OnDisable ()
	{
		DomainEventManager.StopListening (this.gameObject, "Collision", collisionDamage);
	}

	void OnDestroy ()
	{
		DomainEventManager.StopListening (this.gameObject, "Collision", collisionDamage);
	}
	
	public void StartListening (UnityAction<float> newFloatListener)
	{
		myCollisionDamageEvent.AddListener (newFloatListener);
	}
	
	public void StopListening (UnityAction<float> newFloatListener)
	{
		myCollisionDamageEvent.RemoveListener (newFloatListener);
	}

	public void Trigger ()
	{
		if (myCollisionEvent != null)
			myCollisionEvent.Invoke ();
		else
			Debug.LogError (this.ToString () + " myCollisionEvent == NULL");
	}

	public void Trigger (float value)
	{
		if (myCollisionDamageEvent != null)
			myCollisionDamageEvent.Invoke (value);
		else
			Debug.LogError (this.ToString () + " myCollisionDamageEvent == NULL");
	}

	// use either Send Damage or Reveice Damage
	// dont use both!
	public void ReceiveDamage (float damageValue)
	{
		if (receiveDamageEnabled)
			Trigger (damageValue);
	}

	// use either Send Damage or Reveice Damage
	// dont use both!
	public void SendDamage (CollisionDamageTrigger otherCollisionTrigger, float damageValue)
	{
		if (sendDamageEnabled)
			otherCollisionTrigger.ReceiveDamage (damageValue);
	}
}
