using UnityEngine;
using System.Collections;

public class UserInputScript : MonoBehaviour {

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

		m_Input.x = Input.GetAxis("Horizontal");
		m_Input.y = Input.GetAxis("Vertical");

	}
}
