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

		// 12 pixel border on each side // TODO: Change this style used here
		GUI.Label (new Rect (0, 12, 12, 576), "", "box");
		GUI.Label (new Rect (588, 12, 12, 576), "", "box");
		GUI.Label (new Rect (788, 12, 12, 576), "", "box");
		GUI.Label (new Rect (0, 0, 800, 12), "", "box");
		GUI.Label (new Rect (0, 588, 800, 12), "", "box");

		// 200 pixel wide control panel on the right

		if(GUI.Button(new Rect(600, 12, 188, 50), "Main Menu")){
			inputHandler.goToMainMenu();
		}

		GUI.Label (new Rect (600, 62, 188, 50), "Crazy Canal Tower Defense\n\nRigid-Link Studios", "box");

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
				GUI.Label (playOptionRect, "Game Finished");
			}
			else if (GUI.Button (playOptionRect, "Start Round " + (roundNum + 1))) {
				inputHandler.requestStartRound ();
			}
		}

		GUI.Label (new Rect (600, 162, 188, 25), "Round: " + GameManager.getRoundNum(), "box");
		GUI.Label (new Rect (600, 187, 188, 25), "Leaks: not impl.", "box");
		GUI.Label (new Rect (600, 212, 188, 25), "Kills: not impl.", "box");
		GUI.Label (new Rect (600, 237, 188, 25), "Money: " + curMoney, "box");

		handleTowerMenu ();

		GUI.Label (new Rect (600, 462, 188, Screen.height - 412 - 10), "Info", "box");

		// TODO: put text in info box...
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
		else if(GUI.Button(new Rect(600, 412, 188, 50), "Back")){
			menuState = MenuState.OuterMenu;
		}

		// TODO: Handle canceling build...
	}

	public void onUpgradeTowerMenu(){
		// TODO: ...
		// handle if no tower is selected

		if(GUI.Button(new Rect(600, 412, 188, 50), "Back")){
			menuState = MenuState.OuterMenu;
		}
	}

	public void onDestroyTowerMenu(){
		if(GUI.Button (new Rect (600, 262, 188, 50), "Destroy Tower")){
			inputHandler.destroyTower();
		}

		// TODO: ...
		// handle if no tower is selected

		if(GUI.Button(new Rect(600, 412, 188, 50), "Back")){
			menuState = MenuState.OuterMenu;
		}
	}

	public void onTowerInfoMenu(){
		// TODO: ....
		// handle if no tower is selected

		if(GUI.Button(new Rect(600, 412, 188, 50), "Back")){
			menuState = MenuState.OuterMenu;
		}
	}

	public void setCurMoney(int val){
		curMoney = val;
	}
}

/* TODO:s
 * Show info in the info box
 * Show tower costs for building
 * Show tower rewards for destroying
 * Support destroying
 * Support upgrading
 * Show upgrade costs
 * Upgrade menu
 * Destroy menu
 * 
 * 
 */