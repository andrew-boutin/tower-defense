using UnityEngine;
using System.Collections;

public class LightTower : BaseTower {

	public LightTower () : base (200.0F, 15.0F, 3, "lightTower", 6.0F, 100){
		
	}

	// Use this for initialization
	new void Start () {
		base.Start ();
	}
}
