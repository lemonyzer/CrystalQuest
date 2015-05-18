using UnityEngine;
using System.Collections;

public class ProjectileObjectScript : CrystalQuestObjectScript {

	[SerializeField]
	private CrystalQuestObjectScript ownerObjectScript;

	public CrystalQuestObjectScript OwnerObjectScript {
		get { return ownerObjectScript; }
		set { ownerObjectScript = value; }
	}

}
