using UnityEngine;
using System.Collections;

public class ScoreableObjectController : MonoBehaviour {

	[SerializeField]
	ScoreDataModell scoreData;

	public void AddPoints (float scoreValue)
	{
		scoreData.ScoreValue += scoreValue;
	}

}
