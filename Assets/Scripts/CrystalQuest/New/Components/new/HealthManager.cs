using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public enum HealthManagerEvents
{
	ReceiveDamage,
	OnDie,
	OnGameOver,
	OnHealthValueUpdate,
	OnLifeValueUpdate,
};

public class HealthManager : MonoBehaviour {

	// UnityEvent dynamic parameter
	// http://answers.unity3d.com/questions/917623/how-to-fire-unityevent-with-parameters-passed-prog.html


//	[SerializeField]
//	private HealthDataModel m_healthData;// = new HealthDataModel();		//TODO -.-  BUG #1 not visible in Inspector!

//	void InitHealthDataModellSO (HealthDataModel myHealthData)
//	{
//		myHealthData = ScriptableObject.CreateInstance<HealthDataModel>();
//	}
//	void StartHealthModelListening (HealthDataModel healthData)
//	{
//		// TODO Bug #1 not registered for listening to the riht HealthDataModel object/instance!
//		healthData.onHealthUpdate += OnHealthUpdate;
//		healthData.onLifeUpdate += OnLifeUpdate;
//		healthData.onDied += OnDie;
//		healthData.onGameOver += OnGameOver;
//	}
//	void StopHealthModelListening (HealthDataModel healthData)
//	{
//		healthData.onHealthUpdate -= OnHealthUpdate;
//		healthData.onLifeUpdate -= OnLifeUpdate;
//		healthData.onDied -= OnDie;
//		healthData.onGameOver -= OnGameOver;
//	}

	[SerializeField]
	private float minHealth = 0f;
	
//	public float MinHealth {
//		get {return minHealth;}
//		set {minHealth = value;}
//	}
	
	[SerializeField]
	private float maxHealth = 100f;
	
//	public float MaxHealth {
//		get {return maxHealth;}
//		set {maxHealth = value;}
//	}
	
	[SerializeField]
	private float currentHealth = 100f;
	
//	public float Health {
//		get {return currentHealth;}
//		set {currentHealth = value;}
//	}
	
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
				OnDie ();
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
	
//	public float MinLifes {
//		get {return minLifes;}
//	}
	
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
//		StartHealthModelListening (m_healthData);
		StartTriggerListListening ();
	}
	
	void OnDisable ()
	{
//		StopHealthModelListening (m_healthData);
		StopTriggerListListening ();
	}


	[SerializeField]
	public static MyFloatEvent myStaticHealthUpdateEvent;		// für was?
	[SerializeField]
	public static MyFloatEvent myStaticLifesUpdateEvent;		// für was?

	[SerializeField]
	private MyEvent myDieEvent;

	[SerializeField]
	private MyEvent myGameOverEvent;

	[SerializeField]
	private MyFloatEvent myHealthUpdateEvent;

	[SerializeField]
	private MyIntEvent myLifesUpdateEvent;

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
//		InitHealthDataModellSO (healthData);
		myCollisionTriggerListener = new UnityAction<float> (CollisionAction);
	}

	void CollisionAction (float damageValue)
	{
		ReceiveHealthDamage (damageValue);
		Debug.Log ("YEAH");
	}

	void StartTriggerListListening ()
	{
//		Debug.LogError (this.ToString () + " StartTriggerListListening");
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
//		Debug.LogError (this.ToString () + " StopTriggerListListening");
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
		Debug.Log (this.ToString () + " ReceiveHealthDamage value: " + damageValue);
		if(invincible)
			return;

		//		float temp = Health;
		Health -= damageValue;
		
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
	
//	void Die ()
//	{
//		// only on Player
//		// DecreaseLifeCount ();
//		isDead = true;
////		this.gameObject.SetActive (false);
//	}

	public void ReceiveLifeDamge(int lifeDamage)
	{
		Lifes -= lifeDamage;

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
