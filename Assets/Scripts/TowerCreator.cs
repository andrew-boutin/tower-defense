using UnityEngine;
using System.Collections;

// Handles all tower creation and positioning
public class TowerCreator : MonoBehaviour {
	private GameObject dragTower;
	private InputHandler inputHandler;
	private GridManager gridManager;

	private bool draggingTower;
	private int squarePixelWidth, borderPixelWidth, gridPixelWidth;

	// Use this for initialization
	void Start () {
		squarePixelWidth = MapInfo.squarePixelWidth;
		borderPixelWidth = MapInfo.borderPixelWidth;
		gridPixelWidth = MapInfo.gridPixelWidth;
		draggingTower = false;
		gridManager = gameObject.GetComponent<GridManager> ();
		inputHandler = gameObject.GetComponent<InputHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (draggingTower) { // Either create the tower or check for location update
			if(Input.GetMouseButtonDown(0)){ // Check for tower creation
				int mouseX = (int)(Mathf.Floor (Input.mousePosition.x)), // Get moust position
				    mouseY = (int)(Mathf.Floor (Input.mousePosition.y));

				// Only consider creating the tower if the click was on the game map somewhere
				if (mouseX >= borderPixelWidth && mouseX <= (borderPixelWidth + gridPixelWidth) &&
					mouseY >= borderPixelWidth && mouseY <= (borderPixelWidth + gridPixelWidth)) {
					// TODO: Probably should only create if the click is on the square the tower is on
					finalizeTowerCreation();
				}
				else { // The click was off of the game map
					// Check if the click was on the menu
					if (mouseX > ((borderPixelWidth * 2) + gridPixelWidth))
						cancelDraggingTower(); // Cancel the tower dragging - other option was chosen
				}
			}
			else
				updateDraggingTowerLocation(); // Set the center to the middle of the currently highlighted square
		}
	}

	/**
	 * Updates the dragging tower's location to the center of the grid square that is highlighted for
	 * selection.
	 */
	private void updateDraggingTowerLocation(){
		float x = gridManager.gridX + (squarePixelWidth / 2);
		float y = gridManager.gridY + (squarePixelWidth / 2);

		y = (600 - y);

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (new Vector3(x, y, 0));
		mousePos.z = -1;
		dragTower.transform.position = mousePos;
	}

	/**
	 * Finishes creating the tower - adjusts transparency and activates, updates dragging variables and 
	 * informs other objects of creation.
	 */
	private void finalizeTowerCreation(){
		dragTower.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		dragTower.GetComponent<BaseTower> ().activated = true;
		draggingTower = false;
		inputHandler.towerCreated (dragTower, TowerInfo.getTowerCost (dragTower.GetComponent<BaseTower> ().getTowerName ()));
	}

	/**
	 * Removes the tower from the screen and updates dragging variables.
	 */
	private void cancelDraggingTower(){
		dragTower.GetComponent<BaseTower>().destroy();
		draggingTower = false;
		dragTower = null;
	}

	public void createTower(GameObject tower){
		dragTower = Instantiate(tower, Vector3.zero, Quaternion.identity) as GameObject;
		dragTower.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.5f); // 50% transparent
		draggingTower = true;
	}
}
