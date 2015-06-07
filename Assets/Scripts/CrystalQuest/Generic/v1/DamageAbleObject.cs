using UnityEngine;
using System.Collections;

public class DamageAbleObject : CrystalQuestObjectScript {

	#region Health
	/**
	 * Health
	 **/
	public delegate void LostLife(int numberOfRemainingLifes);
	public static event LostLife onLostLife;
	public delegate void UpdateHealth(float currentHealthValue);
	public static event UpdateHealth onUpdateHealth;
	
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
			if (currentHealth > maxHealth)
			{
				currentHealth = maxHealth;
			}
			else if (currentHealth >= minHealth)
			{
				currentHealth = value;
			}
			else
			{
				currentHealth = minHealth;
				Die ();
			}
		}
	}
	
	[SerializeField]
	private int lifes = 3;
	
	[SerializeField]
	private int minLifes = 0;
	public int Lifes {
		get {return lifes;}
		set
		{
			if (lifes >= minLifes)
				lifes = value;
			else
				lifes = minLifes;
		}
	}
	
	[SerializeField]
	protected bool isDead = false;

	public void ReceiveDamage(float damageValue)
	{
		if(invincible)
			return;

		float temp = Health;
		Health = Health - damageValue;

		if (temp != Health)
			NotifyHealthListener(Health); 
			
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
		DecreaseLifeCount (-1);
		isDead = true;
		this.gameObject.SetActive (false);
	}
	
	void DecreaseLifeCount(int decreaseAmount)
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
		NotifyLostLifeListener(Lifes);
	}
	
	void NotifyHealthListener(float currentHealth)
	{
		if(onUpdateHealth != null)
		{
			onUpdateHealth(currentHealth);
		}
		else
		{
			Debug.LogError("no \"onUpdateHealth\" Listener");
		}
	}
	void NotifyLostLifeListener(int numberOfLifes)
	{
		if(onLostLife != null)
		{
			onLostLife(numberOfLifes);
		}
		else
		{
			Debug.LogError("no \"onLostLife\" Listener");
		}
	}
	#endregion

}
