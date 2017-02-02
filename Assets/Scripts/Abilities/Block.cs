using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : Ability {
	public int blockChance;
	public int blockBonus;
	public float duration;
	bool active;
	float timer = 0f;

	// activate block bonus ability as long as it's not on cooldown (lasts a given duration)
	override public void Execute(Player player, Vector3 direction, List<GameObject> targets){
		if (cooldownTimer == 0f) {
			active = true;
		}
	}
	
	// block incoming attack (taking no damage) if block chance is rolled
	// includes a bonus to block chance while ability is active
	override public float OnHit(Player player, GameObject attacker, float hitDamage){
		if (active && Random.Range (0, 100) <= blockChance + blockBonus) {
			return 0f;
		} else if (Random.Range (0, 100) <= blockChance) {
			return 0f;
		}
		return hitDamage;
	}
	
	// track the duration for which the block bonus ability has been active, and deactivate it when the duration has passed
	// also track the cooldown timer after ability has worn off
	override public void Passive(Player player, List<GameObject> targets){
		if (active) {
			timer = Mathf.Max (0f, timer - Time.deltaTime);
			if (timer == 0f) {
				cooldownTimer = cooldown;
				active = false;
			}
		} else {
			// countdown cooldown timer; ability will check that it's run out before being activated again
			cooldownTimer = Mathf.Max (0f, cooldownTimer - Time.deltaTime);
		}
	}
}
