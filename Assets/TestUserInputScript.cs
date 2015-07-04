using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class TestUserInputScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	[SerializeField]
	Camera testLevelCamera;

	[SerializeField]
	Rect testLevel;

	[SerializeField]
	bool useClamp;

//	[SerializeField]
//	bool useClamp2;

	[SerializeField]
	private Vector2 m_Input;

	[SerializeField]
	float speed = 5;

	void Update () {

		m_Input.x = CrossPlatformInputManager.GetAxis ("Horizontal");
		m_Input.y = CrossPlatformInputManager.GetAxis ("Vertical");

		Move ();

	}

	void Move ()
	{

//		if (currentPosition.x < testLevel.xMin)
//			currentPosition.x = testLevel.xMin;

		this.transform.Translate (m_Input);
		Vector3 currentPosition = this.transform.localPosition;
		
		if (useClamp)
		{
			currentPosition.x = Mathf.Clamp ( currentPosition.x, testLevel.xMin, testLevel.xMax);
			currentPosition.y = Mathf.Clamp ( currentPosition.y, testLevel.yMin, testLevel.yMax);

//			Debug.Log ("xMin = " + testLevel.xMin);
//			Debug.Log ("xMax = " + testLevel.xMax);
//			Debug.Log ("yMin = " + testLevel.yMin);
//			Debug.Log ("yMax = " + testLevel.yMax);

			this.transform.localPosition = currentPosition;
		}

//		if (useClamp2)
//		{
//			if (testLevelCamera == null)
//				return;
//
//			Debug.Log (testLevelCamera.rect);
//			Debug.Log (testLevelCamera.pixelRect);
//			Debug.Log (testLevelCamera.cameraToWorldMatrix);
//			Debug.Log (testLevelCamera.worldToCameraMatrix);
////			currentPosition.x = Mathf.Clamp ( currentPosition.x, testLevelCamera., testLevel.xMax);
////			currentPosition.y = Mathf.Clamp ( currentPosition.y, testLevel.yMin, testLevel.yMax);
//			
////			this.transform.position = currentPosition;
//		}
		

	}

}
