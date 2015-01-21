using UnityEngine;
using System.Collections;

public class MoveToTarget : MonoBehaviour {

	public GameObject target;

	public float aliveTime = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//differenz vektor
		Vector3 direction = target.transform.position - this.transform.position;

		aliveTime += Time.deltaTime;

		if(aliveTime < 0.6f)
		{

		}
		else if(aliveTime > 0.6f && aliveTime < 4f)
		{
			// clamp acceleration vector to unit sphere
			if (direction.sqrMagnitude > 1)
			{
				direction.Normalize();
			}

			direction = direction * (aliveTime/2f);
		}
		else
		{
			if (direction.sqrMagnitude > 1)
			{
				direction.Normalize();
			}
			direction = direction * (3f);
		}

		transform.Translate (direction * Time.deltaTime);
	}

//	void OnCollisionEnter(Collision coll)
//	{
//		
//		if (coll.gameObject.layer == LayerMask.NameToLayer ("Player")) {
//			LevelDamageHit();
//		}
//	}
}
