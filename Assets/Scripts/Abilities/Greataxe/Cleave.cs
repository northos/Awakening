using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cleave : Ability {
	public float range;
	public float arc;

	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {}
	override public void Passive(Player player, List<GameObject> targets) {}
}
