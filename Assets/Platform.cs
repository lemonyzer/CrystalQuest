using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		if (Application.platform == RuntimePlatform.Android)
		{
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
