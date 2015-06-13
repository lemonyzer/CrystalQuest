using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public abstract class CollisionTrigger : MonoBehaviour {

	abstract protected void OnTriggerEnter2D (Collider2D otherCollider2d);

	[SerializeField]
	protected bool sendDamage = false;

	[SerializeField]
	protected bool receiveDamage = false;

	[SerializeField]
	protected float collisionDamage = 0f;

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

	public void Trigger ()
	{
		if (myEvent != null)
			myEvent.Invoke ();
		else
			Debug.LogError (this.ToString () + " myEvent == NULL");
	}


	public void ReceiveDamage (float damageValue)
	{
		if (receiveDamage)
			Trigger (damageValue);
	}

	public void SendDamage (CollisionTrigger otherCollisionTrigger, float damageValue)
	{
		if (sendDamage)
			otherCollisionTrigger.ReceiveDamage (damageValue);
	}
}
