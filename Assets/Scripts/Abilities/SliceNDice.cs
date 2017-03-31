using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SliceNDice : Ability {
	public float bleedDuration;
	public float bleedDamage;

	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {
		// only activate when cooldown is completed
		if (cooldownTimer != 0f) {
			return;
		}

		// use a raycast to determine what object the mouse is over
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		GameObject target = hit.transform.gameObject;
		// if said object is an enemy target, do damage and apply bleeding
		if (targets.Contains (target)) {
			// if enemy dies from the damage, remove it from the target list
			if (target.GetComponent<Enemy> ().TakeDamage (damage)) {
				targets.Remove (target);
			}
			// otherwise, make it bleed
			else {
				target.GetComponent<Enemy> ().takeDamageOverTime (bleedDuration, bleedDamage);
			}
		}

		// begin cooldown timer
		cooldownTimer = cooldown;
	}
}
