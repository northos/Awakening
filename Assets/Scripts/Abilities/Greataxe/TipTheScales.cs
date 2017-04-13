using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TipTheScales : Ability {
	public float radius;
	public float cooldownReduction;

	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {}
	override public void Passive(Player player, List<GameObject> targets) {}
}
