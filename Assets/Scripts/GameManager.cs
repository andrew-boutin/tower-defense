using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private GameMenu gameMenu;
	private int curMoney;

	private EnemyManager enemyManager;
	private GridManager gridManager;

	[HideInInspector]
	public static GameState curGameState;

	private static int roundNum;
	private static int numTotalRounds;

	[HideInInspector]
	public int numKills;

	private MapScript mapScript;

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
		mapScript = GameObject.Find ("MapAdmin").GetComponent<MapScript> ();
		curMoney = mapScript.startingMoneyAmount;
		numTotalRounds = mapScript.getNumRounds();

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

	public void setUpNextRound(){
		roundNum++;
	}

	public static int getRoundNum(){
		return roundNum;
	}

	public static int getNumTotalRounds(){
		return numTotalRounds;
	}
}
