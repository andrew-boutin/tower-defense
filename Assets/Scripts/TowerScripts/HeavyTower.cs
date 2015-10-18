using UnityEngine;
using System.Collections;

public class HeavyTower : BaseTower {

	public HeavyTower() : base(100.0F, 40.0F, 20, "heavyTower", 1.0F, 150){
		
	}

	// Use this for initialization
	new void Start () {

		base.Start ();
	}
}