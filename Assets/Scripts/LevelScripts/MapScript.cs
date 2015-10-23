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

	/**
	 * Retrieves the wave object for the current round.
	 */
	public WaveInfo getRoundWaveInfo(int roundNum){
		return mapWaveInfo[roundNum - 1];
	}

	/**
	 * Waves make up a 'game/map' of enemies. They're made up of multiple groups of enemies.
	 * Waves can have specific attributes applied to all of the groups in it.
	 */
	public class WaveInfo{
		List<EnemyGroupInfo> enemyGroups;

		private int numEnemiesInWave = 0;

		//int roundCompletionBonus; // TODO: Add in these
		//string endOfRoundMessage; // TODO: Add in these
		
		public WaveInfo(List<EnemyGroupInfo> eGroups){
			enemyGroups = eGroups;

			foreach(EnemyGroupInfo enemyGroupInfo in enemyGroups)
				numEnemiesInWave += enemyGroupInfo.getNumEnemies();
		}

		public int getNumEnemiesInWave(){
			return numEnemiesInWave;
		}

		public int getNumEnemyGroups(){
			return enemyGroups.Count;
		}

		public EnemyGroupInfo getEnemyGroup(int enemyGroupNum){
			return enemyGroups[enemyGroupNum];
		}
	}

	/**
	 * Enemy groups make up a wave of enemies. They're all the same type and can have specific
	 * attribute modifications to their group.
	 */
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

		public string getEnemyTypeName(){
			return enemyName;
		}
	}
}
