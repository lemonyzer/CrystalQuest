using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILifesScript : MonoBehaviour {

	[SerializeField]
	private string preString = "Lifes: ";
	[SerializeField]
	private Text uiText;

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
		PlayerScript.onLostLife += UpdateUI;
	}

	void OnDisable() {
		PlayerScript.onLostLife -= UpdateUI;
	}
	#endregion

	#region Update
	void UpdateUI(int value)
	{
		uiText.text = preString + value;
	}
	#endregion
}
