using UnityEngine;
using System.Collections;

public class LightTower : BaseTower {
	public static int buildCost = 200;
	public static int destroyReward = 100;

	// Use this for initialization
	new void Start () {
		base.destroyReward = destroyReward;
		base.buildCost = buildCost;

		base.bulletSpeed = 200.0F;
		base.fireSpeed = 15.0F;
		base.bulletDamage = 3;
		base.towerName = "lightTower";
		base.turnDamp = 6.0F;

		base.Start ();
	}
}
