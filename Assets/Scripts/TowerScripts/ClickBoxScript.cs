using UnityEngine;
using System.Collections;

public class ClickBoxScript : MonoBehaviour {
	BaseTower baseTower;

	// Use this for initialization
	void Start () {
		baseTower = gameObject.transform.parent.GetComponent<BaseTower> ();
	}

	void OnMouseDown(){
		baseTower.clicked ();
	}
}
