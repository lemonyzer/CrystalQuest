using UnityEngine;
using System.Collections;

// coupling
[RequireComponent(typeof(MovementScript))]
public class InputScript : MonoBehaviour {

    MovementScript moveScript;

    private Vector2 moveDirection = Vector2.zero;

    private bool shoot = false;
    private bool useItem = false;

    // Use this for pre-initialization
    void Awake()
    {
        moveScript = GetComponent<MovementScript>();
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        shoot = Input.GetButton("Fire1");
        useItem = Input.GetButton("Jump");

        moveScript.InputDirection = moveDirection;
	}
}
