using UnityEngine;
using System.Collections;

public class MediumTower : BaseTower {
	public static int buildCost = 250;
	public static int destroyReward = 125;

	// Use this for initialization
	new void Start () {
		base.destroyReward = destroyReward;
		base.buildCost = buildCost;

		base.bulletSpeed = 150.0F;
		base.fireSpeed = 25.0F;
		base.bulletDamage = 10;
		base.towerName = "mediumTower";
		base.turnDamp = 3.0F;

		base.Start ();
	}
}
