using UnityEngine;
using System.Collections;

public class ClickBoxScript : MonoBehaviour {
	BaseTower baseTower;

	// Use this for initialization
	void Start () {
		baseTower = gameObject.transform.parent.GetComponent<BaseTower> ();
	}

	/**
	 * Lets the tower know that it was selected so it can handle showing indicators and inform
	 * any other objects in the game such as the menu.
	 */
	void OnMouseDown(){
		baseTower.selected ();
	}
}
