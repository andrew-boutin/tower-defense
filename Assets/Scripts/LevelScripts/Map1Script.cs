using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map1Script : MapScript {
	// Use this for initialization
	void Start () {
		base.nonPlayableAreas = new List<int> (){
			0,3,
			1,3,
			2,3, 2,4, 2,5, 2,6, 2,7, 2,8, 2,9,
			                              3,9,
			                              4,9,
	   5,2, 5,3, 5,4, 5,5, 5,6, 5,7, 5,8, 5,9,
	   6,2,
	   7,2,
	   8,2, 8,3, 8,4, 8,5, 8,6, 8,7, 8,8,
			                         9,8,
			                         10,8,
			                         11,8,
		};

		base.wayPoints = new List<Vector3> (){ new Vector3(-3.65f, 5.0f, 0), new Vector3(-3.65f, 2.8f, 0), 
			new Vector3(1.15f, 2.8f, 0), new Vector3(1.15f, 0.4f, 0), new Vector3(-4.45f, 0.4f, 0),
			new Vector3(-4.45f, -2.0f, 0), new Vector3(0.35f, -2.0f, 0), new Vector3(0.35f, -4.5f)};

		base.startingMoneyAmount = 500;

		List<EnemyGroupInfo> wave1List = new List<EnemyGroupInfo> (){ new EnemyGroupInfo (MapInfo.airCraftCarrierName, 5),
			new EnemyGroupInfo (MapInfo.battleShipName, 5), new EnemyGroupInfo (MapInfo.cruiserName, 5)
		},
		wave2List = new List<EnemyGroupInfo> (){new EnemyGroupInfo(MapInfo.destroyerName, 10),
			new EnemyGroupInfo(MapInfo.galleonName, 15), new EnemyGroupInfo(MapInfo.rowBoatName, 20)
		},
		wave3List = new List<EnemyGroupInfo> (){new EnemyGroupInfo(MapInfo.sailBoatName, 20),
			new EnemyGroupInfo(MapInfo.speedBoatName, 20), new EnemyGroupInfo(MapInfo.superYachtName, 20)
		};

		base.mapWaveInfo = new List<WaveInfo> (){
			new WaveInfo(wave1List), new WaveInfo(wave2List), new WaveInfo(wave3List)
		};

		GameObject.Find ("Main Object").GetComponent<GameManager> ().levelLoad ();
	}
}
