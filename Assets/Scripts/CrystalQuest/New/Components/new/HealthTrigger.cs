using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public abstract class HealthTrigger : MonoBehaviour {

	[SerializeField]
	protected UnityEvent myEvent;					// Event for my Event-Domain

	abstract protected void OnHealthDamage (int damageValue);
	abstract protected void OnHealthHealing (int healingValue);
	abstract protected void OnHealthUpdate (int healthValue);

//	[SerializeField]
//	List<CollisionTrigger> collisionTrigger;

}
