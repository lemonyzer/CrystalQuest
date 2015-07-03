using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class SmartBombUserScript : MonoBehaviour {

	// wenn mehrere Spieler SmarBomb nutzen können und/oder wenn SmartBombManager (zB. nicht im Singleton Patter) zuhören will
//	public delegate void Triggered ();
//	public static event Triggered onTriggered;

	[SerializeField]
	int smartBombsAmount = 3;

	[SerializeField]
	int minAmountToUse = 1;

	[SerializeField]
	int minAmount = 0;

	public int SmartBombsAmount {
		get {return smartBombsAmount;}
		set
		{
			if (value >= minAmount)
				smartBombsAmount = value;
			else
				smartBombsAmount = minAmount;
		}
	}

	[SerializeField]
	float minUseIntervall = 0.1f;

	[SerializeField]
	float nextPossibleTrigger = 0f;

	void Update ()
	{
		if (smartBombsAmount >= minAmountToUse)
		{
			if (CrossPlatformInputManager.GetButton ("Bomb"))
			{
				if (Time.time >= nextPossibleTrigger)
				{
					nextPossibleTrigger = Time.time + minUseIntervall;
					Trigger ();
				}
			}
		}
	}

	void Trigger ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.SmartBombTriggered);
		// SmartBombManager.Instance.BombTriggered (); // singleton nötig ?
		SmartBombsAmount--;

	}

	void NotifySmartBombAmountListener ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.SmartBombAmount, smartBombsAmount);
	}
}
