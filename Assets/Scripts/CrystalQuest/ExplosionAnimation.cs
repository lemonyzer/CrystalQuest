using UnityEngine;
using System.Collections;

public class ExplosionAnimation : MonoBehaviour {

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private Sprite[] sprites;

	[SerializeField]
	private int currentId = 0;

	[SerializeField]
	private bool limitAnimationLoops = false;

	[SerializeField]
	private int animationLoops = 1;

	public int AnimationLoops {
		get {return animationLoops;}
		set {
			if (value == 0)
				Disable ();
			else
				animationLoops = value;
		}
	}

	void Disable ()
	{
		this.gameObject.SetActive (false);
	}

	[SerializeField]
	float nextTimeStep;

	[SerializeField]
	float changeIntervall = 0.1f;
	
	void OnEnable ()
	{
		currentId = 0;
	}

	void OnDisable ()
	{
		
	}

	void LateUpdate ()
	{
		if (Time.time >= nextTimeStep)
		{
			nextTimeStep = Time.time + changeIntervall;
			NextSprite ();
		}
	}

	void NextSprite ()
	{
		currentId++;
		if (limitAnimationLoops)
		{
			if (currentId >= sprites.Length)
			{
				AnimationLoops--;
				if (AnimationLoops == 0)
				{
					// stay on last frame
					currentId = sprites.Length -1;
				}
			}

		}

		currentId = currentId % sprites.Length;

//		if (AnimationLoops >0)
//		{
			if (sprites[currentId] != null)
				spriteRenderer.sprite = sprites[currentId];
//		}
	}


}
