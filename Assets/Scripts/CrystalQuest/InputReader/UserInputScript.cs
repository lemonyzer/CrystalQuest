using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public enum StandaloneInput {
	Keyboard,
	Mouse
}

public class UserInputScript : MonoBehaviour {

	static UserInputScript instance;

	public UserInputScript Instance {
		get {return instance;}
	}

	public StandaloneInput StandaloneInput {
		get {return standaloneInput;}
	}

	[SerializeField]
	StandaloneInput standaloneInput = StandaloneInput.Mouse;

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

#if UNITY_STANDALONE
		switch (standaloneInput) {
		case (StandaloneInput.Mouse): {
			m_Input.x = Input.GetAxis ("Mouse X");
			m_Input.y = Input.GetAxis ("Mouse Y");
			break;
		}
		case (StandaloneInput.Keyboard): {
			m_Input.x = CrossPlatformInputManager.GetAxis ("Horizontal");
			m_Input.y = CrossPlatformInputManager.GetAxis ("Vertical");
			break;
		}
		}
#endif
#if UNITY_ANDROID
		m_Input.x = CrossPlatformInputManager.GetAxis ("Horizontal");
		m_Input.y = CrossPlatformInputManager.GetAxis ("Vertical");
		
#endif

		m_InputFire = CrossPlatformInputManager.GetButton ("Fire");

#if UNITY_STANDALONE
		switch (standaloneInput) {
		case (StandaloneInput.Mouse): {
			if (m_InputFire)
				shootingPooled.TriggerShoot (rb2d.velocity);
			break;
		}
		case (StandaloneInput.Keyboard): {
			if (m_InputFire)
				shootingPooled.TriggerShoot (m_Input);
			break;
		}
		}
#endif
#if UNITY_ANDROID
		if (m_InputFire) {
			shootingPooled.TriggerShoot (m_Input);
		}
#endif

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

	[SerializeField]
	float forceMultiplier = 20;

	void Awake ()
	{
		rb2d = this.GetComponent<Rigidbody2D>();
		shootingPooled = this.GetComponent<ShootingPooled>();

		instance = this;
	}

	void FixedUpdate ()
	{
		#if UNITY_ANDROID
		rb2d.MovePosition (rb2d.position + m_Input * speed * Time.deltaTime);
		#endif

		#if UNITY_STANDALONE
		switch (standaloneInput) {
		case (StandaloneInput.Mouse): {
			rb2d.AddForce (m_Input * speed * forceMultiplier * Time.deltaTime);
			break;
		}
		case (StandaloneInput.Keyboard): {
			rb2d.MovePosition (rb2d.position + m_Input * speed * Time.deltaTime);
			break;
		}
		}
		#endif
//		if (Time.deltaTime > 0.02f)
//			Debug.Log (Time.deltaTime);
	}
}
