using UnityEngine;
using System.Collections;

public class CrystalQuestGameManager : MonoBehaviour {

//	public static Player playerScript;
	
    void OnEnable()
    {
		PlayerObjectScript.onTriggerEnter2D += PlayerTriggerEnter2D;
		EnemyObjectScript.onTriggerEnter2D += EnemyTriggerEnter2D;
		ProjectileObjectScript.onTriggerEnter2D += ProjectileTriggerEnter2D;
    }

    void OnDisable()
    {
		PlayerObjectScript.onTriggerEnter2D -= PlayerTriggerEnter2D;
		EnemyObjectScript.onTriggerEnter2D -= EnemyTriggerEnter2D;
		ProjectileObjectScript.onTriggerEnter2D -= ProjectileTriggerEnter2D;
    }

	// wie kann ich Collision zwischen Sub-Klassen differenzieren? 
	// CrystalQuestObjectScript
	// PlayerObjectScript;
	// EnemyObjectScript;
	// ProjectileObjectScript;

	void PlayerTriggerEnter2D(CrystalQuestObjectScript detectorObjectScript, CrystalQuestObjectScript otherObjectScript)
	{
		//CollisionDetected2D(detectorObjectScript, otherObjectScript);
		if(otherObjectScript != null)
		{
			if(otherObjectScript.GetType() == typeof(PlayerObjectScript))
			{
				// kollision mit Spieler ?!
				Debug.LogError("Spieler kollidiert mit Spieler? " + detectorObjectScript.gameObject.name + "<->" + otherObjectScript.gameObject.name);
			}
			else if(otherObjectScript.GetType() == typeof(EnemyObjectScript))
			{
				// kollision mit enemy
				Debug.LogWarning("Spieler kollidiert mit Enemy? " + detectorObjectScript.gameObject.name + "<->" + otherObjectScript.gameObject.name);
			}
			else if(otherObjectScript.GetType() == typeof(ProjectileObjectScript))
			{
				// kollision mit projectil
				Debug.LogWarning("Spieler kollidiert mit Projectil? " + detectorObjectScript.gameObject.name + "<->" + otherObjectScript.gameObject.name);

				if (detectorObjectScript.SelfAttack)
				{
					// Detector kann schaden von eigenen Projektilen nehmen
				}
				else
				{
					// Detector kann kein schaden von eigenen Projektilen nehmen
					if ( ((ProjectileObjectScript)otherObjectScript).OwnerObjectScript == detectorObjectScript )
					{
						// Own Projectile -> no Damage
					}
					else
					{
						// Enemy Projectile -> Apply Damage
						ApplyCollisionDamage(detectorObjectScript, otherObjectScript);
					}
				}
			}
		}
		else
		{
			Debug.LogError("otherObjectScript == NULL");
		}
	}
	
	void EnemyTriggerEnter2D(CrystalQuestObjectScript detectorObjectScript, CrystalQuestObjectScript otherObjectScript)
	{
		//CollisionDetected2D(detectorObjectScript, otherObjectScript);
		if(otherObjectScript != null)
		{
			if(otherObjectScript.GetType() == typeof(PlayerObjectScript))
			{
				// kollision mit Spieler ?!
				Debug.LogError("Enemy kollidiert mit Spieler? " + detectorObjectScript.gameObject.name + "<->" + otherObjectScript.gameObject.name);
			}
			else if(otherObjectScript.GetType() == typeof(EnemyObjectScript))
			{
				// kollision mit enemy
				Debug.LogWarning("Enemy kollidiert mit Enemy? " + detectorObjectScript.gameObject.name + "<->" + otherObjectScript.gameObject.name);
			}
			else if(otherObjectScript.GetType() == typeof(ProjectileObjectScript))
			{
				// kollision mit projectil
				Debug.LogWarning("Enemy kollidiert mit Projectil? " + detectorObjectScript.gameObject.name + "<->" + otherObjectScript.gameObject.name);
			}
		}
		else
		{
			Debug.LogError("otherObjectScript == NULL");
		}
	}
	
	void ProjectileTriggerEnter2D(CrystalQuestObjectScript detectorObjectScript, CrystalQuestObjectScript otherObjectScript)
	{
		CollisionDetected2D(detectorObjectScript, otherObjectScript);
	}

	void CollisionDetected2D(CrystalQuestObjectScript detectorObjectScript, CrystalQuestObjectScript otherObjectScript)
	{
		Debug.LogWarning(detectorObjectScript.gameObject.name + " -> " + otherObjectScript.gameObject.name);
		
	}

	void ApplyCollisionDamage(CrystalQuestObjectScript detectorObjectScript, CrystalQuestObjectScript otherObjectScript)
	{
		detectorObjectScript.ApplyCollisionDamage(otherObjectScript);
	}

}
