using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	// These variables are set in the editor to make the different kinds of enemies
	public string enemyName;
	public float health;
	public float speed = 0.8F;
	public int reward = 50;

	private List<Vector3> wayPoints = new List<Vector3> ();
	private Vector3 targetWaypoint;

	private EnemyManager enemyManager;

	// Use this for initialization
	void Start () {
		foreach (Vector3 vec in GameObject.Find("MapAdmin").GetComponent<MapScript>().wayPoints) {
			wayPoints.Add(vec);
		}

		if (wayPoints.Count > 0){
			targetWaypoint = wayPoints [0];
			wayPoints.Remove(targetWaypoint);
		}
		else{
			Debug.Log("Enemy didn't have any waypoints, error!");
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.curGameState == GameState.RoundPlaying) {
			if (health <= 0)
				die ();
			else
				move ();
		}
	}

	void move(){
		transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, Time.deltaTime * speed);

		if(transform.position == targetWaypoint){
			if(wayPoints.Count > 0){
				targetWaypoint = wayPoints[0];
				wayPoints.Remove(targetWaypoint);
				faceTargetVec3 (targetWaypoint); // Rotate enemy towards next waypoint
			}
			else{ // Wasn't killed, reached the target
				enemyManager.onEnemyDeath(0); // No reward
				Destroy(gameObject);
			}
		}
	}

	void die(){
		enemyManager.onEnemyDeath (reward);
		Destroy (gameObject);
	}

	public void setHealth(float h){
		health = h;
	}

	public void takeDamage(float damage){
		health -= damage;
	}

	public void setEnemyManager(EnemyManager eM){
		enemyManager = eM;
	}

	/**
	 * Rotates the enemy object to face its next way point.
	 */
	public void faceTargetVec3(Vector3 rotTarget){
		float diffX = rotTarget.x - transform.position.x;
		float diffY = rotTarget.y - transform.position.y;
		float angle = (Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg) - 90;
		Vector3 rotVec = new Vector3 (0, 0, angle);
		transform.rotation = Quaternion.Euler(rotVec);
	}
}
