using UnityEngine;
using System.Collections;

// Handles all tower creation and positioning
public class TowerCreator : MonoBehaviour {
	private GameObject dragTower;
	private bool draggingTower;
	private GridManager gridManager;

	private int gridPixelWidth;

	private InputHandler inputHandler;

	// Use this for initialization
	void Start () {
		gridPixelWidth = MapInfo.gridPixelWidth;
		draggingTower = false;
		gridManager = gameObject.GetComponent<GridManager> ();
		inputHandler = gameObject.GetComponent<InputHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		// if we have a tower that's not set yet and mouse click then set the tower
		// if we have a tower not set yet then we need to update its location to the mouse

		if (draggingTower) {
			if(Input.GetMouseButtonDown(0)){
				dragTower.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
				dragTower.GetComponent<BaseTower>().activated = true;
				draggingTower = false;
				inputHandler.towerCreated(dragTower.GetComponent<BaseTower>().buildCost);
			}
			else{
				float x = gridManager.gridX + (gridPixelWidth / 2);
				float y = gridManager.gridY + (gridPixelWidth / 2);

				y = (600 - y);

				Vector3 mousePos = Camera.main.ScreenToWorldPoint (new Vector3(x, y, 0));
				mousePos.z = 0;
				dragTower.transform.position = mousePos;
			}
		
		}
	}

	public void createTower(GameObject tower){
		dragTower = Instantiate(tower, Vector3.zero, Quaternion.identity) as GameObject;
		dragTower.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.5f); // 50% transparent
		draggingTower = true;
	}
}
