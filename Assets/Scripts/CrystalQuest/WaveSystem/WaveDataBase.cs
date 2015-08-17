using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveDataBase : MonoBehaviour {

//	[SerializeField]
//	List<Wave> waves;

	[SerializeField]
	WaveSystem waves;

	static WaveDataBase m_instance;
	
	public static WaveDataBase Instance {
		get {return m_instance;}
	}

	void Awake ()
	{
		m_instance = this;
//		if (waves == null)
//			waves = new List<Wave>();
	}

	public Wave GetWave(int index)
	{
		if (0<=index && index < waves.waveList.Count)
			return waves.waveList[index];
		else
			return null;
	}

	public int GetNumberOfWaves ()
	{
		return waves.waveList.Count;
	}
}
