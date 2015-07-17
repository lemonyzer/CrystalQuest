using UnityEngine;
using System.Collections;

[System.Serializable]
public class CustomInput {

	[SerializeField]
	string name;

	[SerializeField]
	GameObject gameObject;

	[SerializeField]
	bool active;

}

public class InputManager : MonoBehaviour {

	static InputManager _instance;

	public static InputManager Instance ()
	{
		return _instance;
	}

	[SerializeField]
	GameObject tilt;

	[SerializeField]
	GameObject analogStick;

#if UNITY_ANDROID

#endif 

	void Awake ()
	{
		// check if device can use tilt

		// check if device can use keyboard

		// check if device can use mouse

		// check if device can use touchscreen

		// add to inputManager
		// let user decide -> in settings & in pause menu! -> 

		// in Pause MEnu
		// disable current GameScene (alle GameObjects, remeber last SetActive State)

		// enable tilt calibration / input test zone
		// reenable current gamescene


	}

}
