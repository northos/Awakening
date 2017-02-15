using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Charge : Ability {
	public float distance;
	public float speed;
	public float splashRadius;
	public float impactRange;
	float distanceTraveled;
	Vector3 targetVector = Vector3.zero;
	bool active = false;

	// charge towards the mouse cursor at the chosen speed
	// charge ends when player collides with an enemy or reaches the maximum charge distance
	override public void Execute(Player player, Vector3 direction, List<GameObject> targets){
		if (cooldownTimer != 0f)
			return;
		player.animator.SetBool ("Immobilized", true);
		active = true;
		targetVector = direction;
		distanceTraveled = 0f;
	}
	
	// this ability has no on-hit portion (player takes normal damage)
	override public float OnHit(Player player, GameObject attacker, float hitDamage){return hitDamage;}

	// helper function to carry out splash damage when charge ends
	// finds all enemies within splash radius and does the ability's damage to them
	void doSplashDamage (Player player, List<GameObject> targets) {
		List<GameObject> killed = new List<GameObject>();
		foreach (GameObject target in targets) {
			if (Vector3.Distance (player.transform.position, target.transform.position) <= splashRadius) {
				// do damage to those close enough; if this kills them, add to a list of kills
				if (((Enemy)target.GetComponent (typeof(Enemy))).TakeDamage (damage)) {
					killed.Add (target);
				}
			}
		}
		// remove killed enemies from targets
		foreach (GameObject kill in killed) {
			targets.Remove (kill);
		}
	}

	// while charge is active, move the player at the chosen speed towards the target
	// track how far the player has charged and how close they are to enemies in order to know when to stop the charge and deal splash damage
	// also manage the ability's cooldown
	override public void Passive(Player player, List<GameObject> targets){
		// count down cooldown timer
		cooldownTimer = Mathf.Max (0f, cooldownTimer - Time.deltaTime);
		// skip the rest if the charge isn't active
		if (!active)
			return;
		// do the player's movement at charge speed
		Vector3 movement = targetVector * speed * Time.deltaTime;
		player.transform.Translate (movement);
		distanceTraveled += movement.magnitude;
		// if the player has reached max distance, stop and do splash damage
		if (distanceTraveled >= distance) {
			active = false;
			player.animator.SetBool ("Immobilized", false);
			cooldownTimer = cooldown;
			//TODO
		}
		// then, if an enemy is close enough, stop and do splash damage
		else {
			foreach (GameObject enemy in targets) {
				// check each enemy's distance to see if it's in impact range
				if (Vector3.Distance (player.transform.position, enemy.transform.position) <= impactRange) {
					// when one is found, stop the charge
					active = false;
					player.animator.SetBool ("Immobilized", false);
					cooldownTimer = cooldown;
					// go through all again and damage those in splash range (handled by helper)
					doSplashDamage (player, targets);
					// no need to continue looking after one close enemy is found
					break;
				}
			}
		}
	}
}
