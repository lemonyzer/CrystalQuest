using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISmartBombs : MonoBehaviour {

	[SerializeField]
	private bool usePreString = true;
	
	[SerializeField]
	private string preString = "Smart Bombs: ";
	
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
		DomainEventManager.StartGlobalListening (EventNames.SmartBombAmount, UpdateUI);
		//		PlayerObjectScript.onLifeUpdate += UpdateUI;
	}
	
	void OnDisable() {
		DomainEventManager.StopGlobalListening (EventNames.SmartBombAmount, UpdateUI);
		//		PlayerObjectScript.onLifeUpdate -= UpdateUI;
	}
	#endregion
	
	#region Update
	void UpdateUI (float amount)
	{
		//		Debug.LogWarning (this.ToString () + " UpdateUI () " + value);
		if (uiText != null)
		{
			string text = "";
			if (usePreString)
				text = preString;
			
			text += "" + amount; 
			uiText.text = text;
		}
	}
	#endregion
}
