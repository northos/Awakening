using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallOfBlades : Ability {
	public float duration;

	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {}
	override public float OnHit(Player player, GameObject attacker, float hitDamage) {return hitDamage;}
	override public void Passive(Player player, List<GameObject> targets) {}
}
