using UnityEngine;
using System.Collections;

public class Tilt : MonoBehaviour {

	public Vector3 sensorDefaultPosition;
	public Vector3 refPoint = Vector3.zero;
	
	// Move object using accelerometer
	[SerializeField]
	float speed = 20.0f;

	[SerializeField]
	Vector3 dir;

	float magnitude = 0.0f;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


		float maxRef = 0f;
		if(Mathf.Abs(refPoint.x) < Mathf.Abs(refPoint.y))
		{
			maxRef = refPoint.y;
		}
		else
		{
			maxRef = refPoint.x;
		}
		
		dir.x = (Input.acceleration.x - refPoint.x) * (10/(10*(1-Mathf.Abs(maxRef))));
		//		dir.y = Input.acceleration.y - Gyroskop.refPoint.y;
		dir.y = (Input.acceleration.y - refPoint.y) * (10/(10*(1-Mathf.Abs(maxRef))));
		
		magnitude = Input.acceleration.sqrMagnitude;
		
		// clamp acceleration vector to unit sphere
		if (dir.sqrMagnitude > 1)
			dir.Normalize();
		
		// Make it move 10 meters per second instead of 10 meters per frame...
//		dir *= Time.deltaTime * speed;

//		// max. |Refpunkt| suchen... Bewegung soll sich gleichmäßig anfühlen!
//		float maxRef = 0f;
//		if(Mathf.Abs(refPoint.x) < Mathf.Abs(refPoint.y))
//		{
//			maxRef = refPoint.y;
//		}
//		else
//		{
//			maxRef = refPoint.x;
//		}
//		
//		dir.x = (Input.acceleration.x - refPoint.x) * (1/((1-Mathf.Abs(maxRef))));
//		dir.y = (Input.acceleration.y - refPoint.y) * (1/((1-Mathf.Abs(maxRef))));
//		
//		dir.y = Mathf.Clamp (dir.y, -1f, 1f);
//		
//		magnitude = Input.acceleration.sqrMagnitude;
//		
//		// clamp acceleration vector to unit sphere
//		if (dir.sqrMagnitude > 1)
//			dir.Normalize();
//		
//		// Make it move 10 meters per second instead of 10 meters per frame...
//		dir *= Time.deltaTime *speed;
//		
//		Vector3 forward = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
//		transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
//		
//		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//		angle -= 90;
//		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//
		UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.SetAxis ("Horizontal", dir.x);
		UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.SetAxis ("Vertical", dir.y);
	}
	
	public void SetRef()
	{
		refPoint.x = Input.acceleration.x;
		refPoint.y = Input.acceleration.y;
	}

	void rotate()
	{
		Vector3 relativePos = new Vector3 (dir.x, 0, 0);
		transform.rotation = Quaternion.LookRotation(relativePos, new Vector3(0,0,0));
		
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle -= 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
		//		Vector3 relativePos = new Vector3 (1, 0, 0);
		//		Quaternion rotation = Quaternion.LookRotation(relativePos);
		//		transform.rotation = rotation;
	}
}
