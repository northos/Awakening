using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnleashTheBeast: Ability {
	public float chopDamage;
	public float chopArc;
	public float chopRange;
	public float smashDamage;
	public float smashRadius;
	public float smashDistance;

	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {}
	override public void Passive(Player player, List<GameObject> targets) {}
}
