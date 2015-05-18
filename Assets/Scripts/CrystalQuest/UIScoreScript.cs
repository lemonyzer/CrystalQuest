using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScoreScript : MonoBehaviour {

	[SerializeField]
	private string preScoreString = "Score: ";
	[SerializeField]
	private Text textScore;

	#region Initialisation
	void Awake()
	{
		if(textScore == null)
		{
			textScore = this.GetComponent<Text>();
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
