using UnityEngine;
using System.Collections;

public class MoveToTarget : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//this.transform.position
		Vector3 direction = target.transform.position - this.transform.position;
		transform.Translate (direction * Time.deltaTime);

		//transform.Translate ();

	}
}
