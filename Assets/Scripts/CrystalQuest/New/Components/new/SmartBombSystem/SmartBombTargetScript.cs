using UnityEngine;
using System.Collections;

public class SmartBombTargetScript : MonoBehaviour {

	void OnEnable ()
	{
		SmartBombManager.onSmartBombing += SmartBombTriggered;
	}

	void OnDisable ()
	{
		SmartBombManager.onSmartBombing -= SmartBombTriggered;
	}

	[SerializeField]
	MyEvent OnSmartBombTriggered;


	void SmartBombTriggered ()
	{
		OnSmartBombTriggered.Invoke ();
	}
}
