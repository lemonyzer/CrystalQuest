using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class SmartBombUserScript : MonoBehaviour {

	// wenn mehrere Spieler SmarBomb nutzen können und/oder wenn SmartBombManager (zB. nicht im Singleton Patter) zuhören will
//	public delegate void Triggered ();
//	public static event Triggered onTriggered;

	[SerializeField]
	AudioSource audioSource;

	[SerializeField]
	AudioClip smartBombTriggeredClip;

	[SerializeField]
	AudioClip smartBombCollectedClip;

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
			int temp = smartBombsAmount;

			if (value >= minAmount)
				smartBombsAmount = value;
			else
				smartBombsAmount = minAmount;

			if (temp != smartBombsAmount)
				NotifySmartBombAmountListener ();
		}
	}

	[SerializeField]
	float minTriggerIntervall = 1f;

	[SerializeField]
	float nextPossibleTrigger = 0f;

	[SerializeField]
	bool smartBombActivated = false;

	void Awake ()
	{
		audioSource = this.GetComponent<AudioSource> ();
	}

	void Start ()
	{
		NotifySmartBombAmountListener ();
	}

	void Update ()
	{
		if (!smartBombActivated)
			return;

		if (smartBombsAmount >= minAmountToUse)
		{
			if (CrossPlatformInputManager.GetButton ("Bomb"))
			{
				if (Time.time >= nextPossibleTrigger)
				{
					nextPossibleTrigger = Time.time + minTriggerIntervall;
					Trigger ();
				}
			}
		}
	}

	void Trigger ()
	{
		if (audioSource != null && smartBombTriggeredClip != null)
		{
			audioSource.PlayOneShot (smartBombTriggeredClip);
		}
		else
		{
			Debug.LogError (this.ToString () + " has no AudioSource / AudioClip!");
		}
		DomainEventManager.TriggerGlobalEvent (EventNames.SmartBombTriggered);
		DomainEventManager.TriggerGlobalEvent (EventNames.SmartBombButtonDisabled);

		Invoke ("NotifySmartBombUI", minTriggerIntervall);
		
		// SmartBombManager.Instance.BombTriggered (); // singleton nötig ?
		SmartBombsAmount--;
		NotifySmartBombAmountListener ();
	}

	void NotifySmartBombAmountListener ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.SmartBombAmount, smartBombsAmount);
	}

	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.SmartBombCollected, OnSmartBombCollected);
		DomainEventManager.StartGlobalListening (EventNames.WaveStart, OnWaveStart);
		DomainEventManager.StartGlobalListening (EventNames.WaveFailed, Disable);
		DomainEventManager.StartGlobalListening (EventNames.WaveComplete, Disable);
	}

	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.SmartBombCollected, OnSmartBombCollected);
		DomainEventManager.StopGlobalListening (EventNames.WaveStart, OnWaveStart);
		DomainEventManager.StopGlobalListening (EventNames.WaveFailed, Disable);
		DomainEventManager.StopGlobalListening (EventNames.WaveComplete, Disable);
	}

	void OnWaveStart ()
	{
		Activate ();
	}

	void Activate ()
	{
		smartBombActivated = true;
		NotifySmartBombUI ();
	}

	void NotifySmartBombUI ()
	{
		if (smartBombActivated)
		{
			if (smartBombsAmount > 0)
				DomainEventManager.TriggerGlobalEvent (EventNames.SmartBombButtonEnabled);
			else
				DomainEventManager.TriggerGlobalEvent (EventNames.SmartBombButtonDisabled);
		}
		else
		{
			DomainEventManager.TriggerGlobalEvent (EventNames.SmartBombButtonDisabled);
		}
	}

	void Disable ()
	{
		smartBombActivated = false;
	}

	void OnSmartBombCollected ()
	{
		SmartBombsAmount++;

		if (audioSource != null && smartBombCollectedClip != null)
		{
			audioSource.PlayOneShot (smartBombCollectedClip);
		}
		else
		{
			Debug.LogError (this.ToString () + " has no AudioSource / AudioClip!");
		}
	}
}
