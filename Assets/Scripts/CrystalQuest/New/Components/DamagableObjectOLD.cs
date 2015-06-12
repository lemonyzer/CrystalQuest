using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamagableObjectOLD : EventTrigger {

	[SerializeField]
	List<EventTrigger> damageTrigger;

	#region Health
	/**
	 * Health
	 **/
	public delegate void LifeUpdate (int numberOfRemainingLifes);
	public static event LifeUpdate onLifeUpdate;
	public delegate void HealthUpdate (float currentHealthValue);
	public static event HealthUpdate onHealthUpdate;
	public delegate void Died (int numberOfRemainingLifes);
	public static event Died onDied;
	
	[SerializeField]
	protected bool selfAttack = false;
	public bool SelfAttack {
		get { return selfAttack; }
		set { selfAttack = value; }
	}
	
	[SerializeField]
	protected bool teamAttack = false;
	public bool TeamAttack {
		get { return teamAttack; }
		set { teamAttack = value; }
	}
	
	[SerializeField]
	private bool invincible = false;
	
	//	[SerializeField]
	//	private bool projectileInvincible = false;
	//	[SerializeField]
	//	private bool collisionInvincible = false;
	
	[SerializeField]
	private float minHealth = 0f;
	
	[SerializeField]
	private float maxHealth = 100f;
	
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
				Die ();
			}
			
			if (temp != currentHealth)
				HealthUpdated ();
		}
	}
	
	[SerializeField]
	protected int lifes = 3;
	
	[SerializeField]
	private int minLifes = 0;
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
	
	[SerializeField]
	protected bool isDead = false;
	
	protected virtual void HealthUpdated ()
	{
		
	}
	
	public void ReceiveDamage(float damageValue)
	{
		if(invincible)
			return;
		
		//		float temp = Health;
		Health = Health - damageValue;
		
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
	
	public virtual void Die ()
	{
		// only on Player
		// DecreaseLifeCount ();
		isDead = true;
		this.gameObject.SetActive (false);
	}
	
	protected void DecreaseLifeCount(int decreaseAmount)
	{
		Lifes = Lifes - decreaseAmount;
		if (Lifes == minLifes)
		{
			// GameOver
		}
		else
		{
			// not GameOver, keep trying
		}
		NotifyLifeListener(Lifes);
	}
	
	protected void NotifyHealthListener(float currentHealth)
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
	protected void NotifyLifeListener(int numberOfLifes)
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
	protected void NotifyDiedListener(int numberOfLifes)
	{
		if(onDied != null)
		{
			onDied (numberOfLifes);
		}
		#if UNITY_EDITOR
		else
		{
			Debug.LogError(this.ToString() + " no \"onDie\" Listener");
		}
		#endif
	}
	#endregion

}
