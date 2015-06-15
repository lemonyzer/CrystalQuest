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
		set {minHealth = value;}
	}

	[SerializeField]
	private float maxHealth = 100f;

	public float MaxHealth {
		get {return maxHealth;}
		set {maxHealth = value;}
	}
	
	[SerializeField]
	private float currentHealth = 100f;

//	public float Health {
//		get {return currentHealth;}
//		set {currentHealth = value;}
//	}

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
				Die ();
			}
			
			if (temp != currentHealth)
				NotifyHealthValueUpdateListener (currentHealth);
		}
	}

	public delegate void HealthUpdate (float currentHealthValue);
	public event HealthUpdate onHealthUpdate;

	public delegate void LifeUpdate (int numberOfRemainingLifes);
	public event LifeUpdate onLifeUpdate;

	public delegate void Died ();
	public event Died onDied;

	public delegate void GameOver ();
	public event GameOver onGameOver;

	void Die ()
	{
		Lifes--;
		NotifyDieListener ();
	}

	void NotifyGameOverListener ()
	{
		if (onGameOver != null)
			onGameOver ();
		else
			Debug.LogError ("onGameOver no listener");
	}

	void NotifyDieListener ()
	{
		if (onDied != null)
			onDied ();
		else
			Debug.LogError ("onDied no listener");
	}

	void NotifyHealthValueUpdateListener (float currentHealth)
	{
		if(onHealthUpdate != null)
		{
			onHealthUpdate (currentHealth);
		}
		#if UNITY_EDITOR
		else
		{
			Debug.LogError(this.ToString() + " no \"onHealthUpdate\" Listener");
		}
		#endif
	}

	void NotifyLifeValueUpdateListener(int numberOfLifes)
	{
		if(onLifeUpdate != null)
		{
			onLifeUpdate (numberOfLifes);
		}
		#if UNITY_EDITOR
		else
		{
			Debug.LogError(this.ToString() + " no \"onLifeUpdate\" Listener");
		}
		#endif
	}
	
	[SerializeField]
	private int lifes = 3;

	[SerializeField]
	private int minLifes = 0;

	public float MinLifes {
		get {return minLifes;}
	}

	public int Lifes {
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
				NotifyLifeValueUpdateListener (lifes);	// TODO problem #1, reihenfolge

			if (gameOver)
				NotifyGameOverListener ();				// TODO Fix problem #1
		}
	}
}

public class HealthManager : MonoBehaviour {

	[SerializeField]
	private HealthDataModel healthData;// = new HealthDataModel();		//TODO -.-  BUG #1 not visible in Inspector!

	void StartHealthModelListening (HealthDataModel healthData)
	{
		// TODO Bug #1 not registered for listening to the riht HealthDataModel object/instance!
		healthData.onHealthUpdate += OnHealthUpdate;
		healthData.onLifeUpdate += OnLifeUpdate;
		healthData.onDied += OnDie;
		healthData.onGameOver += OnGameOver;
	}
	void StopHealthModelListening (HealthDataModel healthData)
	{
		healthData.onHealthUpdate -= OnHealthUpdate;
		healthData.onLifeUpdate -= OnLifeUpdate;
		healthData.onDied -= OnDie;
		healthData.onGameOver -= OnGameOver;
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
	void OnDie ()
	{
		// notify die interface
		NotifyDieListener ();
	}
	void OnGameOver ()
	{
		// notify gameOver interface
		NotifyGameOverListener ();
	}

	void OnEnable () {
		StartHealthModelListening (healthData);
		StartTriggerListListening ();
	}
	
	void OnDisable ()
	{
		StopHealthModelListening (healthData);
		StopTriggerListListening ();
	}


	[SerializeField]
	public static MyFloatEvent myStaticHealthUpdateEvent;		// für was?
	[SerializeField]
	public static MyFloatEvent myStaticLifesUpdateEvent;		// für was?

	[SerializeField]
	protected MyEvent myDieEvent;

	[SerializeField]
	protected MyEvent myGameOverEvent;

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
	public List<CollisionDamageTrigger> collisionDamageTriggerScripts;	// <-- CollisionTrigger statt FloatEventScript verhindert endloskette

	[SerializeField]
	public UnityAction<float> myCollisionTriggerListener;
	#endregion 

	void Awake ()
	{
		myCollisionTriggerListener = new UnityAction<float> (CollisionAction);
	}

	void CollisionAction (float damageValue)
	{
		ReceiveHealthDamage (damageValue);
		Debug.Log ("YEAH");
	}

	void StartTriggerListListening ()
	{
		Debug.LogError (this.ToString () + " StartTriggerListListening");
		if (collisionDamageTriggerScripts != null)
		{
			for (int i = 0; i < collisionDamageTriggerScripts.Count; i++)
			{
				collisionDamageTriggerScripts[i].StartListening (myCollisionTriggerListener);
			}
		}
	}
	
	void StopTriggerListListening ()
	{
		Debug.LogError (this.ToString () + " StopTriggerListListening");
		if (collisionDamageTriggerScripts != null)
		{
			for (int i = 0; i < collisionDamageTriggerScripts.Count; i++)
			{
				collisionDamageTriggerScripts[i].StopListening (myCollisionTriggerListener);
			}
		}
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

	// gehört in collisionManagersebene, nicht in healthmanager
	//	[SerializeField]
	//	private bool projectileInvincible = false;
	//	[SerializeField]
	//	private bool collisionInvincible = false;
	
	[SerializeField]
	protected bool isDead = false;
	
	public void ReceiveHealthDamage(float damageValue)
	{
		Debug.Log (this.ToString () + " ReceiveHealthDamage value: " + damageValue);
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
	
	void Die ()
	{
		// only on Player
		// DecreaseLifeCount ();
		isDead = true;
//		this.gameObject.SetActive (false);
	}

	public void ReceiveLifeDamge(int lifeDamage)
	{
		healthData.Lifes -= lifeDamage;

		// wird schon von HealthModell über delegates aufgerufen
//		if (healthData.Lifes == healthData.MinLifes)
//		{
//			// GameOver
//		}
//		else
//		{
//			// not GameOver, keep trying
//		}
//		NotifyLifeValueListener(healthData.Lifes);		
	}

	void NotifyLifeValueListener (int numberOflifes)
	{
		if (myLifesUpdateEvent != null)
			myLifesUpdateEvent.Invoke (numberOflifes);
		else
			Debug.LogError ("myLifesUpdateEvent == NULL");
	}

	void NotifyHealthValueListener (float currentAmountOfHealth)
	{
		if (myHealthUpdateEvent != null)
			myHealthUpdateEvent.Invoke (currentAmountOfHealth);
		else
			Debug.LogError ("myHealthUpdateEvent == NULL");
	}

	void NotifyDieListener ()
	{
		if (myDieEvent != null)
			myDieEvent.Invoke ();
		else
			Debug.LogError ("myDieEvent == NULL");
	}

	void NotifyGameOverListener ()
	{
		if (myGameOverEvent != null)
			myGameOverEvent.Invoke ();
		else
			Debug.LogError ("myGameOverEvent == NULL");
	}
}
