using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Rigidbody2DMovement : Mover {

	// public static Rigidbody2DMovement

//	[SerializeField]
//	Vector2 m_moveDirection;
//
//	public Vector2 MoveDirection {
//		get {return m_moveDirection;}
//		set {m_moveDirection = value;}
//	}
//
//	[SerializeField]
//	float m_Speed = 5f;
//	
//	[SerializeField]
//	float m_DefaultSpeed = 5f;
//
//	[SerializeField]
//	float m_MaxSpeed = 5f;
//
//	[SerializeField]
//	float m_MinSpeed = 5f;
//
//	public float Speed {
//		get {return m_Speed;}
//		set {
//			if (value >= m_MaxSpeed)
//				m_Speed = m_MaxSpeed;
//			else if (value <= m_MinSpeed)
//				m_Speed = m_MinSpeed;
//			else
//				m_Speed = value;
//			}
//	}
//
//	[SerializeField]
//	bool moveEnabled = true;
//
//	public bool MoveEnabled {
//		get {return moveEnabled;}
//		set {moveEnabled = value;}
//	}

	[SerializeField]
	Rigidbody2D rb2d;

	Rigidbody2D InitRigidbody2D (Rigidbody2D rb2d)
	{
		if (rb2d != null)
			return rb2d;
		else
		{
			rb2d = this.GetComponent<Rigidbody2D>();
			if (rb2d == null)
			{
				Debug.LogError (this.ToString () + " kein Rigibody2D vorhanden");
			}
			return rb2d;
		}
	}

	void Awake ()
	{
		rb2d = InitRigidbody2D (rb2d);
	}

	void FixedUpdate ()
	{
		if (MoveEnabled)
		{
			rb2d.MovePosition (MoveDirection * Speed);
		}
	}

}
