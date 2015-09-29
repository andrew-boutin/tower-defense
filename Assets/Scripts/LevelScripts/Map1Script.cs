using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map1Script : MapScript {
	// Use this for initialization
	void Start () {
		base.nonPlayableAreas = new List<int> (){
			0,6, 1,6, 2,6, 3,6, 3,7, 3,8, 3,9, 3,10, 3,11, 4,11, 5,11, 6,11, 7,11, 7,10, 
			7,9, 7,8, 7,7, 7,6, 7,5, 7,4, 8,4, 9,4, 10,4, 11,4, 11,5, 11,6, 11,7, 11,8, 11,9, 11,10, 11,11,
			11,12, 11,13, 11,14, 12,14, 13,14, 14,14, 15,14, 15,13, 15,12, 15,11, 15,10, 15,9, 15,8, 15,7,
			15,6, 15,5, 15,4, 15,3, 15,2, 16,2, 17,2
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
