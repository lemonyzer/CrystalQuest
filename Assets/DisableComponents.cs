using UnityEngine;
using System.Collections;

public class DisableComponents : MonoBehaviour {

	[SerializeField]
	BoolEvent components;

	public void Disable ()
	{
		components.Invoke (false);
	}

	public void Enable ()
	{
		components.Invoke (true);
	}
}
