using UnityEngine;
using System.Collections;

public class PortalGate : MonoBehaviour {

	[SerializeField]
	SpriteRenderer spriteRenderer;
	[SerializeField]
	Collider2D collider2d;
	
	void Awake ()
	{
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		collider2d = this.GetComponent<Collider2D>();
	}
	
	public void CloseGate ()
	{
		spriteRenderer.enabled = true;
		collider2d.enabled = true;
	}
	
	public void OpenGate ()
	{
		//		foreach (Component component in toggleComponents)
		//		{
		//			Debug.Log(component.GetType ());
		//		}
		spriteRenderer.enabled = false;
		collider2d.enabled = false;
	}
}
