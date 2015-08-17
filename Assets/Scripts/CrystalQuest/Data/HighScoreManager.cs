using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
	public static HighScoreManager Instance;

	[SerializeField]
	private ScoreManager scoreManager;

	[SerializeField]
	private UIWaveTime waveTime;

	[SerializeField]
	SingleHighScore currentScore;

	[SerializeField]
	int maxHighScoreSize = 5;

	[SerializeField]
	List<SingleHighScore> highScoreList;

	void Awake ()
	{
		Instance = this;
	}

	public List<SingleHighScore> GetHighScoreList ()
	{
		try {
			highScoreList =  Load ();
		}
		catch (Exception e)
		{
			Debug.LogError (e);
		}
		return highScoreList;
	}

	void OnEnable ()
	{
		if (DomainEventManager.instance != null)
		{
			DomainEventManager.StartGlobalListening (EventNames.PlayerGameOver, OnGameOver);
			DomainEventManager.StartGlobalListening (EventNames.AllWavesCompleted, OnGameOver);
//			DomainEventManager.StartGlobalListening (EventNames.OnGameOver, OnGameOver);
		}
	}

	void OnDisable ()
	{
		if (DomainEventManager.instance != null)
		{
			DomainEventManager.StopGlobalListening (EventNames.PlayerGameOver, OnGameOver);
			DomainEventManager.StopGlobalListening (EventNames.AllWavesCompleted, OnGameOver);
//		    DomainEventManager.StopGlobalListening (EventNames.OnGameOver, OnGameOver);
		}
	}

	void OnGameOver ()
	{
		if (scoreManager == null)
		{
			Debug.LogError ("need ScoreManager ref.");
			return;
		}
		if (waveTime == null)
		{
			Debug.LogError ("need UIWaveTime ref.");
			return;
		}

		currentScore.Score = scoreManager.GetScore ();
		currentScore.PlayTime = waveTime.GetPlayTime ();
		currentScore.Wave = CrystalQuestWaveManager.Instance.GetCurrentWave ().waveName;
		
		UpdateHighScores ();
	}


	const string highScoreFile = "highscore.dat";

	public static string GetHighScoreFilePath ()
	{
		return Application.persistentDataPath + "/" + highScoreFile;
	}

	void HighScoreLog (List<SingleHighScore> highscores)
	{
		for (int i = 0; i< highscores.Count; i++)
		{
			Debug.Log ("["+i+"] Punkte: " + highscores[i].Score );
		}
	}

	void UpdateHighScores ()
	{
		List<SingleHighScore> highscores = null;
		try {
			highscores =  Load ();
		}
		catch (Exception e)
		{
			Debug.LogError (e);
		}

		if (highscores != null)
		{
			// Highscores schon vorhanden

			// neue Highscore hinzufpgen
			highscores.Add (currentScore);

			#if UNITY_EDITOR
			// HighScore Log
			Debug.Log ("unsortiert");
			HighScoreLog (highscores);
			#endif 

			// HighScore sortieren
			highscores.Sort ();

			#if UNITY_EDITOR
			// HighScore Log
			Debug.Log ("sortiert");
			HighScoreLog (highscores);
			#endif 

			// remove scores, if list amount > highscoreSize
//			highscores.RemoveRange (maxHighScoreSize, highscores.Count);
			#if UNITY_EDITOR
			// HighScore Log
			Debug.Log ("sortiert");
			HighScoreLog (highscores);
			#endif 
		}
		else
		{
			// Highscores noch nicht vorhanden
			highscores = new List<SingleHighScore> ();
			highscores.Add (currentScore);
		}

		try {
			Save (highscores);
		}
		catch (Exception e)
		{
			Debug.LogError (e);
		}
	}

	void Save (List<SingleHighScore> highscores)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (GetHighScoreFilePath());

//		List<SingleHighScore> highscores = new List<SingleHighScore> ();
//		highscores.Add ();

		bf.Serialize (file, highscores);
		file.Close ();
	}

	List<SingleHighScore> Load () 
	{
		if (File.Exists (GetHighScoreFilePath ()))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (GetHighScoreFilePath(), FileMode.Open);

			List<SingleHighScore> highscores = new List<SingleHighScore> ();
			highscores = (List<SingleHighScore>) bf.Deserialize (file);
			file.Close ();

			return highscores;
		}

		return null;
	}

}

[System.Serializable]
public class HighScore {
	List<SingleHighScore> highScores;
}

[System.Serializable]
public class SingleHighScore : IComparable<SingleHighScore> {

	[SerializeField]
	float score;

	[SerializeField]
	float playTime;

	[SerializeField]
	string wave;

	public float Score {
		get {return score;}
		set {score = value;}
	}

	public float PlayTime {
		get {return playTime;}
		set {playTime = value;}
	}

	public string Wave {
		get {return wave;}
		set {wave = value;}
	}

	public int CompareTo(SingleHighScore other)
	{
		if ( this.Score < other.Score ) return 1;
		else if ( this.Score > other.Score ) return -1;
		else return 0;
	}
}
