using UnityEngine;
using System.Collections;

public class PooledObject : MonoBehaviour {

	[SerializeField]
	MyEvent reactivated;

	public void Reactivated ()
	{
		OnReactivated ();
	}

	void OnReactivated ()
	{
		NotifyReactivatedObserver ();
	}

	void NotifyReactivatedObserver ()
	{
		reactivated.Invoke ();
	}

	void NotExecuted ()
	{
		reactivated = null;
	}
}
