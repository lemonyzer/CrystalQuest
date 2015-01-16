using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public bool pause = false;

	// Use this for initialization
	void Start () {
	
		if (Application.platform == RuntimePlatform.Android)
		{
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
		{
			pause = !pause;
		}

		if (pause)
						Time.timeScale = 0;
				else
						Time.timeScale = 1;
	}
}
