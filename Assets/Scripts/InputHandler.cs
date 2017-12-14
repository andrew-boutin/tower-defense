using UnityEngine;
using UnityEngine.SceneManagement;

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
		if(gameMenu.currentSelectedTower != null)
			gameMenu.currentSelectedTower.deSelected();

		selectedTower = towerInfo.getTowerGameObject (tower);
		int towerCost = TowerInfo.getTowerCost (tower);

		int curMoney = gameManager.getCurMoney ();

		if(curMoney - towerCost >= 0)
			towerCreator.createTower(selectedTower);
		else{
			// TODO: Display error info on menu!
		}
	}

	public void towerCreated(GameObject tower, int towerCost){
		gameMenu.currentSelectedTower = tower.GetComponent<BaseTower> ();;
		gameMenu.currentSelectedTower.selected ();

		gameManager.subtractMoney (towerCost);
		gameMenu.setCurMoney (gameManager.getCurMoney ());
	}

	public void requestStartRound(){
		if (GameManager.curGameState == GameState.RoundNotStarted || 
			GameManager.curGameState == GameState.LevelCompleted) {
			gameManager.setUpNextRound();
			enemyManager.setUpNextRound();
			GameManager.curGameState = GameState.RoundPlaying;
		}
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
		SceneManager.LoadScene("MainMenu");
	}

	/**
	 * If another tower was selected it hides its 'indicators' and saves the new tower reference as
	 * the currently selected one.
	 */
	public void towerClicked(BaseTower baseTower){
		if(gameMenu.currentSelectedTower != null && gameMenu.currentSelectedTower != baseTower)
			gameMenu.currentSelectedTower.deSelected();

		gameMenu.currentSelectedTower = baseTower;
	}

	// TODO: Set the costs here and in menu correctly
	public void upgradeBulletSpeed(BaseTower baseTower) {
		if (gameManager.getCurMoney () >= 25) {
			baseTower.increaseBulletSpeed ();
			gameManager.subtractMoney (25);
			gameMenu.setCurMoney (gameManager.getCurMoney ());
		}
	}

	public void upgradeTurnSpeed(BaseTower baseTower) {
		if (gameManager.getCurMoney () >= 25) {
			baseTower.increaseTurnSpeed ();
			gameManager.subtractMoney (25);
			gameMenu.setCurMoney (gameManager.getCurMoney ());
		}
	}

	public void upgradeBulletDamage(BaseTower baseTower) {
		if (gameManager.getCurMoney () >= 25) {
			baseTower.increaseBulletDamage ();
			gameManager.subtractMoney (25);
			gameMenu.setCurMoney (gameManager.getCurMoney ());
		}
	}

	public void upgradeFireDelay(BaseTower baseTower) {
		if (gameManager.getCurMoney () >= 25) {
			baseTower.decreaseFireDelay ();
			gameManager.subtractMoney (25);
			gameMenu.setCurMoney (gameManager.getCurMoney ());
		}
	}
}
