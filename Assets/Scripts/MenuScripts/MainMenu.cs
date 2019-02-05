using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	
	void OnGUI(){
		GUI.Label (new Rect (10, 10, 150, 20), "Main Menu");

		// Display 3 buttons
		if(GUI.Button(new Rect(10, 40, 150, 20), "Developer's Website")){
			Application.OpenURL("https://andrewboutin.com");
		}
		else if(GUI.Button(new Rect(10, 70, 150, 20), "Code Repository")){
			Application.OpenURL("https://github.com/andrew-boutin/tower-defense");
		}

		GUI.Label (new Rect (200, 187, 100, 25), "Map 1", "box");
		GUI.Label (new Rect (500, 187, 100, 25), "Map 2", "box");
	}

	void loadMap(int mapNum, string mapName){
		MapInfo.setMapNum(mapNum);
		SceneManager.LoadScene(mapName);
	}
}
