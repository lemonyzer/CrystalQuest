using UnityEngine;
using System.Collections;

public class Collisions : MonoBehaviour {

	int points = 0;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Crystal")
		{
			points++;
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Enemy")
		{
			points = 0;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void OnGUI()
	{
		GUILayout.Box("Points: " + points.ToString());
	}
}
