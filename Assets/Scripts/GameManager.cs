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

	private static int health;

	private static int score;

	// Use this for initialization
	void Start () {
		curGameState = GameState.NotInGame;
		gameMenu = gameObject.GetComponent<GameMenu>();
		enemyManager = gameObject.GetComponent < EnemyManager> ();
		gridManager = gameObject.GetComponent<GridManager> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void levelLoad(){
		mapScript = GameObject.Find ("MapAdmin").GetComponent<MapScript> ();
		curMoney = mapScript.startingMoneyAmount;
		numTotalRounds = mapScript.getNumRounds();

		gameMenu.setCurMoney(curMoney);
		curGameState = GameState.RoundNotStarted;
		roundNum = 0;
		numKills = 0;
		health = 100;
		score = 0;

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
		score += val;
	}

	public void setUpNextRound(){
		roundNum++;
		score += roundNum * health * 2;
		health = 100;
	}

	public static int getRoundNum(){
		return roundNum;
	}

	public static int getNumTotalRounds(){
		return numTotalRounds;
	}

	public static int getHealth() {
		return health;
	}

	public static int getScore() {
		return score;
	}
}
