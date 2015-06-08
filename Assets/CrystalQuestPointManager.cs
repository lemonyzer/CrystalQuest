using UnityEngine;
using System.Collections;

public class CrystalQuestPointManager : MonoBehaviour {

	[SerializeField]
	private float currentLevelPoints;

	[SerializeField]
	private float currentSessionPoints;

	public float CurrentLevelPoints {
		get {return currentLevelPoints;}
		set {currentLevelPoints = value;}
	}

}
