using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapScript : MonoBehaviour {
	[HideInInspector]
	public List<int> nonPlayableAreas;
	[HideInInspector]
	public List<Vector3> wayPoints;
	[HideInInspector]
	public int startingMoneyAmount;
	[HideInInspector]
	public List<WaveInfo> mapWaveInfo;


	public int getNumRounds(){
		return mapWaveInfo.Count;
	}

	// Returns the starting location for enemies on this map
	public Vector3 getStartLoc(){
		return wayPoints [0];
	}

	// Returns the number of enemies for a given round on this map
	public int getNumEnemiesForRound(int roundNum){
		return mapWaveInfo[roundNum - 1].getTotalNumEnemiesInWave();
	}

//	public int getNumEnemiesForWave(){
//
	//}

	public class WaveInfo{
		List<EnemyGroupInfo> enemyGroups;
		
		//int roundCompletionBonus; // TODO: Add in these
		
		//string endOfRoundMessage; // TODO: Add in these
		
		public WaveInfo(List<EnemyGroupInfo> eGroups){
			enemyGroups = eGroups;
		}

		public int getTotalNumEnemiesInWave(){
			int counter = 0;

			foreach(EnemyGroupInfo enemyGroupInfo in enemyGroups)
				counter += enemyGroupInfo.getNumEnemies();

			return counter;
		}
	}

	public class EnemyGroupInfo{
		public string enemyName;
		public int numEnemies;
		//public float speedMultiplier; // TODO: Add in these
		//public float healthMultiplier; // TODO: Add in these
		//public float spawnInterval; // TODO: Add in these

		public EnemyGroupInfo(string eName, int numE){
			enemyName = eName;
			numEnemies = numE;
		}

		public int getNumEnemies(){
			return numEnemies;
		}
	}
}
