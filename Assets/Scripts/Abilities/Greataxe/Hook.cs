﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hook: Ability {
	public float arc;
	public float range;

	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {}
	override public void Passive(Player player, List<GameObject> targets) {}
}
