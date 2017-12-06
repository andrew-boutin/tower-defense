using UnityEngine;
using System.Collections;

// Handles drawing all GUI elements.  Borders, cell highlighting on map
// and controls panel.  Controls that are displayed depend on game 
// information.
public class GameMenu : MonoBehaviour {
	private int curMoney;

	private InputHandler inputHandler;

	[HideInInspector]
	public MenuState menuState;

	[HideInInspector]
	public BaseTower currentSelectedTower;

	private int lightTowerCost, mediumTowerCost, heavyTowerCost;

	// Use this for initialization
	void Start () {
		menuState = MenuState.OuterMenu;

		inputHandler = gameObject.GetComponent<InputHandler> ();

		lightTowerCost = TowerInfo.getTowerCost ("lightTower");
		mediumTowerCost = TowerInfo.getTowerCost ("mediumTower");
		heavyTowerCost = TowerInfo.getTowerCost ("heavyTower");
	}

	void OnGUI(){
		GameState curGameState = GameManager.curGameState;

		if(curGameState == GameState.NotInGame)
			return;

		displayMenuDividers ();

		// 200 pixel wide control panel on the right

		if(GUI.Button(new Rect(600, 12, 188, 50), "Main Menu")){
			inputHandler.goToMainMenu();
		}

		GUI.Label (new Rect (600, 62, 188, 25), "Crazy Canal Tower Defense", "box");
		GUI.Label (new Rect (600, 87, 188, 25), "Baking Bits Studios", "box");

		Rect playOptionRect = new Rect (600, 112, 188, 50);

		if (curGameState == GameState.RoundNotStarted) {
			if (GUI.Button (playOptionRect, "Start Round")) {
				inputHandler.requestStartRound ();
			}	
		} else if (curGameState == GameState.RoundPlaying) {
			if (GUI.Button (playOptionRect, "Pause Round")) {
				inputHandler.requestPause ();
			}	
		} else if (curGameState == GameState.Paused) {
			if (GUI.Button (playOptionRect, "Resume Round")) {
				inputHandler.resumeRound ();
			}	
		} else if (curGameState == GameState.LevelCompleted) {
			int roundNum = GameManager.getRoundNum ();

			if (roundNum == GameManager.getNumTotalRounds ()) {
				if(GUI.Button(playOptionRect, "End Game")){
					inputHandler.goToMainMenu ();
				}
			}
			else if (GUI.Button (playOptionRect, "Start Round " + (roundNum + 1))) {
				inputHandler.requestStartRound ();
			}
		}

		displayGameStats ();

		handleTowerMenu ();

		displayTowerStats ();
	}

	private void displayGameStats() {
		GUI.Label (new Rect (600, 162, 188, 25), "Round: " + GameManager.getRoundNum() + " / " + GameManager.getNumTotalRounds(), "box");
		GUI.Label (new Rect (600, 187, 188, 25), "Leaks: not impl.", "box");
		GUI.Label (new Rect (600, 212, 188, 25), "Kills: not impl.", "box");
		GUI.Label (new Rect (600, 237, 188, 25), "Money: " + curMoney, "box");
	}

	private void displayMenuDividers() {
		// 12 pixel border on each side // TODO: Change this style used here
		GUI.Label (new Rect (0, 12, 12, 576), "", "box");
		GUI.Label (new Rect (588, 12, 12, 576), "", "box");
		GUI.Label (new Rect (788, 12, 12, 576), "", "box");
		GUI.Label (new Rect (0, 0, 800, 12), "", "box");
		GUI.Label (new Rect (0, 588, 800, 12), "", "box");
	}

	private void displayTowerStats() {
		float bulletSpeed = 0;
		float turnDamp = 0;
		float fireSpeed = 0;
		int bulletDamage = 0;

		if (currentSelectedTower != null) {
			bulletSpeed = currentSelectedTower.getBulletSpeed ();
			turnDamp = currentSelectedTower.getTurnDamp ();
			bulletDamage = currentSelectedTower.getBulletDamage ();
			fireSpeed = currentSelectedTower.getFireSpeed ();
		}
	
		GUI.Label (new Rect(600, 462, 188, 25), "Current Tower Stats", "box");
		GUI.Label (new Rect(600, 487, 188, 25), "Bullet Speed: " + bulletSpeed, "box");
		GUI.Label (new Rect(600, 512, 188, 25), "Turn Speed: " + turnDamp, "box");
		GUI.Label (new Rect(600, 537, 188, 25), "Bullet Damage: " + bulletDamage, "box");
		GUI.Label (new Rect(600, 562, 188, 25), "Fire Delay: " + fireSpeed, "box");
	}

	public void handleTowerMenu(){
		if (menuState == MenuState.OuterMenu) {
			onOuterMenu();		
		}
		else if(menuState == MenuState.CreateTowerMenu){
			onCreateTowerMenu();
		}
		else if (menuState == MenuState.UpgradeTowerMenu){
			onUpgradeTowerMenu();
		}
		else if(menuState == MenuState.DestroyTowerMenu){
			onDestroyTowerMenu();
		}
		else if(menuState == MenuState.TowerInfoMenu){
			onTowerInfoMenu();
		}
	}

	public void onOuterMenu(){
		if (GUI.Button (new Rect (600, 262, 188, 50), "Create Tower")) {
			menuState = MenuState.CreateTowerMenu;
		}
		else if (GUI.Button (new Rect (600, 312, 188, 50), "Upgrade Info")) {
			menuState = MenuState.UpgradeTowerMenu;
		}
		else if (GUI.Button (new Rect (600, 362, 188, 50), "Destroy Info")) {
			menuState = MenuState.DestroyTowerMenu;
		}
		else if (GUI.Button (new Rect (600, 412, 188, 50), "Tower Options")) {
			menuState = MenuState.TowerInfoMenu;
		}
	}
	
	public void onCreateTowerMenu(){
		if (GUI.Button (new Rect (600, 262, 188, 50), "Light Tower: " + lightTowerCost)) {
			inputHandler.createTower("lightTower");
		}
		else if (GUI.Button (new Rect (600, 312, 188, 50), "Medium Tower: " + mediumTowerCost)) {
			inputHandler.createTower("mediumTower");
		}
		else if (GUI.Button (new Rect (600, 362, 188, 50), "Heavy Tower: " + heavyTowerCost)) {
			inputHandler.createTower("heavyTower");
		}

		backButton ();

		// TODO: Handle canceling build...
	}

	public void onUpgradeTowerMenu(){
		if (currentSelectedTower != null) {
			int containerHeight = 37;
			int firstContainerY = 262;

			if (GUI.Button (new Rect(600, firstContainerY, 188, containerHeight), "++ Bullet Speed: 25")) {
				inputHandler.upgradeBulletSpeed (currentSelectedTower);
			}
			else if (GUI.Button (new Rect(600, firstContainerY + containerHeight, 188, 37), "++ Turn Speed: 25")) {
				inputHandler.upgradeTurnSpeed (currentSelectedTower);
			}
			else if (GUI.Button (new Rect(600, firstContainerY + (2 * containerHeight), 188, 37), "++ Bullet Damage: 25")) {
				inputHandler.upgradeBulletDamage (currentSelectedTower);
			}
			else if (GUI.Button (new Rect(600, firstContainerY + (3 * containerHeight), 188, 37), "-- Fire Delay: 25")) {
				inputHandler.upgradeFireDelay (currentSelectedTower);
			}
		}
		else
			noTowerSelectedLabel ();

		backButton ();
	}

	public void onDestroyTowerMenu(){
		if (currentSelectedTower != null) {
			int val = currentSelectedTower.getDestroyReward ();

			if (GUI.Button (new Rect (600, 262, 188, 150), "Destroy Tower\n\nWorth " + val)) {
				inputHandler.destroyTower ();
				menuState = MenuState.OuterMenu;
			}
		}
			
		backButton ();
	}

	public void onTowerInfoMenu(){
		if (currentSelectedTower != null) {
			
		}
		else
			noTowerSelectedLabel ();

		backButton ();
	}

	public void noTowerSelectedLabel(){
		GUI.Label (new Rect (600, 262, 188, 150), "No Tower Selected", "box");
	}

	public void backButton(){
		if(GUI.Button(new Rect(600, 412, 188, 50), "Back")){
			menuState = MenuState.OuterMenu;
		}
	}

	public void setCurMoney(int val){
		curMoney = val;
	}
}
