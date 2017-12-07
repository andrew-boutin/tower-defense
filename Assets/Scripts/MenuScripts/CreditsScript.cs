using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour {
	
	void OnGUI(){
		// Hide the button background
		//GUI.backgroundColor = Color.clear;

		GUI.Label (new Rect (10, 10, 150, 20), "Credits");

		// Display 3 buttons
		if (GUI.Button (new Rect (10, 40, 150, 25), "Main Menu")) {
			// Load the level selection scene	
			SceneManager.LoadScene("MainMenu");
		}
		else if(GUI.Button(new Rect(10, 70, 150, 25), "Portfolio Website")){
			Application.OpenURL("http://www.andrewboutin.com");
		}
	}
	
}