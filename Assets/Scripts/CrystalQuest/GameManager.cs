//using UnityEngine;
//using System.Collections;
//
//public class GameManager : MonoBehaviour {
//
//	public static PlayerScript playerScript;
//
//    void OnEnable()
//    {
//		CollisionScript.onTriggerEnter += onTriggerEnter;
//		CollisionScript.onTriggerEnter2D += onTriggerEnter2D;
//		//PlayerCollisionScript.onTriggered2D += OnPlayerCollision2D;
//    }
//
//    void OnDisable()
//    {
//		CollisionScript.onTriggerEnter -= onTriggerEnter;
//		CollisionScript.onTriggerEnter2D -= onTriggerEnter2D;
//		//PlayerCollisionScript.onTriggered2D -= OnPlayerCollision2D;
//    }
//
//	// Use this for initialization
//	void Start () {
//	
//	}
//
//	// Update is called once per frame
//	void Update () {
//	
//	}
//
//
//	/// <summary>
//	/// Raises the trigger enter event.
//	/// </summary>
//	/// <param name="collisionScript">Collision script.</param>
//	/// <param name="otherCollisionScript">Other collision script.</param>
//	void onTriggerEnter(CollisionScript collisionScript, CollisionScript otherCollisionScript)
//	{
//	}
//
//	/// <summary>
//	/// Raises the trigger enter2 d event.
//	/// </summary>
//	/// <param name="collisionScript">Collision script.</param>
//	/// <param name="otherCollisionScript">Other collision script.</param>
//	void onTriggerEnter2D(CollisionScript collisionScript, CollisionScript otherCollisionScript)
//	{
//		collisionScript.CollisionObject.Triggered(otherCollisionScript.CollisionObject);
//	}
//	
//
//	//void OnCollision2D(CollisionScript collisionScript, CollisionObject collisionObject, Collider2D otherCollider)
//	//void OnCollision2D(CollisionScript collisionScript, Collider2D otherCollider)
//	void onCollision2D(CollisionScript collisionScript, CollisionScript otherCollisionScript)
//	{
//		//collisionScript.CollisionObject.Collecting();
//	}
//
//	void onTrigger2D(CollisionScript collisionScript, CollisionScript otherCollisionScript)
//	{
//		//collisionScript.CollisionObject.Collecting();
//	}
//
//	void onPlayerCollision2D(PlayerCollisionScript collisionScript, CollisionObject collisionObject, Collider2D otherCollider)
//    {
//
//    }
//}
