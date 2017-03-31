using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Disarm : Ability {
	public float arc;
	public float range;
	public float debuff;

	// damage enemies within the given range and arc
	// also apply a debuff to them for a given duration
	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {
		if (cooldownTimer != 0f)
			return;

		direction.Normalize ();
		// loop through possible targets
		List<GameObject> killed = new List<GameObject>();
		foreach (GameObject target in targets) {
			// skip those that are out of range
			if (Vector3.Distance(player.transform.position, target.transform.position) > range){
				continue;
			}
			Vector3 targetDirection = (target.transform.position - player.transform.position).normalized;
			// check if angle to target is within cone of attack
			if (Mathf.Abs (Mathf.Acos (Vector3.Dot(direction, targetDirection))) <= arc){
				if (target.GetComponent<Enemy> ().TakeDamage (damage)) {
					killed.Add (target);
				}
				// if the target survives, apply the damage debuff
				else {
					target.GetComponent<Enemy> ().reduceDamage (debuff);
				}
			}
		}
		foreach (GameObject enemy in killed) {
			targets.Remove (enemy);
		}

		cooldownTimer = cooldown;
	}
}
