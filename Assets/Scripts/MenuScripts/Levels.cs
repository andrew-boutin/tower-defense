using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour {

	void OnGUI(){
		GUI.Label (new Rect (10, 10, 150, 20), "Levels");

		if (GUI.Button (new Rect (10, 40, 150, 20), "Main Menu")) {
			SceneManager.LoadScene("MainMenu");
		}

		GUI.Label (new Rect (200, 187, 100, 25), "Map 1", "box");
		GUI.Label (new Rect (500, 187, 100, 25), "Map 2", "box");
	}

	void loadMap(int mapNum, string mapName){
		MapInfo.setMapNum(mapNum);
		SceneManager.LoadScene(mapName);
	}
}
