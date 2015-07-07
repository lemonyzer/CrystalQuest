using UnityEngine;
using System.Collections;

public class GameOverPanelManager : MonoBehaviour {

	[SerializeField]
	GameObject gameOverPanel;

	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.OnGameOver, OnGameOver);
		DomainEventManager.StartGlobalListening (EventNames.PlayerGameOver, OnGameOver);
	}

	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.OnGameOver, OnGameOver);
		DomainEventManager.StopGlobalListening (EventNames.PlayerGameOver, OnGameOver);
	}

	void OnGameOver ()
	{
		gameOverPanel.SetActive (true);
	}

}
