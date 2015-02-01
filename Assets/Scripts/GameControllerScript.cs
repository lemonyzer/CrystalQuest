using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

	public AudioClip newHighScoreClip;
	public AudioClip gateOpenClip;

	public GameObject PrefabCrystal;
	public GameObject PrefabPlayer;
	public GameObject PrefabEnemy;

	private GameObject Player;

	public GameObject GetPlayerGO() {
		return Player;
	}

	public Text UITextScore;
	public Text UITextTotalEnemyKills;
	public Text UITextActiveEnemys;
	public Text UITextCurrentKillCombo;
	public Text UITextCrystals;
	public Text UITextLevelInfo;

	public int minCrystalQuantity = 3;
	public int currentLevelNumber = 1;
	public int currentLevelCrystalQuantity = 0;

	public int CrystalScoreValue = 1000;
	public int EnemyKillScoreValue = 100;
	


	public int currentPlayerCollectedCrystals = 0;
	public int currentPlayerTotalEnemyKills = 0;
	public int currentPlayerScore = 0;
	
	public float KillComboIntervalLength = 2.0f;
	public float currentKillComboInterval = 0f;
	public int currentPlayerKillComboCount = 0;
	public int currentPlayerMaxKillComboCount = 0;
	public bool killComboActive = false;

	public float CrystalComboIntervalLength = 2.5f;
	public float currentCrystalComboInterval = 0f;
	public int currentPlayerCrystalCombo = 0;

	float currentRoundDuration = 0f;

	void Awake () {

	}

	// Use this for initialization
	void Start () {
		InitLevel ();
		StartCoroutine (WaitAndResume(freezTime));
	}

	public void ButtonPlay()
	{
		GamingView ();
		ResetToLevelZero ();
		Start ();
		Application.LoadLevel (Application.loadedLevel);
	}

	public void ResetToLevelZero()
	{
		currentLevelNumber = 1;
		currentPlayerTotalEnemyKills = 0;
		currentPlayerCollectedCrystals = 0;
		currentPlayerKillComboCount = 0;
		currentPlayerMaxKillComboCount = 0;
		currentPlayerCrystalCombo = 0;
		currentPlayerScore = 0;
		int startHealth = 100;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Collisions> ().currentHealth = startHealth;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Collisions> ().healthSlider.value = startHealth;
	}

	public void InitLevel()
	{
		CalculateCrystalQuantity ();
		SpawnCrystals (currentLevelCrystalQuantity);
	}
	
	// Update is called once per frame
	void Update () {

		currentRoundDuration += Time.deltaTime;

		SetCrystalCountText (currentPlayerCollectedCrystals, currentLevelCrystalQuantity);
		UITextLevelInfo.text = "Level: " + currentLevelNumber + "\nCrystals: " + currentLevelCrystalQuantity;
		UITextScore.text = "Score: " + currentPlayerScore.ToString ();
		UITextCurrentKillCombo.text = "KillCombo: " + currentPlayerKillComboCount.ToString ()+"x";
		UITextTotalEnemyKills.text = "Total Kills: " + currentPlayerTotalEnemyKills.ToString ();
		UITextActiveEnemys.text = "Enemys: " + GameObject.FindGameObjectsWithTag ("Enemy").Length.ToString ();

		if(killComboActive)
		{
			currentKillComboInterval -= Time.deltaTime;
			if (currentKillComboInterval <= 0) {
				// current combo complete/finished
				currentPlayerScore += currentPlayerKillComboCount*EnemyKillScoreValue + currentPlayerKillComboCount*EnemyKillScoreValue/10;
				currentPlayerKillComboCount = 0;
				killComboActive = false;
			}
		}
		
		
		
	}

	public Vector3 GetRandomPosInLevelArea()
	{
		return new Vector3(Random.Range(LevelScript.left,LevelScript.right),
		                   Random.Range(LevelScript.bottom,LevelScript.top),
		                   0);
	}

	public void RemoveEnemys()
	{
		GameObject[] enemys = GameObject.FindGameObjectsWithTag ("Enemy");
		
		int currentEnemyCount = enemys.Length;
		
		for(int i=0; i<currentEnemyCount; i++)
		{
			Destroy(enemys[i]);
		}
		
		//		Destroy (enemys);
	}

	public void nextLevel()
	{
		currentPlayerCollectedCrystals = 0;


		DisableEnemySpawn ();	// disable Enemyspawn
		RemoveEnemys ();		// remove Enemys
		DisablePowerUpSpawn ();
		RemovePowerUps ();		// remove PowerUps
		RemoveCrystals ();
		RemoveBullets ();
		currentLevelNumber++;	// count Levelnumber up
		CalculateCrystalQuantity (); // calculate new Crystal Quantity
		SpawnCrystals (currentLevelCrystalQuantity); // SpawnCrystals

		DisablePlayerMovement ();
		ResetPlayerPos ();

		CloseNextLevelGate ();
		// animation (levelübergang)
		// alle gegner entfernen
		// crystals spawnen
		// player auf startPos setzen
		// gegner nach freeztime/cooldown spawnen
		StartCoroutine (WaitAndResume (freezTime));
	}

	public static string MAXLEVEL = "MaxLevel";
	public static string HIGHSCORE = "HighScore";
	public static string PLAYTIME = "PlayTime";
	public static string MAXCOMBO = "MaxCombo";
	public static string LONGESTROUND = "MaxRoundTime";
	public static string LONGESTSTAGE = "MaxStageTime";
	public static string ROUNDSCOUNT = "Rounds";
	public static string TOTALKILLS = "TOTALKILLS";
	public static string ROUNDTOTALKILLS = "ROUNDTOTALKILLS";

	public void CompareStats() {
	
		int lastMaxLevel = 0;
		int lastHightScore = 0;
		int lastMaxCombo = 0;
		float lastPlayTime = 0f;
		int lastRoundsCount = 0;
		int lastTotalKills = 0;
		int lastTotalRoundKills = 0;

		try {
			lastMaxLevel = PlayerPrefs.GetInt(MAXLEVEL);
			lastHightScore = PlayerPrefs.GetInt(HIGHSCORE);
			lastMaxCombo = PlayerPrefs.GetInt(MAXCOMBO);
			lastPlayTime = PlayerPrefs.GetFloat(PLAYTIME);
			lastRoundsCount = PlayerPrefs.GetInt(ROUNDSCOUNT);
			lastTotalKills = PlayerPrefs.GetInt(TOTALKILLS);
			lastTotalRoundKills = PlayerPrefs.GetInt(ROUNDTOTALKILLS);

		} catch ( UnityException exp) 
		{
			Debug.LogException(exp);
		}

		if(lastMaxLevel < currentLevelNumber)
		{
			audio.PlayOneShot(newHighScoreClip);
			PlayerPrefs.SetInt(MAXLEVEL, currentLevelNumber);
			ScoreboardMaxLevel.text = "Max Level: " +  currentLevelNumber.ToString();
		}
		else
			ScoreboardMaxLevel.text = "Max Level: " +  lastMaxLevel.ToString();			

		if (lastHightScore < currentPlayerScore)
		{
			PlayerPrefs.SetInt(HIGHSCORE, currentPlayerScore);
			ScoreboardScore.text = "High Score: " +  currentPlayerScore.ToString();
		}
		else
			ScoreboardScore.text = "High Score: " +  lastHightScore.ToString();

		if (lastMaxCombo < currentPlayerMaxKillComboCount)
		{
			PlayerPrefs.SetInt(MAXCOMBO, currentPlayerMaxKillComboCount);
			ScoreboardMaxCombo.text = "Highest Combo: " + currentPlayerMaxKillComboCount.ToString();
			
		}
		else
			ScoreboardMaxCombo.text = "Highest Combo: " + lastMaxCombo.ToString();

		PlayerPrefs.SetFloat (PLAYTIME, lastPlayTime + currentRoundDuration);
		PlayerPrefs.SetInt (ROUNDSCOUNT, lastRoundsCount++);
		PlayerPrefs.SetInt (TOTALKILLS, lastTotalKills+currentPlayerTotalEnemyKills);
		ScoreboardTotalKills.text = "Total Kills: " + (lastTotalKills+currentPlayerTotalEnemyKills).ToString();

		if (lastTotalRoundKills < currentPlayerTotalEnemyKills)
		{
			PlayerPrefs.SetInt(ROUNDTOTALKILLS, currentPlayerTotalEnemyKills);
		}
	
	}

	public void SaveStats() {
		//duration (TIME)
//		currentLevelNumber;
//		currentPlayerScore;
//		int currentPlayerHighestKillCombo;
//		int currentPlayerBombsUsed;
//		int currentPlayerHighestSingleBombKillCombo;
//		currentPlayerTotalEnemyKills;
	}

	public void ResetStats()
	{
		PlayerPrefs.SetInt (MAXLEVEL, 0);
		PlayerPrefs.SetInt (HIGHSCORE, 0);
		PlayerPrefs.SetInt (MAXCOMBO, 0);
		PlayerPrefs.SetFloat (PLAYTIME, 0.0f);
		PlayerPrefs.SetInt (TOTALKILLS,0);
		PlayerPrefs.SetInt (ROUNDSCOUNT,0);
		PlayerPrefs.SetInt (ROUNDTOTALKILLS,0);
	}

	public void LoadStats()
	{
		int lastMaxLevel = 0;
		int lastHightScore = 0;
		int lastMaxCombo = 0;
		float lastPlayTime = 0f;
		int lastRoundsCount = 0;
		int lastTotalKills = 0;
		int lastTotalRoundKills = 0;

		try {
			lastMaxLevel = PlayerPrefs.GetInt(MAXLEVEL);
			lastHightScore = PlayerPrefs.GetInt(HIGHSCORE);
			lastMaxCombo = PlayerPrefs.GetInt(MAXCOMBO);
			lastPlayTime = PlayerPrefs.GetFloat(PLAYTIME);
			lastRoundsCount = PlayerPrefs.GetInt(ROUNDSCOUNT);
			lastTotalKills = PlayerPrefs.GetInt(TOTALKILLS);
			lastTotalRoundKills = PlayerPrefs.GetInt(ROUNDTOTALKILLS);
			
		} catch ( UnityException exp) 
		{
			Debug.LogException(exp);
		}

		ScoreboardMaxLevel.text = "Max Level: " +  lastMaxLevel.ToString();
		ScoreboardScore.text = "High Score: " +  lastHightScore.ToString();
		ScoreboardMaxCombo.text = "Highest Combo: " + lastMaxCombo.ToString();
		ScoreboardTotalKills.text = "Total Kills: " + (lastTotalKills+currentPlayerTotalEnemyKills).ToString();
	}

	public void StopGame()
	{
		Time.timeScale = 0;
	}

	public void PlayerDied()
	{
		DisablePlayerMovement ();
		ResetPlayerPos ();		// center Player
		DisableEnemySpawn ();	// disable Enemy spawn
		RemoveEnemys ();		// remove Enemys
		DisablePowerUpSpawn ();	// disable PowerUp spawn
		RemovePowerUps ();		// remove PowerUps
		RemoveCrystals ();
		RemoveBullets ();

		ShowScoreBoard ();
	}

	public void ShowScoreBoard()
	{
		StopGame ();
		CompareStats ();
		ScoreboardView ();
	}

	public GameObject ScoreBoardCanvas;
	public GameObject GamingCanvas;

	public Text ScoreboardMaxLevel;
	public Text ScoreboardScore;
	public Text ScoreboardTotalKills;
	public Text ScoreboardMaxCombo;


	public void ScoreboardView()
	{
		DisableGamingUI ();
		EnableScoreBoardUI ();
	}

	public void EnableScoreBoardUI ()
	{
		ScoreBoardCanvas.SetActive (true);
	}

	public void DisableScoreBoardUI ()
	{
		ScoreBoardCanvas.SetActive (false);
	}

	public void DisableGamingUI ()
	{
		GamingCanvas.SetActive (false);
	}

	public void EnableGamingUI ()
	{
		GamingCanvas.SetActive (true);
	}

	public void GamingView()
	{
		DisableScoreBoardUI ();
		EnableGamingUI ();
	}

	public float freezTime = 2.0f;

	public void levelStart()
	{
		EnablePlayerMovement ();
		EnableEnemySpawn ();
		EnablePowerUpSpawn ();
	}

	IEnumerator WaitAndResume(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		levelStart ();
		print("WaitAndPrint " + Time.time);
	}
	
	public void DisablePlayerMovement()
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Accelerator> ().translationEnabled = false;
	}

	
	public void EnablePlayerMovement()
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Accelerator> ().translationEnabled = true;
	}

	public void ResetPlayerPos()
	{
		GameObject.FindGameObjectWithTag ("Player").transform.position = Vector3.zero;
	}
	
	public void DisableEnemySpawn()
	{
		GetComponent<SpawnEnemy> ().SpawnEnemyEnable = false;
	}

	public void EnableEnemySpawn()
	{
		GetComponent<SpawnEnemy> ().SpawnEnemyEnable = true;
	}

	public void DisablePowerUpSpawn() 
	{
		GetComponent<SpawnPowerUp> ().SpawnPowerUpEnable = false;
	}

	public void EnablePowerUpSpawn() 
	{
		GetComponent<SpawnPowerUp> ().SpawnPowerUpEnable = true;
	}

	public void RemovePowerUps ()
	{
		GameObject[] powerUps = GameObject.FindGameObjectsWithTag ("PowerUp");
		
		int currentPowerUpCount = powerUps.Length;
		
		for(int i=0; i<currentPowerUpCount; i++)
		{
			Destroy(powerUps[i]);
		}
	}

	public void RemoveCrystals ()
	{
		GameObject[] crystals = GameObject.FindGameObjectsWithTag ("Crystal");
		
		int currentPowerUpCount = crystals.Length;
		
		for(int i=0; i<currentPowerUpCount; i++)
		{
			Destroy(crystals[i]);
		}
	}

	public void RemoveBullets ()
	{
		GameObject[] bullets = GameObject.FindGameObjectsWithTag ("Bullet");
		
		int currentPowerUpCount = bullets.Length;
		
		for(int i=0; i<currentPowerUpCount; i++)
		{
			Destroy(bullets[i]);
		}
	}

	public void CalculateCrystalQuantity()
	{
		currentLevelCrystalQuantity = currentLevelNumber + minCrystalQuantity;
	}

	void SpawnCrystals(int quantity) {

		for(int i=0; i<=quantity; i++)
		{
			GameObject randomPosCrystal = Instantiate (PrefabCrystal,
			                                           GetRandomPosInLevelArea (),
			                                           Quaternion.identity) as GameObject;
			
			//			crystalList.Add (randomPosCrystal);
		}
		currentLevelCrystalQuantity = currentLevelNumber + minCrystalQuantity;
	}

	public void BombHitEnemy()
	{
		killComboActive = true;
		currentPlayerKillComboCount ++;
		if(currentPlayerMaxKillComboCount < currentPlayerKillComboCount)
		{
			currentPlayerMaxKillComboCount = currentPlayerKillComboCount;
		}
		currentPlayerTotalEnemyKills ++;
		currentKillComboInterval = KillComboIntervalLength;
	}
	
	public void PlayerHitCrystal()
	{
		currentPlayerCollectedCrystals ++;
		currentPlayerScore += CrystalScoreValue; 
		
		SetCrystalCountText (currentPlayerCollectedCrystals, currentLevelCrystalQuantity);
		//		crystalCountText.text = "Crystals: " + currentCrystalCount + "/" + LevelScript.currentLevelCrystalQuantity;
		
		if(currentPlayerCollectedCrystals == currentLevelCrystalQuantity)
		{
			audio.PlayOneShot(gateOpenClip);
			OpenNextLevelGate();
		}
	}

	public void OpenNextLevelGate()
	{
		GameObject gate = GameObject.FindGameObjectWithTag("Gate");
		gate.GetComponents<BoxCollider>()[0].enabled = false;
		gate.GetComponents<BoxCollider>()[1].enabled = false;
		gate.GetComponent<MeshRenderer>().enabled = false;
	}

	public void CloseNextLevelGate()
	{
		GameObject gate = GameObject.FindGameObjectWithTag("Gate");
		gate.GetComponents<BoxCollider>()[0].enabled = true;
		gate.GetComponents<BoxCollider>()[1].enabled = true;
		gate.GetComponent<MeshRenderer>().enabled = true;
	}

	public void SetCrystalCountText(int collected, int available)
	{
		UITextCrystals.text = "Crystals: " + collected + "/" + available; 
	}
}
