using UnityEngine;
using System.Collections;

[System.Serializable]
public class IdleMovement : Movement {

	public IdleMovement (Transform myTransform) : base (myTransform)
	{
		
	}

	public override Vector2 NextMoveDirection ()
	{
		return base.NextMoveDirection ();
	}
}
