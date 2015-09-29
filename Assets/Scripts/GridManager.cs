using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// tracks what is in each grid

// knows the playable grid locations

// looks for mouse clicks to select towers - informs either input handler or gui

public class GridManager : MonoBehaviour {
	[HideInInspector]
	public float gridX, gridY;
	
	private int gridPixelWidth;

	bool[,] playableAreas = new bool[18, 18];

	// Use this for initialization
	void Start () {
		gridPixelWidth = MapInfo.gridPixelWidth;

		for (int x = 0; x < playableAreas.GetLength(0); x += 1) {
			for (int y = 0; y < playableAreas.GetLength(1); y += 1) {
				playableAreas[x, y] = true;
			}
		}
	}

	public void levelLoad(){
		List<int> tmp = GameObject.Find ("MapAdmin").GetComponent<MapScript> ().nonPlayableAreas;
		
		for (int x = 0; x < tmp.Count; x += 2) {
			int row = tmp[x];
			int col = tmp[x + 1];
			playableAreas[row, col] = false;
		}
	}

	void OnGUI(){
		if(GameManager.curGameState == GameState.NotInGame)
			return;

		float originalX = gridX;
		float originalY = gridY;

		gridX = (int)(Mathf.Floor (Event.current.mousePosition.x)); // Get position on GUI
		gridY = (int)(Mathf.Floor (Event.current.mousePosition.y));
		
		gridX -= 12; // Subtract the border width
		gridY -= 12;
		
		if(gridX <= 0) // Keep between 0 and 576
			gridX = 0;
		else if(gridX >= (576 - gridPixelWidth))
			gridX = 576 - gridPixelWidth;
		
		if(gridY <= 0)
			gridY = 0;
		else if(gridY >= (576 - gridPixelWidth))
			gridY = 576 - gridPixelWidth;
		
		gridX -= (gridX % gridPixelWidth); // Get the top left corner of the grid square
		gridY -= (gridY % gridPixelWidth);
		
		gridX += 12; 
		gridY += 12;

		int col = (int)(Mathf.Floor (gridX - 12)) / 32;
		int row = (int)(Mathf.Floor (gridY - 12)) / 32;

		//if(Input.GetMouseButtonDown(0))
		//	Debug.Log ("Row: " + row + " |Col: " + col);

		if (playableAreas [row, col] == false) {
			gridX = originalX;
			gridY = originalY;
		}
		
		// Current map selection area on grid
		GUI.Label (new Rect (gridX, gridY, gridPixelWidth, gridPixelWidth), "", "box"); // TODO: Change the style used here
	}
}
