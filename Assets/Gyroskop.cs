using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gyroskop : MonoBehaviour {

	public GameObject sensor;
	public GameObject sensorRelative;
	public Vector3 sensorDefaultPosition;

	public Text textCurrentSpeed;
	public Text textAccCurrentPoint;
	public Text textAccRefPoint;

	public static Vector3 refPoint = Vector3.zero;

	// Move object using accelerometer
	float speed = 10.0f;
	Vector3 dir;
	float magnitude = 0.0f;

	// Use this for initialization
	void Start () {
		sensorDefaultPosition = sensor.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		
		textAccCurrentPoint.text = Input.acceleration.ToString ();
		
		sensor.transform.position = sensorDefaultPosition + Input.acceleration;
		sensorRelative.transform.position = sensorDefaultPosition + Input.acceleration - refPoint;
		
		dir.x = Input.acceleration.x - refPoint.x;
		dir.y = Input.acceleration.y - refPoint.y;
		
		magnitude = Input.acceleration.sqrMagnitude;
		
		// clamp acceleration vector to unit sphere
		if (dir.sqrMagnitude > 1)
			dir.Normalize();
		
		// Make it move 10 meters per second instead of 10 meters per frame...
		dir *= Time.deltaTime;
		
		textCurrentSpeed.text = (dir * speed).ToString ();
		
		
		Vector3 forward = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
		transform.rotation = Quaternion.LookRotation(forward, Vector3.up);

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle -= 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
	}

	public void ButtonSetRef()
	{
		textAccRefPoint.text = Input.acceleration.ToString ();
		refPoint.x = Input.acceleration.x;
		refPoint.y = Input.acceleration.y;
	}
	

}
