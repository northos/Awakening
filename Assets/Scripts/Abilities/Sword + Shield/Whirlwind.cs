using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Whirlwind : Ability {
	public float radius;

	// damage all enemies within the assigned radius
	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {
		// only activate when cooldown is completed
		if (cooldownTimer != 0f) {
			return;
		}

		// check each enemy target to see if it's in range
		List<GameObject> killed = new List<GameObject> ();
		foreach (GameObject target in targets) {
			if (Vector3.Distance (target.transform.position, player.transform.position) <= radius) {
				// if in range, damage it
				if (target.GetComponent <Enemy> ().TakeDamage (damage)) {
					// if this killed the enemy, store it to be removed from targets
					killed.Add (target);
				}
			}
		}
		// remove any killed enemies from the target list
		foreach (GameObject enemy in killed) {
			targets.Remove (enemy);
		}

		// begin cooldown timer
		cooldownTimer = cooldown;
	}
}
