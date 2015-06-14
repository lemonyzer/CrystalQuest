using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class HealthDataModel {

	[SerializeField]
	private float minHealth = 0f;

	public float MinHealth {
		get {return minHealth;}
	}

	[SerializeField]
	private float maxHealth = 100f;

	public float MaxHealth {
		get {return maxHealth;}
	}
	
	[SerializeField]
	private float currentHealth = 100f;
	
	public float Health {
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
//				Die ();
			}
			
//			if (temp != currentHealth)
//				HealthUpdated ();
		}
	}
	
	[SerializeField]
	protected int lifes = 3;

	[SerializeField]
	private int minLifes = 0;

	public float MinLifes {
		get {return minLifes;}
	}

	public int Lifes {
		get {return lifes;}
		set
		{
			if (value >= minLifes)
				lifes = value;
			else
				lifes = minLifes;
		}
	}
}

public class HealthManager : MonoBehaviour {

	[SerializeField]
	private HealthDataModel healthData;

	[SerializeField]
	public static MyFloatEvent myStaticHealthUpdateEvent;		// für was?
	[SerializeField]
	public static MyFloatEvent myStaticLifesUpdateEvent;		// für was?

	[SerializeField]
	protected MyFloatEvent myHealthUpdateEvent;

	[SerializeField]
	protected MyIntEvent myLifesUpdateEvent;

	#region collision trigger AKA Health Modifiing Trigger 
//	[SerializeField]
//	List<MyFloatEvent> healthModifiyingTrigger;				<-- TODO #1 Problem: kann nicht im Inspector ausgefüllt werden 

//	[SerializeField]
//	List<ScriptWithFloatEvent> healthModifyingTrigger;		<-- TODO #1 Lösung

	[SerializeField]
	public List<CollisionTrigger> collisionTriggerScripts;	// <-- CollisionTrigger statt FloatEventScript verhindert endloskette

	[SerializeField]
	public UnityAction<float> myCollisionTriggerListener;
	#endregion 

	void Awake ()
	{
		myCollisionTriggerListener = new UnityAction<float> (CollisionAction);
	}

	void CollisionAction (float damageValue)
	{
		ReceiveDamage (damageValue);
		Debug.Log ("YEAH");
	}

	void StartTriggerListening ()
	{
		Debug.LogError (this.ToString () + " StartTriggerListening");
		if (collisionTriggerScripts != null)
		{
			for (int i = 0; i < collisionTriggerScripts.Count; i++)
			{
				collisionTriggerScripts[i].StartListening (myCollisionTriggerListener);
			}
		}
	}
	
	void StopTriggerListening ()
	{
		Debug.LogError (this.ToString () + " StopTriggerListening");
		if (collisionTriggerScripts != null)
		{
			for (int i = 0; i < collisionTriggerScripts.Count; i++)
			{
				collisionTriggerScripts[i].StopListening (myCollisionTriggerListener);
			}
		}
	}
	
	void OnEnable () {
		StartTriggerListening ();
	}
	
	void OnDisable ()
	{
		StopTriggerListening ();
	}
	
//	public void StartListening (UnityAction<float> floatListener)
//	{
//		myEvent.AddListener (listener);
//	}
//	
//	public void StopListening (UnityAction<float> floatListener)
//	{
//		myEvent.RemoveListener (listener);
//	}

	[SerializeField]
	private bool invincible = false;
	
	//	[SerializeField]
	//	private bool projectileInvincible = false;
	//	[SerializeField]
	//	private bool collisionInvincible = false;
	

	
	[SerializeField]
	protected bool isDead = false;
	
	protected void HealthUpdated ()
	{
		
	}
	
	public void ReceiveDamage(float damageValue)
	{
		if(invincible)
			return;

		//		float temp = Health;
		healthData.Health = healthData.Health - damageValue;
		
		// only on Player -> HealthUpdated
		//		if (temp != Health)
		//			NotifyHealthListener(Health); 
		
		//		if (Health <= minHealth)
		//		{
		//			// damageValue > health -> lebensabzug
		//			Die();
		//		}
		//		else
		//		{
		//			// not dead, gogogo
		//		}
	}
	
	protected void Die ()
	{
		// only on Player
		// DecreaseLifeCount ();
		isDead = true;
		this.gameObject.SetActive (false);
	}

	protected void DecreaseLifeCount(int decreaseAmount)
	{
		healthData.Lifes = healthData.Lifes - decreaseAmount;
		if (healthData.Lifes == healthData.MinLifes)
		{
			// GameOver
		}
		else
		{
			// not GameOver, keep trying
		}
		NotifyLifeListener(healthData.Lifes);
	}

	void NotifyLifeListener (int lifes)
	{
		if (myLifesUpdateEvent != null)
			myLifesUpdateEvent.Invoke (lifes);
		else
			Debug.LogError ("");
	}
}
