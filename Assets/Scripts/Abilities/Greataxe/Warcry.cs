using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Warcry: Ability {
	public float radius;
	public float stunDuration;

	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {}
	override public void Passive(Player player, List<GameObject> targets) {}
}
