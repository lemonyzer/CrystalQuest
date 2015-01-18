using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gyroskop : MonoBehaviour {

	public GameObject sensor;
	public GameObject sensorRelative;
	public GameObject sensorTranslated;
	public Vector3 sensorDefaultPosition;

	public Text textCurrentSpeed;
	public Text textAccCurrentPoint;
	public Text textAccTranslationCurrentPoint;
	public Text textAccTranslationClampedCurrentPoint;
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
	
		
		textAccCurrentPoint.text = "Acc:" + Input.acceleration.ToString ();
		
		sensor.transform.position = sensorDefaultPosition + Input.acceleration;
		sensorRelative.transform.position = sensorDefaultPosition + Input.acceleration - refPoint;

		// max. |Refpunkt| suchen... Bewegung soll sich gleichmäßig anfühlen!
		float maxRef = 0f;
		if(Mathf.Abs(refPoint.x) < Mathf.Abs(refPoint.y))
		{
			maxRef = refPoint.y;
		}
		else
		{
			maxRef = refPoint.x;
		}

		dir.x = (Input.acceleration.x - refPoint.x) * (1/((1-Mathf.Abs(maxRef))));
		dir.y = (Input.acceleration.y - refPoint.y) * (1/((1-Mathf.Abs(maxRef))));

		if(textAccTranslationCurrentPoint != null)
			textAccTranslationCurrentPoint.text = "AccT:" + dir.ToString ();

		dir.y = Mathf.Clamp (dir.y, -1f, 1f);

		if(textAccTranslationClampedCurrentPoint != null)
			textAccTranslationClampedCurrentPoint.text = "AccTC:" + dir.ToString ();

		sensorTranslated.transform.position = sensorDefaultPosition + dir;
		


		
		magnitude = Input.acceleration.sqrMagnitude;
		
		// clamp acceleration vector to unit sphere
		if (dir.sqrMagnitude > 1)
			dir.Normalize();
		
		// Make it move 10 meters per second instead of 10 meters per frame...
		dir *= Time.deltaTime;
		
		textCurrentSpeed.text = "Speed:" + (dir * speed).ToString ();
		
		
		Vector3 forward = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
		transform.rotation = Quaternion.LookRotation(forward, Vector3.up);

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle -= 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
	}

	public void ButtonSetRef()
	{
		textAccRefPoint.text = "RefP:" + Input.acceleration.ToString ();
		refPoint.x = Input.acceleration.x;
		refPoint.y = Input.acceleration.y;
	}
	

}
