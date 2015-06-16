using UnityEngine;
using System.Collections;

public class PlayerCrystalQuestTrigger : MonoBehaviour {

	public delegate void Died ();
	public static event Died onDied;

	public void Die ()
	{
		if (onDied != null)
			onDied ();
		else
			Debug.LogError ("no onDied listener");
	}
}
