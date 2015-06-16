using UnityEngine;
using System.Collections;

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
		{
			#if UNITY_EDITOR
			Debug.LogError ("no \"onGameOver\" listener");
			#endif
		}
	}
	
	void NotifyDieListener ()
	{
		if (onDied != null)
			onDied ();
		else
		{
			#if UNITY_EDITOR
			Debug.LogError ("no \"onDied\" listener");
			#endif
		}
	}
	
	void NotifyHealthValueUpdateListener (float currentHealth)
	{
		if(onHealthUpdate != null)
		{
			onHealthUpdate (currentHealth);
		}
		else
		{
			#if UNITY_EDITOR
			Debug.LogError(this.ToString () + " no \"onHealthUpdate\" Listener");
			#endif
		}
	}
	
	void NotifyLifeValueUpdateListener(int numberOfLifes)
	{
		if(onLifeUpdate != null)
		{
			onLifeUpdate (numberOfLifes);
		}
		else
		{
			#if UNITY_EDITOR
			Debug.LogError(this.ToString () + " no \"onLifeUpdate\" Listener");
			#endif
		}
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