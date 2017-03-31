using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Thrust : Ability {
	public float arc;
	public float range;

	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {
		// only activate when cooldown is completed
		if (cooldownTimer != 0f) {
			return;
		}
			
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
				// if target within appropriate angle, damage it
				if ((target.GetComponent<Enemy> ().TakeDamage (damage))) {
					// if this kills the enemy, store it to be removed from targets later
					killed.Add (target);
				}
			}
		}
		// remove all killed enemies from targets list
		foreach (GameObject enemy in killed) {
			targets.Remove (enemy);
		}

		// begin cooldown timer
		cooldownTimer = cooldown;
	}
}
