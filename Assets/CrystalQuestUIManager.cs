using UnityEngine;
using System.Collections;

public class CrystalQuestUIManager : MonoBehaviour {

	// https://www.youtube.com/watch?v=LYhCuhuKoIc#t=16m

	// TODO UIScoreScript -> Singleton (static zugriff, keine referenz nötig)
	[SerializeField]
	private UIScoreScript score;

	// TODO UILifesScript -> Singleton (static zugriff, keine referenz nötig)
	[SerializeField]
	private UILifesScript life;

	void Awake () 
	{
		if (score == null)
		{
			score = FindObjectOfType<UIScoreScript>();
		}

		if (life == null)
		{
			life = FindObjectOfType<UILifesScript>();
		}
	}

//	void OnEnable ()
//	{
//		PointsObject.onReleasePoints += UpdateScore;
//	}
//
//	void OnDisable ()
//	{
//		PointsObject.onReleasePoints -= UpdateScore;
//	}
//
//	void UpdateScore (int value)
//	{
//		score.Score = value;
//	}
}
