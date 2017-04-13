using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vault: Ability {
	public float launchRadius;
	public float launchDamage;
	public float distance;

	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {}
	override public void Passive(Player player, List<GameObject> targets) {}
}
