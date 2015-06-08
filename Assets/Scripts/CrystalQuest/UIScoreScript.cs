using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScoreScript : MonoBehaviour {

	[SerializeField]
	private static string preScoreString = "Score: ";

	[SerializeField]
	private static Text textScore;

	public static void SetScore (int value) {
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
		PointsObject.onReleasePoints += UpdateUI;
	}
	
	void OnDisable() {
		PointsObject.onReleasePoints -= UpdateUI;
	}
	#endregion

	// UpdateUI (T template)
	void UpdateUI (int value)
	{
		SetScore (value);
	}
	
}
