using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public enum HealthManagerEvents
{
	OnReceiveDamage,
	OnDie,
	OnGameOver,
	OnHealthValueUpdate,
	OnLifeValueUpdate,
	count
};

public class HealthManager : MonoBehaviour {

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
	
	void Die ()
	{
		Lifes--;
		IsDead = true;
	}

	public void Respawn ()
	{
		// eigentlich erst hier Life--
		Health = maxHealth;
		IsDead = false;
	}
	
	[SerializeField]
	private int lifes = 3;
	
	[SerializeField]
	private int minLifes = 0;
	
	private int Lifes {
		get {return lifes;}
		set
		{
			int tempLifes = lifes;
			bool gameOver = false;
			
			if (value > minLifes)
				lifes = value;
			else
			{
				lifes = minLifes;
				// TODO FIX problem #1  gameOver flag, zum merken und späteren ausführen von GameOver () notify, als LifeUpdate ()
				gameOver = true;
				// NotifyGameOverListener ();			// TODO problem #1, reihenfolge!
			}
			
			if (lifes != tempLifes)
				OnLifeUpdate (lifes);	// TODO problem #1, reihenfolge
			
			if (gameOver)
				OnGameOver ();				// TODO Fix problem #1
		}
	}

	void OnHealthUpdate (float newHealth)
	{
		// notify healthValue listener
		NotifyHealthValueListener (newHealth);
	}
	void OnLifeUpdate (int newAmountOfLifes)
	{
		// notify lifeValue listener
		NotifyLifeValueListener (newAmountOfLifes);
	}

	void OnNoHealth ()
	{
		OnDie ();
	}

	void OnDie ()
	{
		Die ();
		// notify die interface
		NotifyDieListener ();
	}
	void OnGameOver ()
	{
		// notify gameOver interface
		NotifyGameOverListener ();
	}

	void OnEnable ()
	{
		DomainEventManager.StartListening (this.gameObject, EventNames.OnReceiveDamage, ReceiveHealthDamage);
		DomainEventManager.StartListening (this.gameObject, EventNames.OnReceiveFullDamage, ReceiveFullDamage);
		DomainEventManager.StartListening (this.gameObject, EventNames.OnRespawn, Respawn);
	}
	
	void OnDisable ()
	{
		DomainEventManager.StopListening (this.gameObject, EventNames.OnReceiveDamage, ReceiveHealthDamage);
		DomainEventManager.StopListening (this.gameObject, EventNames.OnReceiveFullDamage, ReceiveFullDamage);
		DomainEventManager.StopListening (this.gameObject, EventNames.OnRespawn, Respawn);
	}

	[SerializeField]
	private MyEvent myDieEvent;

	[SerializeField]
	private MyEvent myLifeEvent;

	[SerializeField]
	private MyEvent myGameOverEvent;

	[SerializeField]
	private MyFloatEvent myHealthUpdateEvent;

	[SerializeField]
	private MyIntEvent myLifesUpdateEvent;

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
	
	public void ReceiveHealthDamage(float damageValue)
	{
		Debug.Log (this.ToString () + " ReceiveHealthDamage value: " + damageValue + " but INVINCIBLE!!!");
		if(invincible)
			return;

		Debug.Log (this.ToString () + " ReceiveHealthDamage value: " + damageValue);

		Health -= damageValue;
	}
	

	public void ReceiveLifeDamge(int lifeDamage)
	{
		Lifes -= lifeDamage;
	}

	void NotifyLifeValueListener (int numberOflifes)
	{
		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnLifeValueUpdate, numberOflifes);

		if (myLifesUpdateEvent != null)
			myLifesUpdateEvent.Invoke (numberOflifes);
		else
			Debug.LogError ("myLifesUpdateEvent == NULL");
	}

	void NotifyHealthValueListener (float currentAmountOfHealth)
	{
		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnHealthValueUpdate, currentAmountOfHealth);

		if (myHealthUpdateEvent != null)
			myHealthUpdateEvent.Invoke (currentAmountOfHealth);
		else
			Debug.LogError ("myHealthUpdateEvent == NULL");
	}

	void NotifyDieListener ()
	{
		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnDie);
		
		if (myDieEvent != null)
			myDieEvent.Invoke ();
		else
			Debug.LogError ("myDieEvent == NULL");
	}

	void NotifyLifeListener ()
	{
		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnLife);
		
		if (myLifeEvent != null)
			myLifeEvent.Invoke ();
		else
			Debug.LogError ("myLifeEvent == NULL");
	}

	void NotifyGameOverListener ()
	{
		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnGameOver);

		if (myGameOverEvent != null)
			myGameOverEvent.Invoke ();
		else
			Debug.LogError ("myGameOverEvent == NULL");
	}
}
