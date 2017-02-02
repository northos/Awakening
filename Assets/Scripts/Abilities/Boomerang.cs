using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boomerang : Ability {
	public GameObject projectile;

	// throw your shield so that it travels straight in the target direction, then returns to the player, damaging enemies on the way
	override public void Execute(Player player, Vector3 direction, List<GameObject> targets){
		if (cooldownTimer != 0f)
			return;
		// spawn a shield projectile on the player headed in the target direction
		GameObject newProjectile = (GameObject)Instantiate (projectile, player.transform.position, player.transform.rotation);
		newProjectile.GetComponent<BoomerangProjectile>().direction = direction;
		newProjectile.GetComponent<BoomerangProjectile>().player = player;
		newProjectile.GetComponent<BoomerangProjectile>().targets = targets;
		cooldownTimer = cooldown;
	}
	
	// this ability has no on-hit portion (player takes normal damage)
	override public float OnHit(Player player, GameObject attacker, float hitDamage){return hitDamage;}
	
	// passively track cooldown timer
	override public void Passive(Player player, List<GameObject> targets){
		// count down cooldown timer
		cooldownTimer = Mathf.Max (0f, cooldownTimer - Time.deltaTime);
	}
}
