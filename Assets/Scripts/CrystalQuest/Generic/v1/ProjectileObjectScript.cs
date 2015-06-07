using UnityEngine;
using System.Collections;

public class ProjectileObjectScript : MovingObject {

	[SerializeField]
	protected CrystalQuestObjectScript ownerObjectScript;

	public CrystalQuestObjectScript OwnerObjectScript {
		get { return ownerObjectScript; }
		set { ownerObjectScript = value; }
	}

}
