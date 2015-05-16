using UnityEngine;
using System.Collections;

public class PlayerCollisionScript : MonoBehaviour {

	public delegate void OnTriggered(PlayerCollisionScript collisionScript, Collider otherCollider);
    public static event OnTriggered onTriggered;

	public delegate void OnTriggered2D(PlayerCollisionScript collisionScript, Collider2D otherCollider);
    public static event OnTriggered2D onTriggered2D;

    void Awake() {

    }

    /// <summary>
    /// Collision 3D
    /// </summary>
    /// <param name="otherCollider"></param>
    void OnTriggerEnter(Collider otherCollider)
    {
        if (onTriggered != null)
            onTriggered(this, otherCollider);
    }

    /// <summary>
    /// Collision 2D
    /// </summary>
    /// <param name="otherCollider"></param>
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
		// Abfrage in Enemy/Item/Projectile
		// nur eine Abfrage 
		// Trigger/Collision mit Spieler


		// Abfrage in Player macht kein Sinn 
		// n unterschiedliche Typen

		// otherCollider in Enemy Layer?
		// otherCollider in Item Layer?
		//  * Level Crystal
		//  * Smart Bomb
		//  * Bonus Lifes
		//  * Bonus Crystal
		//  * Bonus Points
		// otherCollider in Projectile Layer?
		//	* own Projectile
		//  * enemy Projectile?
		// otherCollider in  

        if (onTriggered2D != null)
            onTriggered2D(this, otherCollider);
    }
}
