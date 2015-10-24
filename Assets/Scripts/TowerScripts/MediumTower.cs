using UnityEngine;
using System.Collections;

public class MediumTower : BaseTower {

	public MediumTower() : base(150.0F, 10.0F, 10, "mediumTower", 8.0F, 125){
		
	}

	// Use this for initialization
	new void Start () {

		base.Start ();
	}
}