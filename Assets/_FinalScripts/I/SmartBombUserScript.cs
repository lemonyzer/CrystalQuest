﻿using UnityEngine;
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
	float minUseIntervall = 0.5f;

	[SerializeField]
	float nextPossibleTrigger = 0f;

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
		if (audioSource != null && smartBombTriggeredClip != null)
		{
			audioSource.PlayOneShot (smartBombTriggeredClip);
		}
		else
		{
			Debug.LogError (this.ToString () + " has no AudioSource / AudioClip!");
		}
		DomainEventManager.TriggerGlobalEvent (EventNames.SmartBombTriggered);
		// SmartBombManager.Instance.BombTriggered (); // singleton nötig ?
		SmartBombsAmount--;
		NotifySmartBombAmountListener ();
	}

	void NotifySmartBombAmountListener ()
	{
		DomainEventManager.TriggerGlobalEvent (EventNames.SmartBombAmount, smartBombsAmount);
	}
}