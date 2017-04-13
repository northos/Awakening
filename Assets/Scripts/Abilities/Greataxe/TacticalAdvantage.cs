using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TacticalAdvantage : Ability {
	public float debuffStrength;
	public float debuffDuration;

	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {}
	override public float OnHit(Player player, GameObject attacker, float hitDamage) {return hitDamage;}
}
