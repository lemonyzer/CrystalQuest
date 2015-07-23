using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public void StartGame ()
	{
		Application.LoadLevel (1);
	}

	public void LoadMenu ()
	{
		Application.LoadLevel (0);
	}
}
