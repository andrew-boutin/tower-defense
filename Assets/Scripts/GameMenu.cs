using UnityEngine;

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

	private int menuX = 600;
	private int menuItemWidth = 188;

	private int buttonMenuStartY = 312;
	private int buttonMenuStepY = 50;
	private int buttonMenuHeight = 100;

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

		if(GUI.Button(new Rect(menuX, 12, menuItemWidth, 50), "Main Menu")){
			inputHandler.goToMainMenu();
		}

		GUI.Label (new Rect (menuX, 62, menuItemWidth, 25), "Crazy Canal Tower Defense", "box");
		GUI.Label (new Rect (menuX, 87, menuItemWidth, 25), "Baking Bits Studios", "box");

		Rect playOptionRect = new Rect (menuX, 112, menuItemWidth, 50);

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
		} else if (curGameState == GameState.LevelCompleted || curGameState == GameState.GameOver) {
			int roundNum = GameManager.getRoundNum ();

			if (roundNum == GameManager.getNumTotalRounds () || curGameState == GameState.GameOver) {
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
		GUI.Label (new Rect (menuX, 162, menuItemWidth, 25), "Round: " + GameManager.getRoundNum() + " / " + GameManager.getNumTotalRounds(), "box");
		GUI.Label (new Rect (menuX, 187, menuItemWidth, 25), "Health: " + GameManager.getHealth (), "box");
		GUI.Label (new Rect (menuX, 212, menuItemWidth, 25), "Leaks: " + GameManager.getNumLeaks(), "box");
		GUI.Label (new Rect (menuX, 237, menuItemWidth, 25), "Kills: " + GameManager.getNumKills(), "box");
		GUI.Label (new Rect (menuX, 262, menuItemWidth, 25), "Money: " + curMoney, "box");
		GUI.Label (new Rect (menuX, 287, menuItemWidth, 25), "Score: " + GameManager.getScore (), "box");
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
	
		GUI.Label (new Rect(menuX, 462, menuItemWidth, 25), "Current Tower Stats", "box");
		GUI.Label (new Rect(menuX, 487, menuItemWidth, 25), "Bullet Speed: " + bulletSpeed, "box");
		GUI.Label (new Rect(menuX, 512, menuItemWidth, 25), "Turn Speed: " + turnDamp, "box");
		GUI.Label (new Rect(menuX, 537, menuItemWidth, 25), "Bullet Damage: " + bulletDamage, "box");
		GUI.Label (new Rect(menuX, 562, menuItemWidth, 25), "Fire Delay: " + fireSpeed, "box");
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
	}

	public void onOuterMenu(){
		if (GUI.Button (new Rect (menuX, buttonMenuStartY, menuItemWidth, 50), "Create Tower")) {
			menuState = MenuState.CreateTowerMenu;
		}
		else if (GUI.Button (new Rect (menuX, buttonMenuStartY + buttonMenuStepY, menuItemWidth, buttonMenuStepY), "Upgrade Info")) {
			menuState = MenuState.UpgradeTowerMenu;
		}
		else if (GUI.Button (new Rect (menuX, buttonMenuStartY + (buttonMenuStepY * 2), menuItemWidth, buttonMenuStepY), "Destroy Info")) {
			menuState = MenuState.DestroyTowerMenu;
		}
	}
	
	public void onCreateTowerMenu(){
		int containerHeight = buttonMenuHeight / 3;

		if (GUI.Button (new Rect (menuX, buttonMenuStartY, menuItemWidth, containerHeight), "Light Tower: " + lightTowerCost)) {
			inputHandler.createTower("lightTower");
		}
		else if (GUI.Button (new Rect (menuX, buttonMenuStartY + containerHeight, menuItemWidth, containerHeight), "Medium Tower: " + mediumTowerCost)) {
			inputHandler.createTower("mediumTower");
		}
		else if (GUI.Button (new Rect (menuX, buttonMenuStartY + (2 * containerHeight), menuItemWidth, containerHeight), "Heavy Tower: " + heavyTowerCost)) {
			inputHandler.createTower("heavyTower");
		}

		backButton ();
	}

	public void onUpgradeTowerMenu(){
		if (currentSelectedTower != null) {
			int containerHeight = buttonMenuHeight / 4;

			if (GUI.Button (new Rect(menuX, buttonMenuStartY, menuItemWidth, containerHeight), "++ Bullet Speed ++: 25")) {
				inputHandler.upgradeBulletSpeed (currentSelectedTower);
			}
			else if (GUI.Button (new Rect(menuX, buttonMenuStartY + containerHeight, menuItemWidth, containerHeight), "++ Turn Speed ++: 25")) {
				inputHandler.upgradeTurnSpeed (currentSelectedTower);
			}
			else if (GUI.Button (new Rect(menuX, buttonMenuStartY + (2 * containerHeight), menuItemWidth, containerHeight), "++ Bullet Damage ++: 25")) {
				inputHandler.upgradeBulletDamage (currentSelectedTower);
			}
			else if (GUI.Button (new Rect(menuX, buttonMenuStartY + (3 * containerHeight), menuItemWidth, containerHeight), "-- Fire Delay --: 25")) {
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

			if (GUI.Button (new Rect (menuX, buttonMenuStartY, menuItemWidth, buttonMenuHeight), "Destroy Tower\n\nWorth " + val)) {
				GameManager.destroyTower (currentSelectedTower);
				currentSelectedTower = null;
				menuState = MenuState.OuterMenu;
			}
		}
		else
			noTowerSelectedLabel ();
			
		backButton ();
	}

	public void noTowerSelectedLabel(){
		GUI.Label (new Rect (menuX, buttonMenuStartY, menuItemWidth, buttonMenuHeight), "No Tower Selected", "box");
	}

	public void backButton(){
		if(GUI.Button(new Rect(menuX, 412, menuItemWidth, 50), "Back")){
			menuState = MenuState.OuterMenu;
		}
	}

	public void setCurMoney(int val){
		curMoney = val;
	}
}
