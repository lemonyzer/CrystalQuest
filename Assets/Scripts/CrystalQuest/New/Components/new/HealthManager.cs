using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class HealthManager : MonoBehaviour {

	void NotExecuted ()
	{
		myNoHealthEvent = null;
		myHealthUpdateEvent = null;
	}

	[SerializeField]
	private float minHealth = 0f;
	
	[SerializeField]
	private float maxHealth = 100f;
	
	[SerializeField]
	private float currentHealth = 100f;
	
	private float Health {
		get {return currentHealth;}
		set
		{
			if (IsDead)
				return;

			float temp = currentHealth;
			if (value > maxHealth)
			{
				//				Debug.LogError ("[" + value + "] > " + maxHealth);
				currentHealth = maxHealth;
			}
			else if (maxHealth >= value &&
			         value > minHealth)
			{
				//				Debug.LogError (maxHealth + " >= [" + value + "] > " + minHealth);
				currentHealth = value;
			}
			else if (value <= minHealth)
			{
				//				Debug.LogError ("[" + value + "] <= " + minHealth);
				currentHealth = minHealth;
				OnNoHealth ();
			}
			
			if (temp != currentHealth)
				OnHealthUpdate (currentHealth);
		}
	}

	public void Revive ()
	{
		IsDead = false;
		Health = maxHealth;
	}

	void Die ()
	{
		IsDead = true;
	}

	void OnHealthUpdate (float newHealth)
	{
		// notify healthValue listener
		NotifyHealthValueListener (newHealth);
	}


	void OnNoHealth ()
	{
		Die ();	// cant't regain life if dead
		NotifyNoHealthListener ();
	}


//	void OnEnable ()
//	{
//		DomainEventManager.StartListening (this.gameObject, EventNames.OnReceiveDamage, ReceiveHealthDamage);
//		DomainEventManager.StartListening (this.gameObject, EventNames.OnReceiveFullDamage, ReceiveFullDamage);
//		DomainEventManager.StartListening (this.gameObject, EventNames.OnRespawn, Respawn);
//	}
//	
//	void OnDisable ()
//	{
//		DomainEventManager.StopListening (this.gameObject, EventNames.OnReceiveDamage, ReceiveHealthDamage);
//		DomainEventManager.StopListening (this.gameObject, EventNames.OnReceiveFullDamage, ReceiveFullDamage);
//		DomainEventManager.StopListening (this.gameObject, EventNames.OnRespawn, Respawn);
//	}

	[SerializeField]
	private MyEvent myNoHealthEvent;

	[SerializeField]
	private MyFloatEvent myHealthUpdateEvent;

	void Awake ()
	{

	}

	[SerializeField]
	private bool invincible = false;
	
	[SerializeField]
	private bool m_isDead = false;

	public bool IsDead {
		get {return m_isDead;}
		set {
			if (m_isDead != value)
			{
				m_isDead = value;
				//	NotifyIsDeadUpdate ();		// TODO TO DO and check if nessesary, GGF. auslagern in DeadScript und -> Collision Verhindern
			}
		}
	}

	public void ReceiveFullDamage ()
	{
		ReceiveHealthDamage (Health);
	}

	public void ReceiveFullDamageIgnoreInvincible ()
	{
		Health = minHealth;
	}
	
	public void ReceiveHealthDamage(float damageValue)
	{
		if(invincible)
		{
			#if UNITY_EDITOR
			Debug.Log (this.ToString () + " ReceiveHealthDamage value: " + damageValue + " but INVINCIBLE!!!");
			#endif 
			return;
		}

		#if UNITY_EDITOR
		Debug.Log (this.ToString () + " ReceiveHealthDamage value: " + damageValue);
		#endif 

		Health -= damageValue;
	}

	void NotifyHealthValueListener (float currentAmountOfHealth)
	{
		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnHealthValueUpdate, currentAmountOfHealth);

		if (myHealthUpdateEvent != null)
			myHealthUpdateEvent.Invoke (currentAmountOfHealth);
		else
			Debug.LogError ("myHealthUpdateEvent == NULL");
	}

	void NotifyNoHealthListener ()
	{
		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnDie);
		
		if (myNoHealthEvent != null)
			myNoHealthEvent.Invoke ();
		else
			Debug.LogError ("myDieEvent == NULL");
	}
	
}
