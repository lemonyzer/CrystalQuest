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
		int i = 0;
		foreach(SingleHighScore score in highScoreList)
		{
			i++;
			GameObject newScrollContent = Instantiate (scrollContentPrefab) as GameObject;
			HighScoreScrollItemScript itemScript = newScrollContent.GetComponent<HighScoreScrollItemScript>();
			itemScript.positionText.text = "" + i;
			itemScript.scoreText.text = "" + score.Score;
			itemScript.waveText.text = "" + score.Wave;
			itemScript.timeText.text = "" + score.PlayTime.ToString("F2");
			newScrollContent.transform.SetParent(scrollViewContent.transform, false);
		}
	}

}
