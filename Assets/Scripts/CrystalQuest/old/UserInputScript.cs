﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class UserInputScript : MonoBehaviour {

	[SerializeField]
	ShootingPooled shootingPooled;

	[SerializeField]
	bool m_InputFire;

	private Vector2 m_Input;

	public Vector2 GetInput()
	{
		return m_Input;
	}

	public float GetHorizontal()
	{
		return m_Input.x;
	}

	public float GetVertical()
	{
		return m_Input.y;
	}

	// Update is called once per frame
	void Update () {

		m_Input.x = CrossPlatformInputManager.GetAxis ("Horizontal");
		m_Input.y = CrossPlatformInputManager.GetAxis ("Vertical");

		m_InputFire = CrossPlatformInputManager.GetButton ("Fire");

		if (m_InputFire)
			shootingPooled.TriggerShoot (m_Input);
	}

	[SerializeField]
	Rigidbody2D rb2d;

	[SerializeField]
	float speed = 5;

	void Awake ()
	{
		rb2d = this.GetComponent<Rigidbody2D>();
		shootingPooled = this.GetComponent<ShootingPooled>();
	}

	void FixedUpdate ()
	{
		rb2d.MovePosition (rb2d.position + m_Input * speed * Time.deltaTime);
	}
}
