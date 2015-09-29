using UnityEngine;
using System.Collections;

public class HeavyTower : BaseTower {
	public static int buildCost = 300;
	public static int destroyReward = 150;

	// Use this for initialization
	new void Start () {
		base.destroyReward = destroyReward;
		base.buildCost = buildCost;

		base.bulletSpeed = 100.0F;
		base.fireSpeed = 40.0F;
		base.bulletDamage = 20;
		base.towerName = "heavyTower";
		base.turnDamp = 1.0F;

		base.Start ();
	}
}
