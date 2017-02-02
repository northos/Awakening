using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoomerangProjectile : MonoBehaviour {
	public float range;
	public float flySpeed;
	public float hitRadius;
	public float damage;
	public Vector3 direction;
	public Player player;
	public List<GameObject> targets;
	float distanceTraveled = 0f;

	// on colliding with an object, damage it
	// enemies can be hit multiple times this way, but only if the projectile exits and re-enters
	// TODO: fix
	void OnCollisionEnter (Collision c) {
		print ("hit");
		if (c.gameObject.tag == "Enemy") {
			if (c.gameObject.GetComponent<Enemy> ().TakeDamage (damage))
				targets.Remove (c.gameObject);
		}
	}

	// move projectile as follows:
	//  * if range has not been reached, go straight in the target direction
	//  * once range has been reached, go straight back towards the player and destroy self when within range
	void Update () {
		// first, go in the target direction until chosen range is reached
		if (distanceTraveled < range) {
			Vector3 movement = direction * flySpeed * Time.deltaTime;
			transform.Translate (movement);
			distanceTraveled += movement.magnitude;
		} else {
			// move straight back towards the player
			Vector3 targetVector = (player.transform.position - transform.position).normalized;
			transform.Translate (targetVector * flySpeed * Time.deltaTime);
		}
		// damage enemies passed along the way
		/*List<GameObject> killed = new List<GameObject>();
		foreach (GameObject target in targets) {
			if (Vector3.Distance (transform.position, target.transform.position) <= hitRadius) {
				// do damage to those close enough; if this kills them, add to a list of kills
				if (((Enemy)target.GetComponent (typeof(Enemy))).TakeDamage (damage * Time.deltaTime)) {
					killed.Add (target);
				}
			}
		}
		// remove killed enemies from targets
		foreach (GameObject kill in killed) {
			targets.Remove (kill);
		}*/
		// if within hit range of player, stop and destroy self
		// wait until range has been reached, so it doesn't die immediately
		if (Vector3.Distance (player.transform.position, transform.position) <= hitRadius && distanceTraveled >= range) {
			Destroy (gameObject);
		}
	}
}
