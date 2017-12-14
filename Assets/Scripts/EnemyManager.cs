using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	// All of the enemy prefabs - set in the editor
	public GameObject airCraftCarrier, battleShip, cruiser, destroyer, galleon, rowBoat, sailBoat, speedBoat, superYacht;

	// Maps enemy names to their prefabs
	private Dictionary<string, GameObject> enemyDict;

	private Vector3 startLoc;

	private int enemyCounter;
	private int enemySpawnDelay;
	private int enemySpawnDelayMax = 50;
	private int numEnemiesDestroyed;

	private int numEnemiesInRound;

	private MapScript mapScript;

	private MapScript.WaveInfo waveInfo;

	private MapScript.EnemyGroupInfo enemyGroupInfo;

	private int enemyGroupNum, enemyCounterForGroup;
	private GameObject curEnemyGameObject;

	/**
	 * Initialize some basic info used by the enemy manager.
	 */
	void Start(){
		// Set up the dictionary used to select enemy prefabs based on name.
		enemyDict = new Dictionary<string, GameObject> ();

		enemyDict.Add (MapInfo.airCraftCarrierName, airCraftCarrier);
		enemyDict.Add (MapInfo.battleShipName, battleShip);
		enemyDict.Add (MapInfo.cruiserName, cruiser);
		enemyDict.Add (MapInfo.destroyerName, destroyer);
		enemyDict.Add (MapInfo.galleonName, galleon);
		enemyDict.Add (MapInfo.rowBoatName, rowBoat);
		enemyDict.Add (MapInfo.sailBoatName, sailBoat);
		enemyDict.Add (MapInfo.speedBoatName, speedBoat);
		enemyDict.Add (MapInfo.superYachtName, superYacht);
	}

	public void levelLoad(){
		mapScript = GameObject.Find ("MapAdmin").GetComponent<MapScript> ();
		
		startLoc = mapScript.getStartLoc();

		enemyCounter = enemySpawnDelay = numEnemiesDestroyed = 0;
	}

	public void setUpNextRound(){
		int roundNum = GameManager.getRoundNum ();
		enemyCounter = numEnemiesDestroyed = 0;

		waveInfo = mapScript.getRoundWaveInfo (roundNum); // Grab info for new round/wave

		numEnemiesInRound = waveInfo.getNumEnemiesInWave ();

		enemyGroupNum = enemyCounterForGroup = 0;
		setUpEnemyGroupInfo ();
	}

	// Update is called once per frame
	void Update () {
		if(GameManager.curGameState == GameState.RoundPlaying){ // Only spawning enemies when the game is playing
			if(numEnemiesDestroyed == numEnemiesInRound){
				GameManager.curGameState = GameState.LevelCompleted;
			}

			if(enemyCounter >= numEnemiesInRound) // Done spawning enemies
				return; 

			enemySpawnDelay++;

			if (enemySpawnDelay >= enemySpawnDelayMax) { // Time to spawn an enemy
				enemySpawnDelay = 0;
				spawnEnemy();
				enemyCounter++;
				enemyCounterForGroup++;
			}
		}
	}

	/**
	 * Sets up for using the next group of enemies - uses the group info to select the enemy
	 * prefab to use for object creation & other info like # enemies in group.
	 */
	private void setUpEnemyGroupInfo(){
		enemyGroupInfo = waveInfo.getEnemyGroup (enemyGroupNum);

		enemyCounterForGroup = 0;

		string enemyTypeName = enemyGroupInfo.getEnemyTypeName ();

		curEnemyGameObject = enemyDict[enemyTypeName];
	}

	void spawnEnemy(){
		// TODO: Update the EnemyGroupInfo if necessary - get enemyName & numEnemies

		if (enemyCounterForGroup == enemyGroupInfo.getNumEnemies ()) {
			enemyGroupNum++;
			setUpEnemyGroupInfo ();
		}

		GameObject e = Instantiate(curEnemyGameObject, startLoc, Quaternion.identity) as GameObject;
		e.GetComponent<Enemy>().setHealth(50);
		e.GetComponent<Enemy> ().setEnemyManager (this);
		// TODO: "start"
	}

	// 0 reward means wasn't killed, reached the end
	public void onEnemyDeath(int killWorth){
		numEnemiesDestroyed++;

		if (killWorth == 0) {
			GameManager.onEnemyLeak ();
		} 
		else { // Tally the kill, get money
			GameManager.onEnemyKill (killWorth);
		}
	}
}