using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateHighScoreList : MonoBehaviour {

	public HighScoreManager highScoreManager;
	public GameObject scrollViewContent;
	public GameObject scrollContentPrefab;
	public List<SingleHighScore> highScoreList;

	// Use this for initialization
	void Start () {
		PopulateList ();
	}

	void PopulateList ()
	{
		highScoreList = highScoreManager.GetHighScoreList ();
		foreach(SingleHighScore score in highScoreList)
		{
			GameObject newScrollContent = Instantiate (scrollContentPrefab) as GameObject;
			HighScoreScrollItemScript itemScript = newScrollContent.GetComponent<HighScoreScrollItemScript>();
			itemScript.scoreText.text = "" + score.Score;
			itemScript.waveText.text = "" + score.Wave;
			itemScript.timeText.text = "" + score.PlayTime;
			newScrollContent.transform.SetParent(scrollViewContent.transform, false);
		}
	}

}
