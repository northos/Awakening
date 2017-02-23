using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingOfBlades : Ability {
	public float duration;
	public int numBlades;
	public List<GameObject> blades;
	public GameObject bladePrefab;
	public float radius;

	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {}
	override public void Passive(Player player, List<GameObject> targets) {}
}
