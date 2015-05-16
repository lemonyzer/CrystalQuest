using UnityEngine;
using System.Collections;

/// <summary>
/// MvementScript braucht Eingabe
/// 
/// Konzept A
/// Eingabe ist eigenes MonoBehviour-Script
///     Vorraussetzung: Eingabe-Script muss als Componente vor MovementScript sitzen da sonst die Eingabe vom letzten Update gelesen wird
///     
/// Eingabe ist eigene Base-Klasse
/// und hat je nach Platform andere eingabeabfrage algorithmen, stellt aber Platformübergreifend generische Schnittstellen bereit
/// 
/// 
/// Konzept B
///     MovemntScript arbeitet undabhängig vom Eingabescript - das erlaubt eine Steuerung über AI
/// 
/// 
/// </summary>

// 3D
//[RequireComponent(typeof(Rigidbody))]
// 2D
[RequireComponent(typeof(Rigidbody2D))]
public class MovementScript : MonoBehaviour {

    private float maxVelocity = 5f;
    //public float MaxVelocity { get; set; }

    private Vector2 inputDirection;
    public Vector2 InputDirection { get; set; }


    private Rigidbody2D rb2D;
    [SerializeField]
    private bool canMove = true;
    [SerializeField]
    private bool canChangeMove;
    [SerializeField]
    private bool canUpdateMove;

    void Awake()
    {
        rb2D = this.GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // FixedUpdate is called frameRate independent
    void FixedUpdate()
    {
        // inputDirection abs [-1;+1]
        Mathf.Clamp(InputDirection.x, -1f, +1f);
        Mathf.Clamp(InputDirection.y, -1f, +1f);

        // Better
        Vector3.Normalize(InputDirection);

        // Movement
        if(canMove)
            rb2D.MovePosition(rb2D.position + (InputDirection*maxVelocity) * Time.fixedDeltaTime);
    }
}
