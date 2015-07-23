using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	public void DoRestart ()
	{
		Application.LoadLevel (Application.loadedLevel);
		Time.timeScale = 1f;
	}
}
