using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILifesScript : MonoBehaviour {

	[SerializeField]
	private static string preString = "Lifes: ";

	[SerializeField]
	private static int lifes;

	public int Lifes {
		get {return lifes;}
		set {
			if (value >= 0)
				lifes = value;
			else
				lifes = 0;
			UpdateUI (lifes);
		}
	}
	
	[SerializeField]
	private static Text uiText;

	#region Initialisation
	void Awake()
	{
		if(uiText == null)
		{
			uiText = this.GetComponent<Text>();
		}
	}
	#endregion

	#region Subscriptions
	void OnEnable() {
		PlayerObjectScript.onLifeUpdate += UpdateUI;
	}

	void OnDisable() {
		PlayerObjectScript.onLifeUpdate -= UpdateUI;
	}
	#endregion

	#region Update
	void UpdateUI (int value)
	{
//		Debug.LogWarning (this.ToString () + " UpdateUI () " + value);
		if (uiText != null)
			uiText.text = preString + value;
	}
	#endregion
}
