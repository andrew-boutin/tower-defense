using UnityEngine;
using UnityEngine.SceneManagement;

public class mapSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		Debug.Log ("Hit on mouse down.");

		string name = this.gameObject.name;
		int num = int.Parse(name.Substring (name.Length - 1));

		Debug.Log ("Name: " + name);
		Debug.Log ("Num: " + num);

		MapInfo.setMapNum(num);
		SceneManager.LoadScene(name);
	}
}
