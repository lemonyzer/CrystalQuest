using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverPanelManager : MonoBehaviour {

	[SerializeField]
	GameObject gameOverPanel;

	[SerializeField]
	Text panelTitel;

	[SerializeField]
	string gameOver = "Game Over";

	[SerializeField]
	string win = "Congratulations! You Won!";


	void OnEnable ()
	{
		DomainEventManager.StartGlobalListening (EventNames.OnGameOver, OnGameOver);
		DomainEventManager.StartGlobalListening (EventNames.PlayerGameOver, OnGameOver);
		DomainEventManager.StartGlobalListening (EventNames.AllWavesCompleted, OnAllWavesCompleted);
	}

	void OnDisable ()
	{
		DomainEventManager.StopGlobalListening (EventNames.OnGameOver, OnGameOver);
		DomainEventManager.StopGlobalListening (EventNames.PlayerGameOver, OnGameOver);
		DomainEventManager.StopGlobalListening (EventNames.AllWavesCompleted, OnAllWavesCompleted);
	}

	void OnGameOver ()
	{
		if (panelTitel != null)
			panelTitel.text = gameOver;
		gameOverPanel.SetActive (true);
	}

	void OnAllWavesCompleted ()
	{
		if (panelTitel != null)	
			panelTitel.text = win;
		gameOverPanel.SetActive (true);
	}

}
