using UnityEngine;
using System.Collections;

public class EnemyCrystalQuestEventHandler : MonoBehaviour {

	void OnEnable ()
	{
		CrystalQuestClassicEventManager.onPlayerDied += OnPlayerDied;
	}

	void OnDisable ()
	{
		CrystalQuestClassicEventManager.onPlayerDied -= OnPlayerDied;
	}

	void OnPlayerDied ()
	{
		this.gameObject.SetActive (false);
	}
}
