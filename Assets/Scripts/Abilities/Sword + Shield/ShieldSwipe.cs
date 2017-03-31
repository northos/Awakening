using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldSwipe : Ability {
	public float duration;
	public float range;

	// deal damage and stun the target highlighted by the mouse, so long as it is within the appropriate hit range
	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {
		// only activate when cooldown is completed
		if (cooldownTimer != 0f) {
			return;
		}

		// use a raycast to determine what object the mouse is over
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		GameObject target = hit.transform.gameObject;
		// if said object is an enemy target, do damage and stun it
		if (targets.Contains (target)) {
			// if enemy dies from the damage, remove it from the target list
			if (target.GetComponent<Enemy> ().TakeDamage (damage)) {
				targets.Remove (target);
			}
			// otherwise, stun it
			else {
				target.GetComponent<Enemy> ().stun (duration);
			}
		}

		// begin cooldown timer
		cooldownTimer = cooldown;
	}
}
