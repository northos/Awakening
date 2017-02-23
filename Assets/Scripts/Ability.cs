using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Ability : MonoBehaviour {
	// basic class data that all Abilities will need:
	//  * an icon to display on the ability bar
	//  * a cooldown duration (though some will be 0)
	//  * an amount of damage to inflict
	//  * a text description of the effect
	//  * a countdown of the time remaining in the cooldown
	public Sprite icon;
	public int cooldown;
	public float damage;
	public string description;
	public float cooldownTimer;

	// all Abilities can override these three methods to define their functionality (some will implement one or more to do nothing)
	//  * the effect on activating the ability - this MUST be overridden as there is really no common functionality
	//  * the effect of the ability upon being hit by an enemy
	//  * the passive effect of the ability over time (most will just manage the cooldown timer)
	abstract public void Execute(Player player, Vector3 direction, List<GameObject> targets);

	// most abilities have no on-hit functionality; this is the default
	virtual public float OnHit(Player player, GameObject attacker, float hitDamage){return hitDamage;}

	// most abilities will just passively track the cooldown timer; this is the default
	virtual public void Passive(Player player, List<GameObject> targets){
		// count down cooldown timer
		cooldownTimer = Mathf.Max (0f, cooldownTimer - Time.deltaTime);
	}
}
