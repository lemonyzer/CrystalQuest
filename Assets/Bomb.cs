using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public bool triggered = false;

	Vector3 triggerPos;
	Vector3 startPos;
	Vector3 endPos;

	float startRadius = 0.1f;
	float radiusStepps = 0.2f;
	float endRadius = 4f;
	float currentRadius;

	// Use this for initialization
	void Start () {
		currentRadius = startRadius;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (triggered) {
			currentRadius += radiusStepps;
			Physics.CheckSphere (this.transform.position, currentRadius);
			DebugCircle();
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player")
		{
			if(!triggered)
				TriggerAction();
		}
		else if(other.tag == "Crystal")
		{
			Debug.Log("crystal bombed");
			Destroy(other.transform.gameObject);
		}
	}

	void TriggerAction()
	{
		triggered = true;
		triggerPos = this.transform.position;

		this.GetComponent<Animator> ().SetTrigger ("explode");
	}

	void DebugCircle()
	{
//		for(int i=0; i< 360; i++)
//		{
			Debug.DrawLine(triggerPos,triggerPos+new Vector3(currentRadius,currentRadius,0),Color.green,2f);
//		}		

	}

}
