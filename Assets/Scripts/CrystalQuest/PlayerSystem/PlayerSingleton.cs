using UnityEngine;
using System.Collections;

public class PlayerSingleton : MonoBehaviour {

	//Here is a private reference only this class can access
	private static PlayerSingleton _instance;
	
	//This is the public reference that other classes will use
	public static PlayerSingleton Instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			if(_instance == null)
				_instance = GameObject.FindObjectOfType<PlayerSingleton>();
			return _instance;
		}
	}

	// es gibt schon ein event das ausgeführt wird wenn spieler stirbt.
//	float health;
//	
//	public void UpdateHealthValue (float value)
//	{
//		if (value != health)
//		{
//			
//		}
//	}

	// gibt es ein event um enemy ai mitzuteilen wie viel spieler sich bewegt?
	// enemy kann crossplatforminputmanager auslesen!

	public void Play()
	{
		//Play some audio!
	}
}
