using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteAnimation2 : MonoBehaviour {

	[SerializeField]
	float timeBetweenSprites = 0.1f;

	[SerializeField]
	float nextSpriteStep = 0f;

	[SerializeField]
	int currentSpriteId = 0;

	[SerializeField]
	List<Sprite> spriteList;

	[SerializeField]
	SpriteRenderer spriteRenderer;

//	// nested Sprite Asset (with Sprite Editor sliced)
//	[SerializeField]
//	Sprite sprite;	// unity asset nested iterate access sliced sprite


	// Use this for initialization
	void Start () {
		nextSpriteStep = Time.time + timeBetweenSprites;
	}
	
	// Update is called once per frame
	void Update () {

		currentSpriteId %= spriteList.Count;
		spriteRenderer.sprite = spriteList[currentSpriteId];
		if (nextSpriteStep <= Time.time)
		{
			// nächster Zeitpunkt erreicht, wechsele Bild
			nextSpriteStep += timeBetweenSprites;
			currentSpriteId = (currentSpriteId + 1) % spriteList.Count;
		}
	}
}
