using UnityEngine;
using System.Collections;

public class LightTower : BaseTower {

	public LightTower () : base (200.0F, 5.0F, 3, "lightTower", 10.0F, 100){
		
	}

	// Use this for initialization
	new void Start () {
		base.Start ();
	}
}
