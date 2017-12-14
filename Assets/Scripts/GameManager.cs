using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static GameMenu gameMenu;

	private EnemyManager enemyManager;
	private GridManager gridManager;

	[HideInInspector]
	public static GameState curGameState;

	private static int roundNum;
	private static int numTotalRounds;
	private static int numKills;
	private static int numLeaks;
	private static int curMoney;
	private static int health;
	private static int score;

	private MapScript mapScript;

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
		numLeaks = 0;

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

	public static int getNumKills() {
		return numKills;
	}

	public static void addKill() {
		numKills++;
	}

	public static int getNumLeaks() {
		return numLeaks;
	}

	public static void onEnemyLeak() {
		numLeaks++;

		health -= 10;

		if (health <= 0) {
			health = 0;
			curGameState = GameState.GameOver;
		}
	}

	private static void addMoney(int value) {
		curMoney += value;
		score += value;
	}

	public static void onEnemyKill(int killWorth) {
		numKills++;
		addMoney (killWorth);
		gameMenu.setCurMoney (curMoney);
	}

	public static void destroyTower(BaseTower curBaseTower) {
		int value = curBaseTower.getDestroyReward();
		Destroy (curBaseTower.gameObject);
		addMoney (value);
		gameMenu.setCurMoney (curMoney);
	}
}
