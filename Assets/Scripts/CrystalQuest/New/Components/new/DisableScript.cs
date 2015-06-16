using UnityEngine;
using System.Collections;

public class DisableScript : MonoBehaviour {

	public void Disable ()
	{
		this.gameObject.SetActive (false);
	}
}
