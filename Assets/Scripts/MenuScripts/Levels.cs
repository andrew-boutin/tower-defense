using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour {

	void OnGUI(){
		GUI.Label (new Rect (10, 10, 150, 20), "Levels");

		if (GUI.Button (new Rect (10, 40, 150, 20), "Main Menu")) {
			SceneManager.LoadScene("MainMenu");
		}
	}

	void loadMap(int mapNum, string mapName){
		MapInfo.setMapNum(mapNum);
		SceneManager.LoadScene(mapName);
	}
}
