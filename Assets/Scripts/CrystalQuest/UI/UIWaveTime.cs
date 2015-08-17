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

	[SerializeField]
	float gameTime = 0f;

	[SerializeField]
	bool count = false;

	// Use this for initialization
	void OnEnable () {
		DomainEventManager.StartGlobalListening (EventNames.WaveInit, OnWaveInit);
		DomainEventManager.StartGlobalListening (EventNames.WaveStart, OnWaveStart);
		DomainEventManager.StartGlobalListening (EventNames.WaveFailed, OnWaveFailed);
		DomainEventManager.StartGlobalListening (EventNames.WaveComplete, OnWaveComplete);
		DomainEventManager.StartGlobalListening (EventNames.OnGameOver, OnGameOver);
	}
	
	// Update is called once per frame
	void OnDisable () {
		DomainEventManager.StopGlobalListening (EventNames.WaveInit, OnWaveInit);
		DomainEventManager.StopGlobalListening (EventNames.WaveStart, OnWaveStart);
		DomainEventManager.StopGlobalListening (EventNames.WaveFailed, OnWaveFailed);
		DomainEventManager.StopGlobalListening (EventNames.WaveComplete, OnWaveComplete);
		DomainEventManager.StopGlobalListening (EventNames.OnGameOver, OnGameOver);
	}

	void StartTime ()
	{
		count = true;
	}
	
	void StopTime ()
	{
		count = false;
	}

	void ResetTime ()
	{
		waveTime = 0f;
	}

	void OnWaveInit ()
	{
		ResetTime ();
	}

	void OnWaveStart ()
	{
		StartTime ();
	}

	void OnWaveFailed ()
	{
		StopTime ();
	}

	void OnGameOver ()
	{
		StopTime ();
	}

	void Update ()
	{
		if (count)
		{
			gameTime += Time.deltaTime; 
			waveTime += Time.deltaTime;
			if (uiText != null)
			{
				uiText.text = pre + waveTime.ToString ("F2");
			}
		}
	}

	void OnWaveComplete ()
	{
		StopTime ();
		CalculateBonusPoints ();
	}

	void CalculateBonusPoints ()
	{
		float currentBonusTimeLimit = CrystalQuestWaveManager.Instance.GetCurrentWave ().bonusTimeLimit;
		float currentTimeBonus = CrystalQuestWaveManager.Instance.GetCurrentWave ().timeBonus;
		Debug.Log ("current Wave Time = " + waveTime + ", bonus time limit = " + currentBonusTimeLimit);
		
		float timeDiff = currentBonusTimeLimit - waveTime;
		
		if (timeDiff > 0f)
		{
			float scoreBonus = timeDiff * currentTimeBonus;
			if (scoreBonus > 0f)
			{
				DomainEventManager.TriggerGlobalEvent (EventNames.ScoredValue, scoreBonus);
				DomainEventManager.TriggerGlobalEvent (EventNames.ScoredExtraBonus);
			}
			else
			{
				Debug.Log ("kein negativer Bonus!");
			}
		}
	}

	public float GetPlayTime ()
	{
		return gameTime;
	}

}
