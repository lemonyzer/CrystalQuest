using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour {

	void NotExecuted ()
	{
		reactivatePos = Vector3.zero;
		myActivatedEvent = null;
	}

//	[SerializeField]
//	private bool setPositionEnabled = true;

	[SerializeField]
	private Vector3 reactivatePos;

//	[SerializeField]
//	private bool spawnDelayEnable = true;

	[SerializeField]
	private float m_ReactivateDelay = 2f;

	[SerializeField]
	MyEvent myActivatedEvent;

//	void OnEnable ()
//	{
//		DomainEventManager.StartListening (this.gameObject, EventNames.RespawnTrigger, ActivateWithDelay);
//	}
//
//	void OnDisable ()
//	{
//		DomainEventManager.StopListening (this.gameObject, EventNames.RespawnTrigger, ActivateWithDelay);
//	}

	public delegate void DelayedReactivateRequest (GameObject my, RespawnScript me, float delay);
	public static event DelayedReactivateRequest onDelayedReactivateRequest;

//	public delegate void ReactivateRequest (GameObject me);
//	public static event ReactivateRequest onReactivateRequest;

	public void PushActivatedEvent ()
	{
		myActivatedEvent.Invoke ();
	}

	public void Activate ()
	{
		this.transform.position = reactivatePos;
		this.gameObject.SetActive (true);

		Rigidbody2D myRigibody2d = this.gameObject.GetComponent<Rigidbody2D>();
		if (myRigibody2d != null)
			myRigibody2d.WakeUp ();

		myActivatedEvent.Invoke ();
//		DomainEventManager.TriggerEvent (this.gameObject, EventNames.OnRespawned);
	}

	public void ActivateInstant ()
	{
		Activate ();
	}

	// TODO funktioniert nicht wenn GameObject deaktiviert wurde
//	public void ActivateWithDelay ()
//	{
////		DomainEventManager.TriggerGlobalEvent (EventNames.OnDelayedReactivateRequest);
//		StartCoroutine (WaitAndSpawn (m_ReactivateDelay));								// doesnt w
//	}

	public void ActivateWithDelay ()
	{
		if (onDelayedReactivateRequest != null)
			onDelayedReactivateRequest (gameObject, this, m_ReactivateDelay);
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
