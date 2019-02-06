using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map2Script : MapScript {
	// Use this for initialization
	void Start () {
		base.nonPlayableAreas = new List<int> (){
			     0,1,                                               0,11,
			     1,1,           1,4,
			     2,1, 2,2, 2,3, 2,4, 2,5, 2,6, 2,7,      2,9,
			                                   3,7,
			     4,1,                          4,7, 4,8,
			                                   5,7, 5,8, 5,9, 5,10, 
			                    6,4, 6,5,                     6,10,
			     7,1,                                         7,10,
			                                   8,7,           8,10,
			          9,2, 9,3, 9,4, 9,5, 9,6, 9,7, 9,8, 9,9, 9,10,
			          10,2,                                         10,11,
			          11,2,     11,4,                         11,10,
		};

		// x 4.45, 3.65, 1.15, 0.35
		// .35, 1.15, 3.65, 4.45
		// .8
		//
		// y 5, 2.8, .4, 2, 4.5
		// -.4 .4
		//
		//base.wayPoints = new List<Vector3> (){ new Vector3(-4.45f, 5.0f, -1), new Vector3(-3.65f, 2.8f, -1), 
		//	new Vector3(1.15f, 2.8f, -1), new Vector3(1.15f, 0.4f, -1), new Vector3(-4.45f, 0.4f, -1),
		//	new Vector3(-4.45f, -2.0f, -1), new Vector3(0.35f, -2.0f, -1), new Vector3(0.35f, -4.5f, -1)};

		// -4.45f
		base.wayPoints = new List<Vector3> (){ new Vector3(-5.25f, 5.0f, -1), new Vector3(-5.25f, 2.8f, -1), 
			new Vector3(-0.4f, 2.8f, -1), new Vector3(-0.4f, 0.4f, -1), new Vector3(2.0f, 0.4f, -1),
			new Vector3(2.0f, -2.8f, -1), new Vector3(-4.45f, -2.8f, -1), new Vector3(-4.45f, -4.5f, -1)};

		base.startingMoneyAmount = 400;

		List<EnemyGroupInfo> wave1List = new List<EnemyGroupInfo> (){ new EnemyGroupInfo (MapInfo.airCraftCarrierName, 5),
			new EnemyGroupInfo (MapInfo.battleShipName, 10), new EnemyGroupInfo (MapInfo.cruiserName, 15)
		},
		wave2List = new List<EnemyGroupInfo> (){new EnemyGroupInfo(MapInfo.destroyerName, 10),
			new EnemyGroupInfo(MapInfo.galleonName, 15), new EnemyGroupInfo(MapInfo.rowBoatName, 20)
		},
		wave3List = new List<EnemyGroupInfo> (){new EnemyGroupInfo(MapInfo.sailBoatName, 20),
			new EnemyGroupInfo(MapInfo.speedBoatName, 25), new EnemyGroupInfo(MapInfo.superYachtName, 30)
		};

		base.mapWaveInfo = new List<WaveInfo> (){
			new WaveInfo(wave1List), new WaveInfo(wave2List), new WaveInfo(wave3List)
		};

		GameObject.Find ("Main Object").GetComponent<GameManager> ().levelLoad ();
	}
}
