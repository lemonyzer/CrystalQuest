using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    void OnEnable()
    {
		CollisionScript.onTriggerEnter += OnTriggerEnter;
		CollisionScript.onTriggerEnter2D += OnTriggerEnter2D;
		//PlayerCollisionScript.onTriggered2D += OnPlayerCollision2D;
    }

    void OnDisable()
    {
		CollisionScript.onTriggerEnter -= OnTriggerEnter;
		CollisionScript.onTriggerEnter2D -= OnTriggerEnter2D;
		//PlayerCollisionScript.onTriggered2D -= OnPlayerCollision2D;
    }

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}


	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="collisionScript">Collision script.</param>
	/// <param name="otherCollisionScript">Other collision script.</param>
	void OnTriggerEnter(CollisionScript collisionScript, CollisionScript otherCollisionScript)
	{
	}

	/// <summary>
	/// Raises the trigger enter2 d event.
	/// </summary>
	/// <param name="collisionScript">Collision script.</param>
	/// <param name="otherCollisionScript">Other collision script.</param>
	void OnTriggerEnter2D(CollisionScript collisionScript, CollisionScript otherCollisionScript)
	{
		collisionScript.CollisionObject.Triggered(otherCollisionScript.CollisionObject);
	}
	

	//void OnCollision2D(CollisionScript collisionScript, CollisionObject collisionObject, Collider2D otherCollider)
	//void OnCollision2D(CollisionScript collisionScript, Collider2D otherCollider)
	void OnCollision2D(CollisionScript collisionScript, CollisionScript otherCollisionScript)
	{
		//collisionScript.CollisionObject.Collecting();
	}

	void OnTrigger2D(CollisionScript collisionScript, CollisionScript otherCollisionScript)
	{
		//collisionScript.CollisionObject.Collecting();
	}

	void OnPlayerCollision2D(PlayerCollisionScript collisionScript, CollisionObject collisionObject, Collider2D otherCollider)
    {

    }
}
