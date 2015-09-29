using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	// TODO: Take out anything specific to an individual enemy and put it into another script with enemy name or type
	// push the information down to this base class

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

		// TODO set targetWaypoint as first one in list
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
		if(GameManager.curGameState == GameState.Paused)
			return;

		if (health <= 0) {
			die ();
		}

		move ();
	}

	void move(){
		transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, Time.deltaTime * speed);

		if(transform.position == targetWaypoint){
			if(wayPoints.Count > 0){
				targetWaypoint = wayPoints[0];
				wayPoints.Remove(targetWaypoint);
			}
			else{
				Destroy(gameObject);
			}
		}
	}

	void die(){
		// TODO: Have to inform someone that death happens - to tally kills and award money
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

	// TODO: Have a "start" method that starts the enemy movement...
}
