using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

//	[SerializeField]
//	private MyEvent myLifeEvent;

	[SerializeField]
	private MyIntEvent myLifesUpdateEvent;

	[SerializeField]
	private MyEvent myDieEvent;

	[SerializeField]
	private MyEvent myRespawnEvent;

	[SerializeField]
	private MyEvent myGameOverEvent;


	void NoExecuted ()
	{
		myDieEvent = null;
		myLifesUpdateEvent = null;
		myGameOverEvent = null;
		myRespawnEvent = null;
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
			bool isGameOver = false;
			
			if (value > minLifes)
			{
				lifes = value;
			}
			else
			{
				lifes = minLifes;
				// TODO FIX problem #1  gameOver flag, zum merken und späteren ausführen von GameOver () notify, als LifeUpdate ()
				isGameOver = true;
				// NotifyGameOverListener ();			// TODO problem #1, reihenfolge!
			}
			
			if (lifes != tempLifes)
				OnLifeUpdate (lifes);	// TODO problem #1, reihenfolge
			
			
			if (isGameOver)
				OnGameOver ();				// TODO Fix problem #1
			else
				OnCanRespawn ();
		}
	}

	void OnGameOver ()
	{
		// notify gameOver interface
		NotifyGameOverListener ();
	}

	void OnCanRespawn ()
	{
		NotifyRespawnListener ();
		// Player
		// disable Controlls
		// request Respawn
	}

	void OnLifeUpdate (int newAmountOfLifes)
	{
		// notify lifeValue listener
		NotifyLifeValueObserver (newAmountOfLifes);
	}

	void NotifyGameOverListener ()
	{
//		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnGameOver);
		
		if (myGameOverEvent != null)
			myGameOverEvent.Invoke ();
		else
			Debug.LogError ("myGameOverEvent == NULL");
	}

	public void Die ()
	{
		OnDie ();
		Lifes--;
	}

	void OnDie ()
	{
		NotifyDieListener ();
	}

	public void ReceiveLifeDamge(int lifeDamage)
	{
		Lifes -= lifeDamage;
	}
	
	void NotifyLifeValueObserver (int numberOflifes)
	{
//		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnLifeValueUpdate, numberOflifes);
		
		if (myLifesUpdateEvent != null)
			myLifesUpdateEvent.Invoke (numberOflifes);
		else
			Debug.LogError ("myLifesUpdateEvent == NULL");
	}

	void NotifyRespawnListener ()
	{
//		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnRespawn);
		
		if (myRespawnEvent != null)
			myRespawnEvent.Invoke ();
		else
			Debug.LogError ("myRespawnEvent == NULL");
	}

	void NotifyDieListener ()
	{
//		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnDie);
		
		if (myDieEvent != null)
			myDieEvent.Invoke ();
		else
			Debug.LogError ("myDieEvent == NULL");
	}

//	void NotifyLifeListener ()
//	{
//		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnLife);
//		
//		if (myLifeEvent != null)
//			myLifeEvent.Invoke ();
//		else
//			Debug.LogError ("myLifeEvent == NULL");
//	}

	void Start ()
	{
		NotifyLifeValueObserver (lifes);
	}
}
