﻿using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	public AudioClip levelFinishClip;

	LevelScript levelScript;
	GameControllerScript gScript;

	// Use this for initialization
	void Start () {
//		levelScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<LevelScript> ();
		gScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameControllerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			audio.PlayOneShot(levelFinishClip);
			gScript.nextLevel();
//			levelScript.nextLevel();
		}
	}
}
