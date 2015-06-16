using UnityEngine;
using System.Collections;

public class SmartBombManager : MonoBehaviour {

	private static SmartBombManager m_Instance;

	public static SmartBombManager Instance {
		get {return m_Instance;}
		private set {m_Instance = value;}
	}

	void Awake ()
	{
		m_Instance = this;
	}

	public delegate void Bombing ();
	public static event Bombing onSmartBombing;

	// kann durch Singleton Pattern ausgeführt werden
	public void BombTriggered ()
	{
		if (onSmartBombing != null)
			onSmartBombing ();
		else
			Debug.LogError ("no \"onSmartBombing\" listener");
	}
}
