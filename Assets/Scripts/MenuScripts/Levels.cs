using UnityEngine;
using System.Collections;

public class Levels : MonoBehaviour {

	void OnGUI(){
		GUI.Label (new Rect (10, 10, 150, 20), "Levels");
		
		// Display 3 buttons
		if (GUI.Button (new Rect (10, 40, 150, 20), "Main Menu")) {
			Application.LoadLevel("MainMenu");
		}
		else if(GUI.Button (new Rect(10, 70, 150, 20), "Map 1")){
			loadMap(1, "Map1");
		}
		else if(GUI.Button (new Rect(10, 100, 150, 20), "Map 2")){
			loadMap (2, "Map2");
		}
	}

	void loadMap(int mapNum, string mapName){
		MapInfo.setMapNum(mapNum);
		Application.LoadLevel(mapName);
	}
}
