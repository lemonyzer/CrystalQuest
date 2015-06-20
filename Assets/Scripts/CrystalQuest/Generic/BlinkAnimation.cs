using UnityEngine;
using System.Collections;

public class BlinkAnimation : MonoBehaviour {

	[SerializeField]
	float nextTimeStep;
	[SerializeField]
	float changeIntervall = 0.5f;

	[SerializeField]
	Color[] colorList;

	[SerializeField]
	int currentColorId = 0;

	SpriteRenderer spriteRenderer;

	void Awake ()
	{
		if (colorList == null)
			colorList = new Color[1];
	}

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Time.time >= nextTimeStep)
		{
			nextTimeStep = Time.time + changeIntervall;
			NextColor ();
		}
	}

	void NextColor ()
	{
		currentColorId++;
		currentColorId = currentColorId % colorList.Length;
//		if (colorList[currentColorId] != null)						// can't compare color == null => always true
			spriteRenderer.color = colorList[currentColorId];
	}
}
