﻿using UnityEngine;
using System.Collections;

public class RespawnManager : MonoBehaviour {

	private static RespawnManager m_instance;

	[SerializeField]
	GameObject player;

	public RespawnManager Instance {
		get {return m_instance;}
		private set {m_instance = value;}
	}

	void Awake ()
	{
		Instance = this;
	}

	void OnEnable ()
	{
//		DomainEventManager.StartGlobalListening (EventNames.WavePreInit, OnWavePreInit);
//		DomainEventManager.StartGlobalListening (EventNames.OnDelayedReactivateRequest, ReactivateWithDelay);
		RespawnScript.onDelayedReactivateRequest += ReactivateWithDelay;
//		RespawnScript.onReactivateRequest += Reactivate;
	}

	void OnDisable ()
	{
//		DomainEventManager.StopGlobalListening (EventNames.WavePreInit, OnWavePreInit);
//		DomainEventManager.StopGlobalListening (EventNames.OnDelayedReactivateRequest, ReactivateWithDelay);
		RespawnScript.onDelayedReactivateRequest -= ReactivateWithDelay;
//		RespawnScript.onReactivateRequest -= Reactivate;
		
	}

	void ReactivateWithDelay (GameObject go, RespawnScript script, float delay)
	{
		if (delay <= 0f)
			Activate (go, script);
			
		else
			StartCoroutine (WaitAndActivate (go, script, delay));
	}

	void Reactivate (GameObject go)
	{
		
	}

	IEnumerator WaitAndActivate(GameObject go, RespawnScript script, float waitTime) {
		Debug.Log ("Start spawn Delay...");
		yield return new WaitForSeconds(waitTime);
		Debug.Log ("Spawn Delay Done, trigger spawning...");
		Activate (go, script);

	}

	void Activate (GameObject go, RespawnScript activateionEventScript)
	{
		activateionEventScript.Activate ();
	}

//	void OnWavePreInit ()
//	{
//
//	}

}
