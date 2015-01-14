
#pragma strict

var tilt : Vector3 = Vector3.zero;
var speed : float;
private var circ : float;
private var previousPosition : Vector3;

@script RequireComponent(Rigidbody)
function Start()
{
	//Find the circumference of the circle so that the circle can be rotated the appropriate amount when rolling
	circ = 2 * Mathf.PI * GetComponent.<Collider>().bounds.extents.x;	
	previousPosition = transform.position;
}


function Update () {
	tilt.x = Input.acceleration.x;
	tilt.y = Input.acceleration.y;
	tilt.z = Input.acceleration.z;
	//GetComponent.<Rigidbody>().AddForce(tilt * speed * Time.deltaTime);
	transform.Translate(tilt * speed * Time.deltaTime, Space.World);
	
	var movement : Vector3 = transform.position - previousPosition;
	movement = Vector3(0,0,0);
	transform.Rotate(movement / circ * 360, Space.World);
	previousPosition = transform.position;	
}

function LateUpdate()
{
//	var movement : Vector3 = transform.position - previousPosition;
//	movement = Vector3(0,0,0);
//	transform.Rotate(movement / circ * 360, Space.World);
//	previousPosition = transform.position;	
}