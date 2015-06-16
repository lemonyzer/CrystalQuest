using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour {

//	[SerializeField]
//	private bool setPositionEnabled = true;

	[SerializeField]
	private Vector3 reactivatePos;

//	[SerializeField]
//	private bool spawnDelayEnable = true;

	[SerializeField]
	private float m_ReactivateDelay = 2f;

	[SerializeField]
	MyEvent activated;

	public delegate void DelayedReactivateRequest (GameObject my, RespawnScript me, float delay);
	public static event DelayedReactivateRequest onDelayedReactivateRequest;

	public delegate void ReactivateRequest (GameObject me);
	public static event ReactivateRequest onReactivateRequest;

	public void PushActivatedEvent ()
	{
		activated.Invoke ();
	}

	public void Activate ()
	{
		this.transform.position = reactivatePos;
		this.gameObject.SetActive (true);
		activated.Invoke ();
	}

	public void ActivateInstant ()
	{
		Activate ();
	}

	public void ActivateWithDelay ()
	{
		StartCoroutine (WaitAndSpawn (m_ReactivateDelay));
	}

	public void ActivateWithDelay (float delay)
	{
		if (onDelayedReactivateRequest != null)
			onDelayedReactivateRequest (gameObject, this, delay);
		else
			Debug.LogError ("no \"onDelayedReactivateRequest\" listener!");
//		if (delay < 0f)
//			ActivateInstant ();
//		else
//			StartCoroutine (WaitAndSpawn (delay));
	}

	// TODO funktioniert nicht wenn GameObject deaktiviert wurde
	IEnumerator WaitAndSpawn(float waitTime) {
		Debug.Log ("Start spawn Delay...");
		yield return new WaitForSeconds(waitTime);
		Debug.Log ("Spawn Delay Done, trigger spawning...");
		Activate ();
	}
}
