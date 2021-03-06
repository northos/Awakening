﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrossCut: Ability {
	public float range;
	public float arc;

	// damage all enemies within the given range and angle from the mouse cursor
	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {
		if (cooldownTimer != 0f)
			return;

		direction.Normalize ();
		// Loop through possible targets
		List<GameObject> killed = new List<GameObject>();
		foreach (GameObject target in targets) {
			// Skip those that are out of range
			if (Vector3.Distance(player.transform.position, target.transform.position) > range){
				continue;
			}
			Vector3 targetDirection = (target.transform.position - player.transform.position).normalized;
			// Check if angle to target is within cone of attack
			if (Mathf.Abs (Mathf.Acos (Vector3.Dot(direction, targetDirection))) <= arc){
				if (((Enemy)target.GetComponent (typeof(Enemy))).TakeDamage (damage)) {
					killed.Add (target);
				} 
			}
		}
		foreach (GameObject enemy in killed) {
			targets.Remove (enemy);
		}

		cooldownTimer = cooldown;
	}
}
