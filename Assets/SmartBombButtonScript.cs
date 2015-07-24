using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SmartBombButtonScript : MonoBehaviour {

	[SerializeField]
	Button btn;


	void OnEnable ()
	{
//		DomainEventManager.StartGlobalListening (EventNames.WaveStart, EnableInput);
//		DomainEventManager.StartGlobalListening (EventNames.WaveFailed, );
//		DomainEventManager.StartGlobalListening (EventNames.WaveComplete);
		DomainEventManager.StartGlobalListening (EventNames.SmartBombButtonEnabled, EnableButton);
		DomainEventManager.StartGlobalListening (EventNames.SmartBombButtonDisabled, DisableButton);
	}

	void OnDisable ()
	{
//		DomainEventManager.StopGlobalListening (EventNames.WaveStart);
//		DomainEventManager.StopGlobalListening (EventNames.WaveFailed);
//		DomainEventManager.StopGlobalListening (EventNames.WaveComplete);
		DomainEventManager.StopGlobalListening (EventNames.SmartBombButtonEnabled, EnableButton);
		DomainEventManager.StopGlobalListening (EventNames.SmartBombButtonDisabled, DisableButton);
	}

	void EnableButton ()
	{
		btn.interactable = true;
	}

	void DisableButton ()
	{
		btn.interactable = false;
	}
}
