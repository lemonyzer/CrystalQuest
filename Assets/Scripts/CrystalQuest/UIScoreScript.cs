using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScoreScript : MonoBehaviour {

	[SerializeField]
	private static string preScoreString = "Score: ";

	[SerializeField]
	private static float score;
	
	[SerializeField]
	private static Text textScore;

	public float Score {
		get {return score;}
		set {
			score = value;
			SetScoreText (score);
		}
	}

	void SetScoreText (float value) {
		if (textScore != null)
			textScore.text = preScoreString + value;
	}

	#region Initialisation
	void Awake()
	{
		if(textScore == null)
		{
			textScore = this.GetComponent<Text>();
		}
	}
	#endregion

	#region Subscriptions
	void OnEnable() {
//		// Variante A
//		// 
//		PointsObject.onReleasePoints += UpdateUI;
//		// Variante B
//		// score wird von PlayerObjectScript verwaltet
//		PlayerObjectScript.onScoreUpdate += UpdateUI;
		// Variante C
		// score wird von CrystalQuestScoreManager verwaltet
		CrystalQuestScoreManager.onScoreUpdate += UpdateUI;
	}
	
	void OnDisable() {
//		PointsObject.onReleasePoints -= UpdateUI;
//		PlayerObjectScript.onScoreUpdate -= UpdateUI;
		CrystalQuestScoreManager.onScoreUpdate -= UpdateUI;
	}
	#endregion

	// UpdateUI (T template)
	void UpdateUI (float value)
	{
		Score = value;
	}
	
}
