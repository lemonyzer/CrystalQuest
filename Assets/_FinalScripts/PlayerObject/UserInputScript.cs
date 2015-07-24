using UnityEngine;
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


		if (!readInput)
		{
			if (overrideInput)
			{
				m_Input = overrideDirection;
				m_InputFire = overrideShoot;
			}
			return;
		}

		m_Input.x = CrossPlatformInputManager.GetAxis ("Horizontal");
		m_Input.y = CrossPlatformInputManager.GetAxis ("Vertical");

		m_InputFire = CrossPlatformInputManager.GetButton ("Fire");

		if (m_InputFire)
			shootingPooled.TriggerShoot (m_Input);
	}

	// Use this for initialization
	void OnEnable () {
		DomainEventManager.StartGlobalListening (EventNames.WavePreInit, OnWavePreInit);
		DomainEventManager.StartGlobalListening (EventNames.WaveStart, OnWaveStart);
	}
	
	// Update is called once per frame
	void OnDisable () {
		DomainEventManager.StopGlobalListening (EventNames.WavePreInit, OnWavePreInit);
		DomainEventManager.StopGlobalListening (EventNames.WaveStart, OnWaveStart);
	}

	[SerializeField]
	bool readInput = true;
	[SerializeField]
	bool overrideInput = false;
	[SerializeField]
	Vector2 overrideDirection = Vector2.zero;
	[SerializeField]
	bool overrideShoot = false;

	public void StopInputMovement ()
	{
		readInput = false;
		overrideInput = true;
	}

	public void StartInputReading ()
	{
		readInput = true;
		overrideInput = false;
	}

	void OnWavePreInit ()
	{
		StopInputMovement ();
	}

	void OnWaveStart ()
	{
		StartInputReading ();
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
//		if (Time.deltaTime > 0.02f)
//			Debug.Log (Time.deltaTime);
	}
}
