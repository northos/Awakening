using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoomerangProjectile : MonoBehaviour {
	public float range;
	public float flySpeed;
	public float damage;
	public float hitRadius;
	public Vector3 direction;
	public Player player;
	public List<GameObject> targets;
	float distanceTraveled = 0f;

	// on colliding with an object, damage it
	// enemies can be hit multiple times this way, but only if the projectile exits and re-enters
	void OnTriggerEnter2D (Collider2D c) {
		if (c.gameObject.tag == "Enemy") {
			if (c.gameObject.GetComponent<Enemy> ().TakeDamage (damage))
				// remove any targets which are killed from the list of targets
				targets.Remove (c.gameObject);
		}
		// since this object will only have a Trigger collider, it will not interact with walls
		// this section causes it to reverse upon striking a wall, as one might expect (rather than pass through it)
		// if the object is already returning upon hitting the wall, it will destroy itself
		else if (c.gameObject.tag == "Wall") {
			if (distanceTraveled < range) {
				distanceTraveled = range;
			} else {
				// destroy self if returning as it doesn't make sense to do much else
				Destroy (gameObject);
			}
		}
	}

	// move projectile as follows:
	//  * if range has not been reached, go straight in the target direction
	//  * once range has been reached, go straight back towards the player and destroy self when within range
	void Update () {
		// first, go in the target direction until chosen range is reached
		if (distanceTraveled < range) {
			Vector3 movement = direction * flySpeed * Time.deltaTime;
			transform.position += movement;
			distanceTraveled += movement.magnitude;
		} else {
			// move straight back towards the player
			Vector3 targetVector = (player.transform.position - transform.position).normalized;
			transform.position += targetVector * flySpeed * Time.deltaTime;
		}
		// if within hit range of player, stop and destroy self
		// wait until range has been reached, so it doesn't die immediately
		if (Vector3.Distance (player.transform.position, transform.position) <= hitRadius && distanceTraveled >= range) {
			Destroy (gameObject);
		}
	}
}
