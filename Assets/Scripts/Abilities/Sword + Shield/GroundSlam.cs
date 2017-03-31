using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundSlam : Ability {
	public float radius;
	public float knockbackForce;

	// damage nearby enemyies and apply the assigned knockback force to push them away
	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {
		if (cooldownTimer != 0f)
			return;

		// loop through possible targets
		List<GameObject> killed = new List<GameObject>();
		foreach (GameObject target in targets) {
			// check if potential target is within radius
			if (Vector3.Distance (player.transform.position, target.transform.position) < radius) {
				Vector3 targetDirection = (target.transform.position - player.transform.position).normalized;
				// apply damage and check if this kills the enemy
				if (target.GetComponent<Enemy> ().TakeDamage (damage)) {
					killed.Add (target);
				}
				// if the target survives, apply the knockback force
				else {
					target.GetComponent<Rigidbody2D> ().AddForce ((Vector2)(targetDirection.normalized * knockbackForce));
				}
			}
		}
		foreach (GameObject enemy in killed) {
			targets.Remove (enemy);
		}

		cooldownTimer = cooldown;
	}
}
