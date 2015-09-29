using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private GameMenu gameMenu;
	private int curMoney;

	private EnemyManager enemyManager;
	private GridManager gridManager;

	[HideInInspector]
	public static GameState curGameState;

	[HideInInspector]
	public int roundNum;

	[HideInInspector]
	public int numKills;

	// track the game state

	// track score
	// track money
	// track lives

	// Use this for initialization
	void Start () {
		curGameState = GameState.NotInGame;
		gameMenu = gameObject.GetComponent<GameMenu>();
		enemyManager = gameObject.GetComponent < EnemyManager> ();
		gridManager = gameObject.GetComponent<GridManager> ();
	}

	// Update is called once per frame
	void Update () {

	//	if (Input.GetMouseButtonDown (0)) { // Used for building waypoint system
	//		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	//		Debug.Log("X: " + pos.x + " | y: " + pos.y);		
	//	}

	}

	public void levelLoad(){
		curMoney = GameObject.Find("MapAdmin").GetComponent<MapScript>().startingMoneyAmount;
		gameMenu.setCurMoney(curMoney);
		curGameState = GameState.RoundNotStarted;
		roundNum = 0;
		numKills = 0;

		enemyManager.levelLoad ();
		gridManager.levelLoad ();
	}

	// TODO: Thread safe...?
	public int getCurMoney(){
		return curMoney;
	}

	public void subtractMoney(int val){
		curMoney -= val;
	}

	public void addMoney(int val){
		curMoney += val;
	}
}
