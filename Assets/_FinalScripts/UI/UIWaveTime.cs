using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIWaveTime : MonoBehaviour {

	[SerializeField]
	string pre = "Time: ";

	[SerializeField]
	Text uiText;

	[SerializeField]
	float waveTime = 0f;

	// Use this for initialization
	void OnEnable () {
		DomainEventManager.StartGlobalListening (EventNames.WaveStart, OnWaveStart);
	}
	
	// Update is called once per frame
	void OnDisable () {
		DomainEventManager.StopGlobalListening (EventNames.WaveStart, OnWaveStart);
	}

	void ResetTime ()
	{
		waveTime = 0f;
	}

	void OnWaveStart ()
	{
		ResetTime ();
	}

	void Update ()
	{
		waveTime += Time.deltaTime;
		if (uiText != null)
		{
			uiText.text = pre + waveTime.ToString ("F2");
		}
	}

}
