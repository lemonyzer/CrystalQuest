using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Accelerator : MonoBehaviour {



	// Move object using accelerometer
	float speed = 10.0f;
	Vector3 dir;
	float magnitude = 0.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		dir.x = Input.acceleration.x - Gyroskop.refPoint.x;
		dir.y = Input.acceleration.y - Gyroskop.refPoint.y;

		magnitude = Input.acceleration.sqrMagnitude;

		// clamp acceleration vector to unit sphere
		if (dir.sqrMagnitude > 1)
			dir.Normalize();

		// Make it move 10 meters per second instead of 10 meters per frame...
		dir *= Time.deltaTime;

		transform.Translate (dir * speed, Space.World);

		Vector3 relativePos = new Vector3 (dir.x, 0, 0);
		transform.rotation = Quaternion.LookRotation(relativePos, new Vector3(0,0,0));

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle -= 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

//		Vector3 relativePos = new Vector3 (1, 0, 0);
//		Quaternion rotation = Quaternion.LookRotation(relativePos);
//		transform.rotation = rotation;
		
	}
	
	public void ButtonReset()
	{
		this.transform.position = Vector3.zero;
	}
}
