using UnityEngine;
using System.Collections;

public class SmartBombTrigger : EventTrigger {

	float timeIntervall = 0.5f;
	float lastTime = 0;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
		{
			if (Time.time >= lastTime)
			{
				lastTime = Time.time + timeIntervall;
				TriggerEvent ();
			}
		}
	}
}
