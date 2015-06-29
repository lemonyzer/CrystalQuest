using UnityEngine;
using System.Collections;

public class CrystalQuestLevelManager : MonoBehaviour {

	static CrystalQuestLevelManager m_instance;

	public static CrystalQuestLevelManager Instance {
		get {return m_instance;}
	}

	void Awake ()
	{
		m_instance = this;
	}

	[SerializeField] float m_levelLeft = -10f;
	[SerializeField] float m_levelBottom = -5f;
	[SerializeField] float m_levelWidth = 20f;
	[SerializeField] float m_levelHeight = 10f;


	[SerializeField] Vector3 m_PlayerSpawn = new Vector3 (0f,0f,0f);
	[SerializeField] float m_PlayerSpawnSaveAreaRadius = 2f;


	public float LevelWidth
	{
		get {return m_levelWidth;}
	}

	public float LevelHeight
	{
		get {return m_levelHeight;}
	}

	public Vector3 GetRandomLevelPosition ()
	{
		Vector3 randomLevelPosition = new Vector3 ();
		randomLevelPosition.x = Random.Range (m_levelLeft,m_levelLeft+m_levelWidth);
		randomLevelPosition.y = Random.Range (m_levelBottom,m_levelBottom+m_levelHeight);
		randomLevelPosition.z = 0;
		return randomLevelPosition;
	}


	// wenn Spieler bereits in Scene existiert und aktiv ist - kann auch über Overlap abgefragt werden ob neue SpawnPos in diesem Bereich liegt.
	public Vector3 GetRandomLevelPositionWithoutPlayerSpawn ()
	{
		Vector3 randomLevelPosition = new Vector3 ();
		randomLevelPosition.x = Random.Range (m_levelLeft,m_levelLeft+m_levelWidth);
		randomLevelPosition.y = Random.Range (m_levelBottom,m_levelBottom+m_levelHeight);
		randomLevelPosition.z = 0;

		// check if position is in player spawn area
		if (randomLevelPosition.magnitude <= m_PlayerSpawnSaveAreaRadius)
		{
			// Spieler spawnt auf 0,0,0.
			// wenn länge von randSpawnPos < playerSpawnSaveAreaRadius ist liegt die position inerhalb des Spawn Bereichs des Spielers
			randomLevelPosition.Normalize();
//			Random.Range (m_PlayerSpawnSaveAreaRadius, Mathf.Min (m_levelWidth, m_levelHeight) * 0.5f);
			randomLevelPosition.x += Random.Range (m_PlayerSpawnSaveAreaRadius, m_levelWidth * 0.5f);
			randomLevelPosition.y += Random.Range (m_PlayerSpawnSaveAreaRadius, m_levelHeight * 0.5f);
		}

		// nöglichkeit 2
		//randomLevelPosition = Random.onUnitSphere

		return randomLevelPosition;
	}
}
