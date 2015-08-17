using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	private static AudioManager m_instance;

	public static AudioManager Instance {
		get {return m_instance;}
	}

	void Awake () {
		m_instance = this;
	}

	[SerializeField]
	private AudioSource myAudioSource;

	public void PlayClip (AudioClip clip)
	{
		if (myAudioSource != null)
		{
			if (clip != null)
				myAudioSource.PlayOneShot (clip);
        }
    }
}
