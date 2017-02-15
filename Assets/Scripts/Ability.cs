using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Ability : MonoBehaviour {
	public Sprite icon;
	public int cooldown;
	public float damage;
	public string description;
	public float cooldownTimer;

	abstract public void Execute(Player player, Vector3 direction, List<GameObject> targets);
	abstract public float OnHit(Player player, GameObject attacker, float hitDamage);
	abstract public void Passive(Player player, List<GameObject> targets);
}
