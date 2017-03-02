using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingOfBlades : Ability {
	public float duration;
	float durationCounter;
	public int numBlades;
	public List<GameObject> blades;
	public GameObject bladePrefab;
	public float radius;


	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {
		if (cooldownTimer != 0f)
			return;

		for (int i = 0; i < numBlades; ++i) {
			GameObject blade = Instantiate (bladePrefab);
			blades.Add (blade);
			Quaternion offsetRotation = Quaternion.AngleAxis (360f * i / numBlades, Vector3.forward);
			Vector3 offsetVector = offsetRotation * Vector3.up;
			blade.transform.position = player.transform.position + offsetVector;
			print (360f * i / numBlades);
			blade.transform.Rotate (0, 0, 360f * i / numBlades);
			blade.GetComponent<RingOfBladesProjectile> ().player = player;
			blade.GetComponent<RingOfBladesProjectile> ().orbitDistance = radius;
		}
			
		cooldownTimer = cooldown;
		durationCounter = 0;
	}

	// passively track cooldown and duration timers
	// when duration is reached, destroy all remaining projectiles
	override public void Passive(Player player, List<GameObject> targets){
		// count down cooldown timer
		cooldownTimer = Mathf.Max (0f, cooldownTimer - Time.deltaTime);
		// count up duration timer
		durationCounter = Mathf.Min (duration, durationCounter + Time.deltaTime);
		// if duration has been reached, destroy all remaining projectiles
		if (durationCounter == duration) {
			foreach (GameObject blade in blades) {
				Destroy (blade);
			}
			blades.Clear ();
		}
	}
}
