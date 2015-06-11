using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	//[SerializeField]
	private GameObject myGO;

	public GameObject MyGO {
		get;
		private set;
	}

	[SerializeField]
	private CrystalQuestObjectScript owner;

	public CrystalQuestObjectScript Owner {
		get {return owner;}
		set {owner = value;}
	}

	[SerializeField]
	private float force = 10f;

	[SerializeField]
	private float speed = 5f;

	[SerializeField]
	private Rigidbody2D rb2d;

	void InitRigidbody2D ()
	{
		rb2d = this.GetComponent<Rigidbody2D>();
		Globals.SetupRigidbody2D (rb2d);
	}

	void InitAllCollider2D ()
	{
		Collider2D[] colliders2D = this.GetComponents<Collider2D>();
		if (colliders2D != null)
		{
			for(int i=0; i < colliders2D.Length; i++)
			{
				InitCollider2D (colliders2D[i]);
			}
		}
	}

	void InitCollider2D (Collider2D collider)
	{
		collider.isTrigger = true;
	}

	void Awake () {
		InitRigidbody2D ();
		InitAllCollider2D ();
		myGO = this.gameObject;
	}

//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}

	public void Released (Vector3 direction)
	{
		rb2d.AddForce (direction * force);
	}

	public void Released (Vector3 direction, float force)
	{
		rb2d.AddForce (direction * force);
	}

	public virtual void OnTriggerEnter2D (Collider2D otherCollider)
	{

	}
}
