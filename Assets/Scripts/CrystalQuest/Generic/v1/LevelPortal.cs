using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelPortal : CollisionObject {

	[SerializeField]
	SpriteRenderer spriteRenderer;
	[SerializeField]
	Collider2D collider2d;
	[SerializeField]
	protected bool sendCollisionDamageEnabled = true;

//	// TODO list of Objects that get enabled/disabled
//	[SerializeField]
//	protected List<Component> enableComponents;
//	[SerializeField]
//	protected List<Component> disableComponents;
//	[SerializeField]
//	protected List<Component> toggleComponents;

	protected override void Awake ()
	{
		base.Awake ();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		collider2d = this.GetComponent<Collider2D>();
	}

	// TODO how to Test??!!

	public void CloseGate ()
	{
		spriteRenderer.enabled = true;
		sendCollisionDamageEnabled = true;
		collider2d.enabled = true;
	}

	public void OpenGate ()
	{
//		foreach (Component component in toggleComponents)
//		{
//			Debug.Log(component.GetType ());
//		}
		spriteRenderer.enabled = false;
		sendCollisionDamageEnabled = false;
		collider2d.enabled = false;
	}

	public override void ApplyCollisionDamage (CollisionObject otherObjectScript)
	{
		//base.ApplyCollisionDamage (otherObjectScript);
		// no Damage
	}

	public override void SendCollisionDamage (CollisionObject otherObjectScript)
	{
		if (sendCollisionDamageEnabled)
			base.SendCollisionDamage (otherObjectScript);
	}
}
