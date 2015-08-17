using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

	public static string layerStringEnemy = "Enemy";
	public static int layerValueEnemy = 0;

	public static string layerStringPlayer = "Player";
	public static int layerValuePlayer = 0;

	public static string layerStringEnemyProjectile = "EnemyProjectile";
	public static int layerValueEnemyProjectile = 0;

	public static string layerStringItem = "Item";
	public static int layerValueItem = 0;

	public static void SetupRigidbody2D(Rigidbody2D rb2d)
	{
		// rb2D einstellen
		// Gravitation deaktivieren
		rb2d.gravityScale = 0;
		// Collisionen auch bei hohen geschwindigkeiten erkennen
		rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
	}

	public void Awake()
	{
		// Layer values initialisieren
		//TODO geht vor platform build? LayerMask ?
	}
}
