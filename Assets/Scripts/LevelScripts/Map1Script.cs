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

		base.wayPoints = new List<Vector3> (){ new Vector3(-3.0f, 5.0f, 0), new Vector3(-3.0f, 2.9f, 0), 
			new Vector3(-0.3f, 2.9f, 0), new Vector3(-0.3f, 0.8f, 0), new Vector3(-4.1f, 0.77f, 0),
			new Vector3(-4.1f, -1.4f, 0), new Vector3(1.3f, -1.4f, 0), new Vector3(1.3f, -3.5f, 0),
			new Vector3(-5.2f, -3.5f, 0), new Vector3(-5.2f, -4.9f, 0)};

		base.startingMoneyAmount = 500;

		List<EnemyGroupInfo> wave1List = new List<EnemyGroupInfo> (){ new EnemyGroupInfo ("lightEnemy", 5),
			new EnemyGroupInfo ("mediumEnemy", 5), new EnemyGroupInfo ("heavyEnemy", 5)
		};

		List<EnemyGroupInfo> wave2List = new List<EnemyGroupInfo> (){new EnemyGroupInfo("lightEnemy", 10),
			new EnemyGroupInfo("lightEnemy", 15), new EnemyGroupInfo("lightEnemy", 20)
		};

		List<EnemyGroupInfo> wave3List = new List<EnemyGroupInfo> (){new EnemyGroupInfo("heavyEnemy", 20),
			new EnemyGroupInfo("mediumEnemy", 20), new EnemyGroupInfo("lightEnemy", 20)
		};

		base.mapWaveInfo = new List<WaveInfo> (){
			new WaveInfo(wave1List), new WaveInfo(wave2List), new WaveInfo(wave3List)

		};

		GameObject.Find ("Main Object").GetComponent<GameManager> ().levelLoad ();
	}
}
