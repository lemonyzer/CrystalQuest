using UnityEngine;
using System.Collections;

public class CollideableObject : EventTrigger {

	[SerializeField]
	bool triggerPlayerLayer = false;

	[SerializeField]
	bool triggerEnemyLayer = false;

	[SerializeField]
	bool triggerPlayerProjetileLayer = false;

	[SerializeField]
	bool triggerEnemyProjetileLayer = false;

	[SerializeField]
	bool triggerLevelDamageLayer = false;

	[SerializeField]
	bool triggerCollectables = false;

	void OnTriggerEnter2D (Collider2D otherCollider2d)
	{
		int otherObjectsLayer = otherCollider2d.gameObject.layer;
		if (otherObjectsLayer == LayerMask.NameToLayer ("Player"))
		{
			if (triggerPlayerLayer)
				TriggerEvent ();
		}
		else if (otherObjectsLayer == LayerMask.NameToLayer ("Enemy"))
		{
			if (triggerEnemyLayer)
				TriggerEvent ();
		}
		else if (otherObjectsLayer == LayerMask.NameToLayer ("EnemyProjectiles"))
		{
			if (triggerEnemyProjetileLayer)
				TriggerEvent ();
		}
		else if (otherObjectsLayer == LayerMask.NameToLayer ("PlayerProjectiles"))
		{
			if (triggerPlayerProjetileLayer)
				TriggerEvent ();
		}
		else if (otherObjectsLayer == LayerMask.NameToLayer ("LevelDamage"))
		{
			if (triggerLevelDamageLayer)
				TriggerEvent ();
		}
		else if (otherObjectsLayer == LayerMask.NameToLayer ("Collectables"))
		{
			if (triggerCollectables)
				TriggerEvent ();
		}
		else
			Debug.LogError (otherCollider2d.gameObject.name + " ist in Layer = " + LayerMask.LayerToName (otherObjectsLayer) + " und nicht in " + this.ToString () + " registriert");
	}

}
