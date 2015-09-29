using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public GameObject enemy;

	private Vector3 startLoc;

	private int enemyCounter;
	private int enemySpawnDelay;
	private int enemySpawnDelayMax = 100;

	private int numEnemiesInRound;
	private int numRounds;

	private int curRound;

	private MapScript mapScript;
	private InputHandler inputHandler;

	public void levelLoad(){
		mapScript = GameObject.Find ("MapAdmin").GetComponent<MapScript> ();
		inputHandler = gameObject.GetComponent<InputHandler> ();

		curRound = 0;
		
		startLoc = mapScript.getStartLoc();
		numRounds = mapScript.getNumRounds();
		
		enemyCounter = enemySpawnDelay = 0;
	}

	public void setUpNextRound(){
		if (curRound >= numRounds) {
			return;		
		}
		curRound++;

		numEnemiesInRound = mapScript.getNumEnemiesForRound (curRound);
		enemyCounter = 0;
	}

	// Update is called once per frame
	void Update () {
		if(enemyCounter >= numEnemiesInRound){ // when all enemies are gone the level/round is over
			// inform GUI?

			return; 
		}

		enemySpawnDelay++;
		if (enemySpawnDelay >= enemySpawnDelayMax) { // Time to spawn an enemy
			enemySpawnDelay = 0;
			enemyCounter++;
			spawnEnemy();
		}
	}

	/*
	public void createTower(GameObject tower){
		dragTower = Instantiate(tower, Vector3.zero, Quaternion.identity) as GameObject;
		dragTower.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.5f); // 50% transparent
		draggingTower = true;
	}
	*/

	void spawnEnemy(){
		GameObject e = Instantiate(enemy, startLoc, Quaternion.identity) as GameObject;
		e.GetComponent<Enemy>().setHealth(50);
		e.GetComponent<Enemy> ().setEnemyManager (this);
		// TODO: "start"
	}

	public void onEnemyDeath(int reward){
		inputHandler.onEnemyKill (reward);
	}
}