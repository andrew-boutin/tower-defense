using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	void OnGUI(){
		// Hide the button background
		//GUI.backgroundColor = Color.clear;

		GUI.Label (new Rect (10, 10, 150, 20), "Main Menu");

		// Display 3 buttons
		if (GUI.Button (new Rect (10, 40, 150, 20), "Select Level")) {
			// Load the level selection scene		
			Application.LoadLevel("Levels");
		}
		else if(GUI.Button(new Rect(10, 70, 150, 20), "Game Info / Options")){
			// Load the gameinfo/options scene
			Application.LoadLevel("GameInfo");
		}
		else if(GUI.Button(new Rect(10, 100, 150, 20), "Credits")){
			// Load the credits scene
			Application.LoadLevel("Credits");
		}
	}
	
}
