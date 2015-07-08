using UnityEngine;
using System.Collections;

public class LaserProjectile : Projectile {

	[SerializeField]
	BoxCollider2D boxCollider;

//	[SerializeField]
//	float speed;

	[SerializeField]
	public new Transform transform;

//	[SerializeField]
	Vector3 startScale;

	protected override void Awake ()
	{
		base.Awake ();
		transform = this.GetComponent<Transform>();

		startScale = this.transform.localScale;

		if (boxCollider == null)
			boxCollider = this.GetComponent<BoxCollider2D>();
	}

	public override void ReleasedWithVelocity (Vector3 direction, float speed)
	{
		Release ();		// destruct timer starten

		// reset used laser beams to startScale
		this.transform.localScale = startScale;

		// rotate laser around its center in release direction
		direction.Normalize ();

		Rotate (direction);
		this.releaseForce = speed;
	}

	[SerializeField]
	float angleOffset = -90;
	void Rotate (Vector3 direction)
	{
		Vector3 relativePos = new Vector3 (direction.x, 0, 0);
		transform.rotation = Quaternion.LookRotation(relativePos, new Vector3(0,0,0));
		
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		angle += angleOffset;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void FixedUpdate ()
	{
		Grow ();
	}

	void Grow ()
	{
		Vector3 localScale = this.transform.localScale;
		localScale.x = localScale.x + releaseForce * Time.fixedDeltaTime;
		this.transform.localScale = localScale;
	}
}
