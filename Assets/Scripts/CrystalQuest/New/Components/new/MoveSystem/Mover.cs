﻿using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	[SerializeField]
	Vector2 m_moveDirection;
	
	public Vector2 MoveDirection {
		get {return m_moveDirection;}
		set {m_moveDirection = value;}
	}

	[SerializeField]
	float m_Speed = 5f;
	
	[SerializeField]
	float m_DefaultSpeed = 5f;
	
	[SerializeField]
	float m_MaxSpeed = 5f;
	
	[SerializeField]
	float m_MinSpeed = 5f;
	
	public float Speed {
		get {return m_Speed;}
		set {
			if (value >= m_MaxSpeed)
				m_Speed = m_MaxSpeed;
			else if (value <= m_MinSpeed)
				m_Speed = m_MinSpeed;
			else
				m_Speed = value;
		}
	}
	
	[SerializeField]
	bool moveEnabled = true;

	public bool MoveEnabled {
		get {return moveEnabled;}
		set {moveEnabled = value;}
	}

}