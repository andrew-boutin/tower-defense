using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	
	void OnGUI(){
		// Hide the button background
		//GUI.backgroundColor = Color.clear;

		GUI.Label (new Rect (10, 10, 150, 20), "Main Menu");

		// Display 3 buttons
		if (GUI.Button (new Rect (10, 40, 150, 20), "Select Level")) {
			// Load the level selection scene		
			SceneManager.LoadScene("Levels");
		}
		else if(GUI.Button(new Rect(10, 70, 150, 20), "Game Info / Options")){
			// Load the gameinfo/options scene
			SceneManager.LoadScene("GameInfo");
		}
		else if(GUI.Button(new Rect(10, 100, 150, 20), "Credits")){
			// Load the credits scene
			SceneManager.LoadScene("Credits");
		}
		else if(GUI.Button(new Rect(10, 130, 150, 20), "Exit Game")){
			Application.Quit();
		}
	}
	
}
