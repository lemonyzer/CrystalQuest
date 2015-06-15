using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScoreDataModell {
	[SerializeField]
	float scoreValue;

	public float ScoreValue {
		get {return scoreValue;}
		set {scoreValue = value;}
	}
}

public class ScoreableObjectScript : MonoBehaviour {

	[SerializeField]
	ScoreDataModell scoreData = new ScoreDataModell();

	[SerializeField]
	MyFloatEvent releaseScore = new MyFloatEvent();	// kann direkt im Unity Editor (Inspector gesetzt werden)

	public void ReleaseScore ()
	{
		if (releaseScore != null)
			releaseScore.Invoke (scoreData.ScoreValue);
		else
			Debug.LogError (this.ToString () + " releaseScore == NULL");
	}
}
