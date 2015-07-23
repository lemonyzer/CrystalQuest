using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIWaveScript : MonoBehaviour {

	[SerializeField]
	private bool usePreString = false;

	[SerializeField]
	private string preString = "Wave: ";

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
		DomainEventManager.StartListeningInitWave (UpdateUI);
//		PlayerObjectScript.onLifeUpdate += UpdateUI;
	}
	
	void OnDisable() {
		DomainEventManager.StopListeningInitWave (UpdateUI);
//		PlayerObjectScript.onLifeUpdate -= UpdateUI;
	}
	#endregion
	
	#region Update
	void UpdateUI (Wave currentWave)
	{
		//		Debug.LogWarning (this.ToString () + " UpdateUI () " + value);
		if (uiText != null)
		{
			string text = "";
			if (usePreString)
				text = preString;

			text += currentWave.waveName; 
			uiText.text = text;
		}
	}
	#endregion
}
