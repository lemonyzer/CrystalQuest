using UnityEngine;
using System.Collections;

public class BouncyAudio : MonoBehaviour {

	[SerializeField]
	AudioClip bouncyClip;

	[SerializeField]
	AudioSource audioSource;

	public void PlayBouncyClip ()
	{
		if (audioSource != null && bouncyClip != null)
			audioSource.PlayOneShot (bouncyClip);
		else
			Debug.LogError (this.ToString () + " audioSource or bouncyClip not set!");
	}

	void OnCollisionEnter2D (Collision2D collision2d)
	{
		PlayBouncyClip ();
	}
}
