using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public GameObject enemy;

	private Vector3 startLoc;

	private int enemyCounter;
	private int enemySpawnDelay;
	private int enemySpawnDelayMax = 125;
	private int numEnemiesDestroyed;

	private int numEnemiesInRound;

	private MapScript mapScript;
	private InputHandler inputHandler;

	public void levelLoad(){
		mapScript = GameObject.Find ("MapAdmin").GetComponent<MapScript> ();
		inputHandler = gameObject.GetComponent<InputHandler> ();
		
		startLoc = mapScript.getStartLoc();

		enemyCounter = enemySpawnDelay = numEnemiesDestroyed = 0;
	}

	public void setUpNextRound(){
		numEnemiesInRound = mapScript.getNumEnemiesForRound (GameManager.getRoundNum());
		enemyCounter = numEnemiesDestroyed = 0;
		// TODO: Enemy spawn delay?
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
				enemyCounter++;
				spawnEnemy();
			}
		}
	}

	void spawnEnemy(){
		GameObject e = Instantiate(enemy, startLoc, Quaternion.identity) as GameObject;
		e.GetComponent<Enemy>().setHealth(50);
		e.GetComponent<Enemy> ().setEnemyManager (this);
		// TODO: "start"
	}

	// 0 reward means wasn't killed, reached the end
	public void onEnemyDeath(int reward){
		if (reward == 0) {
			// TODO: Take away health....
		} 
		else { // Tally the kill, get money
			inputHandler.onEnemyKill (reward);
		}

		numEnemiesDestroyed++;
	}
}