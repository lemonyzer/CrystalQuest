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

	// TODO für EventTrigger<T>
	// TODO für FloatEventTrigger<T>
	// TODO für IntEventTrigger<T>
	// TODO für BoolEventTrigger<T>
//	void Awake ()
//	{
//		// check if CollisionTrigger is listenting itself (wahrscheinlich added trough Inspector)!
//		//TODO
//	}

	[SerializeField]
	List<LayerCollision> layerCollision;// = new List<LayerCollision>();	//TODO -.-  BUG! not visible in Inspector!

	[SerializeField]
	bool IgnoreCollisionIfLayerNotInList = false;

	bool IgnoreCollision (Collider2D otherCollider2D)
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

	protected void OnTriggerEnter2D (Collider2D otherCollider2d)
	{
		CollisionDamageTrigger otherCollisionTrigger = otherCollider2d.GetComponent <CollisionDamageTrigger>(); 
		if (otherCollisionTrigger != null)
		{

			if (IgnoreCollision (otherCollider2d))
			{
				// generic, alle Collisionen werden gleich behandelt/verarbeitet
			}
			else
			{
				Debug.Log (this.ToString() + " Collision is not Ignored -> Start Triggering ");
				float otherSendDamageValue = otherCollisionTrigger.GetDamageValue ();
				float myDamageValue = sendDamageValue * sendDamageMulti;
				Debug.Log (this.ToString() + " otherSendDamageValue " + otherSendDamageValue);
				Debug.Log (this.ToString() + " myDamageValue " + myDamageValue);
				
				// Trigger Collision Event
				Trigger ();

				// Trigger CollisionDamage Event
				if (receiveDamageEnabled)
					Trigger (otherSendDamageValue);
				else
					Trigger (0f);
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

	[SerializeField]
	protected bool sendDamageEnabled = true;

	[SerializeField]
	protected float sendDamageValue = 100f;

	[SerializeField]
	protected float sendDamageMulti = 1f;

	[SerializeField]
	protected bool receiveDamageEnabled = true;

	[SerializeField]
	protected float receiveDamageMulti = 1f;

	public float GetDamageValue ()
	{
		if (sendDamageEnabled)
			return sendDamageValue * sendDamageMulti;
		else
			return 0f;
	}
	
	[SerializeField]
	protected MyFloatEvent myCollisionDamageEvent;					// Event for my Event-Domain

	void Awake ()
	{
//		myCollisionDamageEvent = new MyFloatEvent();
	}

	[SerializeField]
	protected UnityEvent myCollisionEvent;								// Event for my Event-Domain

//	public void StartListening (UnityAction newListener)
//	{
//		myCollisionEvent.AddListener (newListener);
//	}
//
//	public void StopListening (UnityAction newListener)
//	{
//		myCollisionEvent.RemoveListener (newListener);
//	}
	
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
