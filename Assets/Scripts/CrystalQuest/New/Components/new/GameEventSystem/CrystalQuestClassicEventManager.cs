using UnityEngine;
using System.Collections;

public class CrystalQuestClassicEventManager : MonoBehaviour {
	
	public delegate void PlayerDied ();
	public static event PlayerDied onPlayerDied;

	void OnEnable ()
	{
		PlayerCrystalQuestTrigger.onDied += OnPlayerDied;
	}

	void OnDisable ()
	{
		PlayerCrystalQuestTrigger.onDied -= OnPlayerDied;
	}

	void OnPlayerDied () {
		if (onPlayerDied != null)
			onPlayerDied ();
		else
		{
			Debug.LogError ("no \"onPlayerDied\" listener");
		}
	}
}
