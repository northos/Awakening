using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamehead : Enemy {
	public float detectRange;
	public float attackRange;
	public float hitRange;
	public float moveSpeed;
	public float hitDamage; 
	Vector3 targetLocation = Vector3.zero;

	// behavior for Flamehead enemies:
	//  * if player is within <detectRange>, move towards player
	//  * if player is within <attackRange>, charge attack (duration based on animation)
	//  * once attack is charged, attack in the direction of the player
	//    * if player is still in range, inflict damage
	//    * remain in attack stance until animation finishes
	//  * otherwise, wander slowly while player is too far away
	protected override void Update () {
		// set targetLocation back to zero once within hitRange of the target
		// this prevents further pathing until the player is detected again
		if (Vector3.Distance (targetLocation, transform.position) <= hitRange) {
			targetLocation = Vector3.zero;
		}

		// when player is within detection range, do a raycast towards them  to check for line of sight
		// if LOS is available, store the player's location
		if (Vector3.Distance (transform.position, player.transform.position) <= detectRange && !animator.GetBool ("Charging") && !animator.GetBool ("Attacking")) {
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)transform.position, (Vector2)(player.transform.position - transform.position), detectRange, enemyMask);
			if (hit.transform.gameObject.tag == "Player") {
				targetLocation = hit.transform.position;
			}
		}

		// if a target location has been found, move towards it
		if (targetLocation != Vector3.zero) {animator.SetBool ("Walking", true);
			// move at set walking speed towards player
			Vector3 moveVector = (targetLocation - transform.position).normalized * moveSpeed;
			transform.Translate (moveVector * Time.deltaTime);
		}

		// when player is within attack range, begin chargingi an attack
		if (Vector3.Distance (transform.position, player.transform.position) <= attackRange && !animator.GetBool ("Charging") && !animator.GetBool ("Attacking")) {
			animator.SetBool ("Walking", false);
			animator.SetBool ("Charging", true);
		}
	}

	// on colliding with an object tagged "Wall", stop pathing (to avoid funky behavior if the target is on the other side of a wall)
	void OnCollisionStay2D (Collision2D coll) {
		if (coll.gameObject.tag == "Wall") {
			targetLocation = Vector3.zero;
		}
	}

	// public function to perform attack on player
	// called as an event as the "prep" animation ends
	// damage the player if they're within hit range (so hit area is a circle around the Flamehead)
	public void attack () {
		animator.SetBool ("Charging", false);
		animator.SetBool ("Attacking", true);
		if (Vector3.Distance (transform.position, player.transform.position) <= hitRange) {
			player.GetComponent<Player> ().TakeDamage (hitDamage, gameObject);
		}
	}

	// public function to end attack on player
	// called as an event as the "attack" animation ends
	public void endAttack() {
		animator.SetBool ("Attacking", false);
	}
}
