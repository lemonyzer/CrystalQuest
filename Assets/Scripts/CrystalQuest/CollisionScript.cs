using UnityEngine;
using System.Collections;
	


public class CollisionScript : MonoBehaviour {

	// 3D
	//public delegate void OnTriggered(CollisionScript collisionScript, CollisionObject collisionObject, Collider otherCollider);
	//public delegate void OnTriggered(CollisionScript collisionScript, Collider otherCollider);
	public delegate void TriggerEnter(CollisionScript collisionScript, CollisionScript otherCollisionScript);
	public static event TriggerEnter onTriggerEnter;
	public delegate void CollisionEnter(CollisionScript collisionScript, CollisionScript otherCollisionScript);
	public static event CollisionEnter onCollisionEnter;

	// 2D
	//public delegate void OnTriggered2D(CollisionScript collisionScript, CollisionObject collisionObject, Collider2D otherCollider);
	//public delegate void OnTriggered2D(CollisionScript collisionScript, Collider2D otherCollider);
	public delegate void TriggerEnter2D(CollisionScript collisionScript, CollisionScript otherCollisionScript);
	public static event TriggerEnter2D onTriggerEnter2D;
	public delegate void CollisionEnter2D(CollisionScript collisionScript, CollisionScript otherCollisionScript);
	public static event CollisionEnter2D onCollisionEnter2D;

	[SerializeField]
	private CollisionObject _collisionObject;

	public CollisionObject CollisionObject {
		get { return _collisionObject; }
		protected set { _collisionObject = value; }
	}

	void OnCollisionEnter(Collision collision)
	{
		CollisionScript otherScript = collision.gameObject.GetComponent<CollisionScript>();
		if(otherScript != null)
		{
			if (onCollisionEnter != null)
			{
				//onTriggered(this, otherCollider);
				onCollisionEnter(this, otherScript);
			}
			else
			{
				Debug.LogError("kein Listener!");
			}
		}
		else
		{
			Debug.LogError(collision.gameObject.name + " hat kein CollisionScript!");
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		CollisionScript otherScript = collision.gameObject.GetComponent<CollisionScript>();
		if(otherScript != null)
		{
			if (onCollisionEnter2D != null)
			{
				//onTriggered(this, otherCollider);
				onCollisionEnter2D(this, otherScript);
			}
			else
			{
				Debug.LogError("kein Listener!");
			}
		}
		else
		{
			Debug.LogError(collision.gameObject.name + " hat kein CollisionScript!");
		}
	}

	/// <summary>
	/// Collision 3D
	/// </summary>
	/// <param name="otherCollider"></param>
	void OnTriggerEnter(Collider otherCollider)
	{
		CollisionScript otherScript = otherCollider.GetComponent<CollisionScript>();
		if(otherScript != null)
		{
			if (onTriggerEnter != null)
			{
				//onTriggered(this, otherCollider);
				onTriggerEnter(this, otherScript);
			}
			else
			{
				Debug.LogError("kein Listener!");
			}
		}
		else
		{
			Debug.LogError(otherCollider.gameObject.name + " hat kein CollisionScript!");
		}
	}
	
	/// <summary>
	/// Collision 2D
	/// </summary>
	/// <param name="otherCollider"></param>
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		CollisionScript otherScript = otherCollider.GetComponent<CollisionScript>();
		if(otherScript != null)
		{
			if (onTriggerEnter2D != null)
			{
				//onTriggered2D(this, otherCollider);
				onTriggerEnter2D(this, otherScript);
			}
			else
			{
				Debug.LogError("kein Listener!");
			}

        }
		else
		{
			Debug.LogError(otherCollider.gameObject.name + " hat kein CollisionScript!");
        }

	}
}
