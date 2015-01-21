using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsScript : MonoBehaviour {



	public Text pointsText;
	public Text comboCountText;
	public Text killsCountText;
	public Text crystalCountText;
	
	public float points = 0; 

	public int killValue = 100;
	public int crystalValue = 1000;

	public float comboTimer = 2.0f;
	public float currentComboTimer;
	public int currentComboCount;
	public int currentKillsCount;

	bool combo = false;

	public int currentCrystalCount = 0;
	public int currentCrystalComboCount = 0;

	public void BombHitEnemy()
	{
		combo = true;
		currentKillsCount ++;
		currentComboCount ++;
		currentComboTimer = comboTimer;
	}

	public void PlayerHitCrystal()
	{
		currentCrystalCount ++;
		points += currentCrystalCount * crystalValue;				/// !!!!!!!!- fehler 

		SetCrystalCountText (currentCrystalCount, LevelScript.currentLevelCrystalQuantity);
//		crystalCountText.text = "Crystals: " + currentCrystalCount + "/" + LevelScript.currentLevelCrystalQuantity;

		if(currentCrystalCount == LevelScript.currentLevelCrystalQuantity)
		{
			GameObject gate = GameObject.FindGameObjectWithTag("Gate");
			gate.GetComponents<BoxCollider>()[0].enabled = false;
			gate.GetComponents<BoxCollider>()[1].enabled = false;
			gate.GetComponent<MeshRenderer>().enabled = false;
			
		}
	}

	public void SetCrystalCountText(int collected, int available)
	{
		crystalCountText.text = "Crystals: " + collected + "/" + available; 
	}

	// Use this for initialization
	void Start()
	{
//		currentLevelCount = LevelScript.levelNumber + LevelScript.minCrystalQuantity;
		SetCrystalCountText (currentCrystalCount, LevelScript.currentLevelCrystalQuantity);
	}
	
	// Update is called once per frame
	void Update () {

		pointsText.text = points.ToString ();
		comboCountText.text = currentComboCount.ToString ()+"x";
		killsCountText.text = currentKillsCount.ToString ();

		if(combo)
		{
			currentComboTimer -= Time.deltaTime;
			if (currentComboTimer <= 0) {
				// current combo complete/finished
				points = points + currentComboCount*killValue + currentComboCount*killValue/10;
				currentComboCount = 0;
			}
			else
			{
				combo=false;
			}
		}



	}
}
