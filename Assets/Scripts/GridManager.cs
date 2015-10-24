using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// tracks what is in each grid

// knows the playable grid locations

// looks for mouse clicks to select towers - informs either input handler or gui

public class GridManager : MonoBehaviour {
	[HideInInspector]
	public float gridX, gridY;
	
	private int squarePixelWidth, gridPixelWidth, borderPixelWidth;

	bool[,] playableAreas; // = new bool[12, 12]; // TODO: Set w/ borderPixelWidth?

	// Use this for initialization
	void Start () {
		squarePixelWidth = MapInfo.squarePixelWidth;
		gridPixelWidth = MapInfo.gridPixelWidth;
		borderPixelWidth = MapInfo.borderPixelWidth;

		playableAreas = new bool[12, 12];

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
		
		gridX -= borderPixelWidth; // Subtract the border width
		gridY -= borderPixelWidth;
		
		if(gridX <= 0) // Keep x between 0 and 576
			gridX = 0;
		else if(gridX >= (gridPixelWidth - squarePixelWidth))
			gridX = gridPixelWidth - squarePixelWidth;
		
		if(gridY <= 0) // Keep y between 0 and 576
			gridY = 0;
		else if(gridY >= (gridPixelWidth - squarePixelWidth))
			gridY = gridPixelWidth - squarePixelWidth;
		
		gridX -= (gridX % squarePixelWidth); // Get the top left corner of the grid square
		gridY -= (gridY % squarePixelWidth);
		
		gridX += borderPixelWidth; 
		gridY += borderPixelWidth;

		int col = (int)(Mathf.Floor (gridX - borderPixelWidth)) / squarePixelWidth;
		int row = (int)(Mathf.Floor (gridY - borderPixelWidth)) / squarePixelWidth;

		if (playableAreas [row, col] == false) {
			gridX = originalX;
			gridY = originalY;
		}
		
		// Current map selection area on grid
		GUI.Label (new Rect (gridX, gridY, squarePixelWidth, squarePixelWidth), "", "box"); // TODO: Change the style used here
	}
}
