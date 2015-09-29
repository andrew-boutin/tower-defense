using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	[HideInInspector]
	public GameObject selectedTower;

	private TowerCreator towerCreator;
	private GameManager gameManager;
	private GameMenu gameMenu;
	private TowerInfo towerInfo;
	private EnemyManager enemyManager;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	// Remembers state information, makes decisions
	// gets input from others, informs whoever needs it whenever they need it

	// TODO: Next round....

	void Start(){
		towerCreator = gameObject.GetComponent<TowerCreator> ();
		gameManager = gameObject.GetComponent<GameManager> ();
		gameMenu = gameObject.GetComponent<GameMenu> ();
		towerInfo = gameObject.GetComponent<TowerInfo> ();
		enemyManager = gameObject.GetComponent<EnemyManager> ();
	}

	public void createTower(string tower){
		selectedTower = towerInfo.getTowerGameObject (tower);
		int towerCost = towerInfo.getTowerCost (tower);

		int curMoney = gameManager.getCurMoney ();

		if(curMoney - towerCost >= 0)
			towerCreator.createTower(selectedTower);
		else{
			// TODO: Display error info on menu!
		}
	}

	public void towerCreated(int towerCost){
		gameManager.subtractMoney (towerCost);
		gameMenu.setCurMoney (gameManager.getCurMoney ());
	}

	public void requestStartRound(){
		if (GameManager.curGameState == GameState.RoundNotStarted) { // have to be roundNotStarted
			gameManager.roundNum += 1;
			enemyManager.setUpNextRound();
			GameManager.curGameState = GameState.RoundPlaying;
		}
	}

	public void onEnemyKill(int val){
		// TODO: Thread safe...?
		gameManager.numKills++;
		gameManager.addMoney (val);
		gameMenu.setCurMoney (gameManager.getCurMoney ());
	}
	
	public void requestPause(){ 
		if (GameManager.curGameState == GameState.RoundPlaying) { // have to be roundPlaying
			// TODO: something to pause...
			GameManager.curGameState = GameState.Paused;
		}
	}

	public void resumeRound(){
		if (GameManager.curGameState == GameState.Paused) { // have to be roundPlaying
			// TODO: .....
			GameManager.curGameState = GameState.RoundPlaying;
		}
	}

	public void goToMainMenu(){
		GameManager.curGameState = GameState.NotInGame;
		// TODO: Other things...
		Application.LoadLevel("MainMenu");
	}

	public void towerClicked(BaseTower baseTower){
		if(gameMenu.currentSelectedTower != null){
			gameMenu.currentSelectedTower.showHideSelector(false);
		}

		gameMenu.currentSelectedTower = baseTower;
	}

	public void destroyTower(){
		BaseTower curBaseTower = gameMenu.currentSelectedTower;

		if(curBaseTower == null)
			return;

		int value = curBaseTower.destroyReward;
		Destroy (curBaseTower.gameObject);
		gameMenu.currentSelectedTower = null;
		gameManager.addMoney (value);
		gameMenu.setCurMoney (gameManager.getCurMoney ());
	}
}
