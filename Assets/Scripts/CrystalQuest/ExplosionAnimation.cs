using UnityEngine;
using System.Collections;

public class ExplosionAnimation : MonoBehaviour {

	[SerializeField]
	private Object m_eventDomain;
	public Object EventDomain {
		get {return m_eventDomain;}
		set {m_eventDomain = value;}
	}

	[SerializeField]
	AudioSource audioSource;

	void Awake ()
	{
		if (this.audioSource == null)
			this.audioSource = this.GetComponent<AudioSource> ();

	}

	[SerializeField]
	AudioClip explosionClip;

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
				AnimationFinished ();
			else
				animationLoops = value;
		}
	}

	public void Explode ()
	{
		this.audioSource.PlayOneShot (explosionClip);
	}

	void AnimationFinished ()
	{
		DomainEventManager.TriggerEvent (m_eventDomain, EventNames.OnExplosionFinish);		// TODO Achtung: mit unlimited Animation [x] wird event nicht getriggert!
																							// TODO wenn gameObject disabled ist kann ExplosionFinish Event nicht ausgeführt werden.
																							// Hier wird getriggert, explodiertes GO ist aber vermutlich schon disabled oder im pool bereis wieder verewndet worden, gefährliheS EVENT!!! 
		Disable ();
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
