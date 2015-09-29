using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour {

	void OnGUI(){
		// Hide the button background
		//GUI.backgroundColor = Color.clear;
		
		GUI.Label (new Rect (10, 10, 150, 20), "Game Info");
		
		// Display 3 buttons
		if (GUI.Button (new Rect (10, 40, 150, 20), "Main Menu")) {
			// Load the level selection scene	
			Application.LoadLevel("MainMenu");
		}
	}
}
