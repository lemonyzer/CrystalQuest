using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	[SerializeField]
	bool pause = false;


	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			pause = !pause;

		if (pause)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
}
